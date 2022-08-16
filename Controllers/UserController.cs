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

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {
                    message = "Username or password is incorrect"
                });

            return Ok("Okay");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        // CREATE
        [Produces("application/json")]
        [HttpPost]
        public async Task<AuthCreateResponseDto> Create([FromBody] AuthCreateRequestDto dto)
        {
            return await _userService.Create(dto);
        }
    }
}
