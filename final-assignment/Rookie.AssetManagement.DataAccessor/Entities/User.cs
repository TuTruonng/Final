using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rookie.AssetManagement.DataAccessor.Entities
{
    public class User : IdentityUser<int>
    {
        [PersonalData]
        public string FullName { get; set; }

        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime JoinedDate { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Location { get; set; }

        [PersonalData]
        public bool Status { get; set; }
        [PersonalData]
        public bool ChangePassword { get; set; }
        [PersonalData]
		public DateTime CreateDate { get; set; }

        // Navigation property
        public virtual List<Assignment> AssignmentsBy { get; set; }
        public virtual List<Assignment> AssignmentsTo { get; set; }
    }
}
