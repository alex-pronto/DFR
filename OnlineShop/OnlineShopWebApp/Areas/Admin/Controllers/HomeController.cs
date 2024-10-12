using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Area(Constants.AdminRoleName)]
        [Authorize(Roles = Constants.AdminRoleName)]
        public IActionResult Index()
        {
            return View();
        }

    }
}
