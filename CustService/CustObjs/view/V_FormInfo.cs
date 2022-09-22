using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using CustService.CustObjs.tb;

namespace CustService.CustObjs.view
{
    public class V_FormInfo
    {
        public V_FormInfo()
        {
            Items = new List<V_FormItem>();
            Files = new List<V_FormFile>();
            Route = new List<V_RouteDtl>();
        }
        public string SID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string FORMNO { get; set; }
        public string REMARK { get; set; }
        public string STATUS { get; set; }
        public string M_ROUTEDTL_ID { get; set; }
        public string UID { get; set; }
        public string FORMTYPE { get; set; }
        public string M_FORMSET_ID { get; set; }
        public string UDT { get; set; }
        public string CDT { get; set; }


        public List<V_FormFile> Files { get; set; }
        public List<V_FormItem> Items { get; set; }
        public List<V_RouteDtl> Route { get; set; }
        public List<V_FormSign> Sign { get; set; }

    }

    public class V_FormItem : M_FORMSETDTL
    {
        public string Value { get; set; }

      
    }

    public class V_FormFile : SysService.objs.tb._BaseTable
    {
        public HttpPostedFileBase FileBase { get; set; }
        public string Path { get; set; }
        public string url { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        public string M_ROUTEDTL_ID { get; set; }
    }

    public class V_FormSign :CustService.CustObjs.tb.W_FORMSIGN
    {

    }

}
