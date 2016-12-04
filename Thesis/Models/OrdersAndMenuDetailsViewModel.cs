using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class OrdersAndMenuDetailsViewModel
    {
        public Order DisplayOrder { get; set; }
        public DailyMenuDetail DisplayMenuDetail { get; set; }

        [Display(Name="Összesen")]
        public int? OrderTotalSummary {
            get
            {
                if (this.Orders.Count() > 0)
                {
                    return Orders.Sum(x => x.Amount * x.Dish.Price);
                }
                else
                {
                    return 0;
                }
            }
        }


        public IEnumerable<Thesis.Models.Order> Orders { get; set; }
        public IEnumerable<Thesis.Models.DailyMenuDetail> MenuDetails { get; set; }
        public IEnumerable<Thesis.Models.DishCategory> DisplayDishCategories { get; set; }
    }
}