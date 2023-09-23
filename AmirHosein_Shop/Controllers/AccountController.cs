using DataLayer;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AmirHosein_Shop.Controllers
{
    public class AccountController : Controller
    {
        private EshopContext _context;
        private UnitOfWork _unitOfWork;

        IUserRepository _userRepository;
        public AccountController(EshopContext context, IUserRepository userRepository)
        {
            _unitOfWork = new UnitOfWork(context);
            _context = context;
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
                //string HashPassword = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(model.Password))).Replace("-", "");
                    _unitOfWork.usersRepository.Insert(new Users
                    {
                        Email = model.Email.ToLower(),
                        Password = model.Password,
                        RegisterDate = DateTime.Now,
                        IsAdmin = false
                    });

                    _unitOfWork.usersRepository.Save();
                    return View("SuccessRegister", model);
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
                //string HashPassword = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(login.Password))).Replace("-", "");

                var User = _userRepository.GetUserForLogin(login.Email.ToLower(), login.Password);

                if (User != null)
                {
                    var climas = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, User.UserId.ToString()),
                        new Claim(ClaimTypes.Name, User.Email),
                        new Claim("IsAdmin",User.IsAdmin.ToString())
                    };

                    var identity = new ClaimsIdentity(climas, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principle = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principle, properties);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Email", errorMessage: "اطلاعات صحیح نیست!");
                }
            }
            return View(login);
        }

        #endregion


        public IActionResult VerifyEmail(string email)
        {
            if (_userRepository.IsExistUserByEmail(email))
            {
                return Json($"ایمیل ورودی تکراری است");
            }
            return Json(true);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
