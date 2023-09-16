using DataLayer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AmirHosein_Shop.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string HashPassword = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(model.Password))).Replace("-", "");
                if (_userRepository.IsExistUserByEmail(model.Email) == false)
                {
                    _userRepository.AddUser(new Users
                    {
                        Email = model.Email.Trim().ToLower(),
                        Password = HashPassword,
                        RegisterDate = DateTime.Now,
                        IsAdmin = false
                    });

                    return View("SuccessRegister", model);
                }
                else
                {
                    ModelState.AddModelError("Email", errorMessage: "کاربر گرامی شخصی با این ایمیل ثبت نام کرده است");
                }
            }

            return View(model);
        }

        #endregion


        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                string HashPassword = BitConverter
                    .ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(login.Password)))
                    .Replace("-", "");

                var User = _userRepository.GetUserForLogin(login.Email, HashPassword);

                if (User != null)
                {

                }
                else
                {
                    ModelState.AddModelError("Email", errorMessage: "اطلاعات صحیح نیست!");
                }
            }
            return View();
        }

        #endregion
    }
}
