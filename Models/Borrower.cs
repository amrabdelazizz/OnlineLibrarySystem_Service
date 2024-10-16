using System.ComponentModel.DataAnnotations;

namespace OnlineLibrarySystem.Models
{
    public class Borrower
    {
        [Key]
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }

    }
}
