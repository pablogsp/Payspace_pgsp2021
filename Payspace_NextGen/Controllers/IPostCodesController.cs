using System.Threading.Tasks;
using System.Web.Mvc;
using Payspace_NextGen.Models;

namespace Payspace_NextGen.Controllers
{
    public interface IPostCodesController
    {
        ActionResult Create();
        Task<ActionResult> Create([Bind(Include = "Id,PostCodeName,TaxRate")] PostCode postCode);
        Task<ActionResult> Delete(string id);
        Task<ActionResult> DeleteConfirmed(string id);
        Task<ActionResult> Details(string id);
        Task<ActionResult> Edit([Bind(Include = "Id,PostCodeName,TaxRate")] PostCode postCode);
        Task<ActionResult> Edit(string id);
        Task<ActionResult> Index();
    }
}