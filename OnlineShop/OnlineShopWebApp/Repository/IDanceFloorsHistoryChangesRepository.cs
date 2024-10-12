using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System;

namespace OnlineShopWebApp
{
    public interface IDanceFloorsHistoryChangesRepository
    {
        DanceFloorHistoryChanges GetFloor(Guid id);
        List<DanceFloorHistoryChanges> GetDanceFloors();
        void Add(DanceFloorHistoryChanges change);
    }
}