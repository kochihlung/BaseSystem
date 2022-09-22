using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysService.objs.View
{
    public class TableColumn
    {
        public string NAME   { get; set; }
        public string COL { get; set; }
        public string REMARK { get; set; }
        public string S_MDSETUP_SID { get; set; }
    }

    public static class TableColumnExt
    {
        public static List<SysService.objs.ComboboxItem> ToComboboxItem(this List<SysService.objs.View.TableColumn> ls )
        {
            List<SysService.objs.ComboboxItem> Result = new List<ComboboxItem>();
            ls.ForEach(o =>
            {
                Result.Add(new ComboboxItem() { id=o.COL,text=o.COL});
            });

            return Result;
        }
    }


}
