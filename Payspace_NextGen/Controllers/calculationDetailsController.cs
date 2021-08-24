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
    public class calculationDetailsController : Controller
    {
        private localdbEntities db = new localdbEntities();

        // GET: calculationDetails
        public async Task<ActionResult> Index()
        {
            var calculationDetails = db.calculationDetails.Include(c => c.Calculation).Include(c => c.Employe);
            return View(await calculationDetails.ToListAsync());
        }

        // GET: calculationDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return HttpNotFound();
            }
            return View(calculationDetail);
        }

        // GET: calculationDetails/Create
        public ActionResult Create()
        {
            ViewBag.IdCalculation = new SelectList(db.Calculations, "Id", "NamePayment");
            ViewBag.EmployeID = new SelectList(db.Employes, "CPF", "Name");
            return View();
        }

        // POST: calculationDetails/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeID,DetailName,DetailValue,IdCalculation")] calculationDetail calculationDetail)
        {
            if (ModelState.IsValid)
            {
                db.calculationDetails.Add(calculationDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdCalculation = new SelectList(db.Calculations, "Id", "NamePayment", calculationDetail.IdCalculation);
            ViewBag.EmployeID = new SelectList(db.Employes, "CPF", "Name", calculationDetail.EmployeID);
            return View(calculationDetail);
        }

        // GET: calculationDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCalculation = new SelectList(db.Calculations, "Id", "NamePayment", calculationDetail.IdCalculation);
            ViewBag.EmployeID = new SelectList(db.Employes, "CPF", "Name", calculationDetail.EmployeID);
            return View(calculationDetail);
        }

        // POST: calculationDetails/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeID,DetailName,DetailValue,IdCalculation")] calculationDetail calculationDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calculationDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCalculation = new SelectList(db.Calculations, "Id", "NamePayment", calculationDetail.IdCalculation);
            ViewBag.EmployeID = new SelectList(db.Employes, "CPF", "Name", calculationDetail.EmployeID);
            return View(calculationDetail);
        }

        // GET: calculationDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return HttpNotFound();
            }
            return View(calculationDetail);
        }

        // POST: calculationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            db.calculationDetails.Remove(calculationDetail);
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
