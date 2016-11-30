using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ScheduleMVC_Update.Controllers
{
    public class LecturerController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Lecturer
        public ActionResult Index(string lec)
        {
            if (lec == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var lecturer = db.ScheduleMainSet.Where(x => x.LecturerSet.LecturerFullName == lec).OrderByDescending(x => x.Date).ToList();
            return View(lecturer);
        }
    }
}