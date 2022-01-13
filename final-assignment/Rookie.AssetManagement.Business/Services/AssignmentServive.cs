using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos;
using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
	public class AssignmentServive : IAssignmentService
	{
		private readonly IBaseRepository<Assignment> _assignmentRepository;
		//private readonly IBaseRepository<Asset> _assetRepository;
		//private readonly IBaseRepository<Category> _categoryRepository;
		//private readonly IBaseRepository<State> _stateRepository;
		//private readonly IBaseRepository<User> _userRepository;
		private readonly IMapper _mapper;

		public AssignmentServive(
			IBaseRepository<Assignment> assignmentRepository,
			IMapper mapper)
		{
			_assignmentRepository = assignmentRepository;
			_mapper = mapper;
		}

		public PagedResponseModel<AssignmentDto> GetByPage(
			AssignmentQueryCriteriaDto assignmentQueryCriteria,
			CancellationToken cancellationToken)
		{
			var assignmentDtoQuery = AssignmentFilter(
				_assignmentRepository.Entities.AsQueryable().AsNoTracking(),
				assignmentQueryCriteria);

			var assignmentDtoPM = assignmentDtoQuery.AsQueryable().AsNoTracking()
				.PaginateAssignments(
					assignmentQueryCriteria.Page,
					assignmentQueryCriteria.Limit,
					assignmentQueryCriteria.OnTopAssignmentNumber);

			return new PagedResponseModel<AssignmentDto>
			{
				CurrentPage = assignmentDtoPM.CurrentPage,
				TotalPages = assignmentDtoPM.TotalPages,
				TotalItems = assignmentDtoPM.TotalItems,
				Items = assignmentDtoPM.Items
			};
		}

		#region Private Method
		private IEnumerable<AssignmentDto> AssignmentFilter(
			IQueryable<Assignment> assignmentQuery,
			AssignmentQueryCriteriaDto assignmentQueryCriteria)
		{
			var dtoQuery = assignmentQuery.Select(am => new
			{
				am.AssignmentId,
				am.AssetId,
				CategoryPrefix = am.Asset.Category.Prefix,
				am.Asset.AssetName,
				AssignedTo = am.AssignedToUser.UserName,
				AssignedBy = am.AssignedByUser.UserName,
				am.AssignedDate,
				am.State,
			});
			// Error in IQueryable: Translation of method 'string.Format' failed. 
			// Change from IQueryable to IEnumerable
			var assignmentDtoEnumer = dtoQuery.AsEnumerable().Select(d => new AssignmentDto
			{
				AssignmentNumber = d.AssignmentId,
				AssetCode = string.Format("{0}1{1:D5}", d.CategoryPrefix, d.AssetId),
				AssetName = d.AssetName,
				AssignedTo = d.AssignedTo,
				AssignedBy = d.AssignedBy,
				AssignedDate = d.AssignedDate,
				State = d.State.GetDisplayName(),
			});

			if (!string.IsNullOrEmpty(assignmentQueryCriteria.Search))
			{
				var searchTerm = assignmentQueryCriteria.Search;
				assignmentDtoEnumer = assignmentDtoEnumer.Where(am =>
					am.AssetCode.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
					|| am.AssetName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
					|| am.AssignedBy.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
					|| am.AssignedTo.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
			}

			if (assignmentQueryCriteria.States != null &&
				assignmentQueryCriteria.States.Length > 0)
			{
				var states = assignmentQueryCriteria.States.Select(s => s.ToLower());
				assignmentDtoEnumer = assignmentDtoEnumer.Where(am => states.Contains(am.State.ToLower()));
			}

			if (assignmentQueryCriteria.AssignedDate != null)
			{
				var assignedDate = assignmentQueryCriteria.AssignedDate;
				assignmentDtoEnumer = assignmentDtoEnumer.Where(am => am.AssignedDate.Date.CompareTo(assignedDate) == 0);
			}

			assignmentDtoEnumer = assignmentQueryCriteria.SortColumn switch
			{
				SortAssignmentColumnEnumDto.AssignmentNumber => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssignmentNumber)
					: assignmentDtoEnumer.OrderBy(am => am.AssignmentNumber),
				SortAssignmentColumnEnumDto.AssetCode => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssetCode)
					: assignmentDtoEnumer.OrderBy(am => am.AssetCode),
				SortAssignmentColumnEnumDto.AssetName => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssetName)
					: assignmentDtoEnumer.OrderBy(am => am.AssetName),
				SortAssignmentColumnEnumDto.AssignedTo => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssignedTo)
					: assignmentDtoEnumer.OrderBy(am => am.AssignedTo),
				SortAssignmentColumnEnumDto.AssignedBy => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssignedBy)
					: assignmentDtoEnumer.OrderBy(am => am.AssignedBy),
				SortAssignmentColumnEnumDto.AssignedDate => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.AssignedDate)
					: assignmentDtoEnumer.OrderBy(am => am.AssignedDate),
				SortAssignmentColumnEnumDto.State => (assignmentQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assignmentDtoEnumer.OrderByDescending(am => am.State)
					: assignmentDtoEnumer.OrderBy(am => am.State),
				_ => assignmentDtoEnumer.OrderBy(am => am.AssignmentNumber),
			};

			return assignmentDtoEnumer;
		}
		#endregion
	}
}
