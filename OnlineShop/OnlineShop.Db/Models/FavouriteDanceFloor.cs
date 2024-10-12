

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class FavouriteDanceFloor
    {
        [Key]
        public Guid Id { get; set; }
        public Guid userId { get; set; }
        public DanceFloor DanceFloor { get; set;}
    }
}
