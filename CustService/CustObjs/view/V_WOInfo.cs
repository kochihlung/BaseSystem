using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs;

namespace CustService.CustObjs.view
{
    public class V_WOInfo :CustService.CustObjs.tb.W_WO
    {
        [EasyUI_DataGrid_Attribute("產品",3)]
        public string Prod { get; set; }

        [EasyUI_DataGrid_Attribute("途程",4)]
        public string Route { get; set; }

        [EasyUI_DataGrid_Attribute("作業人員",8)]
        public string UN { get; set; }

        [EasyUI_DataGrid_Attribute("交易項目",7)]
        public string TRANS { get; set; }
    }
}
