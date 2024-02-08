using Microsoft.EntityFrameworkCore;
using ModuloLimitadorVelocidad.Models.Domain;

namespace ModuloLimitadorVelocidad.Data
{
    public class MVC_DbContext : DbContext
    {
        public MVC_DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
