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
    
    public partial class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public int LevelID { get; set; }
    
        public virtual Level Level { get; set; }
        public virtual Company Company { get; set; }
    }
}
