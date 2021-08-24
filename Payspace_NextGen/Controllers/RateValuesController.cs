using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class RateValuesController : Controller
    {
        private localdbEntities db = new localdbEntities();

        // GET: RateValues
        public async Task<ActionResult> Index()
        {
            var rateValues = db.RateValues.Include(r => r.Type);
            return View(await rateValues.ToListAsync());
        }

        // GET: RateValues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateValue rateValue = await db.RateValues.FindAsync(id);
            if (rateValue == null)
            {
                return HttpNotFound();
            }
            return View(rateValue);
        }

        // GET: RateValues/Create
        public ActionResult Create()
        {
            ViewBag.TypeRate = new SelectList(db.Types, "TypeRate", "TypeRate");
            return View();
        }

        // POST: RateValues/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TypeRate,From,To,Rate")] RateValue rateValue)
        {
            if (ModelState.IsValid)
            {
                db.RateValues.Add(rateValue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeRate = new SelectList(db.Types, "TypeRate", "TypeRate", rateValue.TypeRate);
            return View(rateValue);
        }

        // GET: RateValues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateValue rateValue = await db.RateValues.FindAsync(id);
            if (rateValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeRate = new SelectList(db.Types, "TypeRate", "TypeRate", rateValue.TypeRate);
            return View(rateValue);
        }

        // POST: RateValues/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TypeRate,From,To,Rate")] RateValue rateValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rateValue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeRate = new SelectList(db.Types, "TypeRate", "TypeRate", rateValue.TypeRate);
            return View(rateValue);
        }

        // GET: RateValues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateValue rateValue = await db.RateValues.FindAsync(id);
            if (rateValue == null)
            {
                return HttpNotFound();
            }
            return View(rateValue);
        }

        // POST: RateValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RateValue rateValue = await db.RateValues.FindAsync(id);
            db.RateValues.Remove(rateValue);
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
