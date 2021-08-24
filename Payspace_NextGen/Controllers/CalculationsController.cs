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
    public class CalculationsController : Controller
    {
        private localdbEntities db = new localdbEntities();

        // GET: Calculations
        public async Task<ActionResult> Index()
        {
            return View(await db.Calculations.ToListAsync());
        }

        // GET: Calculations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // GET: Calculations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calculations/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                APIcalculationDetailsController detailsController = new APIcalculationDetailsController();
                await detailsController.PostProcesscalculation(calculation);
                return RedirectToAction("Details\\"+calculation.Id.ToString());
            }

            return View(calculation);
        }

        // GET: Calculations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Calculations/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Month,Year,NamePayment")] Calculation calculation)
        {
            if (ModelState.IsValid)
            {
                APIcalculationDetailsController aPIcalculationDetailsController = new APIcalculationDetailsController();
                foreach (var item in aPIcalculationDetailsController.GetcalculationDetails())
                {
                    item.DetailValue = item.Employe.Salary;
                    await aPIcalculationDetailsController.PutcalculationDetail(item.Id, item);

                }
                return RedirectToAction("Detail");
            }
            return View(calculation);
        }

        // GET: Calculations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return HttpNotFound();
            }
            return View(calculation);
        }

        // POST: Calculations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            db.Calculations.Remove(calculation);
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
