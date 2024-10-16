using Microsoft.EntityFrameworkCore;
namespace OnlineLibrarySystem.Models
{
    public class LibraryDbCotext : DbContext
    {
        public LibraryDbCotext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set;}
    }
}
