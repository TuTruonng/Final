using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Business.Request
{
	public class ChangePassword
	{
		public string Username { get; set; }
		public string NewPassword { get; set; }
	}
}
