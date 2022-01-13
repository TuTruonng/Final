using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Enums
{
	public enum AssignmentStateEnum
	{
		[Display(Name = "Accepted")]
		Accepted = 0, // Assignment is accepted by assignee
		[Display(Name = "Waiting for acceptance")]
		WaitingForAcceptance = 1, // Assignment has been just created and has not been responded by assignee
	}
}
