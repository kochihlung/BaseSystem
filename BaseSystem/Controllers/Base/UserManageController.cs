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

        string MainTable = "S_USERINFO";
        public ActionResult GetFunBase()
        {
            try
            {
                //生成MainDtl物件(直接調用並填入主表名，主表及明細表需依規範命名)
                var R = new SysService.objs.View.B_MainDtl(MainTable);
                R.Text = MainTable;

                //調整欄位屬性

                //主表
                Modling_Rule Rule = new Modling_Rule();
                R.Main.Base.Col.Where(q => q.ColName == "CODE").ToList().ForEach(o =>
                {
                    o.text = "登入帳號";
                    o.Required = true;
                });
                R.Main.Base.Col.Where(q => q.ColName == "NAME").ToList().ForEach(o =>
                {
                    o.text = "姓名";
                    o.Required = true;
                });
                R.Main.Base.Col.Where(q => q.ColName == "PWD").ToList().ForEach(o =>
                {
                    o.text = "密碼";
                    o.Required = true;
                });
                R.Main.Base.Col.Find(o => o.ColName == "REMARK").text = "備註";
                R.Main.Base.Col.Find(o => o.ColName == "UDT").Show = true;
                R.Main.Base.Col.Find(o => o.ColName == "TOKEN").Show = true;
                R.Main.Base.Col.Find(o => o.ColName == "EXPTIME").Show = true;
                R.Main.Base.Col.Find(o => o.ColName == "LAT").Show = true;
                R.Main.Base.Col.Find(o => o.ColName == "LON").Show = true;
                R.Main.Base.Col.Where(q => q.ColName == "ISPASS").ToList().ForEach(o =>
                {
                    o.UI = "Combobox";
                    o.Source = new ComboboxItem().IsBool();
                });

            

                Rule.OverrideModlingSetup(R);

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
                ls.Find(o => o.Name == "PWD").Value = ls.Find(o => o.Name == "PWD").Value.DESEncrypt();
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
                Rule.SetPwd(ls);//設定密碼加密
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

                Rule.DeleteByDictionary(MainTable, _dic);

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