using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.ViewModel
{
    public class SigninViewModel
    {
#nullable enable
        [Required]
        public string? Username { get; set; }
        public string? LoginHint { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
    }
}
