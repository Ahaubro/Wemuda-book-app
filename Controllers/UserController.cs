using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Service;
using Wemuda_book_app.Shared;

namespace Wemuda_book_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // AUTHENTICATE
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestDto model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {
                    message = "Username or password is incorrect"
                });

            return Ok(response);
        }


        // GET ALL USERS - (Virker self kun når [Authorize] er udkommenteret)
        [Authorize]
        [Produces("application/json")]
        [HttpGet]
        public async Task<AuthGetAllResponseDto> GetAll()
        {
            return await _userService.GetAll();
        }


        // GET USER BY ID
        [Produces("application/json")]
        [HttpGet("{id:int}")]
        public async Task<AuthGetByIdResponseDto> GetById(int id)
        {
            return await _userService.GetById(id);
        }


        // CREATE USER
        [Produces("application/json")]
        [HttpPost]
        public async Task<AuthCreateResponseDto> Create([FromBody] AuthCreateRequestDto dto)
        {
            return await _userService.Create(dto);
        }


        //DELETE USER
        [Produces("application/json")]
        [HttpDelete("{id:int}")]
        public async Task<AuthDeleteResponseDto> Delete(int id)
        {
            return await _userService.Delete(id);
        }

    }
}
