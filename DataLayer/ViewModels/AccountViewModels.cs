using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "این فیلد نمیتواند بیش از 350 کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        [Remote("VerifyEmail", "Account")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "این فیلد نمیتواند بیش از 350 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "این فیلد نمیتواند بیش از 350 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور باید با یکدیگر یکسان باشند")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
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

        [Display(Name = "مرا به خاطر بسپار")]           
        public bool RememberMe { get; set; }
    }
}
