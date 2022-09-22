using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_USERINFO : _BaseTable
    {
        public string PWD { get; set; }
        public string TOKEN { get; set; }
        public string EXPTIME { get; set; }

        public Double LAT { get; set; }
        public Double LON { get; set; }


    }
}
