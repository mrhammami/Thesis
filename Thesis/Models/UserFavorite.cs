//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Thesis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserFavorite
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DishID { get; set; }
    
        public virtual Dish Dish { get; set; }
        public virtual User User { get; set; }
    }
}