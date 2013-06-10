using System;
using System.Collections.Generic;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Entities;
using iTechArt.Shop.Entities.PresentationModels;

namespace iTechArt.Shop.Common.Services
{
    public interface ITimeSlotsService
    {
        List<BookingSlot> GetSlots(DateTime startTime, DateTime endTime, SlotsType type, string userEmail);
        bool AddUserToSlot(DateTime startTime, SlotsType type, string userEmail);
        void RemoveUserFromSlot(DateTime startTime, SlotsType type, string userEmail);
        List<DeliverySpot> GetUserSlots(string email);
    }
}
