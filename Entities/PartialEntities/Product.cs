namespace iTechArt.Shop.Entities
{
	public partial class Product : IEntity
	{
		public override string ToString()
		{
			return string.Format("id: {0}, name: {1}, Price: {2}, category: {3}",
				Id, Name, Price, Category.Name);
		}
	}
}
