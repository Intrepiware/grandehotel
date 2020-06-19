using GrandeHotel.Lib.Services.Security;
using GrandeHotel.Web.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrandeHotel.Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost, Route("Authenticate")]
        public async Task<ActionResult> Authenticate(UserAuthenticateModel data)
        {
            var result = await _authenticationService.Authenticate(data.Username, data.Password);
            if (result == null) return BadRequest(new { message = "Username/password combination was not found." });

            return Ok(result);
        }

        // Temporary route to prove authorization works
        [HttpGet("Me")]
        [Authorize]
        public ActionResult Me()
        {
            return Ok("Authenticated");
        }
    }
}