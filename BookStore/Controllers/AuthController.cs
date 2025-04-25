using BookStore.Data.ViewModels.Auths;
using BookStore.Service.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BookStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IToastNotification toastNotification;

        public AuthController(IAuthService authService, IToastNotification toastNotification)
        {
            this.authService = authService;
            this.toastNotification = toastNotification;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await authService.Login(model);
                if (response.HasError)
                {
                    // toastr mesaj
                    toastNotification.AddErrorToastMessage(response.ErrorMessage);
                    return Redirect(Request.Headers["Referer"].ToString()); // login sayfası olmadığı için geldiği sayfaya geri atsın
                }

                // toastr başarılı
                toastNotification.AddSuccessToastMessage("Giriş başarılı!");
                return Redirect(Request.Headers["Referer"].ToString());     // geldiği sayfasına geri atsın.
                //return RedirectToAction("Index", "Home");
            }

            toastNotification.AddErrorToastMessage("Alanları eksiksiz doldurunuz!");
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await authService.Register(model);
                if (response.HasError)
                {
                    // toastr mesaj
                    toastNotification.AddErrorToastMessage(response.ErrorMessage);
                    return View(model);
                }

                // toastr başarılı
                toastNotification.AddSuccessToastMessage("Kayıt başarılı!");
                return RedirectToAction("Login");
            }

            toastNotification.AddErrorToastMessage("Alanları eksiksiz doldurunuz!");
            return View(model);
        }
    }
}
