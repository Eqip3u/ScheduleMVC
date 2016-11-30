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

        // Автодополнение дисциплин
        public ActionResult AutocompleteSearchDiscipline(string term)
        {
            var models = db.ScheduleMainSet.Where(a => a.DisciplineSet.TitleDiscipline.Contains(term))
                            .Select(a => new { value = a.DisciplineSet.TitleDiscipline })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        // GET: Schedule
        public async Task<ActionResult> Index()
        {
            var scheduleMainSet = db.ScheduleMainSet.Include(s => s.AuditorySet).Include(s => s.DisciplineSet).Include(s => s.GroupSet).Include(s => s.LecturerSet).Include(s => s.PairSet);
            return View(await scheduleMainSet.ToListAsync());
        }

        // GET: Schedule/Details/5
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
        public ActionResult Create()
        {
            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName");
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline");
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName");
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName");
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairStart");
            return View();
        }

        // POST: Schedule/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduleMainSet scheduleMainSet)
        {
            if (ModelState.IsValid)
            {
                if (String.IsNullOrWhiteSpace(scheduleMainSet.PairSet.PairStart) || String.IsNullOrWhiteSpace(scheduleMainSet.PairSet.PairEnd))
                {
                    switch (scheduleMainSet.PairSet.PairNumber)
                    {
                        case "1":
                            scheduleMainSet.PairSet.PairStart = "9:00";
                            scheduleMainSet.PairSet.PairEnd = "10:35";
                            break;

                        case "2":
                            scheduleMainSet.PairSet.PairStart = "10:45";
                            scheduleMainSet.PairSet.PairEnd = "12:20";
                            break;

                        case "3":
                            scheduleMainSet.PairSet.PairStart = "13:00";
                            scheduleMainSet.PairSet.PairEnd = "14:35";
                            break;

                        case "4":
                            scheduleMainSet.PairSet.PairStart = "14:45";
                            scheduleMainSet.PairSet.PairEnd = "16:20";
                            break;

                        case "5":
                            scheduleMainSet.PairSet.PairStart = "16:30";
                            scheduleMainSet.PairSet.PairEnd = "18:05";
                            break;

                        default:
                            break;
                    }
                }
                db.ScheduleMainSet.Add(scheduleMainSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuditoryAuditoryId = new SelectList(db.AuditorySet, "AuditoryId", "AuditoryName", scheduleMainSet.AuditoryAuditoryId);
            ViewBag.DisciplineDisciplineId = new SelectList(db.DisciplineSet, "DisciplineId", "TitleDiscipline", scheduleMainSet.DisciplineDisciplineId);
            ViewBag.GroupGroupId = new SelectList(db.GroupSet, "GroupId", "GroupName", scheduleMainSet.GroupGroupId);
            ViewBag.LecturerLecturerId = new SelectList(db.LecturerSet, "LecturerId", "LecturerFullName", scheduleMainSet.LecturerLecturerId);
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairStart", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        // GET: Schedule/Edit/5
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
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairStart", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        // POST: Schedule/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ScheduleMainSet scheduleMainSet)
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
            ViewBag.PairPairId = new SelectList(db.PairSet, "PairId", "PairStart", scheduleMainSet.PairPairId);
            return View(scheduleMainSet);
        }

        // GET: Schedule/Delete/5
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
