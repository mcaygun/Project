using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
