using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface ITimeRepository
    {
        Time GetTime(Guid id);

		List<Time> GetTimes();
        void Add(Time time);
        void Delete(Guid id);


    }
}
