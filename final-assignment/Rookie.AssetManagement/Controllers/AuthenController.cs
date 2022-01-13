using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rookie.AssetManagement.Business.Request;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AuthenController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return BadRequest(new
                    {
                        message = "Username or password is incorrect. Please try again"
                    }
                     );
                }
                var isCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isCorrect)
                {
                    return BadRequest(new
                    {
                        message = "Username or password is incorrect. Please try again"
                    }
                     );
                }
                if (_userManager.FindByNameAsync(request.Username).Result.Status == false)
                {
                    return BadRequest(new
                    {
                        message = "Your account is disabled. Please contact with IT Team"
                    }
                    );
                }
                if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        User = user.UserName,
                        //Password = _userManager.PasswordHasher.HashPassword(user, user.NewPassword)
                        Status = user.ChangePassword,
                        Active = user.Status,
                        Role = userRoles,
                        Location = user.Location,
                    });
                }
            }
            return Unauthorized();
        }
        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword request)
        {
            User user = await _userManager.FindByNameAsync(request.Username);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
            user.ChangePassword = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(new
            {
                Status = user.ChangePassword
            });
        }
    }
}
