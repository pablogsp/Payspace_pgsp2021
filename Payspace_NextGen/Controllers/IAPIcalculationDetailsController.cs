using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IAPIcalculationDetailsController
    {
        Task<IHttpActionResult> DeletecalculationDetail(int id);
        Task<IHttpActionResult> GetcalculationDetail(int id);
        IQueryable<calculationDetail> GetcalculationDetails();
        decimal GetcalculationResultRate(Employe employe);
        Task<IHttpActionResult> PostcalculationDetail(calculationDetail calculationDetail);
        Task<IHttpActionResult> PostProcesscalculation(Calculation calculation);
        Task<IHttpActionResult> PutcalculationDetail(int id, calculationDetail calculationDetail);
    }
}