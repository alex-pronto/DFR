using System.Collections.Generic;
using System;
using OnlineShopWebApp.Areas.Admin.Models;

namespace OnlineShopWebApp.Repository
{
    public interface IRolesRepository
    {
        RoleViewModel GetRole(string name);
        List<RoleViewModel> GetRoles();
        void Remove(string name);
        void Add(RoleViewModel role);
       


    }
}
