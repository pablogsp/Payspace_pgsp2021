using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IAPICalculationsController
    {
        Task<IHttpActionResult> DeleteCalculation(int id);
        Task<IHttpActionResult> GetCalculation(int id);
        IQueryable<Calculation> GetCalculations();
        Task<IHttpActionResult> PostCalculation(Calculation calculation);
        Task<IHttpActionResult> PutCalculation(int id, Calculation calculation);
    }
}