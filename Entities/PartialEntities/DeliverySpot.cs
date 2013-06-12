namespace iTechArt.Shop.Entities
{
    public partial class DeliverySpot : IEntity
    {
        public override string ToString()
        {
            var st = string.Format("id: {0}, type: {1}, start time: {2}, end time: {3}", this.Id, this.Type, this.StartTime, this.EndTime);
            return st;
        }

    }
}
