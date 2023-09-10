using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ShopCart
    {
        public ShopCart()
        {
            ShopCartItems = new List<ShopCartItem>();
        }
        public int OrderId { get; set; }
        public List<ShopCartItem> ShopCartItems { get; set; }

        public void AddShopCartItem(ShopCartItem cart_item)
        {
            if (ShopCartItems.Exists(i => i.Item.Id == cart_item.Item.Id))
            {
                ShopCartItems.Find(i => i.Item.Id == cart_item.Item.Id).Quantity += 1;
            }
            else
            {
                ShopCartItems.Add(cart_item);
            }
        }

        public void DeleteShopCartItem(int cart_id)
        {
            var item = ShopCartItems.SingleOrDefault(i => i.Item.Id == cart_id);

            if (item?.Quantity <= 1)
            {
                ShopCartItems.Remove(item);
            }
            else if (item != null)
            {
                item.Quantity -= 1;
            }
        }

    }
}
