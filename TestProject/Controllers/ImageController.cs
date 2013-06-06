using System;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            return null;
        }

        public ActionResult CategoryImg(int id)
        {
            string filePath = Server.MapPath("~/Content/images/categories/" + Convert.ToString(id) + ".jpg");

            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "image");
            }
            return File(Server.MapPath("~/Content/images/categories/NoImage.jpg"), "Image");
        }

        public ActionResult ProductImg(int id)
        {
            string filePath = Server.MapPath("~/Content/images/products/" + Convert.ToString(id) + ".jpg");

            if (System.IO.File.Exists(filePath))
            {
                return File(filePath, "image");
            }
            return File(Server.MapPath("~/Content/images/products/NoImage.jpg"), "Image");
        }

    }
}
