using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thesis.Models
{
    public class AppUser : IdentityUser
    {
        public string ExtraProperty { get; set; }
    }
}