using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models;
public class Time
{
    
	public Guid Id { get; set; }
	public DateTime SelectedDate { get; set; } //  { get { return DateTime.Now;} set { SelectedDate = SelectedDate; } }выбранная дата в календаре
	public int SelectedTime { get; set; } //выбранное время, которое приходит из представления после выбора дат и далее присоединяется к полной дате аренды

    public List<int> RangeTimeForOrders = new List<int>() //доступный диапазон бронирования
        {
            8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23
        };
    
}
