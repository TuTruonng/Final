using Rookie.AssetManagement.DataAccessor.Entities;
using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
	public static class DefaultAssignments
	{
		public static Assignment[] SeedAssignments()
		{
			int idx = 1;
			return new Assignment[]
			{
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 3,
					AssignedToUserId = 2,
					AssignedByUserId = 4,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.WaitingForAcceptance,
					AssetId = 7,
					AssignedToUserId = 11,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.WaitingForAcceptance,
					AssetId = 8,
					AssignedToUserId = 12,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 13,
					AssignedToUserId = 13,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 14,
					AssignedToUserId = 13,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 20,
					AssignedToUserId = 14,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.WaitingForAcceptance,
					AssetId = 21,
					AssignedToUserId = 14,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 22,
					AssignedToUserId = 14,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 24,
					AssignedToUserId = 15,
					AssignedByUserId = 1,
				},
				new()
				{
					AssignmentId = idx++,
					AssignedDate = new DateTime(2021, 12, 9),
					State = AssignmentStateEnum.Accepted,
					AssetId = 25,
					AssignedToUserId = 15,
					AssignedByUserId = 1,
				},
			};

		}
	}
}
