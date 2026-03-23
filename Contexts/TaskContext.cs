using Microsoft.EntityFrameworkCore;

namespace API_Kazakov.Contexts
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Task {  get; set; }
        public TaskContext()
        {
            Database.EnsureCreated();
            Task.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;uid=root;pwd=;database=TaskManager", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
