using Microsoft.EntityFrameworkCore;
using Patika.EF.Shared;

namespace Patika.AuthenticationServer
{
    public class SqlServerDbInitiator<T> : IDbContextInitiator<T>
    {
        public string ConnectionString { get; }
        public SqlServerDbInitiator(string connectionString) => ConnectionString = connectionString;


        public void Init(DbContextOptionsBuilder options) => options.UseSqlServer(ConnectionString);
    }
}