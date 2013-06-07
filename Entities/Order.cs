//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace iTechArt.Shop.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public Order()
        {
            this.Buyes = new HashSet<Buye>();
        }
    
        public int Id { get; set; }
        public int DeliverySpotId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public int Discount { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
    
        public virtual ICollection<Buye> Buyes { get; set; }
        public virtual DeliverySpot DeliverySpot { get; set; }
        public virtual User User { get; set; }
    }
}
