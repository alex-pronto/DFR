using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApp.Models
{
    public class UserHistoryChanges
    {
        public Guid Id { get; set; } //здесь айдишник всегда равен айдишнику изменяемого gользователя  присвоение в контроллере либо если идет создание пользователя админом, то создается объект  User  и из него берется айдишник для этого класса
        public DateTime TimeOfChanges { get { return DateTime.Now; } }

        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана Фамилия")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Не указан Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректно введен адрес электронной почты")]
        public string EMail { get; set; }

        public SetStatus Status = SetStatus.Changed;

        public enum SetStatus
        {
            Changed,
            Deleted,
            Created
        }


    }
}
