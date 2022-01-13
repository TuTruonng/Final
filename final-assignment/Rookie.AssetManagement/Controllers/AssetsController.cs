using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.Contracts;
using Rookie.AssetManagement.Contracts.Dtos.AssetDtos;
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
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public ActionResult<PagedResponseModel<AssetDto>> GetAssets(
            [FromQuery] AssetQueryCriteriaDto assetCriteriaDto,
            CancellationToken cancellationToken)
        {
            var assetsResponse = _assetService.GetByPage(
                assetCriteriaDto,
                cancellationToken);

            return Ok(assetsResponse);
        }

        [HttpPost]
        public async Task<ActionResult<AssetDto>> CreateAsset(
            [FromBody] CreateAssetDto createAssetDto)
        {
            var assetDto = await _assetService.AddAsync(createAssetDto);
            if (assetDto == null)
            {
                return Problem(statusCode: 500);
            }

            return CreatedAtAction(nameof(CreateAsset), new { assetCode = assetDto.AssetCode }, assetDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssetDto>> EditAsset(string id,
            [FromForm] EditAssetDto editAssetDto)
		{
            int assetId = 0;
            if (int.TryParse(id.Substring(3), out int res))
			{
                assetId = res;
			}

            var assetDto = await _assetService.UpdateAsync(assetId, editAssetDto);
            if (assetDto == null)
			{
                return NotFound();
			}
            return Ok(assetDto);
        }
	}
}