using SysService.objs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustService.CustObjs.view
{
    public class V_HoldControlInfo : CustService.CustObjs.tb.W_HOLDCONTROL
    {
        [EasyUI_DataGrid_Attribute("鎖定人員", 8)]
        public string UserName { get; set; }

        [EasyUI_DataGrid_Attribute("鎖定代碼", 4)]
        public string ReasonCode { get; set; }

        [EasyUI_DataGrid_Attribute("鎖定原因", 5)]
        public string ReasonName { get; set; }

        [EasyUI_DataGrid_Attribute("類型代碼", false)]
        public string ReasonTypeCode { get; set; }

        [EasyUI_DataGrid_Attribute("類型", false)]
        public string ReasonTypeName { get; set; }
    }
}
