using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана Фамилия")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Не указан Телефон")]
        public string Phone { get; set; }

        //[Remote("CheckLogin", "Registration" , ErrorMessage = "Пользователь с таким адресом уже существует")]
        [Required(ErrorMessage = "Не указан адрес электронной почты")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно введен адрес электронной почты")]
       public string EMail { get; set; }

        //пока что ЗАКОММЕНТИРОВАЛ валидацию и для облдасти админа  
        //[Remote("CheckLogin", "Users", ErrorMessage = "Пользователь с таким адресом уже существует")]
        //[Required(ErrorMessage = "Не указан адрес электронной почты")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно введен адрес электронной почты")]
        public string UserEMail { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 18 символов")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Не указан повторный пароль")]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

        public Registration()
        {
          
            Name = "Name";
            SurName = "Surname";
            Phone = "0000000000";
            UserEMail = "example@mail.ru";
            EMail = "example@mail.ru";      //ДВА адреса одной электронной почты и два одинаковых метода для валидации. Один валидируется - со стороны пользователя при регистрации а второй - со стороны админа при добавлении нового пользователя. Пришлось так сделать так как я не знаю как сделать так чтобы в атрибуте remote получилось дотянуться из areas admin до метода CheckEmail
            Password = "_aA123456";
            ConfirmPassword = "_aA123456";
            ReturnUrl = "/";
        }

    }
}
