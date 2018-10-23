using System.Linq;
using System.Threading.Tasks;
using Common;
using DNTPersianUtils.Core;
using DTO.Login;
using EntityCode;
using EntityCode.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers.Security
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private static bool _databaseChecked;
        private readonly DataBaseContext _context;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            DataBaseContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        /// <summary>
        /// ثبت نام اولیه کاربر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            EnsureDatabaseCreated(_context);
            if (ModelState.IsValid)
            {
                bool duplicate = false;
                var user = await _userManager.FindByNameAsync(model.NationalCode);
                if (user != null)
                {
                    duplicate = true;
                    ModelState.AddModelError("NationalCode", "کد ملی در سیستم قبلا ثبت شده است");
                }

                var user1 = _context.Users.Any(x=>x.PhoneNumber == model.PhonNumber);

                if (user1)
                {
                    duplicate = true;
                    ModelState.AddModelError("PhoneNumber", "شماره تلفن همراه قبلا ثبت شده است");
                }


                if (!model.PhonNumber.IsValidIranianMobileNumber())
                {
                    duplicate = true;
                    ModelState.AddModelError("PhoneNumber", "شماره تلفن همراه صحیح نمی باشد");
                }

                if (duplicate)
                {
                    return BadRequest(ModelState);
                }

                user = new ApplicationUser
                {
                    UserName = model.PhonNumber,
                    PhoneNumber = model.PhonNumber,
                    PasswordHash = HashClass.BitConverterHasj(model.Password),
                    FullName = model.FullName,
                    NationalCode = model.NationalCode

                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(true);
                }
                AddErrors(result);
            }

            // If we got this far, something failed.
            return BadRequest(ModelState);
        }

        #region Helpers

        // The following code creates the database and schema if they don't exist.
        // This is a temporary workaround since deploying database through EF migrations is
        // not yet supported in this release.
        // Please see this http://go.microsoft.com/fwlink/?LinkID=615859 for more information on how to do deploy the database
        // when publishing your application.
        private static void EnsureDatabaseCreated(DataBaseContext context)
        {
            if (!_databaseChecked)
            {
                _databaseChecked = true;
                context.Database.EnsureCreated();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
