using System.ComponentModel.DataAnnotations;

namespace HealthRecordsPro.ViewModel
{
    public class LoginViewModel:ValidationAttribute
    {
        [Required(ErrorMessage = "Username or Password is Incorrect")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
