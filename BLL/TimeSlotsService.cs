using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.membership;
using Entities;
using Helpers;
using Interfaces;
using Ninject;

namespace BLL
{
    public class TimeSlotsService : ITimeSlotsService
    {
        private TimeSlotsRepository _timeRepository;
        private UserRepository _userRepository;

        private const int usersPerSlotLimit = 4;
        private const int hoursToStopAcceptBook = 3;

        [Inject]
        public TimeSlotsService(TimeSlotsRepository repo, UserRepository us)
        {
            _timeRepository = repo;
            _userRepository = us;
        }

        public List<BookinSlot> GetSlots(DateTime startTime, DateTime endTime, SlotsType type, string userEmail)
        {
            var slots = _timeRepository.ReadAll().Where(m => (m.StartTime >= startTime) && (m.EndTime <= endTime) && (m.Type == (int)(type))).ToList();

            List<BookinSlot> bookinSlots = new List<BookinSlot>();

            foreach (var slot in slots)
            {
                bookinSlots.Add(DeliverySpotToBookingSlot(slot, userEmail));
            }

            return bookinSlots;
        }

        public bool AddUserToSlot(DateTime startTime, SlotsType type, string userEmail)
        {
            User us = GetUser(userEmail);

            var findSlot =
                _timeRepository.ReadAll()
                               .FirstOrDefault(m => (m.StartTime == startTime) && (m.Type == (int)(type))) ??
                CreateSlot(startTime, type);


            if (findSlot.Users.Count >= usersPerSlotLimit)
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

            List<DeliverySpot> lst = new List<DeliverySpot>();

            foreach (var deliverySpot in us.DeliverySpots)
            {
                if ((deliverySpot.StartTime > DateTime.Now.AddHours(4.0)) &&
                    (us.Orders.FirstOrDefault(m => m.DeliverySpotId == deliverySpot.Id) == null))
                {
                    lst.Add(deliverySpot);
                }
            }

            return lst;
        }

        private DeliverySpot CreateSlot(DateTime startTime, SlotsType type)
        {
            DeliverySpot slot = new DeliverySpot();

            slot.StartTime = startTime;
            slot.Type = (int) type;
            slot.Users = new Collection<User>();

            DateTime endTime = new DateTime();

            endTime = startTime.AddHours(Convert.ToDouble((int)type)); //type - enum. Every record it them associated whith count of hours

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
        private BookinSlot DeliverySpotToBookingSlot(DeliverySpot ds, string UserEmail)
        {
            //map all parameters
            BookinSlot bookinSlot = Mapper.Map<DeliverySpot, BookinSlot>(ds);


            //generatin status field
            bookinSlot.Type = (SlotsType)ds.Type;

            SlotStatus slotStatus = SlotStatus.Free;

            if (ds.Users.Count < usersPerSlotLimit)
                slotStatus = SlotStatus.Middle;
            if (ds.Users.Count >= usersPerSlotLimit)
                slotStatus = SlotStatus.Fool;
             if (DateTime.Now.AddHours(3) >= ds.StartTime)
                 slotStatus = SlotStatus.Fool;

             if ((ds.Users.FirstOrDefault(m => m.email == UserEmail) != null) && (ds.Orders.FirstOrDefault(m => m.User.email == UserEmail) == null))
                slotStatus = SlotStatus.My;
            
            
            bookinSlot.Status = slotStatus;

            return bookinSlot;


        }

    }



}
