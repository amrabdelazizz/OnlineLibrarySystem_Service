using System.ComponentModel.DataAnnotations;

namespace OnlineLibrarySystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN {  get; set; }
        public int AvailableCopies { get; set; }

        
    }
}
