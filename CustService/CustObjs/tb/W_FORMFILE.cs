using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;

namespace CustService.CustObjs.tb
{
   public class W_FORMFILE :_BaseTable
    {
        public string W_FORMMAIN_ID { get; set; }
        public string PATH { get; set; }
        public string M_ROUTEDTL_ID { get; set; }
    }
}
