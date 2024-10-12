using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using System.Net.Http;

namespace OnlineShop.Db
{
    
    public class CartsDBRepository : ICartsRepository 
    {
        private readonly DataBaseContext databaseContext;
        public readonly IItemsRepository itemsRepository;

        public CartsDBRepository(DataBaseContext databaseContext, IItemsRepository itemsRepository)
        {
            this.databaseContext = databaseContext;
            
            this.itemsRepository = itemsRepository;
        }


        public List<Cart> GetAll()
        {
            return databaseContext.Carts.Include(x => x.Items)
             .ThenInclude(x => x.DanceFloor)
              .ToList();
        }
        public Cart TryGetByUserId(Guid userId)
        {
            var carts = GetAll();
            
            return carts.FirstOrDefault(x => x.UserId == userId);
            
            
        }

        

        public void Add(Cart cart)
        {
            //проверку сделать на существование
            databaseContext.Carts.Add(cart);
            databaseContext.SaveChanges();
        }
        public void AddItem(DanceFloor danceFloor, Guid userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = Guid.NewGuid(),
                            Quantity = 1,
                            DanceFloor = danceFloor,
                            OrderedTime = danceFloor.Time.SelectedDate,
                        }
                    }
                };
                databaseContext.Carts.Add(newCart);
            }
            
            else
            {
                var newItem = new Item()
                {
                    Id = Guid.NewGuid(),
                    Quantity = 1,
                    DanceFloor = danceFloor,
                    OrderedTime = danceFloor.Time.SelectedDate,
                };
                itemsRepository.Add(newItem); // сначала добавляем позицию в репозиторий БД 
                existingCart.Items.Add(newItem); //затем добавляем ее в корзину и она получает свой номер  иначе ее как бы не существует  
                    
                
            }

            databaseContext.SaveChanges();
        }

        public void AddItems(List<Item> cartItems, Guid userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = cartItems
                };
                databaseContext.Carts.Add(newCart);
            }

            else
            {
                
                foreach(var item in cartItems)
                {
                    existingCart.Items.Add(item);
                    //itemsRepository.Add(item);
                }
                 // сначала добавляем позицию в репозиторий БД 
                 //затем добавляем ее в корзину и она получает свой номер  иначе ее как бы не существует  


            }

            databaseContext.SaveChanges();
        }
        public void RemoveItem(Guid itemId, Guid userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {
                return;
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(x => x.Id == itemId);

                if (existingCartItem.Quantity == 1)
                {
                    existingCart.Items.Remove(existingCartItem);
                }
                else
                {
                    existingCartItem.Quantity -= 1;
                }
                databaseContext.SaveChanges();
            }

        }
        public void Clear(Guid userId)
        {
            var existingCart = TryGetByUserId(userId);

            if (existingCart == null)
            {

                return;
            }

            databaseContext.Carts.Remove(existingCart);
            databaseContext.SaveChanges();
        }


        public void Update(Cart cart)
        {
            
            var existingCart = databaseContext.Carts.FirstOrDefault(x => x.Id == cart.Id);
            
            if (existingCart != null) { return; }
            if (existingCart == null) 
            {
                   existingCart = new Cart()
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    Items = cart.Items,
                    CreatedDateTime = cart.CreatedDateTime,
                    
                };
                
            }
           
            databaseContext.SaveChanges();
        }

        //public void RemoveLine(DanceFloor danceFloor)
        //{
        //    newCart.Items.RemoveAll(l => l.DanceFloor.Id == danceFloor.Id);
        //}

        //public IEnumerable<CartItem> Lines
        //{
        //    get { return Items; }
        //}
    }
}
