using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        public readonly IDanceFloorsRepository danceFloorsRepository;
        public readonly ITimeRepository timeRepository;
        
        
        public HomeController(IDanceFloorsRepository danceFloorsRepository, ITimeRepository timeRepository)
        {
            this.danceFloorsRepository = danceFloorsRepository;
            this.timeRepository = timeRepository;
            
        }

       
        public IActionResult Index()
        {
            
            var danceFloors = danceFloorsRepository.GetDanceFloors();
            var danceFloorViewModels = danceFloors.ToDanceFoorsViewModel();
            return View(danceFloorViewModels);
        }


    }
}
