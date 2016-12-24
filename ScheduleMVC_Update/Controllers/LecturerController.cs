using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ScheduleMVC_Update.Controllers
{
    public class LecturerController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Lecturer
        public ActionResult Index(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var lecturer = db.ScheduleMainSet.Where(x => x.LecturerSet.LecturerFullName == id).ToList();

            ViewBag.Name = id;

            return View(lecturer);
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

        // GET: Lecturer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleMainSet scheduleMainSet = await db.ScheduleMainSet.FindAsync(id);
            if (scheduleMainSet == null)
            {
                return HttpNotFound();
            }

            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairNumber", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ScheduleId,Date,DisciplineDisciplineId,LecturerLecturerId,GroupGroupId,AuditoryAuditoryId,PairPairId,Annotation,DaysOfWeek")] ScheduleMainSet scheduleMainSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleMainSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
                });
            }
            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairNumber", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

    }
}