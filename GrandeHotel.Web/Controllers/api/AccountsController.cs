using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GrandeHotel.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [Authorize]
        [HttpGet(Name = "Accounts_Get")]
        public ActionResult Get()
        {
            return Ok(new
            {
                Identity = User.Identity,
                Claims = User.Claims.Select(claim => $"{claim.Type}: {claim.Value}")
            });
        }
    }
}