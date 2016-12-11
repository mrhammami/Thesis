using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    [MetadataType(typeof(OrderViewModel))]
    public partial class Order
    {
        [ForeignKey("UserID")]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name="Cég")]
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }

        [Display(Name="Szint")]
        public string LevelName { get; set; }
        public int LevelID { get; set; }
        
        [Display(Name="Étel")]
        public string DishName { get; set; }

        [Display(Name = "Kategória")]
        public string DishCategoryName { get; set; }

        public int DishCategoryID { get; set; }


    }

    public class OrderViewModel
    {
        [Display(Name="Mennyiség")]
        [Range(1,100,ErrorMessage ="A megadott értéknek 1 és 100 közé kell esnie.")]
        [Required(ErrorMessage ="A mező kitöltése kötelező.")]
        public int Amount { get; set; }
    }
}