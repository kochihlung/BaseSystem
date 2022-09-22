using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.View
{
    public class V_UserMenu
    {
        public V_UserMenu() {
            children = new List<V_UserMenu_children>();
            IsAction = false;
        }
        public string SID { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public string iconCls { get; set; }
        public string url { get; set; }
        public bool IsAction { get; set; }
        public List<V_UserMenu_children> children { get; set; }

    }

    public class V_UserMenu_children
    {
        public string url { get; set; }
        public string text { get; set; }
        public bool IsAction { get { return true; } }
        public bool CheckUser { get { return true; } }
        public string iconCls { get; set; }
        public string MD { get; set; }
        public string SETTYPE { get; set; }

    }
}
