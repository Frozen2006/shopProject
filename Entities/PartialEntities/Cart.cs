namespace iTechArt.Shop.Entities
{
    partial class Cart : IEntity
    {
        public override string ToString()
        {
			return string.Format("id: {0}, product count: {1}", Id, Count);
        }
    }
}
