//using JetBrains.Annotations;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using OnlineShopWebApp.Areas.Admin.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Numerics;
//using System.Xml.Linq;

//namespace OnlineShopWebApp.Models
//{
//    public class User
//    {
//        public Guid Id { get; set; }
       
//        public string Name { get; set; }
        
//        public string SurName { get; set; }
       
//        public string Phone { get; set; }
        
//        public string EMail { get; set; }
//        public bool StayOnSite { get; set; }
//        public string Password { get; set; }
//        public CartViewModel Cart { get; set; }
//        public List<CartViewModel> Carts { get; set; }
//        public Role UserStatus { get; set; }

//        public User()
//        {
//            Id = Guid.NewGuid();
//            Name = "неизвестно";
//            SurName = "неизвестно";
//            Phone = "неизвестно";
//            EMail = "неизвестно";
//            Password = "неизвестно";
//            Cart = new CartViewModel();
//            Carts = new List<CartViewModel>();
//            UserStatus = new Role();
//        }

//        public User(string name, string surName, string phone, string eMail)
//        {
//            Id = Guid.NewGuid();
//            Name = name;
//            SurName = surName;
//            Phone = phone;
//            EMail = eMail;
//            UserStatus = new Role();
//        }
//    }


//}
