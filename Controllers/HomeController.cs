using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;


namespace ApiPerfil.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}