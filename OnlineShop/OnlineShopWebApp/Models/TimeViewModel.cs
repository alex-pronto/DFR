using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models;
public class TimeViewModel
{
        [Key]
        public Guid Id { get; set; }
        public DateTime SelectedDate { get; set; } // выбранная дата в календаре
        public int SelectedTime { get; set; } //выбранное время, которое приходит из представления после выбора дат и далее присоединяется к полной дате аренды

        public List<int> RangeTimeForOrders = new List<int>() //доступный диапазон бронирования
        {
            8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
        };
        public TimeViewModel() 
        {
            Id = Guid.NewGuid();
            SelectedDate = DateTime.MinValue;
            SelectedTime = 0;


        }


    }
