using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class DanceFloor
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Time Time { get; set; }
        public Guid TimeId { get; set; }   

    }
}
