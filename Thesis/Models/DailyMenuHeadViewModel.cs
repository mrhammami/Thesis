using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    [MetadataType(typeof(DailyMenuHeadViewModel))]
    public partial class DailyMenuHead
    {

    }
    public class DailyMenuHeadViewModel
    {
        [Display(Name = "Menü dátuma")]
        public System.DateTime MenuDate { get; set; }
    }
}