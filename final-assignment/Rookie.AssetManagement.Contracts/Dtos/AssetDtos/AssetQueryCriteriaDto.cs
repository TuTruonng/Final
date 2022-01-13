using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetQueryCriteriaDto : BaseQueryCriteria
    {
        public string Search { get; set; }
        public new int Limit { get; set; } = 5;
        public string[] States { get; set; }
        public string[] Categories { get; set; }
        public SortAssetColumnEnumDto SortColumn { get; set; }
        public SortOrderEnumDto SortOrder { get; set; }
        public string Location { get; set; }
        public string OnTopAssetCode { get; set; } = null;
    }
}