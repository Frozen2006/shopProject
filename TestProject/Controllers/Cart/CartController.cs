using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Helpers;
using Interfaces;
using TestProject.Filters;
using TestProject.Models;

namespace TestProject.Controllers.Cart
{
    [CustomAuthrize(Roles = RolesType.User)]
    public class CartController : BaseController
    {
        //
        // GET: /Cart/
        private readonly ITimeSlotsService _slotsService;
        private readonly IOrderService _orderService;

        public CartController(ICategoryService ps, IUserService us, ICart cs, ITimeSlotsService slotsService, IOrderService os)
            : base(ps, us, cs)
        {
            _orderService = os;
            _slotsService = slotsService;
        }

        public ActionResult Index()
        {

            string userEmail = GetUserEmail();

            var model = new CartViewModel
                {
                    Products = _cartService.GetAllChart(userEmail),
                    TotalPrice = _cartService.GetTotalPrice(userEmail)
                };


            return View(model);
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult Remove(string productId)
        {
            if (productId == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (productId.Length > 0))
            {
                string userEmail = GetUserEmail();

                _cartService.DeleteProduct(userEmail, Convert.ToInt32(productId));

                return Content(Convert.ToString(_cartService.GetTotalPrice(userEmail)));
            }
            return View("Index");
        }

        //Ajax jquery responce method
        [HttpPost]
        public ActionResult SetNewValue(string productId, int count)
        {
            if (productId == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (productId.Length > 0))
            {
                string userEmail = GetUserEmail();

                double newPositionTotalPrice = _cartService.UpateCount(userEmail, Convert.ToInt32(productId), count);

                var outData =
                    new
                        {
                            positionPrice = Convert.ToString(newPositionTotalPrice),
                            totalPrice = Convert.ToString(_cartService.GetTotalPrice(userEmail))
                        };


                return Json(outData);
            }
            return View("Index");
        }




        public ActionResult ConfirmOrder()
        {
            string userEmail = GetUserEmail();
            var odm = new OrderDetailsModel {TimeSlot = _slotsService.GetUserSlots(userEmail)};

            return View(odm);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(int? Id, string Comments)
        {
            if (Id == null)
                return RedirectToAction("Index", "Home");

            string userEmail = GetUserEmail();
            int orderId = _orderService.CreateOrder(userEmail, (int)Id, Comments);

            return RedirectToAction("Pay", "Payment", new { orderId = orderId });
        }


        public class jsonUpdateAll
        {
            public string Id;
            public string positionPrice;
            public string totalPrice;
            public string count;
        }


        //Ajax jquery responce method
        [HttpPost]
        public ActionResult SetNewValueToAll(List<int> Id, List<int> Count)
        {
           if (Id == null)
                return null;
            Request.Headers["X-Requested-With"] = "XMLHttpRequest";
            if (Request.IsAjaxRequest() && (Id.Count > 0))
            {
                string userEmail = GetUserEmail();

                var outData = new List<jsonUpdateAll>();

                for (int q = 0; q < Id.Count; q++)
                {
                        double newPositionTotalPrice = _cartService.UpateCount(userEmail, Convert.ToInt32(Id[q]), Count[q]);
                        outData.Add(new jsonUpdateAll { Id = Convert.ToString(Id[q]), positionPrice = Convert.ToString(newPositionTotalPrice), count = Convert.ToString(Count[q])});
                }

                string totalPrice = Convert.ToString(_cartService.GetTotalPrice(userEmail));

                foreach (var jsonUpdateAll in outData)
                {
                    jsonUpdateAll.totalPrice = totalPrice;
                }
                return Json(outData);
            }
            return View("Index");
        }
    }
}
