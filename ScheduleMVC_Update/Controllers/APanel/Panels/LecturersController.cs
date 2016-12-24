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
    public class LecturersController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Lecturers
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.LecturerSet.ToListAsync());
        }

        // GET: Lecturers/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LecturerSet lecturerSet = await db.LecturerSet.FindAsync(id);
            if (lecturerSet == null)
            {
                return HttpNotFound();
            }
            return View(lecturerSet);
        }

        // GET: Lecturers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lecturers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LecturerSet lecturerSet)
        {
            if (ModelState.IsValid)
            {
                db.LecturerSet.Add(lecturerSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lecturerSet);
        }

        // GET: Lecturers/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LecturerSet lecturerSet = await db.LecturerSet.FindAsync(id);
            if (lecturerSet == null)
            {
                return HttpNotFound();
            }
            return View(lecturerSet);
        }

        // POST: Lecturers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LecturerSet lecturerSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecturerSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lecturerSet);
        }

        // GET: Lecturers/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LecturerSet lecturerSet = await db.LecturerSet.FindAsync(id);
            if (lecturerSet == null)
            {
                return HttpNotFound();
            }
            return View(lecturerSet);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LecturerSet lecturerSet = await db.LecturerSet.FindAsync(id);
            db.LecturerSet.Remove(lecturerSet);
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
