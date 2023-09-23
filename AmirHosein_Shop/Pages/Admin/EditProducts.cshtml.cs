using System.IO;
using System.Linq;
using DataLayer;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmirHosein_Shop.Pages.Admin
{
    public class EditProductsModel : PageModel
    {
        private UnitOfWork _unitOfWork;

        public EditProductsModel(EshopContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [BindProperty]
        public Add_EditProductViewModel Product { get; set; }
        public void OnGet(int id)
        {
            Product = _unitOfWork.ProductRepository.GetAll(includes: "Item").Where(P => P.Id == id)
                .Select(P => new Add_EditProductViewModel()
                {
                    ProductId = P.Id,
                    Name = P.Name,
                    Description = P.Description,
                    Price = P.Item.Price,
                    QuantityInStock = P.Item.QuantityInStock,
                }).FirstOrDefault();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var editProduct = _unitOfWork.ProductRepository.GetAll().SingleOrDefault(p => p.Id == Product.ProductId);
            var item = _unitOfWork.ShopItemRepository.GetAll().SingleOrDefault(p => p.Id == Product.ProductId);

            editProduct.Name = Product.Name;
            editProduct.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;

            //save to db
            _unitOfWork.ProductRepository.Save();

            if (Product.Picture != null)
            {
                string filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    editProduct.Id + Path.GetExtension(Product.Picture.FileName)
                );
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
