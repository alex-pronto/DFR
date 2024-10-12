using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Item> Items { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Cart()
        {
            CreatedDateTime = DateTime.Now;
            Items = new List<Item>();
        }


        //public decimal TotalCost
        //{
        //   get
        //   {
        //       return Items?.Sum(e => e.CostPosition) ?? 0;
        //  }
        //  set
        //  {
        //      TotalCost = value;
        //  }
        // }

        //public int Quantity 
        //{ 
        //    get 
        //    {
        //        return Items?.Sum((e) => e.Quantity) ?? 0;
        //    }
        //    set
        //    {
        //        Quantity = value;
        //   }
        //}

    }
}
