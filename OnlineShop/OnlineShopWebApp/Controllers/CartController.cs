using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Controllers
{
    //[Authorize]
    public class CartController : Controller
    {
        public readonly IDanceFloorsRepository danceFloorsRepository;
        public readonly ITimeRepository timeRepository;
        public readonly ICartsRepository cartsRepository;
        public readonly IItemsRepository itemsRepository;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        
        public CartController(IDanceFloorsRepository danceFloorsRepository, ITimeRepository timeRepository, ICartsRepository cartsRepository, IItemsRepository itemsRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.danceFloorsRepository = danceFloorsRepository;
            this.cartsRepository = cartsRepository;
            this.timeRepository = timeRepository;
            this.itemsRepository = itemsRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index(Guid userId)
        {
            
            if (userId == Guid.Empty) { userId = GetCurrentUserId(); }
           
            var cart = cartsRepository.TryGetByUserId(userId);
            if (cart == null)
            {
                
                return View(new CartViewModel(userId));
            }
            
            var cartViewModel = Mapping.ToCartViewModel(cart);
              
            return View(cartViewModel);
        }

        [Authorize]
        public IActionResult IndexLogin(Guid userId)
        {
            var LogginedUser = userManager.FindByNameAsync(User.Identity.Name).Result;
            if (userId != new Guid(LogginedUser.Id))
            {
                var cart = cartsRepository.TryGetByUserId(userId);
                var cartItems = new List<Item>();
                foreach (var item in cart.Items)
                {
                    cartItems.Add(item);
                }
                cartsRepository.AddItems(cartItems, new Guid(LogginedUser.Id));
                cartsRepository.Clear(userId);
                
            }
            return RedirectToAction("Index", new { userId = LogginedUser.Id });
        }
        public IActionResult Add(Guid Id, int selectedTime, int selectedDay, int selectedMonth, int selectedYear)
        {


            var danceFloor = danceFloorsRepository.GetFloor(Id);
            var times = timeRepository.GetTimes(); //благодаря times в самом вanceFloor cтановится видно Time
            var time = timeRepository.GetTime(danceFloor.Time.Id);

            time.SelectedDate = new DateTime(selectedYear, selectedMonth, selectedDay, 0, 0, 0);//приводим к 0 время заказа чтобы оно не плюсовалось с предыдущими данными
            time.SelectedDate = time.SelectedDate.AddHours(selectedTime); //добавляем часы к выбранной дате
            danceFloor.Time.SelectedDate = time.SelectedDate;
            var currentUserId = GetCurrentUserId();

            cartsRepository.AddItem(danceFloor, currentUserId);
            danceFloorsRepository.Update(danceFloor);
            timeRepository.Add(time);
           

            return RedirectToAction("Index", new { userId = currentUserId });

        }

        

        public IActionResult Remove(Guid Id)
        {
            var userId = GetCurrentUserId();
            //var danceFloor = danceFloorsRepository.GetFloor(Id);
            //if (danceFloor == null)
            //{
            //   return RedirectToAction("Index");
            //}
            cartsRepository.RemoveItem(Id, userId);
            return RedirectToAction("Index");

        }

        public IActionResult ClearCart(Guid userId)
        {
            cartsRepository.Clear(userId);
            return RedirectToAction("Index");
        }

        public Guid GetCurrentUserId()
        {
            return User.Identity.IsAuthenticated ? new Guid(userManager.FindByNameAsync(User.Identity.Name).Result.Id) : UserConst.userId;
        }

    }
}
