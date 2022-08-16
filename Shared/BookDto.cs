﻿namespace Wemuda_book_app.Shared
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    // CREATE REQUEST
    public class BookCreateRequestDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
    }

    // CREATE RESPONSE
    public class BookCreateResponseDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string StatusText { get; set; }
    }

    // UPDATE REQUEST
    public class BookUpdateRequestDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string? Genre { get; set; }

        public DateTime ReleaseDate { get; set; }
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
}