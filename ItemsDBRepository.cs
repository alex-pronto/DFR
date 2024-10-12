using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class ItemsDBRepository : IItemsRepository
    {
        private readonly DataBaseContext databaseContext;

        public ItemsDBRepository(DataBaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Item GetItem(Guid id)
        {
            return databaseContext.Items.FirstOrDefault(item => item.Id == id); // lобавить Include если нужно чтобы видно было влодженные сущности

        }

       
        public void Add(Item item)
        {
            databaseContext.Items.Add(item);
            databaseContext.SaveChanges();

        }

    }
}
