namespace iTechArt.Shop.Entities.PresentationModels
{
    public class UserDetails
    {
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
        public RolesType Role { get; set; }
    }
}
