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
    public class PairsController : Controller
    {
        private ScheduleContext db = new ScheduleContext();

        // GET: Pairs
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.PairSet.ToListAsync());
        }

        // GET: Pairs/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PairSet pairSet = await db.PairSet.FindAsync(id);
            if (pairSet == null)
            {
                return HttpNotFound();
            }
            return View(pairSet);
        }

        // GET: Pairs/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pairs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PairId,PairNumber,PairStart,PairEnd")] PairSet pairSet)
        {
            if (ModelState.IsValid)
            {
                db.PairSet.Add(pairSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pairSet);
        }

        // GET: Pairs/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PairSet pairSet = await db.PairSet.FindAsync(id);
            if (pairSet == null)
            {
                return HttpNotFound();
            }
            return View(pairSet);
        }

        // POST: Pairs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PairId,PairNumber,PairStart,PairEnd")] PairSet pairSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pairSet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pairSet);
        }

        // GET: Pairs/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PairSet pairSet = await db.PairSet.FindAsync(id);
            if (pairSet == null)
            {
                return HttpNotFound();
            }
            return View(pairSet);
        }

        // POST: Pairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PairSet pairSet = await db.PairSet.FindAsync(id);
            db.PairSet.Remove(pairSet);
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
