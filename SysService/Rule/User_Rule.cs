using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBHelpers;
using SysService.objs;
using SysService.objs.tb;
using SysService.objs.View;
using SysService.svc;

namespace SysService.Rule
{
    public class User_Rule
    {
        /// <summary>
        /// 取得公用功能
        /// </summary>
        /// <returns></returns>
        public V_UserInfo PublicFun()
        {
            V_UserInfo Result = new V_UserInfo();
            if (System.Configuration.ConfigurationManager.AppSettings["DevMode"].ToString() == "Y")
            {
                Result = UserSignIn(System.Configuration.ConfigurationManager.AppSettings["UID"].ToString(), System.Configuration.ConfigurationManager.AppSettings["PWD"].ToString(), 0, 0);
                //Result = UserSignIn("1","1");
            }
            else
            {
                Result.UserMenu = GetpublicMenu();
            }


            return Result;
        }

        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public V_UserInfo UserSignIn(string UID, string PWD, Double lat, Double lon)
        {
            //1.檢查帳號是否存在
            //2.檢查密碼是否正確(DES加解密)
            //3.產生Token(SID+yyyyMMddHHmmss→DES)
            //4.產生Token及有效期(24小時)
            //modyfi by kurt 2022/09/06 新增坐標位置
            //5.寫入UserInfo
            //6.取得UserMenuList
            //7.回傳使用者資訊

            //檢查是否為系統管理員(WebConfig內寫死帳號&密碼)
            if (UID == System.Configuration.ConfigurationManager.AppSettings["UID"].ToString())
            {
                if (PWD == System.Configuration.ConfigurationManager.AppSettings["PWD"].ToString())
                {
                    S_USERINFO admin = new S_USERINFO();
                    admin.NAME = "系統管理者";
                    admin.CODE = "Admin";
                    admin.TOKEN = (Guid.NewGuid() + DateTime.Now.ToString("yyyyMMddHHmmss")).DESEncrypt();
                    BaseExt.AdminToken = admin.TOKEN;
                    admin.EXPTIME = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm:ss");
                    var R = admin.ToObject<V_UserInfo>();
                    R.UserMenu = GetAdminMenu();
                    return R;
                }
            }

            //1.檢查帳號是否存在
            if (UID == "") { throw new Exception(string.Format("使用者帳號不得為空!")); }
            if (PWD == "") { throw new Exception(string.Format("使用者密碼不得為空!")); }

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_USERINFO where CODE='{0}'", UID);
            var UserInfo = db.ExecuteObject<S_USERINFO>(SqlCmd);
            if (UserInfo == null)
            {
                throw new Exception(string.Format("找不到指定的帳號:「{0}」", UID));
            }

            //2.檢查密碼是否正確(DES加解密)
            if (UserInfo.PWD.DESDecrypt() != PWD)
            {
                throw new Exception(string.Format("密碼錯誤!"));
            }

            //3.產生Token(SID+yyyyMMddHHmmss→DES)
            UserInfo.TOKEN = (UserInfo.SID + DateTime.Now.ToString("yyyyMMddHHmmss")).DESEncrypt();

            //4.產生Token及有效期(24小時)
            UserInfo.EXPTIME = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm:ss");

            //modyfi by kurt 2022/09/06 新增坐標位置
            UserInfo.LON = lon;
            UserInfo.LAT = lat;

            //5.寫入UserInfo
            UserInfo.UpdateBySID<S_USERINFO>(TransType.SignIn);

            //6.轉型成檢視物件且取得UserMenuList
            var Result = UserInfo.ToObject<V_UserInfo>();
            Result.UserMenu = GetUserMenu(UserInfo.SID);

            //7.回傳使用者資訊
            return Result;
        }

        /// <summary>
        /// 使用者登出
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public S_USERINFO UserSignOut(string UID)
        {
            //1.更新Token
            //2.回傳使用者資訊

            //若登入者為系統管理員，則直接登出
            if (UID == System.Configuration.ConfigurationManager.AppSettings["UID"].ToString())
            {
                S_USERINFO Admin = new S_USERINFO();
                Admin.TOKEN = "";
                Admin.EXPTIME = "2000/01/01 00:00:00";
                return Admin;
            }


            //1.更新Token
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_USERINFO where CODE='{0}'", UID);
            var UserInfo = db.ExecuteObject<S_USERINFO>(SqlCmd);

            UserInfo.TOKEN = "";
            UserInfo.EXPTIME = "2000/01/01 00:00:00";

            //5.寫入UserInfo
            UserInfo.UpdateBySID<S_USERINFO>(TransType.SignOut);

            //6.回傳使用者資訊
            return UserInfo;
        }

        /// <summary>
        /// 取得使用者資料
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public S_USERINFO GetUserInfoByToken(string Token)
        {
            return BaseExt.GetUserInfo(Token);
        }

