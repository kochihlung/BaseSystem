using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_USERINFO : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("備註", 3)]
        public string PWD { get; set; }
        public string TOKEN { get; set; }
        public string EXPTIME { get; set; }
        public int ISPASS { get; set; }

        public Double LAT { get; set; }
        public Double LON { get; set; }


    }
}
