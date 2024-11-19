using Microsoft.AspNetCore.Mvc;

namespace Deepin.Server.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        [HttpGet("checkSession")]
        public IActionResult CheckSession()
        {
            return Ok();
        }
    }
}
