using System;

namespace iTechArt.Shop.Entities.PresentationModels
{
    public class BookingSlot
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public SlotsType Type;
        public SlotStatus Status;
    }
}
