using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_USERROLEDTL : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("用戶權限ID", false)]
        public string S_USERROLE_ID { get; set; }
    }
}
