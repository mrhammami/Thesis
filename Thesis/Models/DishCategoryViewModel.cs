using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    [MetadataType(typeof(DishCategoryViewModel))]
    public partial class DishCategory
    {

    }
    public class DishCategoryViewModel
    {
        [Display(Name="Kategória neve")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A mező kitöltése kötelező.")]
        public string Name { get; set; }
    }
}