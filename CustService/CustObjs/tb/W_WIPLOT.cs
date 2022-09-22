using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class W_WIPLOT :_BaseTable
    {

        [EasyUI_DataGrid_Attribute("狀態", 7)]
        public string STATUS { get; set; }
        
        [EasyUI_DataGrid_Attribute("途程", false)]
        public string M_ROUTE { get; set; }

        [EasyUI_DataGrid_Attribute("站點", false)]
        public string M_OPER { get; set; }
        
        [EasyUI_DataGrid_Attribute("產品", false)]
        public string M_PROD { get; set; }

        [EasyUI_DataGrid_Attribute("工單", false)]
        public string W_WO { get; set; }

        [EasyUI_DataGrid_Attribute("數量", 8)]
        public decimal QTY { get; set; }

        [EasyUI_DataGrid_Attribute("更新人員", false)]
        public string UID { get; set; }

        [EasyUI_DataGrid_Attribute("客製序號", 9)]
        public string CUSTSN { get; set; }

        [EasyUI_DataGrid_Attribute("前一階ID", false)]
        public string PrevID { get; set; }
    }
}
