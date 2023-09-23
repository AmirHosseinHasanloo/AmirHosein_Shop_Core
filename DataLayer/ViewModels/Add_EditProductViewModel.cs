using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModels
{
    public class Add_EditProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350,ErrorMessage = "نام محصول نمی تواند بیش از 350 کرکتر باشد")]
        public string Name { get; set; }
        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1200, ErrorMessage = "توضیحات محصول نمی تواند بیش از 1200 کرکتر باشد")]
        public string Description { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public decimal Price { get; set; }
        [Display(Name = "تعداد موجود در انبار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int QuantityInStock { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile Picture { get; set; }
    }
}
