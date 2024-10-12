using OnlineShop.Db;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class DanceFloorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана стоимость")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Добавьте описание")]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        
        //public Guid? TimeId { get; set; }
        public TimeViewModel? Time = new TimeViewModel();
        

		//public DanceFloorViewModel(string name, decimal cost, string description, string imagePath)
		//{
		//  Id = Guid.NewGuid();
		// Name = name;
		//Cost = cost;
		// Description = description;
		// ImagePath = imagePath;
		//Time = new Time();

		//}

	}
}
