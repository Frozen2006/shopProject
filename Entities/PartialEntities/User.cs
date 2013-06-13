namespace iTechArt.Shop.Entities
{
	partial class User : IEntity
	{
		public override string ToString()
		{
			return string.Format("id: {0}, first name: {1}, last name: {2}, email: {3}", Id, FirstName,
								   LastName, Email);
		}
	}
}
