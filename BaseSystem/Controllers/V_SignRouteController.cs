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
    public class V_SignRouteController : JsonNetController
    {
        //定義主表表名
        string MainTable = "M_ROUTE";

        // GET: V_Role
        public ActionResult Index()
        {
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
                var R = new SysService.objs.View.B_MainDtl(MainTable);//M_ROUTE----M_ROUTEDTL
                R.Text = "權限群組";
                //調整欄位屬性
                //主表
                R.Main.Base.Col.Where(q => q.ColName == "SID").ToList().ForEach(o =>
                {
                    o.Required = true;//必填
                    o.Readonly = true;//不可修改
                });
                R.Main.Base.Col.Where(q => q.ColName == "CODE").ToList().ForEach(o =>
                {
                    o.Required = true;//必填
                });
                R.Main.Base.Col.Where(q => q.ColName == "NAME").ToList().ForEach(o =>
                {
                    o.Required = true;//必填
                    o.text = "簽核名稱";
                });
                R.Main.Base.Col.Where(q => q.ColName == "UDT").ToList().ForEach(o =>
                {
                    o.Readonly = true;//不可修改
                    o.Show = true;
                });

                //明細表
                R.Dtl.Base.Col.Where(q => q.ColName == "CODE").ToList().ForEach(o =>
                {
                    o.Show = true;
                });
                R.Dtl.Base.Col.Where(q => q.ColName == "NAME").ToList().ForEach(o =>
                {
                    o.Show = true;
                });
                R.Dtl.Base.Col.Where(q => q.ColName == "UDT").ToList().ForEach(o =>
                {
                    o.Show = true;
                });
                R.Dtl.Base.Col.Where(q => q.ColName == "M_OPER_ID").ToList().ForEach(o =>
                {
                    o.Required = true;//必填
                    o.UI = "Combobox";
                    o.Source = new Modling_Rule().GetComboboxData("M_OPER");
                });
                R.Dtl.Base.Col.Where(q => q.ColName == "SQE").ToList().ForEach(o =>
                {
                    o.Required = true;//必填
                    o.UI = "Numberbox";
                });

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

                return new JsonNetResult() { Data = Rule.GetDatas(MainTable) };
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

                var Result = Rule.CreateByDictionary(MainTable, _dic);

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

                var Result = Rule.UpdateByDictionary(MainTable, _dic);


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

                Rule.DeleteMainDtlData(MainTable, _dic["SID"]);

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
                return new JsonNetResult() { Data = new Modling_Rule().GetDatas(MainTable + string.Format("DTL where {0}='{1}' ", MainTable + "_ID", SID), "order by SQE") };
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
                _dic[MainTable + "_ID"] = _dic["MainSID"];
                _dic.Remove("MainSID");

                var Result = Rule.CreateByDictionary(MainTable + "DTL", _dic, false);

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
                _dic[MainTable + "_ID"] = _dic["MainSID"];
                _dic.Remove("MainSID");

                var Result = Rule.UpdateByDictionary(MainTable + "DTL", _dic);


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

                Rule.DeleteByDictionary(MainTable + "DTL", _dic);


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