using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using SysService.svc;

namespace SysService.objs.tb
{
    public class _BaseTable
    {
        public _BaseTable()
        {
            UDT = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        [EasyUI_DataGrid_Attribute("系統ID", false)]
        public string SID { get; set; }
        [EasyUI_DataGrid_Attribute("代碼", 1)]
        public string CODE { get; set; }
        [EasyUI_DataGrid_Attribute("名稱", 2)]
        public string NAME { get; set; }
        [EasyUI_DataGrid_Attribute("備註", 998)]
        public string REMARK { get; set; }
        [EasyUI_DataGrid_Attribute("更新時間", 997)]
        public string UDT { get; set; }


    }

    public static class _BaseTableExt
    {
        public static List<ComboboxItem> ToComboboxItem(this List<_BaseTable> ls, bool GetName = false)
        {

            List<ComboboxItem> Result = new List<ComboboxItem>();

            ls.ForEach(o =>
            {
                if (GetName)
                {
                    Result.Add(new ComboboxItem() { id = o.SID, text = string.Format("{0}[{1}]", o.CODE + o.NAME) });
                }
                else
                {
                    Result.Add(new ComboboxItem() { id = o.SID, text = string.Format("{0}", o.NAME) });
                }
            });
            return Result;
        }
        public static EasyUI_DataGrid ToEasyUI_DataGrid<T>(this List<T> ls)
        {
            EasyUI_DataGrid Result = new EasyUI_DataGrid();

            Type t = typeof(T);

            var p = t.GetProperties();

            List<EasyUI_DataGrid_Column> col = new List<EasyUI_DataGrid_Column>();
            col.Add(new EasyUI_DataGrid_Column { field = "SID", title = "SID", OrderNbr = 0, hidden = true });
            foreach (PropertyInfo item in p)
            {
                var objAttributes = item.GetCustomAttributes(true).FirstOrDefault();
                EasyUI_DataGrid_Attribute obj = new EasyUI_DataGrid_Attribute();
                if (objAttributes != null)
                {
                    obj = (EasyUI_DataGrid_Attribute)objAttributes;

                    if (obj.Show)
                    {
                        col.Add(new EasyUI_DataGrid_Column { field = item.Name, title = obj.Name, OrderNbr = obj.OrderNbr });
                    }
                }

            }
            col = col.OrderBy(o => o.OrderNbr).ToList();

            Result.columns.Add(col);


            Result.data = ls;

            return Result;
        }

        public static T ToObject<T>(this _BaseTable obj) where T : new()
        {
            T Result = new T();

            Type To_type = typeof(T);
            Type From_type = obj.GetType();

            foreach (var item in To_type.GetProperties())
            {
                if (From_type.GetProperty(item.Name) != null)
                {
                    To_type.GetProperty(item.Name).SetValue(Result, From_type.GetProperty(item.Name).GetValue(obj));
                }
            }

            return Result;
        }

        public static T GetDataBySID<T>(this _BaseTable obj, string SID) where T : new()
        {
            T Result = new T();
            Type _type = typeof(T);
            DBHelper db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from {0} where SID='{1}'", _type.Name, SID);
            Result = db.ExecuteObject<T>(SqlCmd);
            return Result;
        }
   
    }

}


