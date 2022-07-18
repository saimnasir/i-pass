using Patika.Shared.Entities;
using Patika.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Patika.Shared.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = query.Count();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderByExpression)
        {
            if (string.IsNullOrEmpty(orderByExpression))
                return query;

            string propertyName, orderByMethod;
            string[] strs = orderByExpression.Split(' ');
            propertyName = strs[0];

            if (strs.Length == 1)
                orderByMethod = "OrderBy";
            else
                orderByMethod = strs[1].Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";

            ParameterExpression pe = Expression.Parameter(query.ElementType);
            MemberExpression me = Expression.Property(pe, propertyName);

            MethodCallExpression orderByCall = Expression.Call(typeof(Queryable), orderByMethod, new Type[] { query.ElementType, me.Type }, query.Expression
                , Expression.Quote(Expression.Lambda(me, pe)));

            return query.Provider.CreateQuery(orderByCall) as IQueryable<T>;
        }

        public static IQueryable<T> WhereEqual<T>(this IQueryable<T> query, string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName))
                return query;

            return query.Where($"{propertyName} = {value}");
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, string whereClause)
        {
            if (!condition || string.IsNullOrEmpty(whereClause))
                return query;

            return query.Where(whereClause);
        }

        public static IQueryable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, string whereClause)
        {
            if (!condition || string.IsNullOrEmpty(whereClause))
                return query.AsQueryable();

            return query.AsQueryable().Where(whereClause);
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Condition condition)
        {
            var config = new ParsingConfig
            {
                IsCaseSensitive = false
            };
            if (condition.Operator == ConditionOperator.Contains)
            {
                return query.Where(config, $"{condition.PropertyName}.Contains(@0)", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.In)
            {
                return query.Where($"{condition.PropertyName} in @0", condition.Values);
            }
            else if (condition.Operator == ConditionOperator.Between)
            {
                return query.Where($"{condition.PropertyName} >= @0 AndAlso {condition.PropertyName} <= @1", condition.Values.First(), condition.Values.Skip(1).Take(1));
            }
            else if (condition.Operator == ConditionOperator.Equal)
            {
                return query.Where($"{condition.PropertyName} == @0", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.BiggerThan)
            {
                return query.Where($"{condition.PropertyName} > @0", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.BiggerOrEqual)
            {
                return query.Where($"{condition.PropertyName} >= @0", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.SmallerOrEqual)
            {
                return query.Where($"{condition.PropertyName} <= @0", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.SmallerThan)
            {
                return query.Where($"{condition.PropertyName} < @0", condition.Values.First());
            }
            else if (condition.Operator == ConditionOperator.NotEqual)
            {
                return query.Where($"{condition.PropertyName} != @0", condition.Values.First());
            }
            return query;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, params Condition[] conditions)
        {
            foreach (var item in conditions)
            {
                query = query.Where(item);
            }
            return query;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, IEnumerable<Condition> conditions) => query.Where(conditions.ToArray());

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<Sort> sorts)
        {
            if (sorts == null)
                return query;

            var sortStr = "";

            foreach (var sort in sorts)
                sortStr += $"{(sortStr.Length > 0 ? "," : "")}{sort}";
            return query.OrderBy(sortStr);
        }

        public static IEnumerable<T> Paginate<T>(this IQueryable<T> query, Pagination pagination, int defaultMaxCount = 100)
        {
            var res = HandlePagination(query, pagination, defaultMaxCount);
            return res.ToList();
        }

        public static PagedResult<T> Paginate<T>(this IQueryable<T> query, Pagination pagination)
        {
            if (pagination == null || pagination == default)
            {
                pagination = new Pagination
                {
                    Count = int.MaxValue,
                    Page = 1
                };
            }

            var pagedResult = query.PageResult(pagination.Page, pagination.Count);
            if (pagedResult == null)
            {
                throw new Exception("pagedResult is null");
            }
            if (pagedResult.Queryable == null)
            {
                throw new Exception("pagedResult.Queryable is null");
            }
            pagedResult.Queryable = pagedResult?.Queryable?.ToList().AsQueryable();
            return pagedResult;
        }

        public static async Task<IEnumerable<T>> PaginateAsync<T>(this IQueryable<T> query, Pagination pagination, int defaultMaxCount = 100)
        {
            var res = HandlePagination(query, pagination, defaultMaxCount);
            return await res.ToDynamicListAsync<T>();
        }
        private static IQueryable<T> HandlePagination<T>(IQueryable<T> query, Pagination pagination, int defaultMaxCount)
        {
            var newPagination = new Pagination
            {
                Count = 0,
                Page = 0
            };
            if (pagination == null || pagination.Page < 1 || pagination.Count < 1)
            {
                if(pagination == null)
                {
                    newPagination.Count = defaultMaxCount;
                    newPagination.Page = 1;
                }
                else if (pagination.Page < 1)
                {
                    if(pagination.Count >= 1 || pagination.Count <= defaultMaxCount)
                    {
                        newPagination.Count = pagination.Count;
                        newPagination.Page = 1;
                    }
                    else
                    {
                        newPagination.Count = defaultMaxCount;
                        newPagination.Page = 1;
                    }
                }
                else if (pagination.Count > defaultMaxCount || pagination.Count < 1)
                {
                    if (pagination.Page >= 1)
                    {
                        newPagination.Count = defaultMaxCount;
                        newPagination.Page = pagination.Page;
                    }
                    else
                    {
                        newPagination.Count = defaultMaxCount;
                        newPagination.Page = 1;
                    }
                }
            }
            var res = query.PageResult(newPagination.Page, newPagination.Count);
            newPagination.TotalCount = res.RowCount;
            return res.Queryable;
        }
    }
}
