using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SysService.objs.tb;
using SysService.Rule;
using CustService.CustObjs.tb;
using CustService.CustRule;

namespace BaseSystem.Controllers
{
    public class V_WOManageController : JsonNetController
    {
        // GET: V_WOManage
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetWOWIP(string SID)
        {
            try
            {
                WO_Rule Rule = new WO_Rule();

                return new JsonNetResult() { Data = Rule.GetWIPGrid(SID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult SaveWO(W_WO InData)
        {
            try
            {
                WO_Rule Rule = new WO_Rule();

                return new JsonNetResult() { Data = Rule.SaveWOInfo(InData) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult LoadWoList(string WO,string Status,string Prod)
        {
            try
            {
                WO_Rule Rule = new WO_Rule();

                return new JsonNetResult() { Data = Rule.GetWoList(WO,Status,Prod) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult GetWOHistory(string SID)
        {
            try
            {
                WO_Rule Rule = new WO_Rule();

                return new JsonNetResult() { Data = Rule.GetWOHistory(SID) };
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