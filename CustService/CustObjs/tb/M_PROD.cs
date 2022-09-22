using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;


namespace CustService.CustObjs.tb
{
    public class M_PROD :_BaseTable
    {
        [EasyUI_DataGrid_Attribute("產品類型",4)]
        public string M_PRODTYPE { get; set; }
    }
}
