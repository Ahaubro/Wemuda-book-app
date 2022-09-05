using Wemuda_book_app.Model;

namespace Wemuda_book_app.Shared
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? BookStatus { get; set; }
        public string BookId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        //public List<string> Authors { get; set; }
        public string Thumbnail { get; set; }
        public int? AverageRating { get; set; }
        public int? RatingCount { get; set; }
    }

    // CREATE REQUEST
    public class BookCreateRequestDto
    {
      
        public string BookId { get; set; }

        public int UserId { get; set; }

        public string? BookStatus { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //public List<string> Authors { get; set; }

        public string Thumbnail { get; set; }

        public int AverageRating { get; set; }

        public int RatingCount { get; set; }

    }

    // CREATE RESPONSE
    public class BookCreateResponseDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        //public string? Genre { get; set; }

        public string StatusText { get; set; }
    }

    // UPDATE REQUEST
    public class BookUpdateRequestDto
    {
        public int UserId { get; set; }
        public string BookId { get; set; }
        public string? BookStatus { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
    }

    // UPDATE RESPONSE
    public class BookUpdateResponseDto
    { 
        public string Title { get; set; }
        public string StatusText { get; set; }
    }

    // GET ALL REQUEST
    public class BookGetAllRequestDto
    { 
        
    }

    // GET ALL RESPONSE
    public class BookGetAllResponseDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }


    // GET BY ID REQUEST
    public class BookGetRequestDto
    { 
    
    }


    // GET BY ID RESPONSE
    public class BookGetResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    // DELETE REQUEST
    public class BookDeleteRequestDto
    { 
        
    }

    // DELETE RESPONSE
    public class BookDeleteResponseDto
    {
        public string StatusText { get; set; }
    }

    
    public class BookAddToUserRequestDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

    public class BookAddToUserResponseDto
    {
        public string StatusText { get; set; }
    }

    // GET BOOKS BY USERID REQUEST
    public class BookGetByUseridRequestDto
    {

    }

    // GET BOOKS BY USERID RESPONSE
    public class BookGetByUseridResponseDto
    {
        public IEnumerable<BookDto> Books { get; set; }

    }

    // GET BY BOOKID REQUEST
    public class BookGetByBookidRequestDto
    {

    }


    // GET BY BOOKID RESPONSE
    public class BookGetByBookidResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? BookStatus { get; set; }
    }




}
