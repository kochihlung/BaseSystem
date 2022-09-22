using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace BaseSystem.Controllers
{
    public class V_QueryFormController : JsonNetController
    {
        // GET: V_QueryForm
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetStep()
        {
            try
            {
                return new JsonNetResult() { Data = new SysService.Rule.Modling_Rule().GetComboboxData("M_OPER") };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }
        public ActionResult GetFormSet()
        {
            try
            {
                return new JsonNetResult() { Data = new SysService.Rule.Modling_Rule().GetComboboxData("M_FORMSET") };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }
        public ActionResult GetFormDtl(string SID)
        {
            try
            {
                return new JsonNetResult() { Data = new SysService.Rule.Modling_Rule().GetComboboxData("M_FORMSETDTL", string.Format("and M_FORMSET_ID='{0}'", SID)) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }
        public ActionResult GetFormData(string SID)
        {
            try
            {
                return new JsonNetResult() { Data = new CustService.CustRule.Form_Rule().GetFormDataBySID(SID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetForms()
        {
            try
            {
                return new JsonNetResult() { Data = new CustService.CustRule.Form_Rule().GetSignFormGroupByFormSet() };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }
        public ActionResult QueryForm()
        {
            try
            {
                var aaa = Request.Form.Get("Items");
                var Items = JsonConvert.DeserializeObject<List<CustService.CustObjs.view.V_FormItem>>(Request.Form.Get("Items"));
                return new JsonNetResult() { Data = new CustService.CustRule.Form_Rule().QueryForms(Request.Form, Items) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

    }
}