using Microsoft.EntityFrameworkCore;
using SimpleTodoApi.Models;

namespace SimpleTodoApi.DatabaseContext
{
 public class ApplicationDbContext : DbContext
 {
  public ApplicationDbContext(DbContextOptions options) : base(options) 
  { 
  }

  public ApplicationDbContext()
  {
  }

  public DbSet<Todo> Todos { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
   base.OnModelCreating(modelBuilder);
   
   // Seeding a few Todos data to start
   modelBuilder.Entity<Todo>().HasData(
        new Todo
        {
            Id = 1,
            Name = "Go running",
            DueDate = new DateTime(2025, 06, 01),
            IsCompleted = false
        },
        new Todo
        {
            Id = 2,
            Name = "Buy groceries",
            DueDate = new DateTime(2025, 06, 02),
            IsCompleted = true
        }
    );
  }
 }
}