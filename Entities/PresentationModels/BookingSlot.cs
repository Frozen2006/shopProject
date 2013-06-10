using System;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class BookingSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SlotsType Type { get; set; }
        public SlotStatus Status { get; set; }
    }
}
