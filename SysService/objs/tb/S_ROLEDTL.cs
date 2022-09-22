using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_ROLEDTL : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("權限ID", false)]
        public string S_ROLE_ID { get; set; }

        [EasyUI_DataGrid_Attribute("功能名稱", 3)]
        public string S_MENU_ID { get; set; }
    }
}
