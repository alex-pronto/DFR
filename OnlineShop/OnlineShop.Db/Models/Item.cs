using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class Item
    {
       
        public Guid Id { get; set; }
        public DanceFloor DanceFloor { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderedTime { get; set; }
        //public Guid CartId { get; set; }
        //public Cart? Cart { get; set; }
        

    }
}
