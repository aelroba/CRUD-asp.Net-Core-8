using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Dtos.Account;
using WebApiApp.Interfaces;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserResponseDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost]
        [Route("register")]
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
                        return Ok(new NewUserResponseDto{
                            Username = useritem.UserName,
                            Email = useritem.Email,
                            Token = _tokenService.CreateToken(useritem)
                        });
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

        [HttpGet]
        [Route("user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if(user == null) return BadRequest("");
            return Ok(user);
        }
    }

    
}