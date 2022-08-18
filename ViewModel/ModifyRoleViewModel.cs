using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.ViewModel
{
    public class ModifyRoleViewModel
    {
#nullable enable
        [Required]
        public string? RoleName { get; set; }
        public string? RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
