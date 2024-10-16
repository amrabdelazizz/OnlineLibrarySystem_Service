using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrarySystem.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int BorrowId { get; set; }
        public int BookId { get; set; } // foreign  key for books table
        public int BorrowerId { get; set; } // forieng key borrowers
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }

        // navigations properties 
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }

    }
}
