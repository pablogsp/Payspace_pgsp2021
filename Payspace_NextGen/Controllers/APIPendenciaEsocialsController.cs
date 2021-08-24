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
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    /*
    A classe WebApiConfig pode requerer alterações adicionais para adicionar uma rota para esse controlador. Misture essas declarações no método Register da classe WebApiConfig conforme aplicável. Note que URLs OData diferenciam maiúsculas e minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Payspace_NextGen.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PendenciaEsocial>("APIPendenciaEsocials");
    builder.EntitySet<Esocial_Layout>("Esocial_Layout"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class APIPendenciaEsocialsController : ODataController
    {
        private localdbEntities db = new localdbEntities();

        // GET: odata/APIPendenciaEsocials
        [EnableQuery]
        public IQueryable<PendenciaEsocial> GetAPIPendenciaEsocials()
        {
            return db.PendenciaEsocials;
        }

        // GET: odata/APIPendenciaEsocials(5)
        [EnableQuery]
        public SingleResult<PendenciaEsocial> GetPendenciaEsocial([FromODataUri] int key)
        {
            return SingleResult.Create(db.PendenciaEsocials.Where(pendenciaEsocial => pendenciaEsocial.Id == key));
        }

        // PUT: odata/APIPendenciaEsocials(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<PendenciaEsocial> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(key);
            if (pendenciaEsocial == null)
            {
                return NotFound();
            }

            patch.Put(pendenciaEsocial);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendenciaEsocialExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pendenciaEsocial);
        }

        // POST: odata/APIPendenciaEsocials
        public async Task<IHttpActionResult> Post(PendenciaEsocial pendenciaEsocial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PendenciaEsocials.Add(pendenciaEsocial);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PendenciaEsocialExists(pendenciaEsocial.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(pendenciaEsocial);
        }

        // PATCH: odata/APIPendenciaEsocials(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<PendenciaEsocial> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(key);
            if (pendenciaEsocial == null)
            {
                return NotFound();
            }

            patch.Patch(pendenciaEsocial);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendenciaEsocialExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(pendenciaEsocial);
        }

        // DELETE: odata/APIPendenciaEsocials(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            PendenciaEsocial pendenciaEsocial = await db.PendenciaEsocials.FindAsync(key);
            if (pendenciaEsocial == null)
            {
                return NotFound();
            }

            db.PendenciaEsocials.Remove(pendenciaEsocial);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/APIPendenciaEsocials(5)/Esocial_Layout
        [EnableQuery]
        public SingleResult<Esocial_Layout> GetEsocial_Layout([FromODataUri] int key)
        {
            return SingleResult.Create(db.PendenciaEsocials.Where(m => m.Id == key).Select(m => m.Esocial_Layout));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PendenciaEsocialExists(int key)
        {
            return db.PendenciaEsocials.Count(e => e.Id == key) > 0;
        }
    }
}
