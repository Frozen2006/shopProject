namespace iTechArt.Shop.Entities
{
    public partial class Session : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, user id: {1}, token: {2}", this.Id, this.UserId, this.Guid);
            return st;
        }
    }
}
