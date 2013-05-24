using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryImg(int id)
        {
            string FilePath = Server.MapPath("~/Content/images/categories/" + Convert.ToString(id) + ".jpg");

            if (System.IO.File.Exists(FilePath))
            {
                return File(FilePath, "image");
            }
            else
            {
                return File(Server.MapPath("~/Content/images/categories/NoImage.jpg"), "Image");
            }
            
        }

        public ActionResult ProductImg(int id)
        {
            string FilePath = Server.MapPath("~/Content/images/products/" + Convert.ToString(id) + ".jpg");

            if (System.IO.File.Exists(FilePath))
            {
                return File(FilePath, "image");
            }
            else
            {
                return File(Server.MapPath("~/Content/images/products/NoImage.jpg"), "Image");
            }

        }

    }
}
