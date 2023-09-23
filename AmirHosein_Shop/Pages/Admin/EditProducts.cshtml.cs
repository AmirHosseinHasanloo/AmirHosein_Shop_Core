using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
            ViewData["SelectedCategories"] = _unitOfWork.CategoryToProductRepository.GetAll();
        }


        public IActionResult OnPost(List<int> selectedGroups)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (selectedGroups == null)
            {
                ViewData["Error"] = true;
                ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();
                ViewData["SelectedCategories"] = _unitOfWork.CategoryToProductRepository.GetAll();
                return Page();
            }

            var editProduct = _unitOfWork.ProductRepository.GetAll().SingleOrDefault(p => p.Id == Product.ProductId);
            var item = _unitOfWork.ShopItemRepository.GetAll().SingleOrDefault(p => p.Id == Product.ProductId);

            editProduct.Name = Product.Name;
            editProduct.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;

            //save to db
            _unitOfWork.ProductRepository.Save();

            if (selectedGroups != null)
            {
                if (_unitOfWork.CategoryToProductRepository.GetAll().Any(CP => CP.ProductId == editProduct.Id))
                {
                    var product = _unitOfWork.CategoryToProductRepository.GetAll();
                    foreach (var delete in product.Where(p => p.ProductId == editProduct.Id))
                    {
                        _unitOfWork.CategoryToProductRepository.Delete(delete);
                    }
                    _unitOfWork.CategoryToProductRepository.Save();

                }
                foreach (var groups in selectedGroups)
                {
                    _unitOfWork.CategoryToProductRepository.Insert(new CategoryToProduct()
                    {
                        ProductId = editProduct.Id,
                        CategoryId = groups
                    });
                }
                _unitOfWork.CategoryToProductRepository.Save();

            }

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
