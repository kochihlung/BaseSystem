using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DBHelpers;
using SysService.svc;
using SysService.objs.tb;
using SysService.objs.View;
using SysService.objs;

using CustService.CustObjs.view;
using CustService.CustObjs.tb;

namespace CustService.CustRule
{
    public class Route_Rule
    {
        /// <summary>
        /// 取得流程明細
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public List<V_RouteDtl> GetRouteDtl(string SID)
        {
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select  a.SID,
                                           		  a.CODE,
                                           		  a.NAME ,
                                           		  b.SQE,
                                           		  c.SID OPER_SID,
                                           		  c.NAME OPER
                                            from M_ROUTE a
                                            left join M_ROUTEDTL b on a.sid = b.M_ROUTE_ID
                                            left join M_OPER c on b.M_OPER_ID = c.SID
                                     where a.SID='{0}' order by b.SQE", SID);
            var Result = db.ExecuteList<V_RouteDtl>(SqlCmd);

            return Result;
        }

    }
}
