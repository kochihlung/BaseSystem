using Newtonsoft.Json;
using SysService.objs;
using SysService.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SysService.svc;

namespace BaseSystem.Controllers
{
    public class UserManageController : JsonNetController
    {
        // GET: UserManage
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult Error()
        {
            return PartialView("Error");
        }

        public ActionResult GetAdminToken()
        {
            try
            {
                return new JsonNetResult() { Data = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminToken"].ToString() };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }


        public ActionResult PublicFun()
        {
            try
            {
                return new JsonNetResult() { Data = new User_Rule().PublicFun() };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult SignIn(string UID, string PWD, Double lat = 0, Double lon = 0)
        {
            try
            {
                User_Rule Rule = new User_Rule();

                return new JsonNetResult() { Data = Rule.UserSignIn(UID, PWD, lat, lon) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult SignOut(string UID)
        {
            try
            {
                User_Rule Rule = new User_Rule();

                return new JsonNetResult() { Data = Rule.UserSignOut(UID) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult GetDatas()
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetDatas("S_USERINFO") };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetModlingSetup()
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetModling_UserManage() };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }


        public ActionResult CheckUserLocation(string Token)
        {
            try
            {
                User_Rule Rule = new User_Rule();

                return new JsonNetResult() { Data = Rule.CheckUserLocation(Token) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult CreateModlingDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                ls.Find(o => o.Name == "PWD").Value = ls.Find(o => o.Name == "PWD").Value.DESEncrypt();

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.CreateByDictionary("S_USERINFO", _dic);

                return new JsonNetResult() { Data = Result, ContentType = "Error" };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult UpdateModlingDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                ls.Find(o => o.Name == "PWD").Value = ls.Find(o => o.Name == "PWD").Value.DESEncrypt();

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.UpdateByDictionary("S_USERINFO", _dic);


                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult DeleteModlingDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                Rule.DeleteByDictionary("S_USERINFO", _dic);


                return new JsonNetResult() { Data = "刪除成功" };
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