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
    
    public partial class User
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Sessions = new HashSet<Session>();
            this.DeliverySpots = new HashSet<DeliverySpot>();
            this.Carts = new HashSet<Cart>();
        }
    
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public int Role { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<DeliverySpot> DeliverySpots { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
