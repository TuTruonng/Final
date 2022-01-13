using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.Business.Interfaces;
using Rookie.AssetManagement.Business.Request;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using Rookie.AssetManagement.DataAccessor.Data;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Services
{
	public class AssetService : IAssetService
	{
		//private readonly UserManager<User> _userManager;
		//private readonly ApplicationDbContext _context;
		private readonly IBaseRepository<Asset> _assetRepository;
		private readonly IBaseRepository<State> _stateRepository;
		private readonly IBaseRepository<Category> _categoryRepository;
		private readonly IMapper _mapper;

		public AssetService(
			IBaseRepository<Asset> assetRepository,
			IBaseRepository<State> stateRepository,
			IBaseRepository<Category> categoryRepository,
			IMapper mapper)
		{
			_assetRepository = assetRepository;
			_stateRepository = stateRepository;
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}
		public PagedResponseModel<AssetDto> GetByPage(
			AssetQueryCriteriaDto assetQueryCriteria,
			CancellationToken cancellationToken)
		{
			var assetDtoQuery = AssetFilter(
				_assetRepository.Entities.AsQueryable().AsNoTracking(),
				assetQueryCriteria);

			var assetDtoPM = assetDtoQuery.AsQueryable().AsNoTracking()
				.PaginateAssets(
					assetQueryCriteria.Page,
					assetQueryCriteria.Limit,
					assetQueryCriteria.OnTopAssetCode);

			return new PagedResponseModel<AssetDto>
			{
				CurrentPage = assetDtoPM.CurrentPage,
				TotalPages = assetDtoPM.TotalPages,
				TotalItems = assetDtoPM.TotalItems,
				Items = assetDtoPM.Items
			};
		}

		public async Task<AssetDto> AddAsync(CreateAssetDto createAssetDto)
		{
			Category category = await _categoryRepository.Entities.Where(c => c.Name == createAssetDto.Category).SingleOrDefaultAsync();
			State state = await _stateRepository.Entities.Where(s => s.Name == createAssetDto.State).SingleOrDefaultAsync();
			if (category == null || state == null)
			{
				return null;
			}

			var asset = _mapper.Map<Asset>(createAssetDto);
			asset.Category = category;
			asset.State = state;

			asset = await _assetRepository.Add(asset);

			var assetDto = _mapper.Map<AssetDto>(asset);
			return assetDto;
		}

		public async Task<AssetDto> UpdateAsync(int id, EditAssetDto editAssetDto)
		{
			Asset asset = await _assetRepository.Entities.Where(a => a.AssetId == id).SingleOrDefaultAsync();
			if (asset == null)
			{
				return null;
			}

			asset = _mapper.Map<EditAssetDto, Asset>(editAssetDto, asset);
			State state = await _stateRepository.Entities.Where(s => s.StateId == asset.StateId).SingleOrDefaultAsync();
			if (state.Name != editAssetDto.State)
			{
				state = await _stateRepository.Entities.Where(s => s.Name == editAssetDto.State).SingleOrDefaultAsync();
				if (state == null)
				{
					return null;
				}
				asset.State = state;
			}

			asset = await _assetRepository.Update(asset);

			// Load reference to Category to set AssetCode in AutoMapper
			await _assetRepository.Entry(asset).Reference(a => a.Category).LoadAsync();
			var assetDto = _mapper.Map<AssetDto>(asset);
			return assetDto;
		}


		#region Private Method
		private IEnumerable<AssetDto> AssetFilter(
			IQueryable<Asset> assetQuery,
			AssetQueryCriteriaDto assetQueryCriteria)
		{
			// Same location as admin
			if (!string.IsNullOrEmpty(assetQueryCriteria.Location))
			{
				assetQuery = assetQuery.Where(a => a.Location == assetQueryCriteria.Location);
			}

			var dtoQuery = assetQuery.Select(a => new
			{
				a.AssetId,
				CategoryPrefix = a.Category.Prefix,
				a.AssetName,
				CategoryName = a.Category.Name,
				a.History,
				a.InstalledDate,
				a.IsDisable,
				a.Location,
				a.Specification,
				StateName = a.State.Name,
			});
			// Error in IQueryable: Translation of method 'string.Format' failed. 
			// Change from IQueryable to IEnumerable
			var assetDtoQuery = dtoQuery.AsEnumerable().Select(d => new AssetDto
			{
				AssetCode = string.Format("{0}1{1:D5}", d.CategoryPrefix, d.AssetId),
				AssetName = d.AssetName,
				Category = d.CategoryName,
				History = d.History,
				InstalledDate = d.InstalledDate,
				IsDisable = d.IsDisable,
				Location = d.Location,
				Specification = d.Specification,
				State = d.StateName,
			});

			if (!string.IsNullOrEmpty(assetQueryCriteria.Search))
			{
				var searchTerm = assetQueryCriteria.Search;
				assetDtoQuery = assetDtoQuery.Where(a => 
					a.AssetCode.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
					|| a.AssetName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
			}

			if (assetQueryCriteria.States != null &&
				assetQueryCriteria.States.Length > 0)
			{
				var states = assetQueryCriteria.States.Select(c => c.ToLower());
				assetDtoQuery = assetDtoQuery.Where(a => states.Contains(a.State.ToLower()));
			}

			if (assetQueryCriteria.Categories != null &&
				assetQueryCriteria.Categories.Length > 0)
			{
				var categories = assetQueryCriteria.Categories.Select(c => c.ToLower());
				assetDtoQuery = assetDtoQuery.Where(a => categories.Contains(a.Category.ToLower()));
			}

			assetDtoQuery = assetQueryCriteria.SortColumn switch
			{
				SortAssetColumnEnumDto.AssetCode => (assetQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assetDtoQuery.OrderByDescending(a => a.AssetCode)
					: assetDtoQuery.OrderBy(a => a.AssetCode),
				SortAssetColumnEnumDto.AssetName => (assetQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assetDtoQuery.OrderByDescending(a => a.AssetName)
					: assetDtoQuery.OrderBy(a => a.AssetName),
				SortAssetColumnEnumDto.Category => (assetQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assetDtoQuery.OrderByDescending(a => a.Category)
					: assetDtoQuery.OrderBy(a => a.Category),
				SortAssetColumnEnumDto.State => (assetQueryCriteria.SortOrder == SortOrderEnumDto.Decsending)
					? assetDtoQuery.OrderByDescending(a => a.State)
					: assetDtoQuery.OrderBy(a => a.State),
				_ => assetDtoQuery.OrderBy(a => a.AssetCode),
			};

			return assetDtoQuery;
		}
		#endregion
	}
}