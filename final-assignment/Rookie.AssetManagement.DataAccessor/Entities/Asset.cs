using Rookie.AssetManagement.DataAccessor.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class Asset
    {
        // Primary key
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public DateTime InstalledDate { get; set; }
        public string Location { get; set; }
        public string Specification { get; set; }
        public string History { get; set; }
        public bool IsDisable { get; set; }

        // Foreign key
        public int CategoryId { get; set; }
        public int StateId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }
        public virtual State State { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}