using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System;

namespace OnlineShopWebApp.Repository;

public interface IUserHistoryChangesRepository
{
    UserHistoryChanges GetUser(Guid id);
    List<UserHistoryChanges> GetUsers();
    void Add(UserHistoryChanges change);
    
}