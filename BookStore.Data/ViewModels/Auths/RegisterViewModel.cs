
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.ViewModels.Auths
{
    public class RegisterViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor!")]
        public string PasswordConfirm { get; set; }
    }
}
