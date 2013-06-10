using System;
using System.Collections.Generic;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Web.Models
{
    public class TimeSlotsModel
    {
        public DateTime Today;
        public List<DateTime> StartDay;
        public List<BookingSlot> SlotsOneHour;
        public List<BookingSlot> SlotsTwoHour;
        public List<BookingSlot> SlotsFourHour;
        public bool IsButtonEnable = false;
    }
}