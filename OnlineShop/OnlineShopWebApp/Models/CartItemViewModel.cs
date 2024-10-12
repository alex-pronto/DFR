using System;

namespace OnlineShopWebApp.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public DanceFloorViewModel DanceFloor { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderedTime { get; set; }

        public decimal CostPosition {
            get 
            {
                return DanceFloor.Cost * Quantity;
            }
        }
        
        
    }
}
