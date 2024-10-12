using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum OrderStatusViewModel
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name ="Ожидает")]
        Awaits,
        [Display(Name = "Подтвержден")]
        Confirmed,
        [Display(Name = "Отменен")]
        Canceled,
        [Display(Name = "Отказан")]
        Denied
    }
}

