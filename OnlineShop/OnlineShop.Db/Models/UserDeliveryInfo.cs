﻿
namespace OnlineShop.Db.Models
{
    public class UserDeliveryInfo
    {
      
        public Guid Id {get; set;}
        public string Name { get; set; }
        
        public string SurName { get; set; }

        public string Phone { get; set; }

        public string EMail { get; set; }
    }
}
