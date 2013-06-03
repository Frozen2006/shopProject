//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Sessions = new HashSet<Session>();
            this.DeliverySpots = new HashSet<DeliverySpot>();
            this.Carts = new HashSet<Cart>();
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string title { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string phone { get; set; }
        public string phone2 { get; set; }
        public int zip { get; set; }
        public string city { get; set; }
        public int RoleId { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<DeliverySpot> DeliverySpots { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
