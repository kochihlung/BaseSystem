using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class M_SIGNOWNERDTL :_BaseTable
    {
        [EasyUI_DataGrid_Attribute("主表ID", false)]
        public string M_SIGNOWNER_ID { get; set; }
        [EasyUI_DataGrid_Attribute("用戶", 3)]
        public string S_USERINFO_ID { get; set; }
    }
}
