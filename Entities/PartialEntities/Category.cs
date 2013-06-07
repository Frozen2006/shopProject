namespace iTechArt.Shop.Entities
{
    public partial class Category : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, name: {1}", this.Id, this.Name);
            return st;
        }
    }
}
