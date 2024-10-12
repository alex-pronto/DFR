
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class TimeDBRepository : ITimeRepository
    {
        private readonly DataBaseContext databaseContext;

		public TimeDBRepository(DataBaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		public Time GetTime(Guid id)
        {
			return databaseContext.Time.FirstOrDefault(time => time.Id == id);

        }

		public List<Time> GetTimes()
		{
			return databaseContext.Time.ToList();

		}

        public void Delete(Guid id)
        {
            databaseContext.Time.Remove(GetTime(id));
            databaseContext.SaveChanges();
        }
        


        public void Add(Time time)
        {
            var existingTime = databaseContext.Time.FirstOrDefault(x => x.Id == time.Id);
            //if (exitingTime != null) { return; } зачем вообще? 
            //existingTime.SelectedDate = time.SelectedDate;
            //existingTime.SelectedTime = time.SelectedTime;
            existingTime = time; 
            databaseContext.SaveChanges();
        }

    }
}
