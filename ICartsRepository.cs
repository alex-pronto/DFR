using OnlineShop.Db.Models;
using System.Reflection.Metadata.Ecma335;

namespace OnlineShop.Db
{
    public interface ICartsRepository
    {
        Cart TryGetByUserId(Guid userId);
        void Add(Cart cart);
        void AddItem(DanceFloor danceFloor, Guid userId);
        void RemoveItem(Guid Id, Guid userId);
        void Clear(Guid userId);
        void Update(Cart cart);

        void AddItems(List<Item> cartItems, Guid userId);


    }
}
