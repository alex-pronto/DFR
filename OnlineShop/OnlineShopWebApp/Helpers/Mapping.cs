using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShop.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Helpers
{
    public static partial class Mapping
    {
        public static List<DanceFloorViewModel> ToDanceFoorsViewModel(this List<DanceFloor> danceFloors)
        {
            var danceFloorViewModels = new List<DanceFloorViewModel>();
            foreach (var danceFloor in danceFloors)
            {
                danceFloorViewModels.Add(ToDanceFloorViewModel(danceFloor));
            }
            return danceFloorViewModels;
        }

        public static DanceFloorViewModel ToDanceFloorViewModel(this DanceFloor danceFloor)
        {
            var time = new TimeViewModel();
            if (danceFloor.Time != null)
            {
                time = ToTimeViewModel(danceFloor.Time);
            }

            return new DanceFloorViewModel
            {
                Id = danceFloor.Id,
                Name = danceFloor.Name,
                Cost = danceFloor.Cost,
                Description = danceFloor.Description,
                ImagePath = danceFloor.ImagePath,
                Time = time,



            };
        }

        public static DanceFloor ToDanceFloor(this DanceFloorViewModel danceFloorViewModel)
        {
            return new DanceFloor
            {
                Id = danceFloorViewModel.Id,
                Name = danceFloorViewModel.Name,
                Description = danceFloorViewModel.Description,
                Cost = danceFloorViewModel.Cost,
                ImagePath = danceFloorViewModel.ImagePath,
                Time = ToTime(danceFloorViewModel.Time),
            };
        }



        public static List<DanceFloor> ToDanceFloors(this List<DanceFloorViewModel> danceFloorViewModels)
        {
            var danceFloors = new List<DanceFloor>();
            foreach (var danceFloorViewModel in danceFloorViewModels)
            {
                danceFloors.Add(ToDanceFloor(danceFloorViewModel));
            }
            return danceFloors;
        }

        public static TimeViewModel ToTimeViewModel(this Time time)
        {
            return new TimeViewModel()
            {
                Id = time.Id,
                SelectedDate = time.SelectedDate,
                SelectedTime = time.SelectedTime,
                RangeTimeForOrders = time.RangeTimeForOrders,
            };
        }

        public static Time ToTime(this TimeViewModel timeViewModel)
        {
            return new Time
            {
                Id = timeViewModel.Id,
                SelectedDate = timeViewModel.SelectedDate,
                SelectedTime = timeViewModel.SelectedTime,
                RangeTimeForOrders = timeViewModel.RangeTimeForOrders,

            };
        }



        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo userDeliveryInfo)
        {
            return new UserDeliveryInfoViewModel()
            {
                Id = userDeliveryInfo.Id,
                Name = userDeliveryInfo.Name,
                SurName = userDeliveryInfo.SurName,
                EMail = userDeliveryInfo.EMail,
                Phone = userDeliveryInfo.Phone,
            };
        }

        public static UserDeliveryInfo ToUserDeliveryInfo(this UserDeliveryInfoViewModel userDeliveryInfoViewModel)
        {
            return new UserDeliveryInfo()
            {
                Id = userDeliveryInfoViewModel.Id,
                Name = userDeliveryInfoViewModel.Name,
                SurName = userDeliveryInfoViewModel.SurName,
                EMail = userDeliveryInfoViewModel.EMail,
                Phone = userDeliveryInfoViewModel.Phone,
            };
        }
        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Items = ToCartItemsViewModel(order.Items),
                OrderTime = order.OrderTime,
                //User = order.User,
                DeliveryInfo = ToUserDeliveryInfoViewModel(order.DeliveryInfo),
                Status = (OrderStatusViewModel)order.Status,
            };
        }

        public static List<OrderViewModel> ToOrdersViewModel(this List<Order> orders)
        {
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                ordersViewModel.Add(ToOrderViewModel(order));

            }
            return ordersViewModel;
        }

        public static Order ToOrder(this OrderViewModel orderViewModel)
        {
            return
                new Order()
                {
                    Id = orderViewModel.Id,
                    DeliveryInfo = ToUserDeliveryInfo(orderViewModel.DeliveryInfo),
                    Status = (OrderStatus)orderViewModel.Status,
                    Items = ToCartItems(orderViewModel.Items),
                    OrderTime = orderViewModel.OrderTime,

                };
        }

        public static CartItemViewModel ToCartItemViewModel(this Item cartItem)
        {
            return new CartItemViewModel
            {
                Id = cartItem.Id,
                DanceFloor = ToDanceFloorViewModel(cartItem.DanceFloor),
                Quantity = cartItem.Quantity,
                OrderedTime = cartItem.OrderedTime,
            };
        }

        public static List<CartItemViewModel> ToCartItemsViewModel(this List<Item> cartItems)
        {
            var cartItemsViewModel = new List<CartItemViewModel>();
            foreach (var cartItem in cartItems)
            {
                cartItemsViewModel.Add(ToCartItemViewModel(cartItem));
            }
            return cartItemsViewModel;

        }

        public static Item ToCartItem(this CartItemViewModel cartItemViewModel)
        {
            return new Item
            {
                Id = cartItemViewModel.Id,
                DanceFloor = ToDanceFloor(cartItemViewModel.DanceFloor),
                Quantity = cartItemViewModel.Quantity,
                OrderedTime = cartItemViewModel.OrderedTime,
            };
        }

        public static List<Item> ToCartItems(this List<CartItemViewModel> cartItemsViewModel)
        {
            var cartItems = new List<Item>();
            foreach (var cartItemViewModel in cartItemsViewModel)
            {
                cartItems.Add(ToCartItem(cartItemViewModel));
            }
            return cartItems;

        }

        public static CartViewModel ToCartViewModel(this Cart cart)
        {
            if (cart == null)
            {
                
                return new CartViewModel();
               
            }

            return new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = ToCartItemsViewModel(cart.Items),
                CreatedDateTime = cart.CreatedDateTime,


            };
        }

        public static Cart ToCart(this CartViewModel cartViewModel)
        {
            return new Cart
            {
                Id = cartViewModel.Id,
                UserId = cartViewModel.UserId,
                CreatedDateTime = cartViewModel.CreatedDateTime,
                Items = ToCartItems(cartViewModel.Items),

            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel {Id = user.Id, Email = user.Email, Name = user.Name, SurName = user.SurName, Phone = user.PhoneNumber };

        }

        public static User ToUser(this UserViewModel userViewModel) // в конструктор не передаем почту так как мы ее не меняем   поменять почту может только пользователь.  админ не может  так  должно быть подтверждение по почте у пользователя
        {
            return new User { Id = userViewModel.Id,  Name = userViewModel.Name, SurName = userViewModel.SurName, PhoneNumber = userViewModel.Phone };
        }

        public static List<UserViewModel> ToUsersViewModel(this IList<User> users)
        {
            var usersViewModel = new List<UserViewModel>();
            foreach (var user in users)
            {
                usersViewModel.Add(user.ToUserViewModel());
            }
        
            return usersViewModel;
        }
        public static List<RoleViewModel> ToRolesViewModel(this List<IdentityRole> roles)
        {
            var rolesViewModel = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                
                rolesViewModel.Add(new RoleViewModel() { Name = role.Name });
            }

            return rolesViewModel;
        }
        public static string ToUserRolesViewModel(this IList<string> currentRoles) 
        {
            string userRoleViewModel = ""; 
            foreach (var role in currentRoles)
            {
                userRoleViewModel += role.ToString() + " ";
            }
            return userRoleViewModel;
        }

       
    }
}
