using HealthRecordsPro.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.ViewModel
{
    public class RoleEditViewModel
    {
#nullable enable
        public IdentityRole? Role { get; set; }
        public IEnumerable<ApplicationUser>? Members { get; set; }
        public IEnumerable<ApplicationUser>? NonMembers { get; set; }
    }
}
