using AutoMapper;
using BookStore.Data.Context;
using BookStore.Data.Entities;
using BookStore.Data.ViewModels.Products;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToastNotification toastNotification;
        private readonly IMapper _mapper;

        private AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, IToastNotification toastNotification,  AppDbContext context)
        {
            _logger = logger;
            this.toastNotification = toastNotification;
            _context = context;
        }

        public IActionResult Index()
        {

            //toastNotification.AddErrorToastMessage("selam kanka");
            //toastNotification.AddInfoToastMessage("selam kanka");
            //toastNotification.AddWarningToastMessage("selam kanka");
            //toastNotification.AddSuccessToastMessage("selam kanka");

            var products = _context.Products
                                  .OrderByDescending(p => p.Id)
                                  .Take(10) // Son 10 ürünü getiriyoruz (isteðe baðlý)
                                  .ToList();

            return View(products);
           
        }
     

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        



    }
}
