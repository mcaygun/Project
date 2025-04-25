
using BookStore.Data.Entities;
using BookStore.Data.ViewModels.Auths;
using BookStore.Service.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }


        public async Task<ServiceResponse> Login(LoginViewModel model)
        {
            ServiceResponse response = new ServiceResponse();

            AppUser? user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                response.AddErrorMessage("Email veya şifre yanlış");
                return response;
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                response.AddErrorMessage("Email veya şifre yanlış");
                return response;
            }

            // giriş başarılı.
            return response;
        }

        public async Task<ServiceResponse> Register(RegisterViewModel model)
        {
            ServiceResponse response = new ServiceResponse();

            // password == passwordconfirm ekle. değilse hata mesajı ekle geri return yap

            AppUser user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                response.AddErrorMessage(result.Errors.First().Description);
                return response;
            }

            // rol ekleme mantığı.
            var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Name.ToLower() == "admin");
            await userManager.AddToRoleAsync(user, role.ToString());
            // rol ekleme mantığı

            return response;
        }
    }
}
