using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Repository
{
    public class InMemoryDanceFloorsHistoryChangesRepository : IDanceFloorsHistoryChangesRepository
    {
        
        private List<DanceFloorHistoryChanges> danceFloors = new List<DanceFloorHistoryChanges>();
        
        public DanceFloorHistoryChanges GetFloor(Guid id)
        {
            return danceFloors.FirstOrDefault(danceFloor => danceFloor.Id == id);

        }

        public List<DanceFloorHistoryChanges> GetDanceFloors()
        {
            return danceFloors;
        }

        public void Add(DanceFloorHistoryChanges change)
        {
            danceFloors.Add(change);
        }

    }
}
