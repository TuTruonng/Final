using Rookie.AssetManagement.DataAccessor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Data.Seeds
{
	public static class DefaultCategories
	{
		public static Category[] SeedCategories()
		{
			return new Category[]
			{
				new()
				{
					CategoryId = 1,
					Name = "Laptop",
					Prefix = "LA",
				},
				new()
				{
					CategoryId = 2,
					Name = "Monitor",
					Prefix = "MO",
				},
				new()
				{
					CategoryId = 3,
					Name = "Personal Computer",
					Prefix = "PC",
				}
			};
		}

	}
}
