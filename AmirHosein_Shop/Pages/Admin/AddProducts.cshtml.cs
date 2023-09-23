using System.IO;
using System.Runtime.CompilerServices;
using DataLayer;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmirHosein_Shop.Pages.Admin
{
    public class ProductsModel : PageModel
    {
        private UnitOfWork _unitOfWork;

        public ProductsModel(EshopContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [BindProperty]
        public Add_EditProductViewModel AddProduct { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

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
