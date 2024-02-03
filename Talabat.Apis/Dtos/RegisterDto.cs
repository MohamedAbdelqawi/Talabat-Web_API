using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class RegisterDto
    {

        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{6,}$", ErrorMessage = "the password contains at least one lowercase letter, one uppercase letter, one digit, and one non-alphanumeric character, and is at least 6 characters long. ")]
        public string Password { get; set; }
    }
}
