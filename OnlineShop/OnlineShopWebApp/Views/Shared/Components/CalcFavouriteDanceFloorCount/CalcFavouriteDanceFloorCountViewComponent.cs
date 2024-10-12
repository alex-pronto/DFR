using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Views.Shared.Components.CalcFavouriteDanceFloorCount
{
    public class CalcFavouriteDanceFloorCountViewComponent : ViewComponent
    {
        public readonly IFavouriteDanceFloorsRepository favouriteDanceFloorsRepository;
        public CalcFavouriteDanceFloorCountViewComponent (IFavouriteDanceFloorsRepository favouriteDanceFloorsRepository)
        {
            this.favouriteDanceFloorsRepository = favouriteDanceFloorsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var danceFloorCount = favouriteDanceFloorsRepository.GetAll(UserConst.userId).Count;
            return View("CalcFavouriteDanceFloorCountView", danceFloorCount);
        }
    }
}
