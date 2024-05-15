using Microsoft.EntityFrameworkCore;
using ProjectAuthenticationAPI.Models;

namespace ProjectAuthenticationAPI.Data
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _config { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(IConfiguration config)
        {
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }
    }
}
