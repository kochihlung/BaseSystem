using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysService.objs;
using SysService.objs.tb;

namespace CustService.CustObjs.tb
{
    public class M_FORMSET : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("簽核流程", 3)]
        public string M_ROUTE_ID { get; set; }


    }
}
