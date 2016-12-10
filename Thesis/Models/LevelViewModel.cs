using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class LevelViewModel
    {
        [Required(ErrorMessage ="A mező kitöltése kötelező.")]
        [Display(Name="Szint")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(LevelViewModel))]
    public partial class Level
    {

    }
}