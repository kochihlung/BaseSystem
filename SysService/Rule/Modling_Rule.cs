using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DBHelpers;

using SysService.objs;
using SysService.objs.tb;
using SysService.objs.View;
using SysService.svc;


namespace SysService.Rule
{
    public class Modling_Rule
    {
        public void OverrideModlingSetup(B_MainDtl obj)
        {
            var _db = new DBHelper("MSDB");

            var SetData = _db.ExecuteList<S_MDSETUPDTL>(string.Format("select a.*,b.SETTYPE from S_MDSETUPDTL a left join S_MDSETUP b on a.S_MDSETUP_ID=b.sid where a.code='{0}'", obj.MainTable));
            foreach (var r in obj.Main.Base.Col)
            {
                foreach (var p in SetData)
                {
                    if (p.COLNAME == r.ColName)
                    {
                        r.align = p.ALIGN;
                        r.Show = !p.ISMODLING.Tobool();
                        r.IsModling = p.ISMODLING.Tobool();
                        r.Readonly = p.READONLY.Tobool();
                        r.Required = p.REQUIRED.Tobool();
                        r.text = p.TEXT;
                        r.UI = p.UI;
                        if (p.UI == "Combobox")
                        {
                            r.Source = GetDataSourceItem(p.SOURCE);
                        }
                    }
                }
            }

            SetData = _db.ExecuteList<S_MDSETUPDTL>(string.Format("select a.*,b.SETTYPE from S_MDSETUPDTL a left join S_MDSETUP b on a.S_MDSETUP_ID=b.sid where a.code='{0}'", obj.MainTable + "DTL"));
            foreach (var r in obj.Dtl.Base.Col)
            {
                foreach (var p in SetData)
                {
                    if (p.COLNAME == r.ColName)
                    {
                        r.align = p.ALIGN;
                        r.Show = !p.ISMODLING.Tobool();
                        r.IsModling = p.ISMODLING.Tobool();
                        r.Readonly = p.READONLY.Tobool();
                        r.Required = p.REQUIRED.Tobool();
                        r.text = p.TEXT;
                        r.UI = p.UI;
                        if (p.UI == "Combobox")
                        {
                            r.Source = GetDataSourceItem(p.SOURCE);
                        }
                    }
                }
            }
        }

        public List<ComboboxItem> GetDataSourceItem(string SID)
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();

            var _db = new DBHelper("MSDB");

            var r = _db.ExecuteObject<S_DATASOURCE>(string.Format("select * from S_DATASOURCE where sid='{0}'", SID));

            if (r == null) { throw new Exception("找不到對應的資料來源"); };

            if (r.DATATYPE == "0")
            {//固定資料
                Result = r.DATAS.ToConboboxItem();
            }
            else
            {//來源資料
                Result = GetComboboxData(r.TABLENAME);
            }
            return Result;
        }

        public List<ComboboxItem> GetDataSourceItemByCode(string Code, string WhereStr = "")
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();

            var _db = new DBHelper("MSDB");

            var r = _db.ExecuteObject<S_DATASOURCE>(string.Format("select * from S_DATASOURCE where CODE='{0}'", Code));

            if (r == null) { throw new Exception("找不到對應的資料來源"); };

