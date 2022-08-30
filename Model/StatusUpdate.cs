using System.ComponentModel.DataAnnotations;

namespace Wemuda_book_app.Model
{
    public class StatusUpdate
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeOfUpdate { get; set; }
        public int MinutesRead { get; set; }
        public int CurrentPage { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool FinishedBook { get; set; }
    }
}
