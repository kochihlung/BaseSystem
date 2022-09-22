using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using SysService.svc;
using SysService.objs;
using SysService.objs.tb;
using SysService.objs.View;
using CustService.CustObjs.tb;
using CustService.CustObjs.view;

namespace CustService.CustRule
{
    public class WO_Rule
    {
        /// <summary>
        /// 取得工單WIP DataGrid
        /// </summary>
        /// <param name="HOLD_SID"></param>
        /// <returns></returns>
        public EasyUI_DataGrid GetWIPGrid(string SID)
        {
            EasyUI_DataGrid Result = new EasyUI_DataGrid();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"SELECT a.SID
                                                ,a.CODE
                                                ,a.NAME
                                                ,a.REMARK
                                                ,a.UDT
                                                ,a.STATUS
                                                ,b.NAME ROUTE
                                                ,c.NAME OPER
                                                ,d.NAME PROD
                                                ,e.CODE WO
                                                ,a.QTY
                                                ,a.UID
                                                ,a.CUSTSN
                                                ,a.PrevID
                                                ,f.NAME USERNAME
                                            FROM W_WIPLOT a
                                            left join M_ROUTE b on a.M_ROUTE = b.SID
                                            left join M_OPER c on a.M_OPER = c.SID
                                            left join M_PROD d on a.M_PROD =d.SID
                                            left join W_WO e on a.W_WO=e.SID
                                            left join S_USERINFO f on a.UID=f.SID
                                            where W_WO='{0}'", SID);

            var data = db.ExecuteList<V_WIPLotInfo>(SqlCmd);

            Result = data.ToEasyUI_DataGrid<V_WIPLotInfo>();

            Result.columns[0].Find(o => o.field == "CODE").title = "WIP ID";
            Result.columns[0].Find(o => o.field == "NAME").hidden  =true;


            return Result;
        }

        /// <summary>
        /// 取得工單WIP
        /// </summary>
        /// <param name="HOLD_SID"></param>
        /// <returns></returns>
        public List<V_WIPLotInfo> GetWIPList(string SID)
        {
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"SELECT a.SID
                                                ,a.CODE
                                                ,a.NAME
                                                ,a.REMARK
                                                ,a.UDT
                                                ,a.STATUS
                                                ,b.NAME ROUTE
                                                ,c.NAME OPER
                                                ,d.NAME PROD
                                                ,e.CODE WO
                                                ,a.QTY
                                                ,a.UID
                                                ,a.CUSTSN
                                                ,a.PrevID
                                            FROM W_WIPLOT a
                                            left join M_ROUTE b on a.M_ROUTE = b.SID
                                            left join M_OPER c on a.M_OPER = c.SID
                                            left join M_PROD d on a.M_PROD =d.SID
                                            left join W_WO e on a.W_WO=e.SID
                                            where W_WO='{0}'", SID);

            var data = db.ExecuteList<V_WIPLotInfo>(SqlCmd);

            return data;
        }

        /// <summary>
        /// 保存工單資訊
        /// </summary>
        /// <param name="InData"></param>
        /// <returns></returns>
        public W_WO SaveWOInfo(W_WO InData)
        {
            //1.檢查輸入條件是否滿足
            //2.檢查工單是否已存在
            //3.進行新增/修改並記錄History
            //4.回傳工單資訊

            //1.檢查條件
            if (InData.STATUS == "Hold") { throw new Exception("工單狀態為Hold，無法進行修改!"); };
            if (InData.CODE == null) { throw new Exception("工單不得為空值!"); };
            if (InData.M_PROD == null) { throw new Exception("產品不得為空值!"); };
            if (InData.M_ROUTE == null) { throw new Exception("途程不得為空值!"); };
            if (InData.QTY == 0) { throw new Exception("數量不得為0或空值!"); };

            //2.檢查工單是否已存在
            W_WO Result = new W_WO();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Empty;

            SqlCmd = string.Format("select * from W_WO where CODE='{0}'", InData.CODE);
            var WOInfo = db.ExecuteObject<W_WO>(SqlCmd);

            InData.NAME = "Manual";

            //3.進行新增/修改並記錄History
            if (WOInfo == null)
            {//新增工單
                Result = InData.CreateByObject<W_WO>();
            }
            else
            {//修改工單
                Result = InData.UpdateBySID<W_WO>(TransType.Update);
            }

            //4.回傳工單資訊
            return Result;
        }

        /// <summary>
        /// 取得工單清單
        /// </summary>
        /// <param name="WO"></param>
        /// <param name="Status"></param>
        /// <param name="Prod"></param>
        /// <returns></returns>
        public List<V_WOInfo> GetWoList(string WO, string Status, string Prod)
        {
            List<V_WOInfo> Result = new List<V_WOInfo>();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Empty;

            SqlCmd = string.Format(@"select a.*,b.NAME Prod,c.NAME Route,d.NAME UN from W_WO a 
                                     left join M_PROD b on b.SID=a.M_PROD
                                     left join M_ROUTE c on c.SID=a.M_ROUTE
                                     left join S_USERINFO d on d.SID=a.UID
                                     where 1 = 1");

            if (WO != "")
            {
                SqlCmd += string.Format(" and a.CODE like '%{0}%'", WO);
            }
            if (Status != "")
            {
                SqlCmd += string.Format(" and a.STATUS in ({0})", Status);
            }
            if (Prod != "")
            {
                SqlCmd += string.Format(" and a.M_PROD='{0}'", Prod);
            }

            Result = db.ExecuteList<V_WOInfo>(SqlCmd);

            return Result;
        }

        /// <summary>
        /// 取得工單歷史
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public EasyUI_DataGrid GetWOHistory(string SID)
        {
            //1.產生EasyUI_DataGrid
            //2.產生Column
            //3.取得資料
            //4.回傳工單歷史

            //1.產生EasyUI_DataGrid
            EasyUI_DataGrid Result = new EasyUI_DataGrid();

            //2.產生Column
            List<EasyUI_DataGrid_Column> col = new List<EasyUI_DataGrid_Column>();
            col.Add(new EasyUI_DataGrid_Column { field = "CODE", title = "工單號" });
            col.Add(new EasyUI_DataGrid_Column { field = "Prod", title = "產品" });
            col.Add(new EasyUI_DataGrid_Column { field = "Route", title = "途程" });
            col.Add(new EasyUI_DataGrid_Column { field = "STATUS", title = "狀態" });
            col.Add(new EasyUI_DataGrid_Column { field = "QTY", title = "數量" });
            col.Add(new EasyUI_DataGrid_Column { field = "TRANS", title = "執行項目" });
            col.Add(new EasyUI_DataGrid_Column { field = "UN", title = "執行人員" });
            col.Add(new EasyUI_DataGrid_Column { field = "UDT", title = "更新時間" });
            col.Add(new EasyUI_DataGrid_Column { field = "REMARK", title = "備註" });

            Result.columns.Add(col);

            //3.取得資料
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Empty;
            SqlCmd = string.Format(@"select a.*,b.NAME Prod,c.NAME Route,d.NAME UN from W_WO_HT a 
                                     left join M_PROD b on b.SID=a.M_PROD
                                     left join M_ROUTE c on c.SID=a.M_ROUTE
                                     left join S_USERINFO d on d.SID=a.UID
                                     where a.SID = '{0}'", SID);

            var datas = db.ExecuteList<V_WOInfo>(SqlCmd);

            Result.data = datas;

            //4.回傳工單資訊
            return datas.ToEasyUI_DataGrid<V_WOInfo>();
        }
    }
}
