namespace iTechArt.Shop.Entities
{
    partial class User : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, first name: {1}, last name: {2}, email: {3}", this.Id, this.FirstName,
                                   this.LastName, this.Email);
            return st;
        }
    }
}
