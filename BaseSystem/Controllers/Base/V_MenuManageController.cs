using Newtonsoft.Json;
using SysService.objs;
using SysService.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BaseSystem.Controllers
{
    public class V_MenuManageController : JsonNetController
    {
        // GET: V_MenuManage
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetModlingSetup()
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var Types = Assembly.GetExecutingAssembly().ExportedTypes.Where(o => o.FullName.Contains("BaseSystem.Controllers."));
                List<string> Controllers = new List<string>();
                foreach (var obj in Types)
                {
                    Controllers.Add(obj.Name.Replace("Controller", ""));
                }

                return new JsonNetResult() { Data = Rule.GetModling_MenuManage(Server.MapPath("~/Scripts/EasyUI/themes"), Controllers) };
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

                return new JsonNetResult() { Data = Rule.GetDatas("S_MENU", "order by CODE,SORT") };
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

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.CreateByDictionary("S_MENU", _dic);

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

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.UpdateByDictionary("S_MENU", _dic);

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

                Rule.DeleteByDictionary("S_MENU", _dic);


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