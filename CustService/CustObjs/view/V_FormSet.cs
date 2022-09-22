using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustService.CustObjs.view
{
    public class V_FormSet : CustService.CustObjs.tb.M_FORMSETDTL
    {
        public string MAIN_SID { get; set; }
        public string MAIN_CODE { get; set; }
        public string MAIN_NAME { get; set; }
        public string MAIN_REMARK { get; set; }
        public string M_ROUTE_ID { get; set; }
        
    }
}
