using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScheduleMVC_Update.Models;

namespace ScheduleMVC_Update.Controllers
{
    public class ScheduleController : Controller
    {
        private ScheduleContext db = new ScheduleContext();


        // Автодополнение преподавателей
        public ActionResult AutocompleteSearchLecturer(string term)
        {
            var models = db.ScheduleMainSet.Where(a => a.LecturerSet.LecturerFullName.Contains(term))
                            .Select(a => new { value = a.LecturerSet.LecturerFullName })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        // Автодополнение групп
        public ActionResult AutocompleteSearchGroup(string term)
        {
            var models = db.ScheduleMainSet.Where(a => a.GroupSet.GroupName.Contains(term))
                            .Select(a => new { value = a.GroupSet.GroupName })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        // GET: Schedule
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var date = DateTime.Now.Date;
            var scheduleMainSet = db.ScheduleMainSet
                                        .Include(s => s.AuditorySet)
                                        .Include(s => s.DisciplineSet)
                                        .Include(s => s.GroupSet)
                                        .Include(s => s.LecturerSet)
                                        .Include(s => s.PairSet)
                                        .OrderBy(x => x.GroupSet.GroupName)
                                        .OrderBy(x => x.DaysOfWeek)
                                        .ToListAsync();
            
            return View(await scheduleMainSet);
        }

        // GET: Schedule/Details/5
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

        // GET: Schedule/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet.OrderBy(x => x.AuditoryName), "AuditoryId", "AuditoryName");
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet.OrderBy(x => x.TitleDiscipline), "DisciplineId", "TitleDiscipline");
            ViewBag.GroupGroupId = new SelectList(db.GroupSet.OrderBy(x => x.GroupName), "GroupId", "GroupName");
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet.OrderBy(x => x.LecturerFullName), "LecturerId", "LecturerFullName");
            ViewBag.PairPairId = new SelectList(db.PairSet.OrderBy(x => x.PairNumber), "PairId", "PairNumber");
            return View();
        }

        // POST: Schedule/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ScheduleId,Date,DisciplineDisciplineId,LecturerLecturerId,GroupGroupId,AuditoryAuditoryId,PairPairId,Annotation,DaysOfWeek")] ScheduleMainSet scheduleMainSet)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleMainSet.Add(scheduleMainSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet.OrderBy(x => x.PairNumber), "PairId", "PairNumber", scheduleMainSet.PairPairId);
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

        // POST: Schedule/Edit/5
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
                return RedirectToAction("Index");
            }
            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairNumber", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        // GET: Schedule/Delete/5
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

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ScheduleMainSet scheduleMainSet = await db.ScheduleMainSet.FindAsync(id);
            db.ScheduleMainSet.Remove(scheduleMainSet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
