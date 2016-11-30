using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ScheduleMVC.Controllers
{
    public class HomeController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        public ActionResult Index()
        {
            var group = db.ScheduleMainSet.OrderByDescending(x => x.LecturerSet.LecturerFullName);
            return View(group.ToList());
        }
    }
}