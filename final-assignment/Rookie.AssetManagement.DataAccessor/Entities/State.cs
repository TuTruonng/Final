using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
	public class State
	{
		public int StateId { get; set; }
		public string Name { get; set; }
		public virtual List<Asset> Assets { get; set; }
	}
}
