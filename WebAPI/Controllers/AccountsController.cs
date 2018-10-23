using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO.Login;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        public AccountsController()
        {

        }
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody]RegisterDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var result = await _userManager.CreateAsync(userIdentity, model.Password);

        //    if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

        //    await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
        //    await _appDbContext.SaveChangesAsync();

        //    return new OkObjectResult("Account created");
        //}
    }
}
