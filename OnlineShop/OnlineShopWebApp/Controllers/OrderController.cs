using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Controllers
{
   
    public class OrderController : Controller
    {
        public readonly ICartsRepository cartsRepository;
        public readonly IOrdersRepository ordersRepository;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository, UserManager<User> userManager, SignInManager<User> signInManager) //User user
        {
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        
       
        public IActionResult Index(Guid userId)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = userManager.FindByIdAsync(userId.ToString()).Result.ToUserViewModel();
                ViewBag.CurrentUserId = ViewBag.CurrentUser.Id;
            }
            ViewBag.CurrentUserId = userId;
            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel userDeliveryInfoViewModel, Guid userId)
        {
            
            if (ModelState.IsValid)
            {
                var UserDeliveryInfo = userDeliveryInfoViewModel.ToUserDeliveryInfo();

                var cart = cartsRepository.TryGetByUserId(userId);

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    DeliveryInfo = UserDeliveryInfo,
                    Items = cart.Items,
                    Status = OrderStatus.Created,
                    OrderTime = DateTime.Now
                };

                ordersRepository.Add(order);
        
                cartsRepository.Clear(userId);

                return View();
            }
            return View("Index");

        }
    }
}
