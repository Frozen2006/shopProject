namespace iTechArt.Shop.Entities
{
	public partial class Session : IEntity
	{
		public override string ToString()
		{
			return string.Format("id: {0}, user id: {1}, token: {2}", Id, UserId, Guid);
		}
	}
}
