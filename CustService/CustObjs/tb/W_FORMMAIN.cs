using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;

namespace CustService.CustObjs.tb
{
    public class W_FORMMAIN : _BaseTable
    {
        public string CRT_UID { get; set; }
        public string STATUS { get; set; }
        public string M_ROUTEDTL_ID { get; set; }
        public string M_ROUTE_ID { get; set; }
        public string M_FORMSET_ID { get; set; }
        public string CDT { get; set; }

    }
}
