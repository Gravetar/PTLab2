using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab2.Models;
using System.Diagnostics;

namespace PTLab2.Controllers
{
    public class HomeController : Controller
    {
        ShopContext _context;

        public HomeController(ILogger<HomeController> logger, ShopContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult ToShop()
        {
            return RedirectToActionPermanent("CreateShop", "Shop");
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Authorization()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}