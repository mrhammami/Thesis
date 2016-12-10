using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class AspNetUserViewModel
    {
        [Display(Name ="Felhasználó neve")]
        public string UserName { get; set; }

        [Display(Name="Cég")]
        public int? CompanyID { get; set; }
    }

    [MetadataType(typeof(AspNetUserViewModel))]
    public partial class AspNetUser
    {
        public int? CompanyID { get; set; }
    }
}