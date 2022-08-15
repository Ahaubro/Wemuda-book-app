namespace Wemuda_book_app.Shared
{
    public class BookDto
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    // CREATE
    public class BookCreateRequestDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    public class BookCreateResponseDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string StatusText { get; set; }
    }

    // UPDATE
    public class BookUpdateRequestDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    public class BookUpdateResponseDto
    { 
        public string Title { get; set; }
        public string StatusText { get; set; }
    }

    // GET ALL

    public class BookGetAllRequestDto
    { 
        
    }

    public class BookGetAllResponseDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }

    // GET

    public class BookGetRequestDto
    { 
    
    }

    public class BookGetResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    // DELETE
    public class BookDeleteRequestDto
    { 
        
    }

    public class BookDeleteResponseDto
    {
        public string StatusText { get; set; }
    }
}
