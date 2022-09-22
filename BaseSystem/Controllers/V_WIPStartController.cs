using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BaseSystem.objs;
using SysService.objs;
using SysService.Rule;

using Newtonsoft.Json;
using CustService.CustRule;

namespace BaseSystem.Controllers
{
    public class V_WIPStartController : JsonNetController
    {
        // GET: V_WIPStart
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult CreateWIP(string UID,string WO_SID)
        {
            try
            {
                WIP_Rule Rule = new WIP_Rule();

                return new JsonNetResult() { Data = Rule.CreateWIP(UID,WO_SID) };
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