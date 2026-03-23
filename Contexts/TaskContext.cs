using Microsoft.EntityFrameworkCore;
using API_Kazakov.Models;
using Task = API_Kazakov.Models.Task; // Псевдоним, чтобы избежать конфликта с System.Threading.Tasks.Task

namespace API_Kazakov.Contexts
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TaskContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;uid=root;pwd=;database=TaskManager",
                new MySqlServerVersion(new Version(8, 0, 11)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                 .HasKey(t => t.Id);

            modelBuilder.Entity<Task>()
                .ToTable("Tasks");

            base.OnModelCreating(modelBuilder);
        }
    }
}