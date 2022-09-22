using SysService.objs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustService.CustObjs.view
{
    public class V_WIPLotInfo : CustObjs.tb.W_WIPLOT
    {
        [EasyUI_DataGrid_Attribute("途程", 4)]
        public string ROUTE { get; set; }

        [EasyUI_DataGrid_Attribute("站點", 5)]
        public string OPER { get; set; }

        [EasyUI_DataGrid_Attribute("產品", 3)]
        public string PROD { get; set; }

        [EasyUI_DataGrid_Attribute("工單", 6)]
        public string WO { get; set; }

        [EasyUI_DataGrid_Attribute("更新人員", 10)]
        public string USERNAME { get; set; }

        

    }
}
