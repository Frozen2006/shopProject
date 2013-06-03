using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.membership;
using Helpers;
using Ninject;
using TestProject.Models;

namespace TestProject.Controllers
{
    [Filters.CustomAuthrize(Roles = "User")]
    public class TimeSlotsController : Controller
    {
        //
        // GET: /TimeSlots/

        private TimeSlotsService _tss;
        private UsersService _usersService;


        [Inject]
        public TimeSlotsController(TimeSlotsService tsinp, UsersService us)
        {
            _tss = tsinp;
            _usersService = us;
        }


        public ActionResult Index()
        {
            TimeSlotsModel model = GetModel(false);
                return View(model);
        }


        private TimeSlotsModel GetModel(bool whithBackBtn)
        {
            string userEmail = _usersService.GetEmailIfLoginIn();
            TimeSlotsModel model = new TimeSlotsModel();

            model.Today = DateTime.Now;

            DateTime date = DateTime.Now;
            DateTime start = date;

            DateTime end = date.AddDays(7.0);

            model.SlotsOneHour = _tss.GetSlots(start, end, SlotsType.OneHour, userEmail);
            model.SlotsTwoHour = _tss.GetSlots(start, end, SlotsType.TwoHour, userEmail);
            model.SlotsFourHour = _tss.GetSlots(start, end, SlotsType.FourHour, userEmail);

            //colection with date times to Periods from 9.00 to 22.00 with step in 1 hour, setted to start date of the week
            model.startDay = new List<DateTime>();

            DateTime toList = new DateTime(start.Year, start.Month, start.Day, 9, 0, 0);

            for (int q = 0; q < 13; q++)
            {
                model.startDay.Add(toList);
                toList = toList.AddHours(1.0);
            }

            model.isButtonEnable = whithBackBtn;

            return model;
        }


        public ActionResult CheckOut()
        {
            string userEmail = _usersService.GetEmailIfLoginIn();

            if (_tss.GetUserSlots(userEmail).Count == 0)
            {
                TimeSlotsModel model = GetModel(true);
                return View("Index", model);
            }
            else
            {
                return RedirectToAction("ConfirmOrder", "Cart");
            }
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult Book(int hour, int day, int mounth, int year, int slotType)
        {
            if (Request.IsAjaxRequest())
            {
                string userEmail = _usersService.GetEmailIfLoginIn();
                DateTime bookTime = new DateTime(year, mounth, day, hour, 0, 0);
                SlotsType st = SlotsType.OneHour;

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

                if (_tss.AddUserToSlot(bookTime, st, userEmail))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
            return null;
        }

    }
}
