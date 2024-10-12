using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IDanceFloorsRepository
    {
        DanceFloor GetFloor(Guid id);
        List<DanceFloor> GetDanceFloors();
        void Delete(Guid id);
        void Add(DanceFloor danceFloor);

        void Update(DanceFloor danceFloor);

	}
}
