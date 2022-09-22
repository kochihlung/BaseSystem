using CustService.CustObjs.view;
using SysService.objs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseSystem.Controllers
{
    public class V_CreateFormController : JsonNetController
    {
        // GET: V_CreateForm
        public ActionResult Index()
        {
            return PartialView();
        }


        public ActionResult Create()
        {
            try
            {
                //取得表單資料
                var FormInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<V_FormInfo>(Request.Form["FormData"]);
                //取得附加檔案
                List<V_FormFile> Files = new List<V_FormFile>();
                foreach (var item in Request.Files)
                {
                    var file = new V_FormFile();
                    file.FileBase = Request.Files[item.ToString()];
                    file.Path = Server.MapPath("~/FormFile/");
                    file.NAME = item.ToString();
                    FormInfo.Files.Add(file);
                }

                var Result = new CustService.CustRule.Form_Rule().CreateForm(FormInfo);

                return new JsonNetResult() { Data = Result };
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
                return new JsonNetResult() { Data = new CustService.CustRule.Form_Rule().GetForms() };
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