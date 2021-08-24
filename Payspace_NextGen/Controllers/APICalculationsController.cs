using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public class APICalculationsController : ApiController, IAPICalculationsController
    {
        private localdbEntities db = new localdbEntities();

        // GET: api/APICalculations
        public IQueryable<Calculation> GetCalculations()
        {
            return db.Calculations;
        }

        // GET: api/APICalculations/5
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> GetCalculation(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            return Ok(calculation);
        }

        // PUT: api/APICalculations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCalculation(int id, Calculation calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calculation.Id)
            {
                return BadRequest();
            }

            db.Entry(calculation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/APICalculations
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> PostCalculation(Calculation calculation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Calculations.Add(calculation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = calculation.Id }, calculation);
        }

        // DELETE: api/APICalculations/5
        [ResponseType(typeof(Calculation))]
        public async Task<IHttpActionResult> DeleteCalculation(int id)
        {
            Calculation calculation = await db.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            db.Calculations.Remove(calculation);
            await db.SaveChangesAsync();

            return Ok(calculation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalculationExists(int id)
        {
            return db.Calculations.Count(e => e.Id == id) > 0;
        }
    }
}