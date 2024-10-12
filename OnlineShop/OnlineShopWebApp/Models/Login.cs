using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Login
    {
        //[Remote("CheckEmailByLogin", "Account", ErrorMessage = "Пользователя с таким адресом не существует")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно введен адрес электронной почты")]
        
        public string EMail { get; set; }

        
        [Required(ErrorMessage = "Не указан пароль")]
        
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
