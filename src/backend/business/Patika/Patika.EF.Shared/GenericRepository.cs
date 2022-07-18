using Patika.Shared.Extensions;
using Patika.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Patika.Shared.Entities;
using Condition = Patika.Shared.Entities.Condition;

namespace Patika.EF.Shared
{
    public abstract class GenericRepository<T, TDbContext, U> : IGenericRepository<T, U> where T : class, IEntity<U>, new() where TDbContext : DbContext
    {
        private const string IsNotLogicalDeletedWhereClause = $"{nameof(ILogicalDelete.Deleted)} = false";

        protected DbContextOptions<TDbContext> DbOptions { get; } 
        protected int DefaultMaxCountForSelect { get; private set; } = 100;
        private static IDatabase CacheDb => RedisConnectorHelper.Db;

        public GenericRepository(DbContextOptions<TDbContext> options)
        {

            DbOptions = options;
            IsLogicalDelete = typeof(T).GetInterface(typeof(ILogicalDelete).FullName, true) != null;
            IsCachable = typeof(T).GetInterface(typeof(ICachableEntity).FullName, true) != null;
            if (IsCachable)
            {
                CacheKey = (new T() as ICachableEntity).GetCacheKey();
                CacheTimeout = (new T() as ICachableEntity).GetExpireTime();
            }
        }
  
