using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArt.Shop.Entities;
using Helpers;
using iTechArt.Shop.Entities;

namespace Interfaces
{
    public interface ITimeSlotsService
    {
        List<BookinSlot> GetSlots(DateTime startTime, DateTime endTime, SlotsType type, string userEmail);
        bool AddUserToSlot(DateTime startTime, SlotsType type, string userEmail);
        void RemoveUserFromSlot(DateTime startTime, SlotsType type, string userEmail);
        List<DeliverySpot> GetUserSlots(string email);
    }
}