        /// <summary>
        /// 檢查使用者位置
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public V_UserInfo CheckUserLocation(string Token)
        {
            //1.檢查使用者是否有效
            //2.檢查使用者位置是否合法

            var Result = new V_UserInfo();

            //1.檢查使用者是否有效
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_USERINFO where TOKEN='{0}'", Token);
            var _U = db.ExecuteObject<S_USERINFO>(SqlCmd);

            if (_U == null)
            {
                throw new Exception("登入失效!無法執行考勤登記");
            }
            else
            {
                Result = _U.ToObject<V_UserInfo>();

                DateTime ExpTime = DateTime.Parse(Result.EXPTIME);

                if (ExpTime < DateTime.Now)
                {
                    throw new Exception("登入失效!無法執行考勤登記");
                }
            }


            //2.檢查使用者位置是否合法
            if (!Result.ISPASS.ToBool())
            {
                if (Result.LAT > Result.LT.LAT || Result.LAT < Result.RB.LAT) { throw new Exception("所地的位置不合法!無法執行考勤登記"); };
                if (Result.LON < Result.LT.LON || Result.LON > Result.RB.LON) { throw new Exception("所地的位置不合法!無法執行考勤登記"); };
            }
            


            return Result; ;
        }

        /// <summary>
        /// 取得使用者功能清單
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<V_UserMenu> GetUserMenu(string UID)
        {
            List<V_UserMenu> Result = new List<V_UserMenu>();

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select distinct d.* from S_USERROLE a 
                                            left join S_USERROLEDTL b on a.SID = b.S_USERROLE_ID
                                            left join S_RoleDtl c on b.CODE = c.S_Role_ID
                                            left join S_Menu d on c.S_MENU_ID = d.SID  or d.IsPublic=1
                                            where a.code = '{0}' order by d.SORT", UID);
            var tmpMenu = db.ExecuteList<S_MENU>(SqlCmd);

            return CreateMenuList(tmpMenu);
        }


        /// <summary>
        /// 取得公用功能清單
        /// <returns></returns>
        public List<V_UserMenu> GetpublicMenu()
        {
            List<V_UserMenu> Result = new List<V_UserMenu>();

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select * from S_Menu where ispublic =1");
            var tmpMenu = db.ExecuteList<S_MENU>(SqlCmd);

            return CreateMenuList(tmpMenu);
        }

        private List<V_UserMenu> CreateMenuList(List<S_MENU> ls)
        {
            List<V_UserMenu> Result = new List<V_UserMenu>();

            //載入根目錄
            ls.Where(o => o.CODE == "/").ToList().ForEach(o =>
            {
                Result.Add(new V_UserMenu { text = o.NAME, iconCls = o.ICONCLS, state = "closed", SID = o.SID }); ;
                //加載參數設定
                if (o.URL == "V_ModlingSetup")
                {
                    var db = new DBHelper("MSDB");
                    var ModlingList = db.ExecuteList<V_UserMenu_children>("select case when settype='MD' then 'V_ModlingSetupMD' else  'V_ModlingSetup' end url,NAME text,CODE MD,case when settype='MD' then 'icon-docs' else  NULL end iconCls from S_MDSETUP");
                    ModlingList.ForEach(p => Result.Where(q => q.SID == o.SID).FirstOrDefault().children.Add(p));
                }
            });
            //依根目錄載入對應功能
            Result.ForEach(o =>
            {
                ls.Where(p => p.CODE == o.SID).ToList().ForEach(q =>
                {
                    o.children.Add(new V_UserMenu_children { text = q.NAME, url = q.URL, iconCls = q.ICONCLS });
                });
            });
            return Result;
        }

        /// <summary>
        /// 取得本地管理者功能清單
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<V_UserMenu> GetAdminMenu()
        {
            List<V_UserMenu> Result = new List<V_UserMenu>();

            var db = new DBHelper("MSDB");
            //加載系統設定
            Result.Add(new V_UserMenu() { text = "系統設定", iconCls = "icon-config", state = "closed" });
            Result[0].children.Add(new V_UserMenu_children { url = "V_MDSETUP", text = "參數設定(V_MDSETUP)", iconCls = "icon-settings" });
            Result[0].children.Add(new V_UserMenu_children { url = "V_DataSource", text = "資料來源(V_DataSource)", iconCls = "icon-settings" });
            Result[0].children.Add(new V_UserMenu_children { url = "UserManage", text = "用戶管理(UserManage)", iconCls = "icon-settings" });
            Result[0].children.Add(new V_UserMenu_children { url = "V_MenuManage", text = "功能管理(V_MenuManage)", iconCls = "icon-settings" });
            Result[0].children.Add(new V_UserMenu_children { url = "V_ROLE", text = "權限群組(V_ROLE)", iconCls = "icon-settings" });
            Result[0].children.Add(new V_UserMenu_children { url = "V_UserRole", text = "權限管理(V_UserRole)", iconCls = "icon-settings" });

            //加載自訂功能
            Result.Add(new V_UserMenu() { text = "表單功能", iconCls = "icon-my-account", state = "closed" });
            string SqlCmd = string.Format(@"select URL url,Name text,'icon-tag' iconCls from S_Menu where code <> '/'");
            var tmpMenu = db.ExecuteList<V_UserMenu_children>(SqlCmd);
            Result[1].children.AddRange(tmpMenu);

            //加載參數設定
            Result.Add(new V_UserMenu() { text = "參數設定", iconCls = "icon-settings", state = "closed" });
            var ModlingList = db.ExecuteList<V_UserMenu_children>("select 'V_ModlingSetup' url,NAME text,CODE MD from S_MDSETUP");
            Result[2].children.AddRange(ModlingList);


            return Result;
        }


    }
}
