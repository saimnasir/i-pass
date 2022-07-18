using IPass.Domain.CommonDomain.Entities;
using IPass.Domain.PasswordDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patika.EF.Shared;
using Patika.Shared.Entities.Loggy.Entities;

namespace IPass.EFRepositories.IPassContext
{
    public partial class MyMemoryDbContext : DbContextWithUnitOfWork<MyMemoryDbContext>
    { 
        public MyMemoryDbContext(DbContextOptions<MyMemoryDbContext> options)
            : base(options)
        {
        }
     
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.EnableDetailedErrors();
        }

        internal DbSet<Log> Logs { get; set; }
        internal DbSet<LogDetail> LogDetails { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }
        public virtual DbSet<MemoryType> MemoryTypes { get; set; }
        public virtual DbSet<EnvironmentType> EnvironmentTypes { get; set; }
        public virtual DbSet<Memory> Memories { get; set; }
        public virtual DbSet<PinCode> PinCodes { get; set; }    
        public virtual DbSet<User> Users { get; set; }    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("MEM");

            // LOGS
            var logSchema = "LOG";
            builder.Entity<Log>().ToTable(nameof(Logs), logSchema);
            builder.Entity<LogDetail>().ToTable(nameof(LogDetails), logSchema);


            builder.Entity<Memory>().ToTable(nameof(Memories), e =>
            {
                e.IsTemporal(t =>
                {
                    t.HasPeriodStart("ValidationStartAt");
                    t.HasPeriodEnd("ValidationEndAt");
                    t.UseHistoryTable("MemoryHistories");
                });
            });

            builder.Entity<User>()
             .OwnsMany(m => m.OTPHistories);
        }
    }
}
