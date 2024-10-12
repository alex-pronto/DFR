using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using System.Data;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> rolesManager;
        private UserManager<User> usersManager;
        public RolesController(RoleManager<IdentityRole> rolesManager, UserManager<User> usersManager)
        {
            this.rolesManager = rolesManager;
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var roles = rolesManager.Roles.ToList();
            return View(roles.ToRolesViewModel());
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            if (rolesManager.FindByNameAsync(role.Name).Result != null)
            {
                ModelState.AddModelError("", "Такая Роль уже существует");

            }
            if (ModelState.IsValid)
            {
                rolesManager.CreateAsync(new IdentityRole(role.Name)).Wait();
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }
        public IActionResult Delete(string name)
        {
            var roleToDelete = rolesManager.FindByNameAsync(name).Result;

            if (usersManager.GetUsersInRoleAsync(roleToDelete.Name).Result.Count != 0)
            {
                ModelState.AddModelError(string.Empty, "Есть пользователи с этой ролью - ее удалить нельзя");
                return RedirectToAction(nameof(Index));
            }
            rolesManager.DeleteAsync(roleToDelete).Wait();

            return RedirectToAction(nameof(Index));
        }

    }
}
