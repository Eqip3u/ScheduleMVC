using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace ScheduleMVC.Controllers
{
    //[System.Web.Mvc.Authorize]
    public class GroupController : Controller
    {

        private ScheduleContext db = new ScheduleContext();

        public ActionResult Index(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var group = db.ScheduleMainSet.Where(x => x.GroupSet.GroupName == id).ToList();

            ViewBag.Group = id;

            return View(group);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ScheduleMainSet scheduleMainSet = await db.ScheduleMainSet.FindAsync(id);

            if (scheduleMainSet == null)
                return HttpNotFound();

            ViewBag.Group = id;

            return PartialView(scheduleMainSet);
        }
    }
}
