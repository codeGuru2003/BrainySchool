using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.ViewModel
{
    public class RoleViewModel
    {
#nullable enable
        [Required]
        public string? Name { get; set; }
        public string? Id { get; set; }
    }
}
