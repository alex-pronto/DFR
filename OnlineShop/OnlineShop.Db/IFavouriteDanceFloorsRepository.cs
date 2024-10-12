using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IFavouriteDanceFloorsRepository
    {
        FavouriteDanceFloor GetFloor(Guid id);


        List<DanceFloor> GetAll(Guid userId);


        void Delete(Guid userId, Guid danceFloorID);

        void Add(Guid userId, DanceFloor danceFloor);


        void Clear(Guid userId);
       
    }
}