            if (r.DATATYPE == "0")
            {//固定資料
                Result = r.DATAS.ToConboboxItem();
            }
            else
            {//來源資料
                Result = GetComboboxData(r.TABLENAME, WhereStr);
            }
            return Result;
        }

        public ModlingSetup GetModlingSetup(string Name)
        {
            ModlingSetup Result = new ModlingSetup();

            var _db = new DBHelper("MSDB");

            var _MD = _db.ExecuteObject<_BaseTable>(string.Format("select SID,CODE,NAME from S_MDSETUP where CODE='{0}'", Name));

            Result.Name = _MD.CODE;
            Result.text = _MD.NAME;

            List<S_MDSETUPDTL> _MDDtl = _db.ExecuteList<S_MDSETUPDTL>(string.Format("select * from S_MDSETUPDTL where S_MDSETUP_ID='{0}' and CODE='{1}'", _MD.SID, Name));
            //強制加入SID並隱藏
            Result.Col.Add(new ModlingSetupDtl()
            {
                Show = true,
                ColName = "SID",
                text = "系統ID"
            });


            _MDDtl.FindAll(o => o.ISMODLING == "1").ForEach(p =>
            {
                ModlingSetupDtl _dtl = new ModlingSetupDtl();

                _dtl.ColName = p.COLNAME;
                _dtl.text = p.TEXT;
                _dtl.UI = p.UI;
                _dtl.Source = "";
                if (_dtl.UI == "Combobox")
                {
                    _dtl.Source = GetDataSourceItem(p.SOURCE);
                    _dtl.IsModling = true;
                }
                _dtl.Readonly = p.READONLY.Tobool();
                _dtl.Required = p.REQUIRED.Tobool();
                Result.Col.Add(_dtl);
            });
            return Result;
        }

        public object GetModlingList()
        {
            var _db = new DBHelper("MSDB");

            var Result = _db.ExecuteList("select 'V_ModlingSetup' url,NAME text,'true' IsAction,CODE MD from S_MDSETUP",
                r => new
                {
                    url = r.Get<string>("url"),
                    text = r.Get<string>("text"),
                    IsAction = r.Get<string>("IsAction"),
                    MD = r.Get<string>("MD"),
                    CheckUser = false
                }
                );

            return Result;
        }

        //保存參數
        public List<MDSetup> SaveColDtl(List<MDSetup> rows, string tb, string col, string TableName)
        {
            //僅使用新增及更新功能
            var IsCreate = false;
            var _db = new DBHelper("MSDB");
            //檢查是否有舊資料

            string SqlCmd = string.Format(@"select * from S_MDSETUPDTL where COLNAME='{0}' and S_MDSETUP_ID='{1}' and CODE='{2}'", col, tb, TableName);

            //組成保存物件
            var obj = _db.ExecuteObject<S_MDSETUPDTL>(SqlCmd);
            if (obj == null) { obj = new S_MDSETUPDTL(); IsCreate = true; };
            obj.S_MDSETUP_ID = tb;
            obj.COLNAME = col;
            obj.CODE = TableName;// string.Format(tb);
            obj.NAME = col;
            rows.ForEach(o =>
            {
                switch (o.name)
                {
                    case "text":
                        obj.TEXT = o.value;
                        break;
                    case "IsModling":
                        obj.ISMODLING = o.value;
                        break;
                    case "UI":
                        var _UI = o.editor.options.data.Find(p => p.text == o.value);
                        if (_UI == null)
                        {
                            obj.UI = "";
                        }
                        else
                        {
                            obj.UI = _UI.id;
                        }
                        break;
                    case "Source":
                        var _Source = o.editor.options.data.Find(p => p.text == o.value);
                        if (_Source == null)
                        {
                            obj.SOURCE = "";
                        }
                        else
                        {
                            obj.SOURCE = _Source.id;
                        }
                        break;
                    case "Readonly":
                        obj.READONLY = o.value;
                        break;
                    case "Required":
                        obj.REQUIRED = o.value;
                        break;
                    case "width":
                        obj.WIDTH = o.value;
                        break;
                    case "align":
                        var _align = o.editor.options.data.Find(p => p.text == o.value);
                        if (_align == null)
                        {
                            obj.ALIGN = "";
                        }
                        else
                        {
                            obj.ALIGN = _align.id;
                        }
                        break;
                    case "VAL":
                        var _Type = o.editor.options.data.Find(p => p.text == o.value);
                        if (_Type == null)
                        {
                            obj.VAL = "";
                        }
                        else
                        {
                            obj.VAL = _Type.id;
                        }
                        break;
                    default:
                        break;
                }
            });


            if (IsCreate)
            {//無舊資料
                Create<S_MDSETUPDTL>(obj);
            }
            else
            {//有舊資料
                Update<S_MDSETUPDTL>(obj);
            }
            //插入History

            return rows;
        }

        //取得設定參數
        public List<MDSetup> GetColDtl(string tb, string col, string TableName)
        {
            //先建立設定參數清單
            List<MDSetup> Result = new List<MDSetup>();
            Result.Add(new MDSetup() { text = "參數名稱", name = "text", value = "", group = "Settings", editor = new MD_editor("textbox") });
            Result.Add(new MDSetup() { text = "UI類型", name = "UI", value = "", group = "Settings", editor = new MD_editor("combobox") });
            Result.Add(new MDSetup() { text = "是否為參數", name = "IsModling", value = "1", group = "Settings", editor = new MD_editor("checkbox") });
            Result.Add(new MDSetup() { text = "數據來源", name = "Source", value = "", group = "Settings", editor = new MD_editor("combobox") });
            Result.Add(new MDSetup() { text = "不可修改", name = "Readonly", value = "0", group = "Settings", editor = new MD_editor("checkbox") });
            Result.Add(new MDSetup() { text = "必填項", name = "Required", value = "0", group = "Settings", editor = new MD_editor("checkbox") });
            Result.Add(new MDSetup() { text = "寬度", name = "width", value = "150", group = "Settings", editor = new MD_editor("numberbox") });
            Result.Add(new MDSetup() { text = "對齊方式", name = "align", value = "", group = "Settings", editor = new MD_editor("combobox") });
            Result.Add(new MDSetup() { text = "類型", name = "VAL", value = "", group = "Settings", editor = new MD_editor("combobox") });

            switch (col)
            {
                case "SID":
                    Result.Find(o => o.name == "text").value = "系統ID";
                    Result.Find(o => o.name == "UI").value = "textbox";
                    Result.Find(o => o.name == "IsModling").value = "0";
                    Result.Find(o => o.name == "Readonly").value = "1";
                    Result.Find(o => o.name == "Required").value = "0";
                    Result.Find(o => o.name == "align").value = "left";
                    break;
                case "CODE":
                    Result.Find(o => o.name == "text").value = "代碼";
                    Result.Find(o => o.name == "UI").value = "textbox";
                    Result.Find(o => o.name == "IsModling").value = "1";
                    Result.Find(o => o.name == "Readonly").value = "0";
                    Result.Find(o => o.name == "Required").value = "0";
                    Result.Find(o => o.name == "align").value = "left";
                    break;
                case "NAME":
                    Result.Find(o => o.name == "text").value = "名稱";
                    Result.Find(o => o.name == "UI").value = "textbox";
                    Result.Find(o => o.name == "IsModling").value = "1";
                    Result.Find(o => o.name == "Readonly").value = "0";
                    Result.Find(o => o.name == "Required").value = "0";
                    Result.Find(o => o.name == "align").value = "left";
                    break;
                case "REMARK":
                    Result.Find(o => o.name == "text").value = "備註";
                    Result.Find(o => o.name == "UI").value = "textbox";
                    Result.Find(o => o.name == "IsModling").value = "1";
                    Result.Find(o => o.name == "Readonly").value = "0";
                    Result.Find(o => o.name == "Required").value = "0";
                    Result.Find(o => o.name == "align").value = "left";
                    break;
                case "UDT":
                    Result.Find(o => o.name == "text").value = "更新時間";
                    Result.Find(o => o.name == "UI").value = "textbox";
                    Result.Find(o => o.name == "IsModling").value = "1";
                    Result.Find(o => o.name == "Readonly").value = "1";
                    Result.Find(o => o.name == "Required").value = "0";
                    Result.Find(o => o.name == "align").value = "left";
                    break;
                case "VAL":
                    Result.Find(o => o.name == "Readonly").value = "1";
                    break;
                default:
                    break;
            }

            //取得設定資料
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select b.* from S_MDSETUP a left join S_MDSETUPDTL b on a.SID = b.S_MDSETUP_ID where a.SID = '{0}' and ColName = '{1}' and b.CODE='{2}'", tb, col, TableName);

            var _R = _db.ExecuteObject<V_MDSetupDTL>(_SqlCmd);

            //填入下拉選單資料
            Result.Find(o => o.name == "Source").editor.options.data = new ComboboxItem().DataSource();

            var align_data = "left;靠左,center;置中,right;靠右";
            Result.Find(o => o.name == "align").editor.options.data = align_data.ToConboboxItem();

            var Type_data = "B;單表,M;主表,D;明細表";

            Result.Find(o => o.name == "VAL").editor.options.data = Type_data.ToConboboxItem();
            string TbType = new DBHelper("MSDB").ExecuteScalar<string>(string.Format("select SETTYPE from S_MDSETUP where sid='{0}'", tb));
            if (TbType == "Base")
            {
                Result.Find(o => o.name == "VAL").value = "單表";
            }
            else
            {
                if (TableName.Substring(TableName.Length - 3, 3).ToUpper() != "DTL")
                {
                    Result.Find(o => o.name == "VAL").value = "主表";
                }
                else
                {
                    Result.Find(o => o.name == "VAL").value = "明細表";
                }
            }

            var UI_data = "textbox;文字方塊,Number;數字方塊,checkbox;核選方塊,Combobox;下拉選單,Time;時間選單,Date;日期選單";
            Result.Find(o => o.name == "UI").editor.options.data = UI_data.ToConboboxItem();

            //更新清單參數
            foreach (var o in Result)
            {
                if (_R == null) { continue; }
                //填值
                switch (o.name)
                {
                    case "text":
                        o.value = _R.text;
                        break;
                    case "IsModling":
                        o.value = _R.IsModling.ToIntStr();
                        break;
                    case "UI":
                        var _UI = o.editor.options.data.Find(p => p.id == _R.UI.ToString());
                        if (_UI == null)
                        {
                            o.value = "";
                        }
                        else
                        {
                            o.value = _UI.text;
                        }
                        break;
                    case "Source":
                        var _Source = o.editor.options.data.Find(p => p.id == _R.Source.ToString());
                        if (_Source == null)
                        {
                            o.value = "";
                        }
                        else
                        {
                            o.value = _Source.text;
                        }
                        break;
                    case "Readonly":
                        o.value = _R.Readonly.ToIntStr();
                        break;
                    case "Required":
                        o.value = _R.Required.ToIntStr();
                        break;
                    case "width":
                        o.value = _R.width.ToString();
                        break;
                    case "align":
                        var _align = o.editor.options.data.Find(p => p.id == _R.align.ToString());
                        if (_align == null)
                        {
                            o.value = "";
                        }
                        else
                        {
                            o.value = _align.text;
                        }
                        break;
                    case "VAL":
                        var _Type = o.editor.options.data.Find(p => p.id == _R.VAL.ToString());
                        if (_Type == null)
                        {
                            o.value = "";
                        }
                        else
                        {
                            o.value = _Type.text;
                        }
                        break;
                    default:
                        break;
                }
            }
            return Result;
        }

        //取得資料來源的Modling內容，供前端自動產生介面
        public ModlingSetup GetModling_DataSource()
        {
            //自訂議物件內容並回傳
            ModlingSetup _S_DATASOURCE = new ModlingSetup() { Name = "S_DATASOURCE", text = "資料來源" };
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "SID", text = "SID", UI = "Textbox", Readonly = true, Required = false });
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "CODE", text = "代碼", UI = "Textbox", Readonly = false, Required = true });
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "NAME", text = "名稱", UI = "Textbox", Readonly = false, Required = true });
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "REMARK", text = "備註", UI = "Textbox", Readonly = false, Required = false });
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "UDT", text = "更新時間", UI = "Time", Readonly = true, Required = false });

            //DataType
            List<object> _Type = new List<object>();
            _Type.Add(new { id = 0, text = "固定資料" });
            _Type.Add(new { id = 1, text = "資料來源" });
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "DATATYPE", text = "資料類型", UI = "Combobox", Readonly = false, Required = false, Source = _Type, IsModling = true });

            //TableName
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "TABLENAME", text = "對應表名", UI = "Combobox", Readonly = false, Required = false, Source = GetMDTables() });
            //ColumnName
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "COLUMNNAME", text = "對應欄位", UI = "Combobox", Readonly = false, Required = false });
            //Datas
            _S_DATASOURCE.Col.Add(new ModlingSetupDtl() { ColName = "DATAS", text = "固定資料", UI = "Textbox", Readonly = false, Required = false });

            return _S_DATASOURCE;
        }

        /// <summary>
        /// 取得表單管理結構
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FunctionList"></param>
        /// <returns></returns>
        public ModlingSetup GetModling_MenuManage(string path, List<string> FunctionList)
        {
            //自訂議物件內容並回傳
            ModlingSetup S_MENU = new ModlingSetup() { Name = "S_MENU", text = "功能管理" };
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "SID", text = "SID", UI = "Textbox", Readonly = true, Required = false });
            //S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "CODE", text = "代碼", UI = "Textbox", Readonly = false, Required = true });
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "CODE", text = "上一層級", UI = "Combobox", Readonly = false, Required = false, Source = GetFunctionFolder(), IsModling = true });
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "NAME", text = "名稱", UI = "Textbox", Readonly = false, Required = true });
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "REMARK", text = "備註", UI = "Textbox", Readonly = false, Required = false });
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "UDT", text = "更新時間", UI = "Time", Readonly = true, Required = false });

            List<object> IsBool = new List<object>();
            IsBool.Add(new { id = 0, text = "否" });
            IsBool.Add(new { id = 1, text = "是" });


            //ISACTION
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "ISACTION", text = "是否為功能", UI = "Combobox", Readonly = false, Required = true, Source = IsBool, IsModling = true });

            //CHECKUSER
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "IsPublic", text = "是否為公用", UI = "Combobox", Readonly = false, Required = true, Source = IsBool, IsModling = true });

            //ICONCLS
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "ICONCLS", text = "圖示", UI = "Combobox", Readonly = false, Required = true, Source = GetIcons(path), IsModling = true });

            //URL
            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "URL", text = "對應程式", UI = "Combobox", Readonly = false, Required = true, Source = GetFunction(FunctionList), IsModling = true });

            S_MENU.Col.Add(new ModlingSetupDtl() { ColName = "SORT", text = "排序", UI = "Textbox", Readonly = false, Required = false });

            return S_MENU;
        }

        public List<ComboboxItem> GetFunctionFolder()
        {
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select SID id,NAME text from S_MENU where ISACTION=0");

            var _Result = _db.ExecuteList<ComboboxItem>(_SqlCmd);

            _Result.Add(new ComboboxItem { id = "/", text = "根目錄" });

            return _Result;
        }

        public List<ComboboxItem> GetFunction(List<string> FunctionList)
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            foreach (var obj in FunctionList)
            {
                Result.Add(new ComboboxItem { id = obj, text = obj });
            }
            return Result;
        }

        public List<ComboboxItem> GetIcons(string path)
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            var css = new BoneSoft.CSS.CSSParser().ParseFile(path + @"\icon.css").RuleSets;


            css.ForEach(o =>
            {
                string ClassName = o.Selectors[0].SimpleSelectors[0].Class;
                Result.Add(new ComboboxItem { id = ClassName, text = ClassName, iconCls = ClassName });
            });

            return Result;

        }

        //取得用戶管理的Modling內容，供前端自動產生介面
        public ModlingSetup GetModling_UserManage()
        {
            //自訂議物件內容並回傳
            ModlingSetup _Modling = new ModlingSetup() { Name = "S_USERINFO", text = "用戶管理" };
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "SID", text = "SID", UI = "Textbox", Readonly = true, Required = false });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "CODE", text = "用戶代碼", UI = "Textbox", Readonly = false, Required = true });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "NAME", text = "姓名", UI = "Textbox", Readonly = false, Required = true });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "PWD", text = "密碼", UI = "Textbox", Readonly = false, Required = false });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "REMARK", text = "備註", UI = "Textbox", Readonly = false, Required = false });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "UDT", text = "更新時間", UI = "Time", Readonly = true, Required = false });
            List<object> IsBool = new List<object>();
            IsBool.Add(new { id = 0, text = "否" });
            IsBool.Add(new { id = 1, text = "是" });
            _Modling.Col.Add(new ModlingSetupDtl() { ColName = "ISPASS", text = "免地區限制", UI = "Combobox", Readonly = false, Source = IsBool, IsModling = true });
            return _Modling;
        }

        //取得Modling設定的清單
        public List<V_MDSETU> GetMDList(string Name)
        {
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select * from S_MDSETUP where NAME like '%{0}%' order by UDT", Name);

            var _Result = _db.ExecuteList<V_MDSETU>(_SqlCmd);

            return _Result;
        }

        //取得資料，但因每張表的欄位不一樣，故使用List<Dictionary<string, object>>傳遞
        public List<Dictionary<string, object>> GetDatas(string Name, string orderby = "order by UDT")
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();

            try
            {
                var db = new DBHelper("MSDB");

                string _SqlCmd = string.Format("select * from {0} {1} ", Name, orderby);

                Result = db.SelectDictionary(_SqlCmd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Result;
        }
        public List<ComboboxItem> UserMenuSet(string UID)
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format(@"select * from S_MENU where sid not in(
                                             select d.S_MENU_ID from S_USERROLE a
                                             left join S_USERROLEdtl b on a.sid = b.S_USERROLE_ID
                                             left join S_ROLE c on b.code = c.sid
                                             left join S_ROLEdtl d on c.sid = d.S_ROLE_ID
                                             where a.code = '{0}')", UID);

            return _db.ExecuteList<_BaseTable>(_SqlCmd).ToComboboxItem();
        }

        //取得指定Table的Code/Name做為下拉選單的資料
        public List<ComboboxItem> GetComboboxData(string Name, string WhereSTr = "", bool GetName = false)
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();

            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select SID,CODE,NAME from {0} where 1=1 {1} order by CODE", Name, WhereSTr);

            var _Result = _db.ExecuteList<_BaseTable>(_SqlCmd);

            return _Result.ToComboboxItem();

        }

        //取得Table的欄位資料
        public List<TableColumn> GetTableColumn(string TableName, string SETTYPE)
        {
            List<TableColumn> Result = new List<TableColumn>();
            var _db = new DBHelper("MSDB");
            string _SqlCmd = string.Empty;
            switch (SETTYPE)
            {
                case "Base":
                    _SqlCmd = string.Format(@"select a.NAME,b.COLUMN_NAME COL,c.VALUE REMARK,d.SID S_MDSETUP_SID from sysobjects a
                                                     left join INFORMATION_SCHEMA.COLUMNS b on a.name = b.TABLE_NAME
                                                     left join sys.extended_properties  c on c.major_id=a.id and c.minor_id=b.ORDINAL_POSITION
                                                     left join S_MDSETUP d on a.name=d.code
                                                     where a.name='{0}'", TableName);
                    Result = _db.ExecuteList<TableColumn>(_SqlCmd);
                    break;
                case "MD":
                    _SqlCmd = string.Format(@"select a.NAME,b.COLUMN_NAME COL,c.VALUE REMARK,d.SID S_MDSETUP_SID from sysobjects a
                                                     left join INFORMATION_SCHEMA.COLUMNS b on a.name = b.TABLE_NAME
                                                     left join sys.extended_properties  c on c.major_id=a.id and c.minor_id=b.ORDINAL_POSITION
                                                     left join S_MDSETUP d on a.name=d.code or a.name=d.code+'DTL'
                                                     where a.name='{0}' or a.name='{0}DTL'", TableName);
                    Result = _db.ExecuteList<TableColumn>(_SqlCmd);
                    break;
                default:
                    throw new Exception("沒有找到正確的設定類型!");
            }



            return Result;
        }

        //取得Table的欄位資料，並轉成ComboboxItem的格式
        public List<ComboboxItem> GetTableColumnToCombobox(string TableName)
        {
            List<TableColumn> Result = new List<TableColumn>();

            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format(@"select a.NAME,b.COLUMN_NAME COL,c.VALUE REMARK,d.SID S_MDSETUP_SID from sysobjects a
                                                     left join INFORMATION_SCHEMA.COLUMNS b on a.name = b.TABLE_NAME
                                                     left join sys.extended_properties  c on c.major_id=a.id and c.minor_id=b.ORDINAL_POSITION
                                                     left join S_MDSETUP d on a.name=d.code
                                                     where a.name='{0}'", TableName);

            return _db.ExecuteList<TableColumn>(_SqlCmd).ToComboboxItem();
        }

        //取得Modling相關的Table Name，並轉成Combobox Item
        public List<ComboboxItem> GetMDTables(bool All = false)
        {
            List<_BaseTable> Result = new List<_BaseTable>();

            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Empty;

            if (All)
            {
                _SqlCmd = string.Format("select NAME,NAME SID from sysobjects where xtype='U' and (name like 'M_%' or name like 'S_%') and name not like '%_HT'  order by name");
            }
            else
            {
                _SqlCmd = string.Format("select NAME,NAME SID from sysobjects where xtype='U' and (name like 'M_%' or name like 'S_%') and name not like '%_HT' and name not in (select code from s_mdsetup) order by name");
            }


            return _db.ExecuteList<_BaseTable>(_SqlCmd).ToComboboxItem();
        }

        //by object進行增刪改查，共用性較差不適用前端自動介面_目前僅供底層功能使用
        #region by object 增刪改查
        public T Delete<T>(T obj) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);

            List<string> _lsColumn = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }
            _lsColumn.Remove(_lsColumn.Find(o => o == "UDT"));

            //建立交易物件
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {


                string _SID = obj.GetType().GetProperty("SID").GetValue(obj).ToString();

                string SqlCmd = string.Format(@"INSERT INTO {1} ({0},TRANS,HID,UDT)
                                                SELECT {0},'Delete','{3}','{5}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, BaseExt.GetSystemGUID(), _SID, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                _db.ExecuteNonQuery(SqlCmd);

                SqlCmd = string.Format(@"delete {0}  where sid='{1}'", _type.Name, _SID);
                _db.ExecuteNonQuery(SqlCmd);

                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return Result;
        }
        public T Update<T>(T obj) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);

            List<string> _lsColumn = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                //建立交易物件
                string _SID = obj.GetType().GetProperty("SID").GetValue(obj).ToString();

                obj.GetType().GetProperty("UDT").SetValue(obj, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                List<string> _ls = new List<string>();
                foreach (var item in _type.GetProperties())
                {
                    if (item.GetValue(obj) != null)
                    {
                        _ls.Add(string.Format("{0}='{1}'", item.Name, item.GetValue(obj)));
                    }
                }
                _ls.Remove(_ls.Find(o => o == "SID"));
                _Cmd.CommandText = string.Format(@"update {0} set {1} where SID='{2}'", _type.Name, string.Join(",", _ls), _SID);
                _Cmd.ExecuteNonQuery();

                _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Update','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, BaseExt.GetSystemGUID(), _SID);
                _Cmd.ExecuteNonQuery();

                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return Result;
        }
        public T SelectSingle<T>(T obj) where T : new()
        {
            T Result = obj;

            //Type _type = typeof(T);
            //try
            //{
            //    using (MSSql db = new MSSql("MSDB"))
            //    {

            //        List<string> _ls = new List<string>();
            //        foreach (var item in _type.GetProperties())
            //        {
            //            if (item.GetValue(obj) != null && item.Name != "UDT")
            //            {
            //                _ls.Add(string.Format("and {0}='{1}'", item.Name, item.GetValue(obj)));
            //            }
            //        }
            //        _ls.Remove(_ls.Find(o => o == "UDT"));

            //        string SqlCmd = string.Format("select * from {0} where 1=1 {1}", _type.Name, string.Join(" ", _ls));

            //        var _Query = db.Select<T>(SqlCmd);
            //        if (_Query.Count != 1) { throw new Exception("找不到指定的資料或有多筆資料."); }
            //        Result = _Query[0];
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            return Result;
        }
        public T Create<T>(T obj) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);
            List<string> _lsColumn = new List<string>();

            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            try
            {
                //建立交易物件
                var _db = new DBHelper("MSDB");
                var _Conn = _db.CreateConnection();
                _Conn.Open();//開啟連線
                var _Cmd = _Conn.CreateCommand();
                var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                _Cmd.Transaction = _Tran;

                //填入SID
                string _SID = BaseExt.GetSystemGUID();

                Result.GetType().GetProperty("SID").SetValue(Result, _SID);

                try
                {
                    var _Type = typeof(T);
                    List<string> col = new List<string>();
                    List<string> Val = new List<string>();

                    var _Properite = _Type.GetProperties();

                    foreach (var item in _Properite)
                    {
                        col.Add(item.Name);
                        var v = _Type.GetProperty(item.Name).GetValue(obj);
                        if (v != null)
                        {
                            Val.Add(v.ToString());
                        }
                        else
                        {
                            Val.Add("");
                        }
                    }

                    _Cmd.CommandText = string.Format("insert into {0}({1}) values('{2}')", _type.Name, string.Join(",", col), string.Join("','", Val));
                    _Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var _Msg = ex.Message + string.Format("[{0}]", _Cmd.CommandText);
                    _Conn.Close();
                    throw new Exception(_Msg);
                }


                //插入History
                string _HID = BaseExt.GetSystemGUID();

                _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Create','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, _HID, _SID);
                _Cmd.ExecuteNonQuery();

                _Tran.Commit();
                _Conn.Close();//關閉連線
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Result;
        }
        #endregion

        //判段該Table是否存在
        public bool TableIsExisted(string TableName)
        {
            bool Result = true;

            var _db = new DBHelper("MSDB");

            string SqlCmd = string.Format("select NAME from sysobjects where xtype='U' and Name='{0}'", TableName);

            return _db.ExecuteReader(SqlCmd).HasRows;
        }


        //尚未確定DBHelper是否支援Transaction，暫時先用舊的DBHelp
        public Dictionary<string, object> CreateByDictionary(string Name, Dictionary<string, string> dic, bool CheckExisted = true)
        {
            List<string> _lsColumn = new List<string>();
            List<string> _lsValue = new List<string>();
            string _SID = string.Empty;

            //建立交易物件
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                string SqlCmd = string.Empty;

                if (CheckExisted)
                {
                    //檢查CODE+Name是否已存在
                    if (_db.IsExisted(string.Format("select * from {0} where CODE='{1}' and Name='{2}'", Name, dic["CODE"], dic["NAME"])))
                    {
                        throw new Exception("資料已存在");
                    }
                }

                _SID = BaseExt.GetSystemGUID();

                if (dic["SID"] == null)
                {
                    dic.Add("SID", _SID);
                }
                else
                {
                    dic["SID"] = _SID;
                }

                if (dic["UDT"] == null)
                {
                    dic.Add("UDT", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                else
                {
                    dic["UDT"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }

                foreach (var item in dic)
                {
                    _lsColumn.Add(item.Key);
                    _lsValue.Add(item.Value);
                }

                SqlCmd = string.Format(@"insert into {0}({1}) values('{2}')", Name, string.Join(",", _lsColumn), string.Join("','", _lsValue)); ;
                _db.ExecuteNonQuery(SqlCmd);


                //插入History
                string _HID = string.Empty;

                SqlCmd = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Create','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), Name + "_HT", Name, BaseExt.GetSystemGUID(), _SID);
                _db.ExecuteNonQuery(SqlCmd);


                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return SelectBySID(Name, _SID);
        }

        public Dictionary<string, object> UpdateByDictionary(string Name, Dictionary<string, string> dic)
        {
            List<string> _lsColumn = new List<string>();
            List<string> _lsValue = new List<string>();
            string _SID = string.Empty;

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                //填入SID

                _SID = dic["SID"];
                dic["UDT"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                dic.Remove("SID");
                _lsColumn.Add("SID");

                foreach (var item in dic)
                {
                    _lsColumn.Add(item.Key);
                    _lsValue.Add(string.Format("{0}='{1}'", item.Key, item.Value));
                }

                string SqlCmd = string.Format(@"update {0} set {1} where SID='{2}'", Name, string.Join(",", _lsValue), _SID);
                _db.ExecuteNonQuery(SqlCmd);


                //插入History

                SqlCmd = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Update','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), Name + "_HT", Name, BaseExt.GetSystemGUID(), _SID);
                _db.ExecuteNonQuery(SqlCmd);

                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return SelectBySID(Name, _SID);
        }

        public void DeleteByDictionary(string Name, Dictionary<string, string> dic)
        {
            List<string> _lsColumn = new List<string>();
            List<string> _lsValue = new List<string>();
            string _SID = string.Empty;

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {

                _SID = dic["SID"];
                dic.Remove("UDT");

                foreach (var item in dic)
                {
                    _lsColumn.Add(item.Key);
                }

                string SqlCmd = string.Format(@"INSERT INTO {1} ({0},TRANS,HID,UDT)
                                                SELECT {0},'Delete','{3}',GETDATE()
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), Name + "_HT", Name, BaseExt.GetSystemGUID(), _SID);
                _db.ExecuteNonQuery(SqlCmd);


                SqlCmd = string.Format(@"Delete {0}  where SID='{1}'", Name, _SID);
                _db.ExecuteNonQuery(SqlCmd);

                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMainDtlData(string Name, string SID)
        {
            //交易模塊
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;
            try
            {
                //取得主表及名細表欄位
                string SqlCmd = string.Format("SELECT  SUBSTRING((SELECT ',' + column_name  FROM iNFORMATION_SCHEMA.COLUMNS  WHERE table_name = '{0}' and column_name <> 'UDT'  FOR xml PATH (''))  , 2, 9999) AS NAME;", Name);
                var MainCol = _db.ExecuteScalar<string>(SqlCmd);


                //插入主表刪除History
                _Cmd.CommandText = string.Format(@"INSERT INTO {0} ({1},TRANS,HID,UDT) 
                                                SELECT {1},'Delete' TRANS,(select NewID()) HID,GETDATE() UDT FROM {2} where {3}='{4}'"
                                              , Name + "_HT"
                                              , MainCol
                                              , Name
                                              , "SID"
                                              , SID);
                _Cmd.ExecuteNonQuery();
                //刪除主表資料
                _Cmd.CommandText = string.Format(@"Delete {0}  where SID='{1}'", Name, SID);
                _Cmd.ExecuteNonQuery();


                SqlCmd = string.Format("SELECT  SUBSTRING((SELECT ',' + column_name  FROM iNFORMATION_SCHEMA.COLUMNS  WHERE table_name = '{0}' and column_name <> 'UDT'  FOR xml PATH (''))  , 2, 9999) AS NAME;", Name + "DTL");
                var DtlCol = _db.ExecuteScalar<string>(SqlCmd);
                if (DtlCol != null)
                {
                    //插入明細刪除History
                    _Cmd.CommandText = string.Format(@"INSERT INTO {0} ({1},TRANS,HID,UDT) 
                                                SELECT {1},'Delete' TRANS,(select NewID()) HID,GETDATE() UDT FROM {2} where {3}='{4}'"
                                                  , Name + "DTL_HT"
                                                  , DtlCol
                                                  , Name + "DTL"
                                                  , Name + "_ID"
                                                  , SID);
                    _Cmd.ExecuteNonQuery();
                    //刪除明細表資料
                    _Cmd.CommandText = string.Format(@"Delete {0}  where {1}='{2}'", Name + "DTL", Name + "_ID", SID);
                    _Cmd.ExecuteNonQuery();
                }



                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, object> SelectBySID(string Name, string SID)
        {
            Dictionary<string, object> Result = new Dictionary<string, object>();
            try
            {
                var db = new DBHelper("MSDB");
                string SqlCmd = string.Format("select * from {0} where SID='{1}'", Name, SID);

                var _Query = db.SelectDictionary(SqlCmd);
                if (_Query.Count != 1) { throw new Exception("找不到指定的資料或有多筆資料."); }
                Result = _Query[0];
            }
            catch (Exception ex)
            {
                throw;
            }

            return Result;
        }

    }
}
