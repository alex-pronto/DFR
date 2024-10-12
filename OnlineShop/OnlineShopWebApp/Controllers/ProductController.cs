using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        public readonly IDanceFloorsRepository danceFloorsRepository;
        public readonly ITimeRepository timeRepository;
        public ProductController(IDanceFloorsRepository danceFloorsRepository, ITimeRepository timeRepository)
        {
            this.danceFloorsRepository = danceFloorsRepository;
            this.timeRepository = timeRepository;
        }
        public IActionResult Index(Guid id, DateTime vm, int SelectedTime)
        {
            var danceFloor = danceFloorsRepository.GetFloor(id); //почему этот метод линк находя продукт по айди вписывает в поле Time  значение null?   
            var times = timeRepository.GetTimes(); //благодаря times в самом вanceFloor cтановится видно Time
            var time = timeRepository.GetTime(danceFloor.Time.Id);

            time.SelectedDate = vm;

            var danceFloorViewModel = danceFloor.ToDanceFloorViewModel();

            if (vm.Year != 1)
            {
                danceFloorViewModel.Time.SelectedDate = vm.Date; // нужно для того чтобы в частичном представлении показать свободные часы
                time.SelectedDate = vm.Date;
                timeRepository.Add(time);

            }

            if (danceFloorViewModel == null)
            {
                return RedirectToAction(nameof(NotFound), nameof(ErrorController));
            }
            return View(danceFloorViewModel);
        }


        [HttpPost]
        public IActionResult Index(DanceFloorViewModel Day)
        {

            return View();

        }

        public IActionResult Calendar(Guid id)
        {
            ViewBag.Id = id;
            return PartialView("Calendar");
        }

        [HttpPost]
        public IActionResult btnDayPartialView(int btnDay)
        {
            
            return PartialView("btnDayPartialView");
        }

        [HttpPost]
        public IActionResult FreeTimeSlots(Time Vm, Guid Id)
        {

            return RedirectToAction("Index", new { vm = Vm.SelectedDate, id = Id });
        }


        public IActionResult SelectedTime(Guid Id, int SelectedTime, int SelectedDay, int SelectedMonth, int SelectedYear)
        {

            return RedirectToAction("Add", "Cart", new { selectedTime = SelectedTime, selectedDay = SelectedDay, selectedMonth = SelectedMonth, selectedYear = SelectedYear, id = Id });
        }


    }
}
