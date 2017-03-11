using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrumiTech.RaspberryPi.BabyMonitor.Controllers
{
    public class MonitorController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }
    }
}
