using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class OrdersDBRepository : IOrdersRepository
    {

       private readonly DataBaseContext dataBaseContext;
        public OrdersDBRepository (DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(Order order)
        {
            dataBaseContext.Orders.Add(order);
            dataBaseContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
           return dataBaseContext.Orders.Include(x => x.DeliveryInfo)
             .Include(x => x.Items)
              .ThenInclude(x => x.DanceFloor).ToList();
            //return  dataBaseContext.Orders.ToList();
        }

        public Order TryGetById(Guid Id)
        {
            return  dataBaseContext.Orders.Include(x => x.DeliveryInfo)
                .Include(x => x.Items)
                .ThenInclude(x => x.DanceFloor).FirstOrDefault(x=>x.Id == Id);
        }

        public void UpdateStatus(Guid orderId, OrderStatus newStatus)
        {
            var order = TryGetById(orderId); 
            if (order != null)
            {
                order.Status = newStatus;
            }
            dataBaseContext.SaveChanges();
        }

       
    }
}
