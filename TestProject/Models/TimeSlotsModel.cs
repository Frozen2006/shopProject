using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;

namespace TestProject.Models
{
    public class TimeSlotsModel
    {
        public DateTime Today;
        public List<DateTime> startDay;
        public List<BookinSlot> SlotsOneHour;
        public List<BookinSlot> SlotsTwoHour;
        public List<BookinSlot> SlotsFourHour;
    }
}