namespace iTechArt.Shop.Entities
{
    partial class Cart : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, product count: {1}", this.Id, this.Count);
            return st;
        }
    }
}
