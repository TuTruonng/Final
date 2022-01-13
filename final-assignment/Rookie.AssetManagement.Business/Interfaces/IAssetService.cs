using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Interfaces
{
    public interface IAssetService
    {
        PagedResponseModel<AssetDto> GetByPage(AssetQueryCriteriaDto AssetQueryCriteria, CancellationToken cancellationToken);
		Task<AssetDto> AddAsync(CreateAssetDto createAssetDto);
		Task<AssetDto> UpdateAsync(int id, EditAssetDto editAssetDto);

		//Task<UserDto> GetByIdAsync(int id);
		//Task<bool> DeleteAsync(int id);
		//Task<AssetDto> DisableAsync(int id);
	}
}