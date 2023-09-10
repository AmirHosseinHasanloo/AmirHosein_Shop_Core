using AmirHosein_Shop.Models;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AmirHosein_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EshopContext _context;

        public HomeController(ILogger<HomeController> logger, EshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [Route("Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.Id == id);
          
            var categories = _context.Categories.Where(c => c.Id == id).SelectMany
                (p => p.categoryToProducts).Select(p => p.Category).ToList();

            if(product== null || categories == null)
            {
                return NotFound();
            }

            DetailsViewModel detailsViewModel = new DetailsViewModel()
            {
                Product = product,
                Categories = categories
            };

            return View(detailsViewModel);
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
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
