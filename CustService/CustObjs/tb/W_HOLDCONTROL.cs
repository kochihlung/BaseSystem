using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class W_HOLDCONTROL:_BaseTable
    {
        [EasyUI_DataGrid_Attribute("鎖定項目", 999)]
        public string HOLD_SID { get; set; }

        [EasyUI_DataGrid_Attribute("鎖定原因", false)]
        public string M_HOLDRESON { get; set; }

        [EasyUI_DataGrid_Attribute("鎖定人員", false)]
        public string UID { get; set; }

        [EasyUI_DataGrid_Attribute("鎖定狀態", 5)]
        public string STATUS { get; set; }

        [EasyUI_DataGrid_Attribute("項目狀態", false)]
        public string FROMSTATUS { get; set; }


    }
}
