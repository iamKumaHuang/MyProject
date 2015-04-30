using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sheico.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            UrlHelper url = new UrlHelper(this.ControllerContext.RequestContext);
            //設定麵包屑
            ViewBag.BreadcrumbTitle = Resources.PageResource.MenuContact;
            List<BreadcrumbItem> breadcrumbs = new List<BreadcrumbItem>();
            breadcrumbs.Add(
                new BreadcrumbItem
                {
                    isActive = false,
                    LinkText = Resources.PageResource.MenuHome,
                    LinkUrl = url.Action("Index", "Home")
                }
            );            
            breadcrumbs.Add(
                new BreadcrumbItem
                {
                    isActive = true,
                    LinkText = Resources.PageResource.MenuContact,
                    LinkUrl = url.Action("Contact", "Home")
                }
            );
            ViewBag.BreadcrumbItem = breadcrumbs;

            return View();
        }

    }
}