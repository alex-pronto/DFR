using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
	public class DataBaseContext : DbContext
	{
		
		public DbSet<DanceFloor> DanceFloors { get; set; }
		public DbSet<Time> Time { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<FavouriteDanceFloor> FavouriteDanceFloors { get; set; }
       
        public DataBaseContext(DbContextOptions <DataBaseContext> options) : base(options)
		{
            Database.Migrate();
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var timeSalsa = new Time
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.MinValue,
                SelectedTime = 0, 
                RangeTimeForOrders = new List<int>()
                            {
                            8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
                            },
            };
            var timeMore = new Time
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.MinValue,
                SelectedTime = 0,
                RangeTimeForOrders = new List<int>()
                            {
                            8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
                            },
            };
            var timeMaliy = new Time
            {
                Id = Guid.NewGuid(),
                SelectedDate = DateTime.MinValue,
                SelectedTime = 0,
                RangeTimeForOrders = new List<int>()
                            {
                            8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
                            },
            };
            var salsa = new DanceFloor
            {
                Id = Guid.NewGuid(),
                Name = "Сальса",
                Cost = 1100,
                Description = "Площадь - 110 м2, Полы - Паркет, Зеркала, Звук",
                ImagePath = "/images/image1.jpg",
                TimeId = timeSalsa.Id,
            };

            var more = new DanceFloor
            {
                Id = Guid.NewGuid(),
                Name = "Море",
                Cost = 700,
                Description = "Площадь - 60 м2, Полы - Ламинат, Зеркала, Звук",
                ImagePath = "/images/image1.jpg",
                TimeId = timeMore.Id,
            };

            var maliy = new DanceFloor
            {
                Id = Guid.NewGuid(),
                Name = "Малый",
                Cost = 550,
                Description = "Площадь - 30 м2, Полы - Ламинат, Зеркала, Звук",
                ImagePath = "/images/image1.jpg",
                TimeId = timeMaliy.Id,
            };

            modelBuilder.Entity<DanceFloor>().HasData(salsa, more, maliy);
            modelBuilder.Entity<Time>().HasData(timeSalsa, timeMore, timeMaliy);
        }


    }
}