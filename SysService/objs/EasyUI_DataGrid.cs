using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs
{
    public class EasyUI_DataGrid_Attribute : Attribute
    {
        public EasyUI_DataGrid_Attribute()
        {
        }
        public EasyUI_DataGrid_Attribute(string n, int o = 999, bool s = true)
        {
            Name = n;
            Show = s;
            OrderNbr = o;
        }
        public EasyUI_DataGrid_Attribute(string n, int o = 999)
        {
            Name = n;
            Show = true;
            OrderNbr = o;
        }
        public EasyUI_DataGrid_Attribute(string n, bool s =true)
        {
            Name = n;
            Show = s;
            OrderNbr = 999;
        }

        public string Name { get; set; }
        public bool Show { get; set; }
        public int OrderNbr { get; set; }

    }
    public class EasyUI_DataGrid
    {
        public EasyUI_DataGrid()
        {
            columns = new List<List<EasyUI_DataGrid_Column>>();
            singleSelect = true;
            fit = true;
        }

        public string title { get; set; }
        public List<List<EasyUI_DataGrid_Column>> columns { get; set; }
        public object data { get; set; }
        public bool singleSelect { get; set; }
        public bool fit { get; set; }
    }

    public class EasyUI_DataGrid_Column
    {
        public string title { get; set; }
        public string field { get; set; }
        public int width { get; set; }
        public int rowspan { get; set; }
        public int colspan { get; set; }
        /// <summary>
        /// Indicate how to align the column data. 'left','right','center' can be used.
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// Indicate how to align the column header. Possible values are: 'left','right','center'. If not assigned, the header alignment is same as data alignment defined via 'align' property. Available since version 1.3.2.
        /// </summary>
        public string halign { get; set; }
        public bool sortable { get; set; }
        /// <summary>
        /// The default sort order, can only be 'asc' or 'desc'. Available since version 1.3.2.
        /// </summary>
        public string order { get; set; }

        public bool resizable { get; set; }
        public bool hidden { get; set; }
        public bool checkbox { get; set; }

        public int OrderNbr { get; set; }

    }
}
