using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using DataLayer;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace AmirHosein_Shop.Pages.Admin
{
    public class ProductsModel : PageModel
    {
        private UnitOfWork _unitOfWork;
        private EshopContext _context;
        public ProductsModel(EshopContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(context);
        }

        [BindProperty]
        public Add_EditProductViewModel AddProduct { get; set; }
        public void OnGet()
        {
            ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
        }

        public IActionResult OnPost(List<int> selectedGroups)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (selectedGroups == null)
            {
                ViewData["Error"] = "لطفا گروه این محصول را مشخص کنید";
                ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
                return Page();
            }

            var item = new ShopItem()
            {
                Price = AddProduct.Price,
                QuantityInStock = AddProduct.QuantityInStock,
            };
            _unitOfWork.ShopItemRepository.Insert(item);
            _unitOfWork.ShopItemRepository.Save();

            var product = new Product()
            {
                Name = AddProduct.Name,
                Description = AddProduct.Description,
                Item = item
            };
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.ProductRepository.Save();
            product.ItemId = product.Id;
            _unitOfWork.ProductRepository.Save();

            foreach (int groups in selectedGroups)
            {
                _unitOfWork.CategoryToProductRepository.Insert(new CategoryToProduct()
                {
                    ProductId = product.Id,
                    CategoryId = groups
                });
            }
            _unitOfWork.CategoryToProductRepository.Save();

            if (AddProduct.Picture?.Length > 0)
            {
                string filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    product.Id + Path.GetExtension(AddProduct.Picture.FileName)
                    );
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    AddProduct.Picture.CopyTo(stream);
                }
            }
            return RedirectToPage("Index");
        }
    }
}
