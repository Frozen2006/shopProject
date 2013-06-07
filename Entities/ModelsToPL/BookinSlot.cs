using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public enum SlotStatus
    {
        Free,
        Middle,
        Fool,
        My
    }
    public class BookinSlot
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public SlotsType Type;
        public SlotStatus Status;
    }
}
