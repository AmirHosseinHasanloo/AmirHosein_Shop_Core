using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            shopCartItems = new List<ShopCartItem>();
        }
        public List<ShopCartItem> shopCartItems { get; set; }
        public decimal OrderTotal { get; set; }

    }
}
