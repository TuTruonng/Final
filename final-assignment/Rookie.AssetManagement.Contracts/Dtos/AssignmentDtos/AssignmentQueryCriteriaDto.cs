using Rookie.AssetManagement.Contracts.Dtos.EnumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssignmentDtos
{
	public class AssignmentQueryCriteriaDto : BaseQueryCriteria
	{
		public string Search { get; set; }
		public new int Limit { get; set; } = 5;
		public string[] States { get; set; }
		public DateTime? AssignedDate { get; set; }
		public SortAssignmentColumnEnumDto SortColumn { get; set; }
		public SortOrderEnumDto SortOrder { get; set; }
		public int OnTopAssignmentNumber { get; set; } = 0;
	}
}
