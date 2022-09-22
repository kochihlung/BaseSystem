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
using SysService.Rule;

namespace CustService.CustRule
{
    public class WIP_Rule
    {
        public V_WIPLotInfo CreateWIP(string UID, string WO_SID)
        {
            //檢查點
            //1.工單狀態
            //2.投產後數量是否大於工單量

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select * from W_WO a where a.SID='{0}'", WO_SID);
            var WO = db.ExecuteObject<W_WO>(SqlCmd);
            if (WO == null) { throw new Exception("取得工單資訊失敗!"); }

            //1-1.工單是否為Hold
            if (WO.STATUS == "Hold") { throw new Exception("工單鎖定中，無法投產!"); }
            //1-2.工單是否為Close
            if (WO.STATUS == "Close") { throw new Exception("工單已關閉，無法投產!"); }
            //1-3.工單是否為Complete
            if (WO.STATUS == "Complete") { throw new Exception("工單已完工，無法投產!"); }

            //2.投產後數量是否大於工單量
            var WIPList = new WO_Rule().GetWIPList(WO_SID);//取得WIP數量
            var Query_WIPList = from r in WIPList
                                where (r.STATUS != "Scrap" & r.STATUS != "")
                                select r;
            if (Query_WIPList.Count() + 1 > WO.QTY) { throw new Exception("工單已達投產目標，無法投產!"); }


            var RouteDtl = new Route_Rule().GetRouteDtl(WO.M_ROUTE);
            if (RouteDtl == null) { throw new Exception("取得流程明細失敗!"); }

            RouteDtl.OrderBy(o => o.SQE).ToList();

            W_WIPLOT NewWIP = new W_WIPLOT()
            {
                CODE = new SN_Rule().GetWIPID(),
                NAME = "",
                UID = UID,
                STATUS = "Start",
                QTY = 1,
                M_OPER = RouteDtl[0].OPER_SID,
                M_PROD = WO.M_PROD,
                M_ROUTE = WO.M_ROUTE,
                W_WO = WO.SID
            };

            NewWIP.CreateByObject<W_WIPLOT>();

            //更新工單狀態
            if(WO.STATUS == "Create")
            {
                WO.STATUS = "WIP";
                WO.UpdateBySID<W_WO>(TransType.Update);
            }

            return GetWIPInfoByCode(NewWIP.CODE);
        }

        public V_WIPLotInfo GetWIPInfoByCode(string Code)
        {
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"SELECT a.SID
                                                ,a.CODE
                                                ,a.NAME
                                                ,a.REMARK
                                                ,a.UDT
                                                ,a.STATUS
                                                ,a.M_ROUTE
                                                ,b.NAME ROUTE
                                                ,a.M_OPER
                                                ,c.NAME OPER
                                                ,a.M_PROD
                                                ,d.NAME PROD
                                                ,a.W_WO
                                                ,a.QTY
                                                ,f.NAME USERNAME
                                                ,a.CUSTSN
                                                ,a.PrevID
                                                ,e.CODE WO
                                            FROM W_WIPLOT a
                                            left join M_ROUTE b on a.M_ROUTE = b.SID
                                            left join M_OPER c on a.M_OPER = c.SID
                                            left join M_PROD d on a.M_PROD =d.SID
                                            left join W_WO e on a.W_WO=e.SID 
                                            left join S_USERINFO f on a.UID=f.SID
                                            where a.CODE='{0}'", Code);
            var Result = db.ExecuteObject<V_WIPLotInfo>(SqlCmd);
            if (Result == null) { throw new Exception("找不到指定的WIP資料!"); }

            return Result;

        }

        public V_WIPLotInfo WIP_MoveOut(Frm_WIPMove data)
        {
            var WIPInfo = GetWIPInfoByCode(data.WIPID);

            WIP_CheckRule CheckRule = new WIP_CheckRule(WIPInfo.CODE);

            #region 檢查項目
            //檢查工單狀態
            CheckRule.WO_Status();

            //檢查WIP狀態
            CheckRule.WIP_Status();

            //檢查其他項目
            #endregion

            //取得下一站
            var Route = new Route_Rule().GetRouteDtl(WIPInfo.M_ROUTE);

            var NowOper = from r in Route
                           where (r.OPER_SID == WIPInfo.M_OPER)
                           select r;

            if (NowOper.Count() != 1) { throw new Exception("WIP站點於途程內不為1筆!"); }

            var NextOper = from r in Route
                           where (r.SQE == NowOper.First().SQE + 1)
                           select r;
            if (NextOper.Count() != 1) { throw new Exception("找不到下一站點資料"); }

           var WIP = WIPInfo.ToObject<W_WIPLOT>();

            //更新WIP站點
            WIP.M_OPER = NextOper.First().OPER_SID;
            WIP.UID = data.UID;
            WIP.STATUS = "WIP";
            WIP.UpdateBySID<W_WIPLOT>(TransType.MoveOut);

            return GetWIPInfoByCode(data.WIPID);
        }


    }
}
