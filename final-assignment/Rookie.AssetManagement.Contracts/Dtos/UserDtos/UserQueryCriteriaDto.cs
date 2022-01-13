using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.UserDtos
{
	public class UserQueryCriteriaDto : BaseQueryCriteria
	{
		public string Search { get; set; }
		public new int Limit { get; set; } = 5;
		public string[] Types { get; set; }
		public SortUserColumnEnumDto SortColumn { get; set; }
		public SortOrderEnumDto SortOrder { get; set; }
		public string Location { get; set; }
		public int OnTopStaffCode { get; set; } = 0;
	}
}