        protected bool IsLogicalDelete { get; }
        protected bool IsCachable { get; }
        protected string CacheKey { get; }
        protected TimeSpan? CacheTimeout { get; }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Pagination pagination = default, List<Sort> sorts = default, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                if (IsLogicalDelete && !includeLogicalDeleted)
                    return await ctx.Set<T>().WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).ToListAsync(cancellationToken: cancellationToken);
                if (pagination != null)
                    pagination.TotalCount = await ctx.Set<T>().CountAsync(cancellationToken: cancellationToken);
                return await ctx.Set<T>().Sort(sorts).PaginateAsync(pagination, DefaultMaxCountForSelect);
            }

            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            if (IsLogicalDelete && !includeLogicalDeleted)
            {
                if (pagination != null)
                    pagination.TotalCount = await lst.AsQueryable().WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).CountAsync(cancellationToken: cancellationToken);

                return lst.AsQueryable().WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
            }
            if (pagination != null)
                pagination.TotalCount = lst.Count();
            return lst.AsQueryable().Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
        }
        public void SetMaxSelectCount(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("Must be bigger than zero");
            }
            DefaultMaxCountForSelect = count;
        }
        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> selector, bool includeAll = false, Pagination pagination = null, List<Sort> sorts = null, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                return GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted).Where(selector).AsQueryable().Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);
            return lst.Where(selector.Compile()).AsQueryable().Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
        }
        public async Task<IEnumerable<T>> WhereAsync(IEnumerable<Condition> conditions, bool includeAll = false, Pagination pagination = default, List<Sort> sorts = default, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                var set = GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted);
                return set.Where(conditions).Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken)).AsQueryable();
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);

            return lst.Where(conditions).Sort(sorts).Paginate(pagination, DefaultMaxCountForSelect);
        }

        public virtual async Task<T> GetByIdAsync(U id, bool includeAll = false, bool includeLogicalDeleted = false,  CancellationToken cancellationToken = default)
            => await SingleOrDefaultAsync(x => x.Id.Equals(id), includeAll, includeLogicalDeleted, cancellationToken);

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                return await GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted).FirstOrDefaultAsync(selector, cancellationToken);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);
            return selector == null ? lst.FirstOrDefault() : lst.FirstOrDefault(selector.Compile());
        }

        public virtual async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                return await GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted).LastOrDefaultAsync(selector, cancellationToken);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);
            return selector == null ? lst.LastOrDefault() : lst.LastOrDefault(selector.Compile());
        }

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                return await GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted).SingleOrDefaultAsync(selector, cancellationToken);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);
            return selector == null ? lst.SingleOrDefault() : lst.SingleOrDefault(selector.Compile());
        }

        public virtual async Task<T> SingleAsync(Expression<Func<T, bool>> selector = null, bool includeAll = false, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            if (!IsCachable)
            {
                using var ctx = GetContext();
                return await GetWithIncludeFlag(includeAll, ctx, includeLogicalDeleted).SingleAsync(selector, cancellationToken);
            }
            var lst = JsonSerializer.Deserialize<IEnumerable<T>>(await GetCachedData(cancellationToken));
            lst = lst.WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause);
            return selector == null ? lst.Single() : lst.Single(selector.Compile());
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> selector = null, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            using var ctx = GetContext();
            if (IsLogicalDelete && !includeLogicalDeleted)
                return ctx.Set<T>().WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).Count();
            return selector == null ? await ctx.Set<T>().CountAsync(cancellationToken) : await ctx.Set<T>().CountAsync(selector, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> selector = null, bool includeLogicalDeleted = false, CancellationToken cancellationToken = default)
        {
            using var ctx = GetContext();
            if (IsLogicalDelete && !includeLogicalDeleted)
                return await ctx.Set<T>().WhereIf(IsLogicalDelete, IsNotLogicalDeletedWhereClause).AnyAsync(selector, cancellationToken);
            return selector == null ? await ctx.Set<T>().AnyAsync(cancellationToken) : await ctx.Set<T>().AnyAsync(selector, cancellationToken);
        }

        public virtual async Task<T> InsertOneAsync(T entity, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            entity.Id = GeneralTypeExtensions.NewId(entity.Id);
            SetUpdatedAndCreated(entity);

            await ctx.Set<T>().AddAsync(entity, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
            return entity;
        }

        private void SetUpdatedAndCreated(T entity)
        {
            var now = DateTime.Now;
            if (entity is IHasCreated hasCreated)
            {
                hasCreated.Created = now;
            }
            if (entity is IHasUpdated hasUpdated)
            {
                hasUpdated.Updated = now;
            }
        }
        private void SetUpdated(T entity)
        {
            if (entity is IHasUpdated hasUpdated)
            {
                hasUpdated.Updated = DateTime.Now;
            }
        }

        public virtual async Task<IEnumerable<T>> InsertManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            var createDate = DateTime.Now;
            foreach (var entity in entities)
            {
                entity.Id = GeneralTypeExtensions.NewId(entity.Id);
                SetUpdatedAndCreated(entity);
            }

            await ctx.Set<T>().AddRangeAsync(entities, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
            return entities;
        }

        public virtual async Task<T> UpdateOneAsync(T entity, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default, Expression<Func<T, object>> includes = null)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            var existingEntity = (includes != null) ?
                await ctx.Set<T>().Where(x => x.Id.Equals(entity.Id)).Include(includes).SingleOrDefaultAsync() :
                await ctx.Set<T>().Where(x => x.Id.Equals(entity.Id)).SingleOrDefaultAsync();

            var existingEntityProperties = existingEntity.GetType().GetProperties();

            foreach (var property in existingEntityProperties)
            {
                property.SetValue(existingEntity, entity.GetType().GetProperty(property.Name).GetValue(entity));
            }

            SetUpdated(existingEntity);

            ctx.Set<T>().Attach(existingEntity);
            ctx.Set<T>().Update(existingEntity);

            await ctx.SaveChangesAsync(cancellationToken);

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
            return existingEntity;
        }

        public async Task<IEnumerable<T>> UpdateManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default, Expression<Func<T, object>> includes = null)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            var updatedEntites = new List<T>();
            var updateDate = DateTime.Now;
            foreach (var entity in entities)
            {

                var existingEntity = (includes != null) ?
                    await ctx.Set<T>().Where(x => x.Id.Equals(entity.Id)).Include(includes).SingleOrDefaultAsync() :
                    await ctx.Set<T>().Where(x => x.Id.Equals(entity.Id)).SingleOrDefaultAsync();

                var existingEntityProperties = existingEntity.GetType().GetProperties();

                foreach (var property in existingEntityProperties)
                {
                    property.SetValue(existingEntity, entity.GetType().GetProperty(property.Name).GetValue(entity));
                }

                SetUpdated(entity);

                ctx.Set<T>().AttachRange(existingEntity);
                ctx.Set<T>().UpdateRange(existingEntity);
                await ctx.SaveChangesAsync(cancellationToken);

                updatedEntites.Add(existingEntity);
            }

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
            return updatedEntites;
        }

        public async Task DeleteOneAsync(U id, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            var item = await ctx.Set<T>().SingleAsync(x => x.Id.Equals(id), cancellationToken);
            if (IsLogicalDelete)
            {
                (item as ILogicalDelete).Deleted = true;
                ctx.Set<T>().Attach(item);
                ctx.Set<T>().Update(item);
            }
            else
            {
                ctx.Set<T>().Remove(item);
            }
            await ctx.SaveChangesAsync(cancellationToken);

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
        }

        public async Task DeleteManyAsync(IEnumerable<T> entities, IUnitOfWorkHostInterface unitOfWorkHost = null, CancellationToken cancellationToken = default)
        {
            var ctx = GetContextWihUOW(unitOfWorkHost);

            var ids = entities.Select(m => m.Id);

            if (IsLogicalDelete)
            {
                foreach (var item in entities)
                {
                    (item as ILogicalDelete).Deleted = true;
                    //ctx.Set<T>().Attach(item);
                    ctx.Set<T>().Update(item);
                }
            }
            else
            {
                ctx.Set<T>().RemoveRange(entities);
            }

            await ctx.SaveChangesAsync(cancellationToken);

            if (unitOfWorkHost != null)
            {
                unitOfWorkHost.Committed += async (s, e) =>
                {
                    await UpdateCache();
                };
            }
            else
            {
                await UpdateCache(ctx, cancellationToken);
                await DisposeLocalContextAsync(ctx);
            }
        }

        protected abstract IQueryable<T> GetDbSetWithIncludes(DbContext ctx);
        protected abstract TDbContext GetContext();

        private async Task UpdateCache(TDbContext dbContext = null, CancellationToken cancellationToken = default) => await FillCache(dbContext, cancellationToken);

        private static async Task DisposeLocalContextAsync(TDbContext ctx)
        {
            if (ctx != null)
            {
                try { await ctx.DisposeAsync(); } catch { }
            }
        }

        private TDbContext GetContextWihUOW(IUnitOfWorkHostInterface uow) => uow == null || uow.DbContext == null ? GetContext() : uow.DbContext as TDbContext;

        private IQueryable<T> GetWithIncludeFlag(bool includeAll, TDbContext ctx, bool includeLogicalDeleted = false)
        {
            if (IsLogicalDelete)
            {
                var res = includeAll ? GetDbSetWithIncludes(ctx).WhereIf(includeLogicalDeleted, $"{nameof(ILogicalDelete.Deleted)}=true") :
                    ctx.Set<T>().WhereIf(includeLogicalDeleted, $"{nameof(ILogicalDelete.Deleted)}=true");
                return res;
            }
            else
                return includeAll ? GetDbSetWithIncludes(ctx) : ctx.Set<T>();
        }
        private async Task<RedisValue> GetCachedData(CancellationToken cancellationToken)
        {
            var json = await CacheDb.StringGetAsync(CacheKey);
            if (string.IsNullOrEmpty(json))
            {
                await FillCache(cancellationToken: cancellationToken);
                json = await CacheDb.StringGetAsync(CacheKey);
            }

            return json;
        }
        private async Task FillCache(TDbContext dbContext = null, CancellationToken cancellationToken = default)
        {
            if (!IsCachable) return;
            var ctx = dbContext ?? GetContext();
            await CacheDb.StringSetAsync(CacheKey, JsonSerializer.Serialize(await ctx.Set<T>().ToListAsync(cancellationToken)), CacheTimeout);
        } 

    }
}
