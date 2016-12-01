using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    [MetadataType(typeof(UserViewModel))]
    public partial class User
    {

    }
    public class UserViewModel
    {
        [Display(Name="Felhasználó neve")]
        public string Name { get; set; }
    }
}