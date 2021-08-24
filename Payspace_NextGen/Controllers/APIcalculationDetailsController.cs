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
    public class APIcalculationDetailsController : ApiController, IAPIcalculationDetailsController
    {
        private localdbEntities db = new localdbEntities();

        // GET: api/APIcalculationDetails
        public IQueryable<calculationDetail> GetcalculationDetails()
        {
            return db.calculationDetails;
        }

        // GET: api/APIcalculationDetails/5
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> GetcalculationDetail(int id)
        {
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return NotFound();
            }

            return Ok(calculationDetail);
        }

        // PUT: api/APIcalculationDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutcalculationDetail(int id, calculationDetail calculationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calculationDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(calculationDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!calculationDetailExists(id))
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

        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> PostProcesscalculation(Calculation calculation)
        {
            calculation.DateGenerate = DateTime.Now;
            APIEmployesController employesController = new APIEmployesController();
            APIPendenciaEsocialsController aPIPendenciaEsocials = new APIPendenciaEsocialsController();
            foreach (var item in employesController.GetEmployes())
            {
                decimal salary = item.Salary;
                decimal impost = GetcalculationResultRate(item);
                // Include value salary on the calculation detail
                await PostcalculationDetail(new calculationDetail()
                {
                    Calculation = calculation,
                    EmployeID = item.CPF,
                    DetailName = "Salary at month (" + calculation.Month.ToString() + "/" + calculation.Year.ToString() + ")",
                    DetailValue = salary,                                        
                });

                // Include value TaxRate on the calculation detail, consult the table and calculation of tax.
                await PostcalculationDetail(new calculationDetail()
                {
                    Calculation = calculation,
                    EmployeID = item.CPF,
                    DetailName = "Impost at month "+ impost.ToString() + "% (" + calculation.Month.ToString() + "/" + calculation.Year.ToString()+")",
                    DetailValue = salary * (impost/100)
                });

                //Include the liquid salary of month
                // Include value TaxRate on the calculation detail, consult the table and calculation of tax.
                await PostcalculationDetail(new calculationDetail()
                {
                    Calculation = calculation,
                    EmployeID = item.CPF,
                    DetailName = "payment salary(" + calculation.Month.ToString() + "/" + calculation.Year.ToString() + ")",
                    DetailValue = salary - (salary * (impost / 100))
                });
                
                await aPIPendenciaEsocials.Post(new PendenciaEsocial()
                {
                    DataHora = DateTime.Now,
                    Description = "Folha" + calculation.Month.ToString() + "/" + calculation.Year.ToString(),
                    Id = aPIPendenciaEsocials.GetAPIPendenciaEsocials().Count()+1,
                    Layout = "S-1200",
                    Mensage="Remuneração mensal, CPF:"+item.CPF+".",
                    Recibo="",
                    Status=0
                });
            }
            return CreatedAtRoute("DefaultApi", new { },new object());
        }

        // POST: api/apicalcrates
        [ResponseType(typeof(calculationDetail))]
        public decimal  GetcalculationResultRate(Employe employe)
        {
            decimal valuenow = 0;
            foreach (RateValue item in (from p in employe.PostCode.Type.RateValues
                                         where p.From <= employe.Salary && p.To >= employe.Salary
                                         select p))
            {
                valuenow = item.Rate;
            }
            return valuenow;
        }


        // POST: api/APIcalculationDetails
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> PostcalculationDetail(calculationDetail calculationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.calculationDetails.Add(calculationDetail);
            await db.SaveChangesAsync();



            return CreatedAtRoute("DefaultApi", new { id = calculationDetail.Id }, calculationDetail);
        }

        // DELETE: api/APIcalculationDetails/5
        [ResponseType(typeof(calculationDetail))]
        public async Task<IHttpActionResult> DeletecalculationDetail(int id)
        {
            calculationDetail calculationDetail = await db.calculationDetails.FindAsync(id);
            if (calculationDetail == null)
            {
                return NotFound();
            }

            db.calculationDetails.Remove(calculationDetail);
            await db.SaveChangesAsync();

            return Ok(calculationDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool calculationDetailExists(int id)
        {
            return db.calculationDetails.Count(e => e.Id == id) > 0;
        }
    }
}