using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ProductGroupsRepository : IProductGroupsRepository
    {
        EshopContext _context;

        public ProductGroupsRepository(EshopContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _context.Categories.Select(c => new ShowGroupViewModel
            {
                GroupId = c.Id,
                Name = c.CategoryName,
                ProductCount = c.categoryToProducts.Count(g => g.CategoryId == c.Id)
            });
        }
    }
}
