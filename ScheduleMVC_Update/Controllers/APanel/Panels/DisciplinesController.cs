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
    public class DisciplinesController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Disciplines
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.DisciplineSet.ToListAsync());
        }

        // GET: Disciplines/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSet disciplineSet = await db.DisciplineSet.FindAsync(id);
            if (disciplineSet == null)
            {
                return HttpNotFound();
            }
            return View(disciplineSet);
        }

        // GET: Disciplines/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disciplines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DisciplineId,TitleDiscipline")] DisciplineSet disciplineSet)
        {
            if (ModelState.IsValid)
            {
                db.DisciplineSet.Add(disciplineSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(disciplineSet);
        }

        [Authorize]
        // GET: Disciplines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSet disciplineSet = await db.DisciplineSet.FindAsync(id);
            if (disciplineSet == null)
            {
                return HttpNotFound();
            }
            return View(disciplineSet);
        }

        // POST: Disciplines/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DisciplineId,TitleDiscipline")] DisciplineSet disciplineSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplineSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(disciplineSet);
        }

        [Authorize]
        // GET: Disciplines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineSet disciplineSet = await db.DisciplineSet.FindAsync(id);
            if (disciplineSet == null)
            {
                return HttpNotFound();
            }
            return View(disciplineSet);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DisciplineSet disciplineSet = await db.DisciplineSet.FindAsync(id);
            db.DisciplineSet.Remove(disciplineSet);
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
