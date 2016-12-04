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
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class OrderViewModel
    {
        [Display(Name="Mennyiség")]
        [Range(1,100,ErrorMessage ="A megadott értéknek 1 és 100 közé kell esnie.")]
        [Required(ErrorMessage ="A mező kitöltése kötelező.")]
        public int Amount { get; set; }
    }
}