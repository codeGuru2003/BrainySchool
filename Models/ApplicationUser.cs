using Microsoft.AspNetCore.Identity;

namespace HealthRecordsPro.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string LoginHint { get; set; } = null;
    }
}
