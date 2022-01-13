using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit,
            CancellationToken cancellationToken)
            where TModel : class
        {

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            // var totalItemsCountTask = await query.CountAsync(cancellationToken);

            var startRow = (page - 1) * limit;

            paged.Items = await query
                        .Skip(startRow)
                        .Take(limit)
                        .ToListAsync(cancellationToken);

            paged.TotalItems = await query.CountAsync(cancellationToken);
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }

        public static async Task<PagedModel<UserDto>> PaginateUsersAsync(
            this IQueryable<UserDto> query,
            int page,
            int limit,
            CancellationToken cancellationToken,
            int onTopstaffCode = 0)
        {
            var paged = new PagedModel<UserDto>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            var startRow = (page - 1) * limit;

            var userList = await query.ToListAsync(cancellationToken);
            var user = userList.Find(u => u.StaffCode == onTopstaffCode);
            if (userList.Remove(user))
            {
                userList.Insert(0, user);
            }

            paged.Items = userList.Skip(startRow).Take(limit).ToList();

            paged.TotalItems = await query.CountAsync(cancellationToken);
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }

		public static PagedModel<AssetDto> PaginateAssets(
			this IEnumerable<AssetDto> query,
			int page,
			int limit,
			string onTopAssetCode = null)
		{
			var paged = new PagedModel<AssetDto>();

			page = (page < 0) ? 1 : page;

			paged.CurrentPage = page;
			paged.PageSize = limit;

			var startRow = (page - 1) * limit;

			var assetList = query.ToList();
			var asset = assetList.Find(u => u.AssetCode == onTopAssetCode);
			if (assetList.Remove(asset))
			{
				assetList.Insert(0, asset);
			}

			paged.Items = assetList.Skip(startRow).Take(limit).ToList();

			paged.TotalItems = query.Count();
			paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

			return paged;
		}

        public static PagedModel<AssignmentDto> PaginateAssignments(
            this IEnumerable<AssignmentDto> query,
            int page,
            int limit,
            int onTopAssignmentNumber = 0)
        {
            var paged = new PagedModel<AssignmentDto>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            var startRow = (page - 1) * limit;

            var assignmentList = query.ToList();
            var assignment = assignmentList.Find(u => u.AssignmentNumber == onTopAssignmentNumber);
            if (assignmentList.Remove(assignment))
            {
                assignmentList.Insert(0, assignment);
            }

            paged.Items = assignmentList.Skip(startRow).Take(limit).ToList();

            paged.TotalItems = query.Count();
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}