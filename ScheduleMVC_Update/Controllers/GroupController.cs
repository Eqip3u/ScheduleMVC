using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ScheduleMVC.Controllers
{
    //[System.Web.Mvc.Authorize]
    public class GroupController : Controller
    {

        private ScheduleContext db = new ScheduleContext();

        public ActionResult Index(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var group = db.ScheduleMainSet.Where(x => x.GroupSet.GroupName == id.ToString()).OrderByDescending(x => x.Date);
            return View(group.ToList());
        }
    }
}
