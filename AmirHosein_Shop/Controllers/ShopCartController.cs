using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AmirHosein_Shop.Controllers
{
    public class ShopCartController : Controller
    {
        private EshopContext _context;
        private static ShopCart _cart = new ShopCart();
        public ShopCartController(EshopContext context)
        {
            _context = context;
        }

        [Route("AddToCart/{id}")]
        public IActionResult AddToCart(int id)
        {
            var Product = _context.Products.Include(i => i.Item).SingleOrDefault(i => i.Id == id);

            if (Product != null)
            {
                var ShopCartItem = new ShopCartItem()
                {
                    Item = Product.Item,
                    Quantity = 1
                };
                _cart.AddShopCartItem(ShopCartItem);
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            var CartVM = new CartViewModel()
            {
                shopCartItems = _cart.ShopCartItems,
                OrderTotal = _cart.ShopCartItems.Sum(c => c.GetTotalPrice())
            };
            return View(CartVM);
        }

        public IActionResult DeleteCart(int id)
        {
            _cart.DeleteShopCartItem(id);
            return RedirectToAction("ShowCart");
        }


    }
}
