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

    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
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
        //public UserDto User { get; set; }
        public string Token { get; set; }

    }

    // CREATE REQUEST
    public class AuthCreateRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

    // CREATE RESPONSE
    public class AuthCreateResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StatusText { get; set; }
    }

    // GET ALL REQUEST
    public class AuthGetAllRequestDto
    {


    }

    // GETALLRESPONSE
    public class AuthGetAllResponseDto
    {
        public IEnumerable<AuthDTO> Users { get; set; }
    }


    //GET BY ID REQUEST
    public class AuthGetByIdRequestDto
    {

    }

    // GET BY ID RESPONSE
    public class AuthGetByIdResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string StatusText { get; set; }
    }

    // DELETE REQUEST
    public class AuthDeleteRequestDto
    {

    }

    // DELETE RESPONSE
    public class AuthDeleteResponseDto
    {
        public string StatusText { get; set; }
    }

    public class ChangePasswordRequestDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordResponseDto
    {
        public string StatusText { get; set; }
    }

}
