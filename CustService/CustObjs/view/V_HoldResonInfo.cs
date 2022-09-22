using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustService.CustObjs.tb;

namespace CustService.CustObjs.view
{
    public class V_HoldResonInfo :M_HOLDRESON 
    {
        public string TYPECODE { get; set; }

        public string TYPENAME { get; set; }
    }
}
