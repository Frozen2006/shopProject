using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using DAL.membership;
using Entities;
using Helpers;
using Ninject;

namespace BLL
{
    public class TimeSlotsService
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

        public List<BookinSlot> GetSlots(DateTime startTime, DateTime endTime, SlotsType type)
        {
            string stringType = GetStringType(type);

            var slots = _timeRepository.ReadAll().Where(m => (m.StartTime >= startTime) && (m.EndTime <= endTime) && (m.Type == stringType)).ToList();

            List<BookinSlot> bookinSlots = new List<BookinSlot>();

            foreach (var slot in slots)
            {
                bookinSlots.Add(DeliverySpotToBookingSlot(slot));
            }

            return bookinSlots;
        }

        public bool AddUserToSlot(DateTime startTime, SlotsType type, string userEmail)
        {
            string stringType = GetStringType(type);
            User us = GetUser(userEmail);

            var findSlot =
                _timeRepository.ReadAll()
                               .FirstOrDefault(m => (m.StartTime == startTime) && (m.Type == stringType)) ??
                CreateSlot(startTime, type);


            if (findSlot.Users.Count >= usersPerSlotLimit)
                return false;

            if (DateTime.Now.AddHours(3) <= findSlot.StartTime)
                return false;

            findSlot.Users.Add(us);


            _timeRepository.Update(findSlot);

            return true;
        }

        public void RemoveUserFromSlot(DateTime startTime, SlotsType type, string userEmail)
        {
            string stringType = GetStringType(type);
            var findSlot =
                _timeRepository.ReadAll()
                               .FirstOrDefault(m => (m.StartTime == startTime) && (m.Type == stringType));

            if (findSlot == null)
                throw new InstanceNotFoundException("Slot not found");

            User us = GetUser(userEmail);


            findSlot.Users.Remove(us);

            _timeRepository.Update(findSlot);
        }

        public List<DeliverySpot> GetUserSlots(string email)
        {
            var us = GetUser(email);

            return us.DeliverySpots.ToList();
        }

        private DeliverySpot CreateSlot(DateTime startTime, SlotsType type)
        {
            DeliverySpot slot = new DeliverySpot();

            slot.StartTime = startTime;
            slot.Type = Convert.ToString(type);
            slot.Users = new Collection<User>();

            DateTime endTime = new DateTime();

            switch (type)
            {
                case SlotsType.OneHour:
                    endTime = startTime.AddHours(1.0);
                    break;
                case SlotsType.TwoHour:
                    endTime = startTime.AddHours(2.0);
                    break;
                case SlotsType.FourHour:
                    endTime = startTime.AddHours(4.0);
                    break;
            }

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
        private BookinSlot DeliverySpotToBookingSlot(DeliverySpot ds)
        {
            BookinSlot bookinSlot = new BookinSlot() {StartTime = ds.StartTime, EndTime = ds.EndTime};

            SlotsType st = SlotsType.OneHour;
            switch (ds.Type)
            {
                case "OneHour":
                   st = SlotsType.OneHour;
                    break;
                case "TwoHour":
                    st = SlotsType.TwoHour;
                    break;
                case "FourHour":
                    st = SlotsType.FourHour;
                    break;
            }

            bookinSlot.Type = st;


            SlotStatus slotStatus = SlotStatus.Free;

            if (ds.Users.Count < usersPerSlotLimit)
                slotStatus = SlotStatus.Middle;
            if (ds.Users.Count >= usersPerSlotLimit)
                slotStatus = SlotStatus.Fool;
             if (DateTime.Now.AddHours(3) <= ds.StartTime)
                 slotStatus = SlotStatus.Fool;


            bookinSlot.Status = slotStatus;

            return bookinSlot;


        }

        private string GetStringType(SlotsType st)
            {
                    
                if (st == SlotsType.TwoHour)
                    return "TwoHour";
                if (st == SlotsType.FourHour)
                    return "FourHour";

                return "OneHour";
            }

    }



}
