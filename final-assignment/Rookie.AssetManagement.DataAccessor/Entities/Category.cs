using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class Category
    {
        // Primary key
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }

        // Navigation property
        public virtual List<Asset> Assets { get; set; }
    }
}