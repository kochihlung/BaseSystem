using SysService.objs;
using SysService.Rule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseSystem.Controllers
{
    public class V_MDSETUPController : JsonNetController
    {
        // GET: V_MDSETUP
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult SaveColDtl(string rows, string tb, string col,string TableName)
        {
            try
            {
                var InData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MDSetup>>(rows);
                Modling_Rule Rule = new Modling_Rule();

                var Result = Rule.SaveColDtl(InData, tb, col, TableName);

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetColDtl(string tb, string col,string TableName)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var Result = Rule.GetColDtl(tb, col, TableName);

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }


        public ActionResult GetMDList(string Name)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var Result = Rule.GetMDList(Name);

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetMDTables(string Name)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var Result = Rule.GetMDTables();

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }

        public ActionResult GetTableColumn(string TableName, string SETTYPE)
        {
            try
            {
                Modling_Rule Rule = new Modling_Rule();
                var Result = Rule.GetTableColumn(TableName, SETTYPE);

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }
        }



        public ActionResult CreateModlingDatas(string Name, string data)
        {
            try
            {
                Name = "S_MDSETUP";//設定記錄表

                //宣告調用Rule
                Modling_Rule Rule = new Modling_Rule();

                //解析請求資料Json→Dictionary
                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);
                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                //檢查表是否存在
                if (!Rule.TableIsExisted(_dic["CODE"]))
                {
                    throw new Exception(string.Format("資料表[{0}]不存在", _dic["CODE"]));
                }

                //新增資料
                var Result = Rule.CreateByDictionary(Name, _dic);

                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult UpdateModlingDatas(string Name, string data)
        {
            Name = "S_MDSETUP";//設定記錄表

            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                var Result = Rule.UpdateByDictionary(Name, _dic);


                return new JsonNetResult() { Data = Result };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 294;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult DeleteModlingDatas(string Name, string data)
        {
            Name = "S_MDSETUP";//設定記錄表
            try
            {
                Modling_Rule Rule = new Modling_Rule();

                var ls = JsonConvert.DeserializeObject<List<ModlingData>>(data);

                Dictionary<string, string> _dic = new Dictionary<string, string>();
                ls.ForEach(o => _dic.Add(o.Name, o.Value));

                Rule.DeleteByDictionary(Name, _dic);

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