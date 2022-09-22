using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SysService.objs.tb;
using DBHelpers;
using SysService.svc;
using SysService.objs.View;
using SysService.objs;

namespace CustService.CustRule
{
    public class WIP_CheckRule
    {
        private string WIPCODE = string.Empty;
        public WIP_CheckRule(string Code)
        {
            WIPCODE = Code;
        }

        public void WO_Status()
        {
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select b.status  from W_WIPLOT a left join W_WO b on a.W_WO=b.sid where a.CODE = '{0}'", WIPCODE);
            var r = db.ExecuteScalar<string>(SqlCmd);
            if (r == "Hold") { throw new Exception("工單狀態為Hold，不可過帳!"); }
        }

        public void WIP_Status()
        {
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select status  from W_WIPLOT  where CODE = '{0}'", WIPCODE);
            var r = db.ExecuteScalar<string>(SqlCmd);
            if (r == "Hold") { throw new Exception("WIP狀態為Hold，不可過帳!"); }
            if (r == "Complete") { throw new Exception("WIP狀態為Hold，不可過帳!"); }
            if (r == "Terminate") { throw new Exception("WIP狀態為Terminate，不可過帳!"); }
        }
    }
}
