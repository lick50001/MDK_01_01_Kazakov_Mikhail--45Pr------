using API_Kazakov.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Kazakov.Contexts
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UsersContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;uid=root;pwd=;database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
