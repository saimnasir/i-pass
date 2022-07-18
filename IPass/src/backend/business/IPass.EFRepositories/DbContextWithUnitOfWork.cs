using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Patika.EF.Shared; 
using Patika.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IPass.EFRepositories
{
    public abstract class DbContextWithUnitOfWork<TDbContext> : DbContext, IUnitOfWorkHost, IUnitOfWorkHostEvents, IUnitOfWorkHostInterface, IUnitOfWorkHostWithInterface where TDbContext : DbContext
    {
        public DbContextWithUnitOfWork([NotNull] DbContextOptions options) : base(options)
        {
        } 
        
        protected IDbContextTransaction Transaction { get; private set; } = null;

        public object DbContext => this;

        public event EventHandler Committed;
        public event EventHandler RollBacked;

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                return;
            await Transaction.CommitAsync(cancellationToken);
            Committed?.Invoke(this, EventArgs.Empty);
            await Transaction.DisposeAsync();
            Transaction = null;
        }

        public void Commit()
        {
            if (Transaction == null)
                return;
            Transaction.Commit();
            Committed?.Invoke(this, EventArgs.Empty);
            Transaction.Dispose();
            Transaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                return;
            await Transaction.RollbackAsync(cancellationToken);
            RollBacked?.Invoke(this, EventArgs.Empty);
            await Transaction.DisposeAsync();
            Transaction = null;
        }

        public void Rollback()
        {
            if (Transaction == null)
                return;
            Transaction.Rollback();
            RollBacked?.Invoke(this, EventArgs.Empty);
            Transaction.Dispose();
            Transaction = null;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
                Transaction = await Database.BeginTransactionAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            if (Transaction == null)
                Transaction = Database.BeginTransaction();
        }
    }
}