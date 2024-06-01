using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiApp.Dtos.Account;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerModel)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var useritem = new ApplicationUser { 
                    UserName = registerModel.Username, 
                    Email = registerModel.Email,
                };

                var createUser = await _userManager.CreateAsync(useritem, registerModel.Password);
                if(createUser.Succeeded)
                {
                    var assignRole = await _userManager.AddToRoleAsync(useritem, "User");
                    if(assignRole.Succeeded)
                        return Ok("User Created!");
                    else 
                        return StatusCode(500, assignRole.Errors);
                }
                else 
                        return StatusCode(500, createUser.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}