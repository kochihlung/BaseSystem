using CustService.CustRule;
using SysService.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseSystem.Controllers
{
    public class V_WOHoldController : JsonNetController
    {
        // GET: V_WOHold
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetHoldControl(string SID)
        {
            try
            {
                Hold_Rule Rule = new Hold_Rule();

                return new JsonNetResult() { Data = Rule.GetHoldControlData(SID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult Hold(string SID, string Reson, string Remark, string UID)
        {
            try
            {
                Hold_Rule Rule = new Hold_Rule();

                return new JsonNetResult() { Data = Rule.HoldBySID(SID, Reson, Remark, UID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult UnHold(string SID, string UID)
        {
            try
            {
                Hold_Rule Rule = new Hold_Rule();

                return new JsonNetResult() { Data = Rule.UnHoldBySID(SID, UID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult UnHoldAll(string SID, string UID)
        {
            try
            {
                Hold_Rule Rule = new Hold_Rule();

                return new JsonNetResult() { Data = Rule.UnHoldAll(SID, UID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetHoldReson()
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetDataSourceItemByCode("WOHoldReson", "and M_HOLDTYPE='MD1Y6420002EA06'") };
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