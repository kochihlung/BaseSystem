using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class M_ROUTEDTL : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("流程ID", false)]
        public string M_ROUTE_ID { get; set; }
        [EasyUI_DataGrid_Attribute("排序", 1)]
        public string SQE { get; set; }
        [EasyUI_DataGrid_Attribute("節點", 2)]
        public string M_OPER_ID { get; set; }

    }
}
