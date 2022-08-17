using System.ComponentModel.DataAnnotations;
using Wemuda_book_app.Model;

namespace Wemuda_book_app.Shared
{
    public class AuthDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }


    public class AuthenticateRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

    }

    public class AuthCreateRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    // GET ALL RESPONSE
    public class AuthCreateResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StatusText { get; set; }
    }

    //public class AuthGetAllRequestDto
    //{


    //}

    //// GETALLRESPONSE
    //public class AuthGetAllResponseDto
    //{
    //    public IEnumerable<AuthDTO> Users { get; set; }
    //}


    ////GETBYID REQUEST
    //public class AuthGetByIdRequestDto
    //{
    //    public int Id { get; set; }

    //}

    //// GETBYIDRESPONSE
    //public class AuthGetByIdResponseDto
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Username { get; set; }
    //    public string StatusText { get; set; }
    //}


}
