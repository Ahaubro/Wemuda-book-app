using System.ComponentModel.DataAnnotations;

namespace Wemuda_book_app.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        //public int MinutesRead { get; set; }

    }
}