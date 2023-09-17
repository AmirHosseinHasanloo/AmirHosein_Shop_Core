using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }

        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }
        public ShopItem Item { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }
    }
}
