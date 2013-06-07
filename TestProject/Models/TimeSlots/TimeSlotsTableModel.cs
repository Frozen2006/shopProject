using System;
using System.Collections.Generic;
using Helpers;

namespace iTechArt.Shop.Web.Models
{
    public class TimeSlotsTableModel
    {
        public DateTime Today;
        public List<DateTime> StartDay;
        public List<BookinSlot> Slots;
        public int Step = 1;
    }
}