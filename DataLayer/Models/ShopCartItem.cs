using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public ShopItem Item { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return Quantity * Item.Price;
        }
    }
}
