using IPass.Domain.CommonDomain.Entities;
using IPass.Domain.PasswordDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;
using System;
using System.Linq;

namespace IPass.EFRepositories.IPassContext.Repositories
{
    public class PinCodeRepository : GenericRepository<PinCode, MyMemoryDbContext, Guid>, IPinCodeRepository
    {
        public PinCodeRepository(DbContextOptions<MyMemoryDbContext> options) : base(options)
        {
        }

        protected override MyMemoryDbContext GetContext() => new(DbOptions);

        protected override IQueryable<PinCode> GetDbSetWithIncludes(DbContext ctx) => ctx.Set<PinCode>();

    }
}
