using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace OnlineShopWebApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }

        public RoleViewModel(string name)
        {
            Name = name;
        }

        public RoleViewModel()
        {

            Name = "User";
            
        }
    }
}
