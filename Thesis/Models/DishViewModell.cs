using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class DishViewModell
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage ="A mező megadása kötelező.")]
        [Display(Name="Név")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A mező megadása kötelező.")]
        [Display(Name = "Ár")]
        [Range(10, int.MaxValue, ErrorMessage ="A megadott érték nem megfelelő.")]
        public int Price { get; set; }

        [Display(Name="Kategória")]
        public int DishCategoryID { get; set; }
    }
}