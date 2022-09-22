using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;

namespace SysService.objs.View
{
    public class V_MDSETU : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("設定類型",4)]
        public string SETTYPE { get; set; }
    }
}
