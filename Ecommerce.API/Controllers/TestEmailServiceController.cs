using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestEmailServiceController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public TestEmailServiceController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail()
        {
            var message = new EmailMessage
            {
                To = "leomarqz2020@gmail.com",
                Subject = "Test Email",
                Body = "This is a test email."
            };

            var result = await _emailService.SendEmailAsync(message, "test-token");

            return result ? Ok("Email sent successfully.") : BadRequest("Failed to send email.");

        }
    }
}
