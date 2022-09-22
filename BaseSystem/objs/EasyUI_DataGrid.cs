using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseSystem.objs
{
    public class EasyUI_DataGrid
    {
        public EasyUI_DataGrid()
        {
            columns = new List<List<EasyUI_DataGrid_Column>>();
            frozenColumns = new List<List<EasyUI_DataGrid_Column>>();
            singleSelect = true;
        }
        public List<List<EasyUI_DataGrid_Column>> columns { get; set; }
        public List<List<EasyUI_DataGrid_Column>> frozenColumns { get; set; }
        public object data { get; set; }
        public bool singleSelect { get; set; }

    }

    public class EasyUI_DataGrid_Column
    {
        public EasyUI_DataGrid_Column() { }
        public EasyUI_DataGrid_Column(string ColumnName, string Field)
        {
            title = ColumnName;
            field = Field;
            width = 80;
            rowspan = 0;
            colspan = 0;
            align = "center";
            halign = "center";
            sortable = false;
            resizable = false;
        }
        public string title { get; set; }
        public string field { get; set; }
        public int width { get; set; }
        public int rowspan { get; set; }
        public int colspan { get; set; }
        public string align { get; set; }
        public string halign { get; set; }
        public bool sortable { get; set; }
        public bool resizable { get; set; }

    }

}