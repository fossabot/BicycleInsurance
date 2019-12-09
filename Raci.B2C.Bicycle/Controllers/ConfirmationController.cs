using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.FormHandlers;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Utils;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Web.Controllers;
using Raci.B2C.Web.Mvc;

namespace Raci.B2C.Bicycle.Controllers
{
    public partial class ConfirmationController : PolicyController<BicycleQuote>
    {
        private readonly IConfirmationFormHandler _confirmantionFormHandler;

        public ConfirmationController(IConfirmationFormHandler confirmantionFormHandler, IContentProvider contentProvider) : base(contentProvider)
        {
            _confirmantionFormHandler = confirmantionFormHandler;
        }

        [NoCache]
        [HttpGet]
        [ValidatePolicy(RedirectIssuedPolicyToPayment = true)]
        public virtual async Task<ActionResult> Index()
        {
            BicycleQuote model = new BicycleQuote();

            PolicyDTO policy = await _confirmantionFormHandler.GetPolicy(this.GetPolicyId());

            model.GoogleDataLayer = await _confirmantionFormHandler.GetGoogleDataLayer(policy.Id);
            _confirmantionFormHandler.UpdateModelFromDto(policy, model);

            SetReferenceData(policy, model);

            ViewData.SetReferenceData(_confirmantionFormHandler.CreateReferenceData());
            return View(model);
        }

        private void SetReferenceData(PolicyDTO policy, BicycleQuote model)
        {
            ViewData.SetReferenceData(_confirmantionFormHandler.CreateReferenceData());

            model.Confirmation.Make = policy?.Detail?.Make;
            model.Confirmation.Model = policy?.Detail?.Model;
            model.Confirmation.Year = policy?.Detail?.Year;
            //
            // Todo: The startdate should be stored in the database.....
            //
            model.Confirmation.PolicyStartDate = DateUtil.CurrentDateTime.ToString("dd MMMM yyyy");

            model.Confirmation.Excess = $"{Convert.ToDecimal(policy?.Option?.Excess):C0}";
            model.Confirmation.Premium = $"{Convert.ToDecimal(policy?.Option?.AnnualPremium):C0}";
        }
    }
}