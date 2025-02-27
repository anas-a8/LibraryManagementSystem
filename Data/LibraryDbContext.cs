using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "C# Programming", Author = "John Doe", ISBN = "1234567890", CopiesAvailable = 10, TimesBorrowed = 20 },
                new Book { Id = 2, Title = "ASP.NET Core Guide", Author = "Jane Smith", ISBN = "0987654321", CopiesAvailable = 8, TimesBorrowed = 25 },
                new Book { Id = 3, Title = "Entity Framework Core", Author = "Michael Brown", ISBN = "1122334455", CopiesAvailable = 5, TimesBorrowed = 18 }
            );
        }
    }
}
