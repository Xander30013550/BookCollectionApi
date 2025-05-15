using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models
{
    public class UserViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
