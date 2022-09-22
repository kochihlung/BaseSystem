using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysService.objs.tb;
using SysService.objs.View;
using SysService.objs;
using SysService.svc;
using DBHelpers;
using System.Collections.Specialized;
using SysService.Rule;
using CustService.CustObjs.tb;
using CustService.CustObjs.view;

namespace CustService.CustRule
{
    public class Form_Rule
    {
        public W_FORMMAIN GetFormMainSetBase(string FormSetID)
        {
            var Result = new W_FORMMAIN();
            DBHelper db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.* ,b.SID M_ROUTEDTL_ID,a.SID M_FORMSET_ID
                                            from M_FORMSET a 
                                            left join M_ROUTEDTL b on a.M_ROUTE_ID = b.M_ROUTE_ID and b.SQE=1
                                            where a.SID='{0}'", FormSetID);
            Result = db.ExecuteObject<W_FORMMAIN>(SqlCmd);

            return Result;
        }

        public V_FormInfo CreateForm(V_FormInfo Info)
        {
            //1.產生表單ID
            //2.產生FormMain物件
            //3.產生FormDtl物件
            //4.產生FormFile物件
            //4.產生FormFile物件

            List<object> CreateList = new List<object>();

            //產生表單ID
            var FormID = new SN_Rule().GetFormID();
            Info.CODE = FormID;


            //2.產生FormMain物件
            var _FormMain = GetFormMainSetBase(Info.SID);
            _FormMain.CODE = FormID;
            _FormMain.CRT_UID = Info.UID;
            _FormMain.STATUS = "Wait";
            _FormMain.NAME = Info.NAME;
            _FormMain.SID = BaseExt.GetSystemGUID();
            _FormMain.UDT = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            _FormMain.CDT = DateTime.Now.ToString("yyyy/MM/dd");
            //_FormMain.M_ROUTEDTL_ID = "Create";
            CreateList.Add(_FormMain);//加入創建集合

            //3.產生FormDtl物件
            foreach (var item in Info.Items)
            {
                var r = new W_FORMDTL();
                r.M_FORMSETDTL_ID = item.SID;
                r.W_FORMMAIN_ID = _FormMain.SID;
                r.VALUE = item.Value;
                r.CODE = _FormMain.CODE;
                r.NAME = item.NAME;
                CreateList.Add(r);//加入創建集合
            }

            //4.產生FormFile物件
            string FoldPath = string.Empty;
            if (Info.Files.Count > 0)
            {
                FoldPath = string.Format(@"{0}\{1}\Create", Info.Files[0].Path, _FormMain.CODE);
            }
            foreach (var item in Info.Files)
            {
                var dir = new System.IO.DirectoryInfo(FoldPath);
                if (!dir.Exists) { dir.Create(); };

                var r = new W_FORMFILE();
                r.PATH = string.Format(@"{0}\{1}", FoldPath, item.FileBase.FileName);
                r.W_FORMMAIN_ID = _FormMain.SID;
                r.CODE = _FormMain.CODE;
                r.NAME = item.FileBase.FileName;
                r.M_ROUTEDTL_ID = "Create";
                CreateList.Add(r);//加入創建集合
                item.FileBase.SaveAs(r.PATH);//保存檔案
            }
            Info.Files.Clear();

            try
            {
                CreateList.CreateByList();
            }
            catch (Exception ex)
            {
                var dir = new System.IO.DirectoryInfo(FoldPath);
                if (dir.Exists) { dir.Delete(true); };
                throw new Exception(ex.Message);
            }


            return Info;
        }
        public V_FormInfo SignForm(V_FormInfo Info)
        {
            //1.修改FormMain狀態
            //2.寫入簽核記錄
            //3.保存附檔

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;
            try
            {
                //1.修改FormMain狀態
                var _Main = new W_FORMMAIN().GetDataBySID<W_FORMMAIN>(Info.SID);
                

                var _SourceDtl = _Main.M_ROUTEDTL_ID.ToString();
                //取得下一節點
                string SqlCmd = string.Format(@"select a.* from M_ROUTEDTL a inner join
                                                    (select M_ROUTE_ID,sqe +1 SQE from M_ROUTEDTL where SID ='{0}') b
                                                    on a.M_ROUTE_ID = b.M_ROUTE_ID and a.SQE=b.SQE", _Main.M_ROUTEDTL_ID);
                var _NextDtl = _db.ExecuteObject<M_ROUTEDTL>(SqlCmd);
                string HID = string.Empty;
                if (_NextDtl != null)
                {
                    _Main.M_ROUTEDTL_ID = _NextDtl.SID;
                    _Main.UpdateBySID<W_FORMMAIN>(_Cmd, TransType.UserSign, out HID);
                    //更新站點
                }
                else
                {
                    _Main.STATUS = "Closed";
                    _Main.M_ROUTEDTL_ID = "/";
                    _Main.UpdateBySID<W_FORMMAIN>(_Cmd, TransType.Close, out HID);
                }

                //2.寫入簽核記錄
                var _Sign = new W_FORMSIGN();
                _Sign.REMARK = Info.REMARK;
                _Sign.M_ROUTEDTL_ID = _SourceDtl;
                _Sign.M_USERINFO_ID = Info.UID;
                _Sign.W_FORMMAIN_ID = Info.SID;
                _Sign.W_FORMMAIN_HID = HID;
                _Sign.RESULT = "Approv";
                _Sign.CreateByObject<W_FORMSIGN>(_Cmd);

                //3.保存附檔
                string FoldPath = string.Empty;
                if (Info.Files.Count > 0)
                {
                    FoldPath = string.Format(@"{0}\{1}\{2}", Info.Files[0].Path, _Main.CODE, _SourceDtl);
                }
                foreach (var item in Info.Files)
                {
                    var dir = new System.IO.DirectoryInfo(FoldPath);
                    if (!dir.Exists) { dir.Create(); };

                    var r = new W_FORMFILE();
                    r.PATH = string.Format(@"{0}\{1}", FoldPath, item.FileBase.FileName);
                    r.W_FORMMAIN_ID = _Main.SID;
                    r.CODE = _Main.CODE;
                    r.NAME = item.FileBase.FileName;
                    r.M_ROUTEDTL_ID = _SourceDtl;
                    r.CreateByObject<W_FORMFILE>(_Cmd);
                    item.FileBase.SaveAs(r.PATH);//保存檔案
                }
                Info.Files.Clear();

                _Tran.Commit();
                _Conn.Close();//關閉連線
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return Info;
        }

        public List<V_FormInfo> GetSignForm(string UID)
        {
            var Result = new List<V_FormInfo>();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.SID,b.NAME,a.CODE,a.STATUS,a.CRT_UID UID,a.M_ROUTEDTL_ID,a.REMARK from W_FORMMAIN a
                                            left join M_formSet b on a.M_FORMSET_ID=b.sid
                                            left join M_ROUTEDTL c on c.sid=a.M_ROUTEDTL_ID
                                            where c.M_OPER_ID in (
                                            select b.M_OPER_ID from M_SIGNOWNERDTL a 
                                            left join M_SIGNOWNER b on a.M_SIGNOWNER_ID=b.sid
                                            where a.S_USERINFO_ID='{0}')", UID);

            Result = db.ExecuteList<V_FormInfo>(SqlCmd);
            return Result;
        }
        public List<EasyUI_TreeData> GetSignFormGroupByFormSet()
        {
            var Result = new List<EasyUI_TreeData>();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.SID,a.CODE,c.NAME,a.REMARK,a.STATUS,a.M_ROUTEDTL_ID,b.name UID,c.name FORMTYPE,a.M_FORMSET_ID,a.UDT,a.CDT
                                            from W_FORMMAIN a 
                                            left join S_USERINFO b on a.crt_UID=b.sid
                                            left join M_FORMSET c on a.M_FORMSET_ID=c.sid
                                            ");

            var datas = db.ExecuteList<V_FormInfo>(SqlCmd);

            var types = datas.Select(o => new { o.FORMTYPE, o.M_FORMSET_ID }).Distinct().ToList();

            //產生主節點
            foreach (var o in types)
            {
                Result.Add(new EasyUI_TreeData { id = o.M_FORMSET_ID, text = o.FORMTYPE, IsAction = false });
            }

            //產生子節點
            foreach (var o in Result)
            {
                var d = datas.Where(q => q.M_FORMSET_ID == o.id).ToList();
                foreach (var p in d)
                {
                    o.children.Add(new EasyUI_TreeData_children { id = p.SID, text = string.Format("{0}_{1}", p.CODE, p.UID), IsAction = true, UDT = p.UDT, CustObj = p });
                }
                o.children = o.children.OrderBy(q => q.UDT).ToList();
            }

            return Result;
        }

        public List<EasyUI_TreeData> QueryForms(NameValueCollection Form, List<V_FormItem> Items)
        {
            //主表資訊查詢條件
            List<string> lsWhere = new List<string>();
            if (Form.Get("FormCode") != "")
            {
                lsWhere.Add(string.Format(@"and a.CODE like '%{0}%' ", Form.Get("FormCode")));
            }

            if (Form.Get("Status") != "")
            {
                var _Status = Form.Get("Status").Split(',');
                lsWhere.Add(string.Format(@"and a.Status in ('{0}') ", string.Join("','", _Status)));
            }
            if (Form.Get("Step") != "")
            {
                var _Step = Form.Get("Step").Split(',');
                lsWhere.Add(string.Format(@"and e.NAME in ('{0}') ", string.Join("','", _Step)));
            }
            if (Form.Get("UID") != "")
            {
                lsWhere.Add(string.Format(@"and b.CODE like '%{0}%' ", Form.Get("UID")));
            }
            if (Form.Get("NAME") != "")
            {
                lsWhere.Add(string.Format(@"and b.NAME like '%{0}%' ", Form.Get("NAME")));
            }
            if (Form.Get("CDT") != "")
            {
                lsWhere.Add(string.Format(@"and convert(varchar, a.CDT , 111) like '%{0}%' ", Form.Get("CDT")));
            }

            //項目資料查詢條件
            List<string> InnerTable = new List<string>();
            string ItemQueyr = "";
            if (Items.Count > 0)
            {
                int ind = 1;
                foreach (var i in Items)
                {
                    InnerTable.Add(string.Format(@"
                                        (select {3}1.W_FORMMAIN_ID 
                                          from W_FORMDTL {3}1 
                                          left join M_FORMSETDTL {3}2 
                                               on {3}1.M_FORMSETDTL_ID={3}2.SID where {3}1.M_formsetdtl_id ='{0}' and {3}2.M_FORMSET_ID ='{1}' and {3}1.value like '%{2}%'
                                        ) ", i.SID,i.M_FORMSET_ID,i.Value,"tb" + ind.ToString()));
                    ind++;
                }
                ItemQueyr = string.Format("inner join({0}) f on f.W_FORMMAIN_ID = a.sid", string.Join(" UNION ", InnerTable)) ;
            }


            var Result = new List<EasyUI_TreeData>();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.SID,a.CODE,c.NAME,a.REMARK,a.STATUS,a.M_ROUTEDTL_ID,b.name UID,c.name FORMTYPE,a.M_FORMSET_ID,a.UDT,a.CDT
                                            from W_FORMMAIN a 
                                            left join S_USERINFO b on a.crt_UID=b.sid
                                            left join M_FORMSET c on a.M_FORMSET_ID=c.sid 
											left join M_ROUTEDTL d on d.sid = a.M_ROUTEDTL_ID
											left join M_OPER e on e.sid=d.M_OPER_ID 
                                            {1}
                                            where 1=1 {0}
                                            ", string.Join(" ", lsWhere), ItemQueyr);

            var datas = db.ExecuteList<V_FormInfo>(SqlCmd);

            var types = datas.Select(o => new { o.FORMTYPE, o.M_FORMSET_ID }).Distinct().ToList();

            //產生主節點
            foreach (var o in types)
            {
                Result.Add(new EasyUI_TreeData { id = o.M_FORMSET_ID, text = o.FORMTYPE, IsAction = false });
            }

            //產生子節點
            foreach (var o in Result)
            {
                var d = datas.Where(q => q.M_FORMSET_ID == o.id).ToList();
                foreach (var p in d)
                {
                    o.children.Add(new EasyUI_TreeData_children { id = p.SID, text = string.Format("{0}_{1}", p.CODE, p.UID), IsAction = true, UDT = p.UDT, CustObj = p });
                }
                o.children = o.children.OrderBy(q => q.UDT).ToList();
            }

            return Result;
        }

        public V_FormInfo GetFormDataBySID(string SID)
        {
            //1.取得項目及資料
            //2.取得檔案
            //3.取得簽核流程
            //4.取得簽核記錄

            var Result = new V_FormInfo();
            var db = new DBHelper("MSDB");

            //1.取得項目及資料
            string SqlCmd = string.Format(@"select b.*,a.VALUE from W_formdtl a
                                            left join M_FORMSETDTL b on b.sid=a.M_FORMSETDTL_id
                                            where w_formmain_id='{0}' 
                                            order by b.SORT", SID);
            Result.Items = db.ExecuteList<V_FormItem>(SqlCmd);

            //2.取得檔案
            var file = db.ExecuteList<W_FORMFILE>(string.Format("select * from w_formfile where W_FormMain_id='{0}'", SID));


            //3.取得簽核流程
            file.ForEach(o =>
            {
                var obj = o.ToObject<V_FormFile>();
                obj.Path = o.PATH;
                obj.FileType = "";
                obj.url = string.Format("{0}/{1}/{2}/{3}", "FormFile", obj.CODE, obj.M_ROUTEDTL_ID, obj.NAME);
                Result.Files.Add(obj);
            });

            Result.Route = db.ExecuteList<V_RouteDtl>(string.Format(@"select a.*,b.name OPER from M_routedtl a
                                                         left join M_oper b on a.M_OPER_ID = b.sid
                                                         left join W_FormMAIN c on c.M_ROUTE_ID = a.M_ROUTE_ID
                                                         where c.sid = '{0}' order by sqe", SID));

            //4.取得簽核記錄
            SqlCmd = string.Format(@"select * from W_FORMSIGN where W_FORMMAIN_ID='{0}'", SID);
            Result.Sign = db.ExecuteList<V_FormSign>(SqlCmd);

            return Result;
        }

        public List<V_FormInfo> GetForms()
        {
            List<V_FormInfo> Result = new List<V_FormInfo>();

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.sid Main_SID,a.M_ROUTE_ID,a.code Main_Code,a.name Main_Name,a.remark Main_Remark,b.* from M_FORMSET a left join M_FORMSETDTL b on a.SID=b.M_FORMSET_ID");
            var datas = db.ExecuteList<V_FormSet>(SqlCmd);

            var MainRow = (from row in datas
                           select new { row.MAIN_SID, row.MAIN_CODE, row.MAIN_NAME, row.MAIN_REMARK }).Distinct().OrderBy(o => o.MAIN_CODE);

            foreach (var item in MainRow)
            {
                Result.Add(new V_FormInfo() { SID = item.MAIN_SID, CODE = item.MAIN_CODE, NAME = item.MAIN_NAME, REMARK = item.MAIN_REMARK });
            }

            foreach (var m in Result)
            {
                var items = from row in datas
                            where row.MAIN_SID == m.SID
                            select row;
                foreach (var i in items)
                {
                    m.Items.Add(i.ToObject<V_FormItem>());
                }
                m.Items = m.Items.OrderBy(o => o.SORT).ToList();
            }

            foreach (var m in Result)
            {
                //string SqlCmd = string.Format(@"select * from M_ROUTEDTL where M_ROUTE_ID='{0}'", m.Route);

                var items = (from row in datas
                             where row.MAIN_SID == m.SID
                             select row.M_ROUTE_ID).Distinct().ToList();
                foreach (var i in items)
                {
                    m.Route.AddRange(new Route_Rule().GetRouteDtl(i));
                }
            }

            return Result;

        }
    }
}
