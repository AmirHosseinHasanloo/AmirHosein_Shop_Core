using DataLayer.ViewModels;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;

namespace AmirHosein_Shop.Pages.Admin
{
    public class DeleteProductModel : PageModel
    {
        private UnitOfWork _unitOfWork;

        public DeleteProductModel(EshopContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int id)
        {
            Product = _unitOfWork.ProductRepository.GetById(id);
        }


        public IActionResult OnPost()
        {
            var product = _unitOfWork.ProductRepository.GetById(Product.Id);
            var shopItem = _unitOfWork.ShopItemRepository.GetById(Product.Id);

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Images",
               product.Id + ".jpg"
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.ProductRepository.Save();
            _unitOfWork.ShopItemRepository.Delete(shopItem);
            _unitOfWork.ShopItemRepository.Save();

            return RedirectToPage("Index");
        }
    }
}
