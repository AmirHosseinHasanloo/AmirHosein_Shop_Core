using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IOrderDetailRepository
    {
        OrderDetails isExistOrderDetails(int orderid, int productid);
    }
}
