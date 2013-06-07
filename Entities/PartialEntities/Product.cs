namespace iTechArt.Shop.Entities
{
    public partial class Product : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, name: {1}, price: {2}, category: {3}", this.Id, this.Name, this.Price, this.Category.Name);
            return st;
        }
    }
}
