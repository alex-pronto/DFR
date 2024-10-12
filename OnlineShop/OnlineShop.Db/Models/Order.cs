
namespace OnlineShop.Db.Models
{
    public class Order
    {
        
        public Guid Id { get; set; }
        public UserDeliveryInfo DeliveryInfo { get; set; }
        public List<Item> Items { get; set; }
        //public User user { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderTime { get; set; }

        public Order() 
        {
            Status = OrderStatus.Created;
            OrderTime = DateTime.Now;
        }

        //public User User { get; set; }
        //public decimal TotalCost
        //{
        //    get
        //    {
        //       return CartItems?.Sum(e => e.CostPosition) ?? 0;
        //    }
        // }

    }
}