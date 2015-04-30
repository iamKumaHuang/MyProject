using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sheico.Controllers
{
    public class MapController : BaseController
    {
        // GET: Map
        public ActionResult Index(string address, string lat, string lng)
        {
            return View();
        }
    }
}