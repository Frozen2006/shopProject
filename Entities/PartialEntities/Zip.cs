namespace iTechArt.Shop.Entities
{
    public partial class Zip : IEntity
    {
        public int Id { get; private set; }

        public override string ToString()
        {
            var st = string.Format("id: {0}, zip code: {1}, city: {2}", this.Id, this.ZipCode, this.City);
            return st;
        }
    }
}
