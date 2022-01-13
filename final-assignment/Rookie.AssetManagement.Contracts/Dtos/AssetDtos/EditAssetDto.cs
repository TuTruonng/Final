﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
	public class EditAssetDto
	{
		public string AssetName { get; set; }
		public string Specification { get; set; }
		public DateTime InstalledDate { get; set; }
		public string State { get; set; }
	}
}
