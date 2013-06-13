namespace iTechArt.Shop.Entities
{
    public partial class Zip : IEntity
    {
        public int Id { get; private set; }

        public override string ToString()
        {
            return string.Format("id: {0}, zip code: {1}, city: {2}", Id, ZipCode, City);
        }
    }
}
