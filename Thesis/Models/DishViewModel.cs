using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    [MetadataType(typeof(DishViewModel))]
    public partial class Dish
    {

    }
    public class DishViewModel
    {
        [Display(Name="Ár")]
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        [Range(10, int.MaxValue, ErrorMessage = "A megadott érték nem megfelelő.")]
        public int Price { get; set; }

        [Display(Name = "Név")]
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        public string Name { get; set; }

        [Display(Name = "Étel kategóriája")]
        [Required(ErrorMessage = "A mező kitöltése kötelező.")]
        public int DishCategoryID { get; set; }
    }
}