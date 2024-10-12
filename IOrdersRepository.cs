using OnlineShop.Db.Models;

namespace OnlineShop.Db;

public interface IOrdersRepository
{
    void Add(Order order);
    List<Order> GetAll();
    Order TryGetById(Guid Id);
    void UpdateStatus(Guid orderId, OrderStatus newStatus);
}