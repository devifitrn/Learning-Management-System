using System.ComponentModel.DataAnnotations;

namespace API.Models.Views
{
    public class ResetPasswordVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
