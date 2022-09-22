using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustService.CustRule;

namespace BaseSystem.Controllers
{
    public class M_AttFunController : JsonNetController
    {

        public ActionResult GetAttLog(string UID)
        {
            try
            {
                return new JsonNetResult() { Data = new AttRule().GetAttRecLog(UID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }


        public ActionResult AttSign(string Token)
        {
            try
            {
                return new JsonNetResult() { Data = new AttRule().SignAttRecLog(Token) };
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