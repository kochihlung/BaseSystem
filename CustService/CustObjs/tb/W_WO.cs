using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class W_WO : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("作業人員ID",false)]
        public string UID { get; set; }

        [EasyUI_DataGrid_Attribute("產品ID",false)]
        public string M_PROD { get; set; }

        [EasyUI_DataGrid_Attribute("途程ID", false)]
        public string M_ROUTE { get; set; }

        [EasyUI_DataGrid_Attribute("數量",6)]
        public int QTY { get; set; }

        [EasyUI_DataGrid_Attribute("工單狀態",5)]
        public string STATUS { get; set; }

    }
}
