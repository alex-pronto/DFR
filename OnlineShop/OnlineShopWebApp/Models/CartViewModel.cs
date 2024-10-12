using OnlineShop.Controllers;
using OnlineShop.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId = UserConst.userId;

        public List<CartItemViewModel> Items { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public CartViewModel(Guid userId)
        {
            Id = userId;
            CreatedDateTime = DateTime.Now;
            Items = new List<CartItemViewModel>();
        }
        public CartViewModel()
        {
            CreatedDateTime = DateTime.Now;
            Items = new List<CartItemViewModel>();
        }
        public decimal TotalCost
        {
            get
            {
                return Items?.Sum(e => e.CostPosition) ?? 0;
            }
            
        }

        public int Quantity 
        { 
            get 
            {
                return Items?.Sum((e) => e.Quantity) ?? 0;
            }

        }

       

    }
}
