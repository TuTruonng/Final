using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Enums
{
	public enum AssetStateEnum
	{
		Available = 0, // Asset does not belong to any assignment and is in good condition
		NotAvailable = 1, // Asset does not belong to any assignment and is being repaired or warranted
		Assigned = 2, // Asset belongs to an assignment
		WaitingForRecycling = 3, // Asset is not able to use and waiting for recycling
		Recycled = 4, // Asset is not able to use and has been recycled
	}
}
