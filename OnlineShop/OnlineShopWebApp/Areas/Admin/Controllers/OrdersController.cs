using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System;
using OnlineShopWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        public IOrdersRepository ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            var orders = ordersRepository.GetAll();
            var ordersViewModel = orders.ToOrdersViewModel();
            return View(ordersViewModel);
        }

        public IActionResult Details(Guid Id)
        {
            var order = ordersRepository.TryGetById(Id);
            var orderViewModel = order.ToOrderViewModel();
            return View(orderViewModel);
        }

        public IActionResult UpdateStatus(Guid orderId, OrderStatusViewModel Status)
        {
            OrderStatus orderStatus = new OrderStatus();
            orderStatus = (OrderStatus)Status;
            ordersRepository.UpdateStatus(orderId, orderStatus);
            return RedirectToAction(nameof(Index));
        }
    }
}
