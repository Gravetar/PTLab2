using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTLab2.Models;

namespace PTLab2.Controllers
{
    public class ShopController : Controller
    {
        ShopContext _context;

        public ShopController(ILogger<HomeController> logger, ShopContext context)
        {
            _context = context;
        }

        public IActionResult CreateShop()
        {
            User? user = _context.Users.FirstOrDefault(u => u.Mail == User.Identity.Name);
            ViewData["TotalAmountUser"] = user?.TotalAmount.ToString();

            List<Product> model = new List<Product>(_context.Products);

            foreach (Product item in model)
            {
                double sale = (double)(user.TotalAmount / 10000 * item.Price / 100);

                item.NewPrice -= (int)Math.Round(sale);

                if (item.NewPrice < 0) item.NewPrice = 1;
            }
            return View("~/Views/Shop/Shop.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(int? id)
        {
            if (id != null)
            {
                Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                User? user = await _context.Users.FirstOrDefaultAsync(p => p.Mail == User.Identity.Name);
                if (product != null && user != null)
                {
                    double sale = (double)(user.TotalAmount / 10000 * product.Price / 100);
                    int newProductPrice = product.Price - (int)Math.Round(sale);

                    if (newProductPrice < 0) newProductPrice = 1;

                    user.TotalAmount += newProductPrice;
                    await _context.SaveChangesAsync();
                    return Redirect("~/Home/");
                }
            }
            return NotFound();
        }
    }
}
