using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private EshopContext _context;


        public OrderDetailRepository(EshopContext context)
        {
            _context = context;
        }
        public OrderDetails isExistOrderDetails(int orderid, int productid)
        {
            return _context.OrderDetails.FirstOrDefault(o => o.OrderId == orderid && o.ProductId == productid);
        }
    }
}
