using System;
using System.Collections.Generic;
using System.Web.Mvc;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Filters;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers .Cart
{
    [CustomAuthrize(Roles = RolesType.User)]
    public class TimeSlotsController : BaseController
    {
        //
        // GET: /TimeSlots/

        private ITimeSlotsService TimeSlotService { get; set; }

        [Inject]
        public TimeSlotsController(ICategoryService categoryService, IUserService userService, ICartService cartService, ITimeSlotsService timeSlotsService)
            : base(categoryService, userService, cartService)
        {
            TimeSlotService = timeSlotsService;
        }

        public ActionResult Index()
        {
            TimeSlotsModel model = GetModel(false);

            return View(model);
        }

        private TimeSlotsModel GetModel(bool whithBackBtn)
        {
            string userEmail = GetUserEmail();
            var model = new TimeSlotsModel {Today = DateTime.Now};

            DateTime date = DateTime.Now;
            DateTime start = date;

            DateTime end = date.AddDays(7.0);

            model.SlotsOneHour = TimeSlotService.GetSlots(start, end, SlotsType.OneHour, userEmail);
            model.SlotsTwoHour = TimeSlotService.GetSlots(start, end, SlotsType.TwoHour, userEmail);
            model.SlotsFourHour = TimeSlotService.GetSlots(start, end, SlotsType.FourHour, userEmail);

            //colection with date times to Periods from 9.00 to 22.00 with step in 1 hour, setted to start date of the week
            model.StartDay = new List<DateTime>();

            var toList = new DateTime(start.Year, start.Month, start.Day, 9, 0, 0);

            for (int i = 0; i < 13; i++)
            {
                model.StartDay.Add(toList);
                toList = toList.AddHours(1.0);
            }

            model.IsButtonEnable = whithBackBtn;

            return model;
        }


        public ActionResult CheckOut()
        {
            string userEmail = GetUserEmail();

            if (TimeSlotService.GetUserSlots(userEmail).Count == 0)
            {
                TimeSlotsModel model = GetModel(true);
                return View("Index", model);
            }
            return RedirectToAction("ConfirmOrder", "Cart");
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult Book(int hour, int day, int mounth, int year, int slotType)
        {
            if (Request.IsAjaxRequest())
            {
                string userEmail = GetUserEmail();
                var bookTime = new DateTime(year, mounth, day, hour, 0, 0);
                var st = SlotsType.OneHour;

                switch (slotType)
                {
                    case(1):
                        st = SlotsType.OneHour;
                        break;
                    case(2):
                        st = SlotsType.TwoHour;
                        break;
                    case(4):
                        st = SlotsType.FourHour;
                        break;
                }

                if (TimeSlotService.AddUserToSlot(bookTime, st, userEmail))
                {
                    return Content("true");
                }
                return Content("false");
            }
            return null;
        }

    }
}
