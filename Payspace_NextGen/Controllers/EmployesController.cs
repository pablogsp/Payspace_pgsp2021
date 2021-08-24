using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class EmployesController : Controller
    {
        private localdbEntities db = new localdbEntities();

        // GET: Employes
        public ActionResult Index()
        {
            var employes = db.Employes.Include(e => e.PostCode);
            return View(employes.ToList());
        }

        // GET: Employes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employes.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // GET: Employes/Create
        public ActionResult Create()
        {
            ViewBag.PostCodeId = new SelectList(db.PostCodes, "Id", "PostCodeName");
            return View();
        }

        // POST: Employes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,SocialName,CPF,PostCodeId,Salary")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Employes.Add(employe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostCodeId = new SelectList(db.PostCodes, "Id", "PostCodeName", employe.PostCodeId);
            return View(employe);
        }

        // GET: Employes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employes.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostCodeId = new SelectList(db.PostCodes, "Id", "PostCodeName", employe.PostCodeId);
            return View(employe);
        }

        // POST: Employes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,SocialName,CPF,PostCodeId,Salary")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostCodeId = new SelectList(db.PostCodes, "Id", "PostCodeName", employe.PostCodeId);
            return View(employe);
        }

        // GET: Employes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employes.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employe employe = db.Employes.Find(id);
            db.Employes.Remove(employe);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {                
            }
            
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
