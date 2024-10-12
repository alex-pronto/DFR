
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DanceFloorsDBRepository : IDanceFloorsRepository
    {
        private readonly DataBaseContext databaseContext;

		public DanceFloorsDBRepository(DataBaseContext databaseContext)
		{
			this.databaseContext = databaseContext;
		}

		//private List<DanceFloor> danceFloors = new List<DanceFloor>()

		//{
		//    new DanceFloor("Зал Сальса", 1500, "Площадь 110 метров. Зеркала. Звук.", "/images/image1.jpg"),
		//    new DanceFloor("Зал Морской", 900, "Площадь 70 метров. Зеркала. Звук.", "/images/image2.jpg"),
		//    new DanceFloor("Зал Персик", 700, "Площадь 40 метров. Зеркала. Звук.", "/images/image3.jpg"),
		//    new DanceFloor("Зал Малый", 550, "Площадь 30 метров. Зеркала. Звук.", "/images/image4.jpg"),
		//    new DanceFloor("Зал Сочи", 900, "Площадь 110 метров. Зеркала. Звук.", "/images/image5.jpg"),
		//    new DanceFloor("Зал Hip-Hop", 450, "Площадь 25 метров. Зеркала. Звук.", "/images/image6.jpg"),
		//    new DanceFloor("Зал Kizomba", 1500, "Площадь 150 метров. Зеркала. Звук.", "/images/image7.jpg"),
		//    new DanceFloor("Зал Bachata", 2000, "Площадь 25 метров. Зеркала. Звук.", "/images/image8.jpg"),

		//};

		public DanceFloor GetFloor(Guid id)
        {
			return databaseContext.DanceFloors.FirstOrDefault(danceFloor => danceFloor.Id == id);

        }

        public List<DanceFloor> GetDanceFloors()
        {
            return databaseContext.DanceFloors.ToList();
		}

        public void Delete(Guid id)
        {
			databaseContext.DanceFloors.Remove(GetFloor(id));
            databaseContext.SaveChanges();
        }

        public void Add(DanceFloor danceFloor)
        {
			danceFloor.ImagePath = "/images/image1.jpg";
			danceFloor.Time = new Time();

            
            databaseContext.DanceFloors.Add(danceFloor);
            databaseContext.SaveChanges();
        }

		public void Update (DanceFloor danceFloor)
		{
			var existingDanceFloor = databaseContext.DanceFloors.FirstOrDefault(x => x.Id == danceFloor.Id);
			//if (existingDanceFloor != null) { return; }
			var time = new Time();
			if (danceFloor.Time != null)
			{
				time = danceFloor.Time;
                
            }
            
            existingDanceFloor.Name = danceFloor.Name;
			existingDanceFloor.Cost = danceFloor.Cost;
			existingDanceFloor.Description = danceFloor.Description;
			
            databaseContext.SaveChanges();
		}
    }
}
