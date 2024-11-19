using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [HttpGet("checkSession")]
        public IActionResult CheckSession()
        {
            return Ok();
        }
    }
}
