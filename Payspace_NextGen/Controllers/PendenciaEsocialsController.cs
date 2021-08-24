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
    public class PendenciaEsocialsController : Controller
    {
        private localdbEntities db = new localdbEntities();

        // GET: PendenciaEsocials
        public async Task<ActionResult> Index()
        {
            var pendenciaEsocials = db.PendenciaEsocials.Include(p => p.Esocial_Layout);
            return View(await pendenciaEsocials.ToListAsync());
        }

        // GET: PendenciaEsocials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(id);
            if (pendenciaEsocial == null)
            {
                return HttpNotFound();
            }
            return View(pendenciaEsocial);
        }

        // GET: PendenciaEsocials/Create
        public ActionResult Create()
        {
            ViewBag.Layout = new SelectList(db.Esocial_Layout, "Id", "Layout");
            return View();
        }

        // POST: PendenciaEsocials/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Layout,DataHora,Status,Recibo,Mensage,Description")] PendenciaEsocial pendenciaEsocial)
        {
            if (ModelState.IsValid)
            {
                db.PendenciaEsocials.Add(pendenciaEsocial);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Layout = new SelectList(db.Esocial_Layout, "Id", "Layout", pendenciaEsocial.Layout);
            return View(pendenciaEsocial);
        }

        // GET: PendenciaEsocials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(id);
            if (pendenciaEsocial == null)
            {
                return HttpNotFound();
            }
            ViewBag.Layout = new SelectList(db.Esocial_Layout, "Id", "Layout", pendenciaEsocial.Layout);
            return View(pendenciaEsocial);
        }

        // POST: PendenciaEsocials/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Layout,DataHora,Status,Recibo,Mensage,Description")] PendenciaEsocial pendenciaEsocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pendenciaEsocial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Layout = new SelectList(db.Esocial_Layout, "Id", "Layout", pendenciaEsocial.Layout);
            return View(pendenciaEsocial);
        }

        // GET: PendenciaEsocials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(id);
            if (pendenciaEsocial == null)
            {
                return HttpNotFound();
            }
            return View(pendenciaEsocial);
        }

        // POST: PendenciaEsocials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(id);
            db.PendenciaEsocials.Remove(pendenciaEsocial);
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
