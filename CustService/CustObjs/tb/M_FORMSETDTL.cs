using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysService.objs.tb;
using SysService.objs;

namespace CustService.CustObjs.tb
{
    public class M_FORMSETDTL : _BaseTable
    {
        [EasyUI_DataGrid_Attribute("M_FORMSET_ID", false)]
        public string M_FORMSET_ID { get; set; }

        [EasyUI_DataGrid_Attribute("參數類型", 4)]
        public string CONTYPE { get; set; }

        [EasyUI_DataGrid_Attribute("資料來源", 5)]
        public string SOURCE { get; set; }

        [EasyUI_DataGrid_Attribute("必填項", 6)]
        public bool REQUIRED { get; set; }


        [EasyUI_DataGrid_Attribute("排序", 7)]
        public int SORT { get; set; }

    }
}
