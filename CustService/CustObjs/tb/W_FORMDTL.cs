using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.objs.tb;

namespace CustService.CustObjs.tb
{
   public class W_FORMDTL :_BaseTable
    {
        public string W_FORMMAIN_ID { set; get; }
        public string M_FORMSETDTL_ID { set; get; }
        public string VALUE { set; get; }
    }
}
