using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.tb
{
    public class S_MENU : _BaseTable
    {
        public string ISACTION { get; set; }
        public bool IsPublic { get; set; }
        public string ICONCLS { get; set; }
        public string URL { get; set; }
    }
}
