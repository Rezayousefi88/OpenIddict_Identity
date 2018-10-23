using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Common;
using DTO;
using EntityCode.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;


namespace WebAPI.Controllers.Security
{
    public class AuthorizationController : Controller
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        
        public AuthorizationController(
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        /// <summary>
        /// گرفتن توکن
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            Debug.Assert(request.IsTokenRequest(),
                "The OpenIddict binder for ASP.NET Core MVC is not registered. " +
                "Make sure services.AddOpenIddict().AddMvcBinders() is correctly called.");

            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                //foreach (var item in resalt.Errors)
                //{
                //    ModelState.AddModelError("", item.Description);
                //}
                var hasError = false;
                if (user == null)
                {
                    ModelState.AddModelError("NationalCode", "نام کاربری موجود نیست");
                    hasError = true;
                    //return BadRequest(new 
                    //{
                    //    //Error = "NationalCode",// OpenIdConnectConstants.Errors.InvalidGrant,
                    //    NationalCode = "نام کاربری موجود نیست"
                    //});
                }
                else
                {
                    var result = HashClass.BitConverterHasj(request.Password) == user.PasswordHash; //  await _signInManager.CheckPasswordSignInAsync(user, HashClass.BitConverterHasj(request.Password), lockoutOnFailure: true);
                    if (!result)//result.Succeeded
                    {
                        ModelState.AddModelError("Password", "کلمه عبور اشتباه است");
                        hasError = true;
                        //return BadRequest(new 
                        //{
                        //    //Error = "Password",//OpenIdConnectConstants.Errors.InvalidGrant,
                        //    Password = "کلمه عبور اشتباه است"
                        //});
                    }
                }



                if (hasError)
                {
                    return BadRequest(ModelState);
                }
                // Create a new authentication ticket.
                var ticket =await CreateTicketAsync(request, user);

                    ticket.SetAccessTokenLifetime(TimeSpan.FromDays(7));
                    //ticket.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(1));
                    //ticket.SetIdentityTokenLifetime(TimeSpan.FromMinutes(30));
                    //ticket.SetRefreshTokenLifetime(TimeSpan.FromDays(2));

                var tocken = SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);

                return tocken;
            }

            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
            //return Ok("");
        }
        
        /// <summary>
        /// ساخت اطلاعات توکن
        /// </summary>
        /// <param name="request"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal,
                new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);
           // ticket.Properties.ExpiresUtc = DateTime.Now.AddDays(2);
            // Set the list of scopes granted to the client application.
            ticket.SetScopes(new[]
            {
                OpenIdConnectConstants.Scopes.OpenId,
                //OpenIdConnectConstants.Scopes.Email,
                OpenIdConnectConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            ticket.SetResources("resource-server");
            

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                {
                    continue;
                }

                var destinations = new List<string>
                {
                    OpenIdConnectConstants.Destinations.AccessToken
                };

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(destinations);
            }

            return ticket;
        }

 

        /// <summary>
        /// گرفتن اطلاعات کاربر
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        [HttpGet("~/connect/getinfo"), Produces("application/json")]
        public async Task<IActionResult> Getinfo()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest("not found");
            }

            var data = new
            {
                user.FullName,
                user.UserName,
                user.NationalCode,
                user.PhoneNumber
            };
            return Ok(data);
        }

        ///// <summary>
        ///// تغییر رمز عبور
        ///// </summary>
        ///// <returns></returns>
        //[Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        //[HttpPost("~/connect/changepassword"), Produces("application/json")]
        //public async Task<IActionResult> ChangePassword(User_Password pass)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return BadRequest("not found");
        //        }
        //        var result = HashClass.BitConverterHasj(pass.OldPassword) == user.PasswordHash;
        //        if (!result)//result.Succeeded
        //        {
        //            ModelState.AddModelError("OldPassword", "پسورد وارد شده صحیح نمی باشد");
        //            return BadRequest(ModelState);
        //        }

        //        user.PasswordHash = HashClass.BitConverterHasj(pass.NewPassword);
        //        var result2 = await _userManager.UpdateAsync(user);
        //        if (result2.Succeeded)
        //        {
        //            return Ok("true");
        //        }
        //        else
        //        {
        //            return Ok("serverError");
        //        }
        //    }

        //    return BadRequest(ModelState);

        //}
        ///// <summary>
        ///// بروز رسانی اطلاعات پروفایل
        ///// </summary>
        ///// <returns></returns>
        //[Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        //[HttpPost("~/connect/updateuser"), Produces("application/json")]
        //public async Task<IActionResult> UpdateUser([FromBody] User_Profile model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return BadRequest("not found");
        //        }
        //        user.PhoneNumber = model.PhoneNumber;
        //        user.FullName = model.FullName;
        //        var result = await _userManager.UpdateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return Ok("true");
        //        }
        //        else
        //        {
        //            return Ok("serverError");
        //        }
        //    }

        //    return BadRequest(ModelState);
        //}

    }
}