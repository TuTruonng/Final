using Microsoft.AspNetCore.Identity;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
	public interface IUserService
	{
		Task<PagedResponseModel<UserDto>> GetByPageAsync(UserQueryCriteriaDto userQueryCriteria, CancellationToken cancellationToken);
		Task<UserDto> DisableAsync(int id);
		Task<UserDto> AddAsync(CreateUserDto createUserDto);
		Task<UserDto> UpdateAsync(int id, EditUserDto editUserDto);
		//Task<UserDto> GetByIdAsync(int id);
		//Task<bool> DeleteAsync(int id);
		Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordDto changeUserPasswordDto);
	}
}
