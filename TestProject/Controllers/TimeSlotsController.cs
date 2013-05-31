using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
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

        [Inject]
        public TimeSlotsController(TimeSlotsService tsinp)
        {
            _tss = tsinp;
        }


        public ActionResult Index()
        {
            TimeSlotsModel model = new TimeSlotsModel();

            model.Today = DateTime.Now;

            DateTime date = DateTime.Now;
            DateTime start = date;

            DateTime end = date;

            model.SlotsOneHour = _tss.GetSlots(start, end, SlotsType.OneHour);
            model.SlotsTwoHour = _tss.GetSlots(start, end, SlotsType.TwoHour);
            model.SlotsFourHour = _tss.GetSlots(start, end, SlotsType.FourHour);
            
            //colection with date times to Periods from 9.00 to 22.00 with step in 1 hour, setted to start date of the week
            model.startDay = new List<DateTime>();

            DateTime toList = new DateTime(start.Year, start.Month, start.Day, 9, 0, 0);

            for (int q = 0; q < 13; q++)
            {
                model.startDay.Add(toList);
                toList = toList.AddHours(1.0);
            }



                return View(model);
        }

    }
}
