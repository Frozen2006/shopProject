namespace iTechArt.Shop.Entities
{
    partial class Order : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, user emai: {1}, product count: {2}, total price: {3}, with discount: {4}",
                                   this.Id, this.User.Email, this.Buyes.Count, this.TotalPrice, this.Discount);
            return st;
        }
    }
}
