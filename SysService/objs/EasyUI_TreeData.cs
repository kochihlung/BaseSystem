using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs
{
    public class EasyUI_TreeData
    {
        public EasyUI_TreeData()
        {
            children = new List<EasyUI_TreeData_children>();
        }
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public bool IsAction { get; set; }
        public List<EasyUI_TreeData_children> children { get; set; }
    }

    public class EasyUI_TreeData_children
    {
        public EasyUI_TreeData_children()
        {
            state = "open";
        }
        public string id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string iconCls { get; set; }
        public bool IsAction { get; set; }
        public string UDT { get; set; }
        public object CustObj {get;set;}
    }
}
