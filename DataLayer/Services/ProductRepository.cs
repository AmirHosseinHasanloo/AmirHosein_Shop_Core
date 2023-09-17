using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ProductRepository : IProductRepository
    {
        private EshopContext _context;

        public ProductRepository(EshopContext context)
        {
            _context = context;
        }
        public Product GetProductForAddCart(int productid)
        {
            return _context.Products.Include(i => i.Item).SingleOrDefault(i => i.Id == productid);
        }
    }
}
