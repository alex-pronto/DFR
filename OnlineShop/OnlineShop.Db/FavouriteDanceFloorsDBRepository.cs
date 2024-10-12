
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class FavouriteDanceFloorsDBRepository : IFavouriteDanceFloorsRepository
    {
        private readonly DataBaseContext databaseContext;

		public FavouriteDanceFloorsDBRepository(DataBaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		
		public FavouriteDanceFloor GetFloor(Guid id)
        {
			return databaseContext.FavouriteDanceFloors.FirstOrDefault(favouriteDanceFloor => favouriteDanceFloor.Id == id);

        }

        public List<DanceFloor> GetAll(Guid userId)
        {
            return databaseContext.FavouriteDanceFloors.Where(x=>x.userId == userId).
                Include(x=>x.DanceFloor).Select(x=>x.DanceFloor).ToList();
		}

        public void Delete(Guid userId, Guid danceFloorID)
        {
            var favouriteDanceFloor = databaseContext.FavouriteDanceFloors.FirstOrDefault(u => u.userId == userId && u.DanceFloor.Id == danceFloorID);
            databaseContext.FavouriteDanceFloors.Remove(favouriteDanceFloor);
            databaseContext.SaveChanges();
        }

        public void Add(Guid userId, DanceFloor danceFloor)
        {
            var existingDanceFloor = databaseContext.FavouriteDanceFloors.FirstOrDefault(x => x.userId == userId && x.DanceFloor.Id == danceFloor.Id);
            if (existingDanceFloor == null)
            {
                databaseContext.FavouriteDanceFloors.Add(new FavouriteDanceFloor { userId = userId, DanceFloor = danceFloor });
                databaseContext.SaveChanges() ; 

            }
        }

        public void Clear(Guid userId)
        {
            var userFavouriteDanceFloors = databaseContext.FavouriteDanceFloors.Where(u => u.userId == userId).ToList();
            databaseContext.FavouriteDanceFloors.RemoveRange(userFavouriteDanceFloors);
            databaseContext.SaveChanges();
        }

       
    }
}
