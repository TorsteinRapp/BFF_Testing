using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossDomainApi
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        public IActionResult Get()
        {
            return new JsonResult("OK");
        }

        [Route("identity")]
        [Authorize]
        public IActionResult Identity()
        {
            var user = new
            {
                name = User.Identity.Name,
                claims = User.Claims.Select(c => new { type = c.Type, value = c.Value }).ToArray()
            };

            return new JsonResult(user);
        }
    }
}