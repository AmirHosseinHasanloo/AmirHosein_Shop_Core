using System.Collections.Generic;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmirHosein_Shop.Pages.Admin
{
    public class IndexModel : PageModel
    {

        private UnitOfWork _unitOfWork;
        public IndexModel(EshopContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }
        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _unitOfWork.ProductRepository.GetAll(includes: "Item");
        }

        public void OnPost()
        {

        }
    }
}
