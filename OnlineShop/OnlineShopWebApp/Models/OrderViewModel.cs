using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        
        public decimal TotalCost
        {
            get
            {
                return Items?.Sum(e => e.CostPosition) ?? 0;
            }
        }
        
        public User User { get; set; }
        public UserDeliveryInfoViewModel DeliveryInfo { get; set; }
        public DateTime OrderTime { get; set; }

        public OrderStatusViewModel Status { get; set; }

        public OrderViewModel()
        {
            Status = OrderStatusViewModel.Created;
            OrderTime = DateTime.Now;
        }

    }
}