namespace Wemuda_book_app.Shared
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }


    // CREATE REQUEST
    public class ReviewCreateRequestDto
    {

        public int  Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }


    }

    // CREATE RESPONSE
    public class ReviewCreateResponseDto
    {
        public string StatusText { get; set; }
    }



    // GET ALL REQUEST
    public class ReviewGetAllRequestDto
    {

    }

    // GET ALL RESPONSE
    public class ReviewGetAllResponseDto
    {
        public IEnumerable<ReviewDto> Reviews { get; set; }
    }



    // GET BY ID REQUEST
    public class ReviewGetRequestDto
    {

    }

    // GET BY ID RESPONSE
    public class ReviewGetResponseDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
