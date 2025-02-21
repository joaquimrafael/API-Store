using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infra.Context
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions<DbContextBase> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            =>  optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
}
