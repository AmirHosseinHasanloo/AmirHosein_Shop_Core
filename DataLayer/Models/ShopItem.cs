using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class ShopItem
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public Product Product { get; set; }
    }
}
