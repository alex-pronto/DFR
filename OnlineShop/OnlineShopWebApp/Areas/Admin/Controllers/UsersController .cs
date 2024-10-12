using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Areas.Admin.Controllers;

[Area(Constants.AdminRoleName)]
[Authorize(Roles = Constants.AdminRoleName)]
public class UsersController : Controller
{
    private UserManager<User> usersManager;
    private IUserHistoryChangesRepository userHistoryChangesRepository;
    private RoleManager<IdentityRole> rolesManager;


    public UsersController(IUserHistoryChangesRepository userHistoryChangesRepository, RoleManager<IdentityRole> rolesManager, UserManager<User> usersManager) //IUsersRepository usersRepository,
    {
        this.usersManager = usersManager;
        this.userHistoryChangesRepository = userHistoryChangesRepository;
        this.rolesManager = rolesManager;
    }

    public IActionResult Index()
    {
        var roles = rolesManager.Roles.ToList();
        var usersInRolesViewModel = new List<UsersInRolesViewModel>();
        foreach (var role in roles)
        {
            var usersInRole = usersManager.GetUsersInRoleAsync(role.Name).Result.ToList();
            usersInRolesViewModel.Add(new UsersInRolesViewModel() { Role = new RoleViewModel() { Name = role.Name }, Users = usersInRole.ToUsersViewModel() });
        }

        return View(usersInRolesViewModel);
    }

    public IActionResult Info(Guid Id)
    {
        var user = usersManager.FindByIdAsync(Id.ToString()).Result;
        var currentRole = usersManager.GetRolesAsync(user).Result;
        ViewBag.CurrentRole = currentRole.ToUserRolesViewModel();
        return View(user.ToUserViewModel());

    }

    public IActionResult Edit(Guid id)
    {
        var user = usersManager.FindByIdAsync(id.ToString()).Result;

        return View(user.ToUserViewModel());

    }

    [HttpPost]
    public IActionResult Edit(UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = usersManager.FindByIdAsync(userViewModel.Id).Result;
            if (user != null)
            {

                user.Name = userViewModel.Name;
                user.PhoneNumber = userViewModel.Phone;
                user.SurName = userViewModel.SurName;
                var result = usersManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(Index), "Users");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }

        }


        return View(userViewModel);

    }

    public IActionResult Delete(Guid id)
    {
        var user = usersManager.FindByIdAsync(id.ToString()).Result;
        usersManager.DeleteAsync(user).Wait();


        return RedirectToAction(nameof(Index));
    }

    public IActionResult ChangePassword(Guid id)
    {
        var user = usersManager.FindByIdAsync(id.ToString()).Result;
        ViewBag.User = user.ToUserViewModel();
        return View();
    }


    [HttpPost]
    public IActionResult ChangePassword(Registration registration, Guid Id)
    {
        if (ModelState.IsValid)
        {
            var user = usersManager.FindByIdAsync(Id.ToString()).Result;
            if (user != null)
            {
                var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;
                IdentityResult result =
                         _passwordValidator.ValidateAsync(usersManager, user, registration.Password).Result;
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, registration.Password);
                    usersManager.UpdateAsync(user).Wait();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
        }
        return View(registration);
    }
    public IActionResult Add()
    {

        return View();
    }

    [HttpPost]
    public IActionResult Add(Registration createdUser)
    {
        if (createdUser.EMail == createdUser.Password)
        {
            ModelState.AddModelError(String.Empty, "Логин и пароль должны различаться");
        }

        if (usersManager.FindByNameAsync(createdUser.EMail).Result != null)
        {
            ModelState.AddModelError(String.Empty, "Пользователь с такой почтой уже существует");
        }

        if (ModelState.IsValid)
        {

            User user = new User()
            {
                Email = createdUser.EMail,
                UserName = createdUser.EMail,
                Name = createdUser.Name,
                SurName = createdUser.SurName,
                PhoneNumber = createdUser.Phone,
            };
            var result = usersManager.CreateAsync(user, createdUser.Password).Result; //здесь задается временный пароль обозначенный в конструкторе Registration eнжно отправлять на почту этот пароль и ссылку для смены пароля
            if (result.Succeeded)
            {
                usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

        }

        return View();
    }

    public IActionResult ChangeRole(Guid Id)
    {
        var roles = rolesManager.Roles.ToList();
        var user = usersManager.FindByIdAsync(Id.ToString()).Result;
        var currentRole = usersManager.GetRolesAsync(user).Result;

        ViewBag.CurrentRoles = currentRole;
        ViewBag.User = user.UserName;
        ViewBag.UserId = user.Id;
        return View(roles.ToRolesViewModel());
    }
    [HttpPost]
    public IActionResult ChangeRole(List<string> roles, Guid Id) //pдесь у меня список но во вьюшке можно выбрать только одну роль радиобаттоном  на случай если надо будет расширять количество ролей одновременно выдаваемых пользователю оставлю 
    {

        var user = usersManager.FindByIdAsync(Id.ToString()).Result;
        var currentRoles = usersManager.GetRolesAsync(user).Result;

        // получаем список ролей, которые были добавлены
        var addedRoles = roles.Except(currentRoles);
        // получаем роли, которые были удалены
        var removedRoles = currentRoles.Except(roles);

        usersManager.AddToRolesAsync(user, addedRoles).Wait();
        usersManager.RemoveFromRolesAsync(user, removedRoles).Wait();


        return RedirectToAction(nameof(Index));
    }
}






//public bool CheckLogin(Registration registration)
//{

//    var userExist = usersRepository.TryGetByEmail(registration.EMail);
//    if (userExist == null) { return true; }
//    return false;
//}




//}
