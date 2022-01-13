using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.Business.Request;
using Rookie.AssetManagement.Business.Response;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole<int>> _roleManager;
		public RolesController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		[HttpPost("AssignRole")]
		public async Task<IActionResult> AssignRole([FromBody] LoginRequest request)
		{
			var userExist = await _userManager.FindByNameAsync(request.Username);
			if (userExist != null)
				return StatusCode(StatusCodes.Status500InternalServerError, new LoginResponse { Status = "Error", Massage = "User already exist" });
			User user = new User()
			{
				UserName = request.Username,
				ChangePassword = false,
				Status = true,
				SecurityStamp = Guid.NewGuid().ToString(),
			};
			var result = await _userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new LoginResponse { Status = "Error", Massage = "User Creation Faild" });
			}
			if (!await _roleManager.RoleExistsAsync(Roles.Admin))
				await _roleManager.CreateAsync(new IdentityRole<int>(Roles.Admin));
			if (!await _roleManager.RoleExistsAsync(Roles.Staff))
				await _roleManager.CreateAsync(new IdentityRole<int>(Roles.Staff));
			if (await _roleManager.RoleExistsAsync(Roles.Admin))
			{
				await _userManager.AddToRoleAsync(user, Roles.Admin);
			}
			return Ok(new LoginResponse { Status = "Success", Massage = "User Created Successfully" });
		}
	}
}
