using Microsoft.AspNetCore.Mvc;
using Wemuda_book_app.Model;
using Wemuda_book_app.Service;

namespace Wemuda_book_app.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;
        public MailController(IMailService mail)
        {
            _mail = mail;
        }
        [HttpPost]
        public async Task<IActionResult> SendMailAsync(Email mailData)
        {
            Console.WriteLine("Attempting to send a mail");
            bool result = await _mail.SendAsync(mailData, new CancellationToken());

            if (result)
            {
                Console.WriteLine("Mail sent");
                return StatusCode(StatusCodes.Status200OK,
                    "Mail has successfully been sent.");
            }
            else
            {
                Console.WriteLine("Mail was not sent");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occured. The Mail could not be sent.");
            }
        }
    }
    
}