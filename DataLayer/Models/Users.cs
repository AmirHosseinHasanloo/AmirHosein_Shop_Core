using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Text;

namespace DataLayer
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "این فیلد نمیتواند بیش از 350 کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "این فیلد نمیتواند بیش از 350 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "ادمین/کاربر")]
        public bool IsAdmin { get; set; }

        //Navigation property
        public List<Order> Orders { get; set; }
    }
}
