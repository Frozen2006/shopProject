namespace iTechArt.Shop.Entities
{
    public partial class DeliverySpot : IEntity
    {
        public override string ToString()
        {
			return string.Format("id: {0}, type: {1}, start time: {2}, end time: {3}", Id, Type, StartTime, EndTime);
        }

    }
}
