using System;
using System.Collections.Generic;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Web.Models
{
    public class TimeSlotsTableModel
    {
        public DateTime Today;
        public List<DateTime> StartDay;
        public List<BookingSlot> Slots;
        public int Step = 1;
    }
}