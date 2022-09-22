using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SysService.objs;
using SysService.Rule;
using SysService.svc;


namespace BaseSystem.Controllers
{
    public class V_ModlingSetupMDController : JsonNetController
    {
      

        // GET: V_Role
        public ActionResult Index()
        {
            Session.Add("ActionTable", Request["ModlingData[MD]"]);
            return PartialView();
        }

        /// <summary>
        /// 取得MainDtl的結構
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFunBase()
        {
            try
            {
                //生成MainDtl物件(直接調用並填入主表名，主表及明細表需依規範命名)
                var R = new SysService.objs.View.B_MainDtl(Session["ActionTable"].ToString());//M_SINGOWNER----M_SINGOWNERDTL
                R.Text = "權限群組";
                //調整欄位屬性

                new Modling_Rule().OverrideModlingSetup(R);

                return new JsonNetResult() { Data = R };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// 取得主表資料
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult GetMainDatas()
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                return new JsonNetResult() { Data = Rule.GetDatas(Session["ActionTable"].ToString()) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 新增主表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult CreateMainDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.CreateByDictionary(Session["ActionTable"].ToString(), _dic, false);

                return new JsonNetResult() { Data = Result, ContentType = "Error" };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 修改主表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult UpdateMainDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.UpdateByDictionary(Session["ActionTable"].ToString(), _dic);


                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 刪除主表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult DeleteMainDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                Rule.DeleteMainDtlData(Session["ActionTable"].ToString(), _dic["SID"]);

                return new JsonNetResult() { Data = "刪除成功" };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 取得明細表資料
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ActionResult GetDtlDatas(string SID)
        {
            try
            {
                return new JsonNetResult() { Data = new Modling_Rule().GetDatas(Session["ActionTable"].ToString() + string.Format("DTL where {0}='{1}' ", Session["ActionTable"].ToString() + "_ID", SID)) };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }


        /// <summary>
        /// 新增明細表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult CreateDtlDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));
                _dic[Session["ActionTable"].ToString() + "_ID"] = _dic["MainSID"];
                _dic.Remove("MainSID");

                var Result = Rule.CreateByDictionary(Session["ActionTable"].ToString() + "DTL", _dic, false);

                return new JsonNetResult() { Data = Result, ContentType = "Error" };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 修改明細表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult UpdateDtlDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));
                _dic[Session["ActionTable"].ToString() + "_ID"] = _dic["MainSID"];
                _dic.Remove("MainSID");

                var Result = Rule.UpdateByDictionary(Session["ActionTable"].ToString() + "DTL", _dic);


                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        /// <summary>
        /// 刪除明細表資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult DeleteDtlDatas(string data)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                Rule.DeleteByDictionary(Session["ActionTable"].ToString() + "DTL", _dic);


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