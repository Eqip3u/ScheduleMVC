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

namespace ScheduleMVC_Update.Controllers.APanel.Panels
{
    public class AuditorysController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Auditorys
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.AuditorySet.ToListAsync());
        }

        // GET: Auditorys/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditorySet auditorySet = await db.AuditorySet.FindAsync(id);
            if (auditorySet == null)
            {
                return HttpNotFound();
            }
            return View(auditorySet);
        }

        // GET: Auditorys/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auditorys/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AuditoryId,AuditoryName")] AuditorySet auditorySet)
        {
            if (ModelState.IsValid)
            {
                db.AuditorySet.Add(auditorySet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(auditorySet);
        }

        // GET: Auditorys/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditorySet auditorySet = await db.AuditorySet.FindAsync(id);
            if (auditorySet == null)
            {
                return HttpNotFound();
            }
            return View(auditorySet);
        }

        // POST: Auditorys/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AuditoryId,AuditoryName")] AuditorySet auditorySet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditorySet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(auditorySet);
        }

        // GET: Auditorys/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditorySet auditorySet = await db.AuditorySet.FindAsync(id);
            if (auditorySet == null)
            {
                return HttpNotFound();
            }
            return View(auditorySet);
        }

        // POST: Auditorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AuditorySet auditorySet = await db.AuditorySet.FindAsync(id);
            db.AuditorySet.Remove(auditorySet);
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
