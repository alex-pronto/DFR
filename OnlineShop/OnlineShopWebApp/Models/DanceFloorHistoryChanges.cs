using System.ComponentModel.DataAnnotations;
using System;


namespace OnlineShopWebApp.Models
{
    public class DanceFloorHistoryChanges
    {
        public Guid Id { get; set; } //здесь айдишник всегда равен айдишнику изменяемого товара  присвоение в контроллере либо если идет создание товара то создается объект  DanceFloor  и из него берется айдишник для этого класса
        public DateTime TimeOfChanges { get { return DateTime.Now; } }

        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана стоимость")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Добавьте описание")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public SetStatus Status = SetStatus.Changed;

        public enum SetStatus
        {
            Changed,
            Deleted,
            Created
        }


    }
}
