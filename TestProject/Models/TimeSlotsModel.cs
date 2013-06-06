﻿using System;
using System.Collections.Generic;
using Helpers;

namespace TestProject.Models
{
    public class TimeSlotsModel
    {
        public DateTime Today;
        public List<DateTime> StartDay;
        public List<BookinSlot> SlotsOneHour;
        public List<BookinSlot> SlotsTwoHour;
        public List<BookinSlot> SlotsFourHour;
        public bool IsButtonEnable = false;
    }
}