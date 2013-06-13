namespace iTechArt.Shop.Entities
{
    public partial class Category : IEntity
    {
        public override string ToString()
        {
            return string.Format("id: {0}, name: {1}", Id, Name);
        }
    }
}
