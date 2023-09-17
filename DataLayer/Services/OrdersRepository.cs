using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class OrdersRepository : IOrdersRepository
    {
        private EshopContext _context;

        public OrdersRepository(EshopContext context)
        {
            _context = context;
        }

        public Order GetOrdersForShowCart(int userid)
        {
            return _context.Orders.Where(o => o.UserId == userid && !o.IsFinally).Include(o => o.OrderDetail)
                .ThenInclude(o => o.Product).FirstOrDefault();
        }

        public Order GetUserOrder(int userid)
        {
            return _context.Orders.FirstOrDefault(u => u.UserId == userid && u.IsFinally == false);
        }
    }
}
