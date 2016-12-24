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
    public class GroupsController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Groups
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.GroupSet.ToListAsync());
        }

        // GET: Groups/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSet groupSet = await db.GroupSet.FindAsync(id);
            if (groupSet == null)
            {
                return HttpNotFound();
            }
            return View(groupSet);
        }

        // GET: Groups/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GroupId,GroupName,DepartmentName,MonitorOfTheTeam,MonitorTel,MonitorEmail")] GroupSet groupSet)
        {
            if (ModelState.IsValid)
            {
                db.GroupSet.Add(groupSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(groupSet);
        }

        // GET: Groups/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSet groupSet = await db.GroupSet.FindAsync(id);
            if (groupSet == null)
            {
                return HttpNotFound();
            }
            return View(groupSet);
        }

        // POST: Groups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GroupId,GroupName,DepartmentName,MonitorOfTheTeam,MonitorTel,MonitorEmail")] GroupSet groupSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(groupSet);
        }

        // GET: Groups/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupSet groupSet = await db.GroupSet.FindAsync(id);
            if (groupSet == null)
            {
                return HttpNotFound();
            }
            return View(groupSet);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GroupSet groupSet = await db.GroupSet.FindAsync(id);
            db.GroupSet.Remove(groupSet);
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
