namespace iTechArt.Shop.Entities.PresentationModels
{
    public class UserDetails
    {
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
        public RolesType Role { get; set; }
    }
}
