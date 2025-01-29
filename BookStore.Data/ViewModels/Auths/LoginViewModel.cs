
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.ViewModels.Auths
{
    public class LoginViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
