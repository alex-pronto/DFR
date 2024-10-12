using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Login(string returnUrl)
        {

            return View(new Login() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Login(Login userLogIn)
        {
            var result = _signInManager.PasswordSignInAsync(userLogIn.EMail, userLogIn.Password, userLogIn.RememberMe, false).Result;
              
            if (result.Succeeded)
            {
                
                if (userLogIn.ReturnUrl == null) { userLogIn.ReturnUrl = "/"; }
                
                return Redirect(userLogIn.ReturnUrl);
                //return RedirectToAction("Index", "Home", new { userId = "" });
            }
            else
            {
                ModelState.AddModelError("", "Неправильный пароль");
            }
            return View(userLogIn);
        }

        public IActionResult Registration(string returnUrl)
        {

            return View(new Registration() { ReturnUrl = returnUrl });

        }

        [HttpPost]
        public IActionResult Registration(Registration registration)
        {
            if (registration.EMail == registration.Password)
            {
                ModelState.AddModelError(String.Empty, "Логин и пароль должны различаться");
            }

            if (ModelState.IsValid && _userManager.FindByNameAsync(registration.EMail).Result == null)
            {

                User user = new User()
                {
                    Email = registration.EMail,
                    UserName = registration.EMail,
                    Name = "unknown",
                    SurName = "unknown",
                };
                var result = _userManager.CreateAsync(user, registration.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false); //на что влияет последний параметр  почему null??
                    _userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    if (registration.ReturnUrl == null) { registration.ReturnUrl = "/"; }
                    return Redirect(registration.ReturnUrl);
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }

            }

            return View(registration);

        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(Index), "Home");

        }

        public bool CheckLogin(Registration registration)
        {

            //var userExist = usersRepository.TryGetByEmail(registration.UserEMail);
            //if (userExist ==  null) { return true; }
            return true;  //заменить на фолс когда будешь делать работающий метод
        }

        public bool CheckEmailByLogin(Login login)
        {

            //var userExist = usersRepository.TryGetByEmail(login.EMail);
            //if (userExist == null) { return false; }
            return true;
        }




    }
}
