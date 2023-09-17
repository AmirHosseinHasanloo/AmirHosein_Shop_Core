using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IOrdersRepository
    {
        Order GetUserOrder(int userid);
        Order GetOrdersForShowCart(int userid);
    }
}
