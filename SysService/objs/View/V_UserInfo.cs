using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.View
{
    public class V_UserInfo : SysService.objs.tb.S_USERINFO
    {
        public V_UserInfo()
        {
            UserMenu = new List<V_UserMenu>();
        }
        public List<SysService.objs.View.V_UserMenu> UserMenu { get; set; }

        public Position LT
        {
            get { return new Position() { LAT = 23.091776, LON = 120.274614 }; }
        }
        public Position RB
        {
            get { return new Position() { LAT = 23.088099, LON = 120.279411 }; }
        }

        public class Position
        {
            public Double LAT { get; set; }
            public Double LON { get; set; }
        }
    }
}
