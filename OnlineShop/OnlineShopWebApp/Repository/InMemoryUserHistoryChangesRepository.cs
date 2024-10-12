using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Repository
{
    public class InMemoryUserHistoryChangesRepository : IUserHistoryChangesRepository
    {
        
        private List<UserHistoryChanges> users = new List<UserHistoryChanges>();
        
        public UserHistoryChanges GetUser(Guid id)
        {
            return users.FirstOrDefault(danceFloor => danceFloor.Id == id);

        }

        public List<UserHistoryChanges> GetUsers()
        {
            return users;
        }

        public void Add(UserHistoryChanges change)
        {
            users.Add(change);
        }

    }
}
