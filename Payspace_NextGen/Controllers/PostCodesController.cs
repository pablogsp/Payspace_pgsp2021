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
    public class PostCodesController : Controller, IPostCodesController
    {
        private localdbEntities db = new localdbEntities();

        // GET: PostCodes
        public async Task<ActionResult> Index()
        {
            var postCodes = db.PostCodes.Include(p => p.Type);
            return View(await postCodes.ToListAsync());
        }

        // GET: PostCodes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCode postCode = await db.PostCodes.FindAsync(id);
            if (postCode == null)
            {
                return HttpNotFound();
            }
            return View(postCode);
        }

        // GET: PostCodes/Create
        public ActionResult Create()
        {
            ViewBag.TaxRate = new SelectList(db.Types, "TypeRate", "TypeRate");
            return View();
        }

        // POST: PostCodes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PostCodeName,TaxRate")] PostCode postCode)
        {
            if (ModelState.IsValid)
            {
                db.PostCodes.Add(postCode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TaxRate = new SelectList(db.Types, "TypeRate", "TypeRate", postCode.TaxRate);
            return View(postCode);
        }

        // GET: PostCodes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCode postCode = await db.PostCodes.FindAsync(id);
            if (postCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.TaxRate = new SelectList(db.Types, "TypeRate", "TypeRate", postCode.TaxRate);
            return View(postCode);
        }

        // POST: PostCodes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PostCodeName,TaxRate")] PostCode postCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postCode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TaxRate = new SelectList(db.Types, "TypeRate", "TypeRate", postCode.TaxRate);
            return View(postCode);
        }

        // GET: PostCodes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCode postCode = await db.PostCodes.FindAsync(id);
            if (postCode == null)
            {
                return HttpNotFound();
            }
            return View(postCode);
        }

        // POST: PostCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PostCode postCode = await db.PostCodes.FindAsync(id);
            db.PostCodes.Remove(postCode);
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
