
using BookStore.Data.ViewModels.Auths;

namespace BookStore.Service.Services.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse> Login(LoginViewModel model);
        Task<ServiceResponse> Register(RegisterViewModel model);
    }
}
