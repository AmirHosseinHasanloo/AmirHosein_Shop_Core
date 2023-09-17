using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
    public class OrderDetails
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Display(Name = "محصول")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        //Navigation property
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
