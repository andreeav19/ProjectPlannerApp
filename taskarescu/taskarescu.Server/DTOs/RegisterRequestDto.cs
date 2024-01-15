using System.ComponentModel.DataAnnotations;
namespace taskarescu.Server.DTOs
{
    public class RegisterRequestDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [MinLength(Consts.UsernameMinLength, ErrorMessage = Consts.UsernameLengthValidationError)]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = Consts.EmailValidationError)]
        public string? Email { get; set; }

        [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordValidationError)]
        public string? Password { get; set; }
    }
}
