using System.ComponentModel.DataAnnotations;

namespace API.Models.Views
{
    public class ChangePasswordVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
