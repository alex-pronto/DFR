using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IItemsRepository
    {
        //Cart TryGetByUserId(Guid userId);
        //void AddItem(DanceFloor danceFloor, Guid userId);
        //void RemoveItem(DanceFloor danceFloor, Guid userId);
        //void Clear(Guid userId);
        void Add(Item item);
        Item GetItem(Guid id);
        
        



    }
}
