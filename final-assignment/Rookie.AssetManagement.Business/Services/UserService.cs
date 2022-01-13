using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Request;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
            IBaseRepository<User> userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponseModel<UserDto>> GetByPageAsync(
            UserQueryCriteriaDto userQueryCriteria,
            CancellationToken cancellationToken)
        {
            var userDtoQuery = await UserFilterAsync(
                _userRepository.Entities.AsQueryable().AsNoTracking(),
                userQueryCriteria);

            var userDtoPM = await userDtoQuery
                .PaginateUsersAsync(
                    userQueryCriteria.Page,
                    userQueryCriteria.Limit,
                    cancellationToken,
                    userQueryCriteria.OnTopStaffCode);

            return new PagedResponseModel<UserDto>
            {
                CurrentPage = userDtoPM.CurrentPage,
                TotalPages = userDtoPM.TotalPages,
                TotalItems = userDtoPM.TotalItems,
                Items = userDtoPM.Items
            };
        }

        public async Task<UserDto> AddAsync(CreateUserDto createUserDto)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string usernameFormat = string.Format("{0}{1}",
                createUserDto.FirstName.Trim().ToLower(),
                string.Join(string.Empty, createUserDto.LastName.Split(
                    delimiterChars,
                    StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToLower().First())));

            int index = 1;
            string username = usernameFormat;
            while ((await _userManager.FindByNameAsync(username)) != null) // Has duplicated userName
            {
                username = string.Format("{0}{1}", usernameFormat, index++); // Postfix increment
            }

            string password = string.Format("{0}@{1}",
                username,
                createUserDto.DateOfBirth.ToString("ddMMyyyy"));

            User user = _mapper.Map<User>(createUserDto);
            user.UserName = username;

            var result1 = await _userManager.CreateAsync(user, password);
            if (result1.Succeeded)
            {
                user = await _userManager.FindByNameAsync(username);
                var result2 = await _userManager.AddToRoleAsync(user, (createUserDto.Type == Roles.Admin) ? Roles.Admin : Roles.Staff);
                if (result2.Succeeded)
                {
                    UserDto userDto = _mapper.Map<UserDto>(user);
                    userDto.Type = (createUserDto.Type == Roles.Admin) ? Roles.Admin : Roles.Staff;
                    return userDto;
                }
            }
            return null;
        }

        public async Task<UserDto> UpdateAsync(int id, EditUserDto editUserDto)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            user = _mapper.Map<EditUserDto, User>(editUserDto, user);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var roles = new string[] { Roles.Admin, Roles.Staff };
                if (roles.Contains(editUserDto.Type))
                {
                    IList<string> userRoles = await _userManager.GetRolesAsync(user);

                    // Remove from current role(s) if have more than 1 role or in different role
                    // User can only have 1 role (either 'Admin' or 'Staff')
                    if (userRoles.Count > 1 || !userRoles.Contains(editUserDto.Type))
                    {
                        await _userManager.RemoveFromRolesAsync(user, userRoles);
                        await _userManager.AddToRoleAsync(user, editUserDto.Type);
                    }
                }
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }

        public async Task<UserDto> DisableAsync(int id)
        {
            //User user = await _userManager.FindByIdAsync(id);
            User user = await _userRepository.Entities.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null /*|| user.Status == false*/)
            {
                throw new NotFoundException("Error");
            }

            user.Status = false;

            var userUpdated = await _userRepository.Update(user);

            var userdUpdatedDto = _mapper.Map<UserDto>(userUpdated);

            return userdUpdatedDto;
        }

        public async Task<bool> ChangeUserPasswordAsync(ChangeUserPasswordDto changeUserPasswordDto)
        {
            User user = await _userManager.FindByNameAsync(changeUserPasswordDto.Username);

            var result = await _userManager.ChangePasswordAsync(user, changeUserPasswordDto.CurrentPassword, changeUserPasswordDto.NewPassword);
            return result.Succeeded;
        }


        #region Private Method
        private async Task<IQueryable<UserDto>> UserFilterAsync(
            IQueryable<User> userQuery,
            UserQueryCriteriaDto userQueryCriteria)
        {
            // Only show undisabled users
            userQuery = userQuery.Where(u => u.Status == true); // True - Not disable

            // Same location as admin
            if (!string.IsNullOrEmpty(userQueryCriteria.Location))
            {
                userQuery = userQuery.Where(u => u.Location == userQueryCriteria.Location);
            }

            if (!string.IsNullOrEmpty(userQueryCriteria.Search))
            {
                int staffCode = 0;
                if (int.TryParse(userQueryCriteria.Search, out int result))
                {
                    staffCode = result;
                }
                userQuery = userQuery.Where(u => u.Id == staffCode
                                     || u.FullName.Contains(userQueryCriteria.Search));
            }

            var adminIdList = (await _userManager.GetUsersInRoleAsync(Roles.Admin)).Select(u => u.Id);
            var staffIdList = (await _userManager.GetUsersInRoleAsync(Roles.Staff)).Select(u => u.Id);

            var userDtoQuery = userQuery.Select(u => new UserDto
            {
                StaffCode = u.Id,
                FullName = u.FullName,
                Username = u.UserName,
                JoinedDate = u.JoinedDate,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DOB,
                Gender = u.Gender,
                Location = u.Location,
                Status = u.Status,
                Type = (adminIdList.Contains(u.Id) && staffIdList.Contains(u.Id))
                    ? string.Join(",", new string[] { Roles.Admin, Roles.Staff }) // Have both 'Admin' and 'Staff' roles
                    : (adminIdList.Contains(u.Id))
                        ? Roles.Admin // Have only 'Admin' role
                        : (staffIdList.Contains(u.Id))
                            ? Roles.Staff // Have only 'Staff' role
                            : string.Empty // Have neither 'Admin' nor 'Staff' role
            });

            if (userQueryCriteria.Types != null &&
                userQueryCriteria.Types.Length > 0)
            {
                var types = userQueryCriteria.Types;
                userDtoQuery = userDtoQuery.Where(u => types.Contains(u.Type));
            }

            userDtoQuery = userQueryCriteria.SortColumn switch
            {
                SortUserColumnEnumDto.StaffCode => (userQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
                    ? userDtoQuery.OrderByDescending(u => u.StaffCode)
                    : userDtoQuery.OrderBy(u => u.StaffCode),
                SortUserColumnEnumDto.FullName => (userQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
                    ? userDtoQuery.OrderByDescending(u => u.FullName)
                    : userDtoQuery.OrderBy(u => u.FullName),
                SortUserColumnEnumDto.Username => (userQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
                    ? userDtoQuery.OrderByDescending(u => u.Username)
                    : userDtoQuery.OrderBy(u => u.Username),
                SortUserColumnEnumDto.JoinedDate => (userQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
                    ? userDtoQuery.OrderByDescending(u => u.JoinedDate)
                    : userDtoQuery.OrderBy(u => u.JoinedDate),
                SortUserColumnEnumDto.Type => (userQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
                    ? userDtoQuery.OrderByDescending(u => u.Type)
                    : userDtoQuery.OrderBy(u => u.Type),
                _ => userDtoQuery.OrderBy(u => u.StaffCode),
            };

            return userDtoQuery;
        }
        #endregion
    }
}
