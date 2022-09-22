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
    public class V_WIPActionController : JsonNetController
    {
        // GET: V_WIPAction
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetWIPInfoByCode(string code)
        {
            try
            {
                WIP_Rule Rule = new WIP_Rule();

                return new JsonNetResult() { Data = Rule.GetWIPInfoByCode(code) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult WIPMoveOut(Frm_WIPMove data)
        {
            try
            {
                WIP_Rule Rule = new WIP_Rule();

                return new JsonNetResult() { Data = Rule.WIP_MoveOut(data) };
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