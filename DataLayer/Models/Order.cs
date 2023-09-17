using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Text;

namespace DataLayer
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Display(Name = "خریدار")] 
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        [Display(Name = "تاریخ ثبت فاکتور")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "پرداخت شده/نشده")]
        public bool IsFinally { get; set; }

        //Navigation property
        public Users User { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }

    }
}
