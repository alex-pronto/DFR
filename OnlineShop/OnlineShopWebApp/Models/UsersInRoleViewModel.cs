using OnlineShopWebApp.Areas.Admin.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class UsersInRolesViewModel
    {
        public int Id { get; set; }
        public List<UserViewModel> Users { get; set; }
        public RoleViewModel Role { get; set; } 
    }
}
