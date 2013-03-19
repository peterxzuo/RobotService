using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalR.Models;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public JsonResult WalkRobot(string robotKey, string degree, string speed, string distance, string pan, string tilt)
        {
            // Walk Robot to certain distance
            var context = GlobalHost.ConnectionManager.GetHubContext<RobotSignalHub>();

            RobotControlSignal robotSignal = new RobotControlSignal()
            {
                Distance = double.Parse(distance),
                Speed = double.Parse(speed),
                Degree = double.Parse(degree),
                Pan = double.Parse(pan),
                Tilt = double.Parse(tilt)
            };

            RobotSignalHub.Walk(robotKey, robotSignal, context);

            return Json(robotSignal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Robot()
        {
            ViewBag.Message = "Your Robot Page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
