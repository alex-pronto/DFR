//using Microsoft.Extensions.Hosting;
//using OnlineShopWebApp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Reflection.Metadata.Ecma335;

//namespace OnlineShopWebApp.Repository
//{
//    public class InMemoryUsersRepository : IUsersRepository
//    {
//        //public  List<User> users = new List<User>();

//        private List<User> users = new List<User>()

//        {
//            new User("Александр", "Егоров", "89182033545","alex.pronto@gmail.com")
           
//        };
//        public User TryGetById(Guid id)
//        {
//            return users.FirstOrDefault(user => user.Id == id);

//        }

//        public User TryGetByEmail(string email)
//        {
//            return users.FirstOrDefault(user => user.EMail == email);

//        }
//        public List<User> GetUsers()
//        {
//            return users;
//        }

//        public void Delete(Guid id)
//        {
//            users.Remove(TryGetById(id)); 
//        }

//        public void Add(User user)
//        {
//            users.Add(user);
//        }
//    }
//}
