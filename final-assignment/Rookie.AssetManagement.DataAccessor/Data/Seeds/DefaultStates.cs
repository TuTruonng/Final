using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
	public class DefaultStates
	{
		public static State[] SeedStates()
		{
			return new State[]
			{
				new()
				{
					StateId = 1,
					Name = "Assigned",
				},
				new()
				{
					StateId = 2,
					Name = "Available",
				},
				new()
				{
					StateId = 3,
					Name = "Not available",
				},
				new()
				{
					StateId = 4,
					Name = "Waiting for recycling",
				},
				new()
				{
					StateId = 5,
					Name = "Recycled",
				}
			};
		}
	}
}
