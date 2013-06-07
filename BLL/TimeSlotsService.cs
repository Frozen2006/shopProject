using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using AutoMapper;
using iTechArt.Shop.DataAccess.Repositories;
using iTechArt.Shop.Entities;
using Helpers;
using Interfaces;
using Ninject;

namespace iTechArt.Shop.Logic.Services
{
    public class TimeSlotsService : ITimeSlotsService
    {
        private readonly TimeSlotsRepository _timeRepository;
        private readonly UserRepository _userRepository;

        private const int UsersPerSlotLimit = 4;
        private const int HoursToStopAcceptBook = 4;

        [Inject]
        public TimeSlotsService(TimeSlotsRepository repo, UserRepository us)
        {
            _timeRepository = repo;
            _userRepository = us;
        }

        public List<BookinSlot> GetSlots(DateTime startTime, DateTime endTime, SlotsType type, string userEmail)
        {
            var slots = _timeRepository.ReadAll().Where(m => (m.StartTime >= startTime) && (m.EndTime <= endTime) && (m.Type == (int)(type))).ToList();

            return slots.Select(m => DeliverySpotToBookingSlot(m, userEmail)).ToList();
        }

        public bool AddUserToSlot(DateTime startTime, SlotsType type, string userEmail)
        {
            User us = GetUser(userEmail);

            var findSlot =
                _timeRepository.ReadAll()
                               .FirstOrDefault(m => (m.StartTime == startTime) && (m.Type == (int)(type))) ??
                CreateSlot(startTime, type);


            if (findSlot.Users.Count >= UsersPerSlotLimit)
                return false;

            if (DateTime.Now.AddHours(3) >= findSlot.StartTime)
                return false;

            findSlot.Users.Add(us);


            _timeRepository.Update(findSlot);

            return true;
        }

        public void RemoveUserFromSlot(DateTime startTime, SlotsType type, string userEmail)
        {
            var findSlot =
                _timeRepository.ReadAll()
                               .FirstOrDefault(m => (m.StartTime == startTime) && (m.Type == (int)(type)));

            if (findSlot == null)
                throw new InstanceNotFoundException("Slot not found");

            User us = GetUser(userEmail);


            findSlot.Users.Remove(us);

            _timeRepository.Update(findSlot);
        }

        public List<DeliverySpot> GetUserSlots(string email)
        {
            var us = GetUser(email);

            return us.DeliverySpots.Where(
                m =>
                (m.StartTime > DateTime.Now.AddHours(Convert.ToDouble(HoursToStopAcceptBook)) &&
                 (us.Orders.FirstOrDefault(q => q.DeliverySpotId == m.Id) == null))).ToList();
        }

        private DeliverySpot CreateSlot(DateTime startTime, SlotsType type)
        {
            var slot = new DeliverySpot {StartTime = startTime, Type = (int) type, Users = new Collection<User>()};

            DateTime endTime = startTime.AddHours(Convert.ToDouble((int)type)); //type - enum. Every record of them associated whith count of hours

            slot.EndTime = endTime;

            _timeRepository.Create(slot);

            return slot;
        }
        private User GetUser(string email)
        {
            User us = _userRepository.ReadAll().FirstOrDefault(m => m.email == email);
            if (us == null)
                throw new InstanceNotFoundException("User not found");

            return us;
        }
        private BookinSlot DeliverySpotToBookingSlot(DeliverySpot ds, string userEmail)
        {
            //map all parameters
            BookinSlot bookinSlot = Mapper.Map<DeliverySpot, BookinSlot>(ds);


            //generatin status field
            bookinSlot.Type = (SlotsType)ds.Type;

            var slotStatus = SlotStatus.Free;

            if (ds.Users.Count < UsersPerSlotLimit)
                slotStatus = SlotStatus.Middle;
            if (ds.Users.Count >= UsersPerSlotLimit)
                slotStatus = SlotStatus.Fool;
             if (DateTime.Now.AddHours(3) >= ds.StartTime)
                 slotStatus = SlotStatus.Fool;

             if ((ds.Users.FirstOrDefault(m => m.email == userEmail) != null) && (ds.Orders.FirstOrDefault(m => m.User.email == userEmail) == null))
                slotStatus = SlotStatus.My;
            
            
            bookinSlot.Status = slotStatus;

            return bookinSlot;


        }

    }



}
