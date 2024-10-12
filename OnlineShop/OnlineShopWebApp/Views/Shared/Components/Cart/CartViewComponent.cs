using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{


    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;
       // private User user;

        public CartViewComponent(ICartsRepository cartsRepository) //User user
        {
            this.cartsRepository = cartsRepository;
           // this.user = user;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserId(UserConst.userId);
            var productCount = cart?.Items.Count ?? 0;
            return View("Cart", productCount);
        }
    }
}
