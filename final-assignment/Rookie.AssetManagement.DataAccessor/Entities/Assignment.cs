using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
	public class Assignment
	{
		// Primary key
		public int AssignmentId { get; set; }
		public DateTime AssignedDate { get; set; }
		public AssignmentStateEnum State { get; set; }
		public string Note { get; set; }

		// Foreign key
		public int AssetId { get; set; }
		public int AssignedToUserId { get; set; }
		public int AssignedByUserId { get; set; }

		// Navigation property
		public virtual Asset Asset { get; set; }
		public virtual User AssignedToUser { get; set; }
		public virtual User AssignedByUser { get; set; }
	}
}
