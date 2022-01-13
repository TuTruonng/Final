using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Contracts.Dtos.AssetDtos
{
    public class AssetDto
    {
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public DateTime InstalledDate { get; set; }
        public string Location { get; set; }
        public string Specification { get; set; }
        public string History { get; set; }
        public bool IsDisable { get; set; }
    }
}