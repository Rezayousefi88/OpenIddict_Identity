using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using EntityCode.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers.Security
{
    [Route("api/[controller]/[action]")]
    public class ResourceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResourceController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            return Content($"{user.UserName} has been successfully authenticated.");
        }
    }
}