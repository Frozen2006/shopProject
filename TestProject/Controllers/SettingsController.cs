using System.Web.Mvc;
using AutoMapper;
using iTechArt.Shop.Common.Enumerations;
using iTechArt.Shop.Common.Services;
using Ninject;
using iTechArt.Shop.Entities.PresentationModels;
using iTechArt.Shop.Web.Filters;
using iTechArt.Shop.Web.Models;

namespace iTechArt.Shop.Web.Controllers 
{
    [CustomAuthrize(Roles = RolesType.User)]
    public class SettingsController : BaseController
    {
        [Inject]
        public SettingsController(ICategoryService categoryService, IUserService userService, ICartService cartService)
            : base(categoryService, userService, cartService) { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Delivery()
        {
            var model = new ChangeDeliveryAddressModel();

            string userEmail = GetUserEmail();

            UserDetails userDetails = UserService.GetUserDetails(userEmail);

            model = Mapper.Map(userDetails, model);

            return View(model);
        }


        [HttpPost]
        public ActionResult Delivery(ChangeDeliveryAddressModel model)
        {
            if (ModelState.IsValid)
            {
               string userEmail = GetUserEmail();
               UserService.ChangeDeliveryData(userEmail, model);
               model.IsSuccess = true;
               return View(model);
            }
            ModelState.AddModelError("", "Input data is bad");
            model.IsSuccess = false;
            ViewBag.Data = "BAD";
            return View(model);
        }


        [HttpPost]
        public ActionResult Account(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string userEmail = GetUserEmail();

                if (UserService.CheckUser(userEmail, model.OldPassword))
                {
                    UserService.ChangePassword(userEmail, model.OldPassword, model.Password);
                    return View(new ChangePasswordModel() { IsSuccess = true});
                }
                ModelState.AddModelError("", "Wrong old password.");
            }
            ModelState.AddModelError("", "Input data is bad");
            model.IsSuccess = false;
            ViewBag.Data = "BAD";
            return View(model);
        }

    }
}
