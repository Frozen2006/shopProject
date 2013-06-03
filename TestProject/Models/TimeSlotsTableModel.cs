using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;

namespace TestProject.Models
{
    public class TimeSlotsTableModel
    {
        public DateTime Today;
        public List<DateTime> startDay;
        public List<BookinSlot> Slots;
        public int Step = 1;
    }
}