using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using SysService.Rule;

namespace BaseSystem.Controllers
{
    public class PublicController : JsonNetController
    {
        // GET: Public
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Error(string id)
        {
            ViewBag.msg = id;
            return PartialView("Error");
        }
        public ActionResult Msg(string Msg)
        {
            ViewBag.msg = Msg;
            return PartialView("Msg");
        }

        public ActionResult GetDataSource(string Code)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetDataSourceItemByCode(Code) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetDataSourceBySID(string SID)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetDataSourceItem(SID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetUserInfo(string Token)
        {
            try
            {
                User_Rule Rule = new User_Rule();

                return new JsonNetResult() { Data = Rule.GetUserInfoByToken(Token) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetControlles()
        {
            try
            {
                var Types = Assembly.GetExecutingAssembly().ExportedTypes.Where(o=>o.FullName.Contains("BaseSystem.Controllers."));
                List<string> Controllers = new List<string>();

                foreach (var obj in Types)
                {
                    Controllers.Add(obj.Name.Replace("Controller", ""));
                }

                

                //"V_MDSETUPController"


                return new JsonNetResult() { Data = { } };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult Test()
        {
            return PartialView("Test");
        }

    }
}