using System.ComponentModel.DataAnnotations;

namespace OnlineLibrarySystem.DTOs
{
    // used when add a new borrow action 
    public class BorrowedBookDTO
    {
        [Key]
        public int BorrowId { get; set; }
        public int BookId { get; set; } // foreign  key for books table
        public int BorrowerId { get; set; } // forieng key borrowers
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
