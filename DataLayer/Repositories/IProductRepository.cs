using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IProductRepository
    {
        Product GetProductForAddCart(int productid);
    }
}
