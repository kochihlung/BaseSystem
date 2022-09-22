using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_MDSETUPDTL : _BaseTable
    {
        public string S_MDSETUP_ID { get; set; }
        public string ISMODLING { get; set; }
        public string SOURCE { get; set; }
        public string READONLY { get; set; }
        public string WIDTH { get; set; }
        public string REQUIRED { get; set; }
        public string ALIGN { get; set; }
        public string TEXT { get; set; }
        public string COLNAME { get; set; }
        public string VAL { get; set; }
        public string UI { get; set; }
    }
}
