using Sheico.Core.ActionFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sheico.Controllers
{
    [SetCulture]
    public class BaseController : Controller
    {
        public ActionResult SetCulture(string id)
        {
            HttpCookie userCookie = Request.Cookies["Culture"];

            userCookie.Value = id;
            userCookie.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(userCookie);

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(userCookie.Value);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }

    public class BreadcrumbItem {
        public string LinkText { get; set; }
        public string LinkUrl { get; set; }
        public bool isActive { get; set; }
    }
}