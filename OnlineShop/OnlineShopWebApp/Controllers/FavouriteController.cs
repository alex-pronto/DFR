using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        public readonly IFavouriteDanceFloorsRepository favouriteDanceFloorsRepository;
        public readonly IDanceFloorsRepository danceFloorsRepository;

        public FavouriteController(IFavouriteDanceFloorsRepository favouriteDanceFloorsRepository, IDanceFloorsRepository danceFloorsRepository)
        {
            this.favouriteDanceFloorsRepository = favouriteDanceFloorsRepository;
            this.danceFloorsRepository = danceFloorsRepository;
        }

        public IActionResult Index()
        {
            var danceFloors = favouriteDanceFloorsRepository.GetAll(UserConst.userId);
            return View(danceFloors.ToDanceFoorsViewModel());
        }

        public IActionResult Add(Guid danceFloorId)
        {
            var danceFloor = danceFloorsRepository.GetFloor(danceFloorId);
            favouriteDanceFloorsRepository.Add(UserConst.userId, danceFloor);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(Guid danceFloorId)
        {
            favouriteDanceFloorsRepository.Delete(UserConst.userId, danceFloorId);

            return RedirectToAction(nameof(Index));
        }

    }
}
