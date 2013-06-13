namespace iTechArt.Shop.Entities
{
    partial class Order : IEntity
    {
        public override string ToString()
        {
            return string.Format("id: {0}, user emai: {1}, product count: {2}, total price: {3}, with discount: {4}",
                                   Id, User.Email, Buyes.Count, TotalPrice, Discount);
        }
    }
}
