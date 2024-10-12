using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Repository
{
    public class InMemoryRolesRepository : IRolesRepository
    {
        //private readonly List<Role> roles = new List<Role>();
        private readonly List<RoleViewModel> roles = new List<RoleViewModel>()
        {
           new RoleViewModel("Администратор"),
           new RoleViewModel("Пользователь")

        };


        public RoleViewModel GetRole(string name)
        {
            return roles.FirstOrDefault(x => x.Name == name);
        }
        public List<RoleViewModel> GetRoles()
        {
            return roles;
        }
        public void Remove(string name)
        {
            roles.RemoveAll(x => x.Name == name);
        }
        public void Add(RoleViewModel role)
        {
            roles.Add(role);
        }

    }
}
