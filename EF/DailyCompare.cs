//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DailyCompare
    {
        public int Id { get; set; }
        public System.DateTime Time { get; set; }
        public int UserId { get; set; }
        public int EditId { get; set; }
        public int OpponentEditId { get; set; }
        public short Score { get; set; }
    
        public virtual DailyEdit DailyEdit { get; set; }
        public virtual DailyEdit DailyEdit1 { get; set; }
        public virtual User User { get; set; }
    }
}
