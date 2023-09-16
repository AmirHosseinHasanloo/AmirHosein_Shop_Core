using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AmirHosein_Shop.Controllers
{
    public class ProductController : Controller
    {
        EshopContext _context;

        public ProductController(EshopContext context)
        {
            _context = context;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id,string name)
        {
            ViewData["GroupName"] = name;
            var Products = _context.CategoryToProducts.Where(p => p.CategoryId == id).Select(p => p.Product).ToList();
            return View(Products);
        }
    }
}
