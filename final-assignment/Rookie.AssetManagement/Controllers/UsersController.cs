using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System.Threading;
using Rookie.AssetManagement.Business.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System;
using Rookie.AssetManagement.Validators;

namespace Rookie.AssetManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IUserService _userService;
		//private readonly RoleManager<IdentityRole> _roleMaaager;

		public UsersController(UserManager<User> userManager,
			IUserService userService/*, RoleManager<IdentityRole> roleManager*/)
		{
			_userManager = userManager;
			_userService = userService;
			//_roleMaaager = roleManager;
		}

		[HttpGet]
		public async Task<ActionResult<PagedResponseModel<UserDto>>> GetUsers(
			[FromQuery] UserQueryCriteriaDto userCriteriaDto,
			CancellationToken cancellationToken)
		{
			var usersResponse = await _userService.GetByPageAsync(
				userCriteriaDto,
				cancellationToken);

			return Ok(usersResponse);
		}

		[HttpPost]
		public async Task<ActionResult<UserDto>> CreateUser(
			[FromBody] CreateUserDto createUserDto)
		{
			UserDto dto = await _userService.AddAsync(createUserDto);
			if (dto == null)
			{
				return Problem(statusCode: 500);
			}

			return CreatedAtAction(nameof(CreateUser), new { staffCode = dto.StaffCode }, dto);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<UserDto>> EditUser(int id,
			[FromForm] EditUserDto editUserDto)
		{
			if ((await _userManager.FindByIdAsync(id.ToString())) == null)
			{
				return NotFound();
			}

			UserDto dto = await _userService.UpdateAsync(id, editUserDto);
			if (dto == null)
			{
				return Problem(statusCode: 500);
			}

			return Ok(dto);
		}

		[HttpPut("disable/{id}")]
		public async Task<ActionResult<UserDto>> DisableUser(
			[FromRoute] int id)
		{
			var updatedUser = await _userService.DisableAsync(id);

			return Ok(updatedUser);
		}

		[HttpPut("password")]
		public async Task<ActionResult> ChangeUserPassword(
			[FromBody] ChangeUserPasswordDto changeUserPasswordDto)
		{
			User user = await _userManager.FindByNameAsync(changeUserPasswordDto.Username);
			if (user == null)
			{
				return NotFound();
			}

			bool succeeded = await _userService.ChangeUserPasswordAsync(changeUserPasswordDto);
			if (succeeded)
			{
				return Ok(new { Message = "Your password has been changed successfully" });
			}
			return BadRequest(new { Message = "Password is incorrect" });
		}
	}
}
