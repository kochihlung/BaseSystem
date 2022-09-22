using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class M_SIGNOWNER : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("節點", 3)]
        public string M_OPER_ID { get;set;}
    }
}

