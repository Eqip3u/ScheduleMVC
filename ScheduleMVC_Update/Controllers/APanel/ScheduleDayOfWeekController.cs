using ScheduleMVC_Update.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ScheduleMVC_Update.Controllers.APanel.Panels
{
    public class ScheduleDayOfWeekController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: ScheduleDayOfWeek
        [Authorize]
        public ActionResult Index(string id)
        {
            var dayPairs = db.ScheduleMainSet.Where(x => x.DaysOfWeek == id).OrderBy(x => x.GroupSet.GroupName).ToList();
            ViewBag.Day = id;
            return View(dayPairs);
        }

        // GET: ScheduleDayOfWeek/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ScheduleMainSet scheduleMainSet = await db.ScheduleMainSet.FindAsync(id);

            if (scheduleMainSet == null)
                return HttpNotFound();

            return View(scheduleMainSet);
        }

        [Authorize]
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

            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet.OrderBy(x => x.AuditoryName), "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet.OrderBy(x => x.TitleDiscipline), "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet.OrderBy(x => x.GroupName), "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet.OrderBy(x => x.LecturerFullName), "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet.OrderBy(x => x.PairNumber), "PairId", "PairNumber", scheduleMainSet.PairPairId);

            return View(scheduleMainSet);
        }

        // POST: ScheduleDayOfWeek/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    controller = "Schedule",
                    action = "Index",
                });
            }
            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairNumber", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        // GET: ScheduleDayOfWeek/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
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
            return View(scheduleMainSet);
        }

        // POST: ScheduleDayOfWeek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ScheduleMainSet scheduleMainSet = await db.ScheduleMainSet.FindAsync(id);
            db.ScheduleMainSet.Remove(scheduleMainSet);
            await db.SaveChangesAsync();
            return RedirectToRoute(new
            {
                controller = "Schedule",
                action = "Index",
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}