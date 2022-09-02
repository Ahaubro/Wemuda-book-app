using System.ComponentModel.DataAnnotations;

namespace Wemuda_book_app.Model
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Status { get; set; }
    }
}
