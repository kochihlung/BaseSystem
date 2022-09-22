using DBHelpers;
using SysService.objs.tb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysService.svc;

namespace SysService.objs
{
    public class ComboboxItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }

        public List<ComboboxItem> IsBool()
        {
            List<ComboboxItem> IsBool = new List<ComboboxItem>();
            IsBool.Add(new ComboboxItem { id = "0", text = "否" });
            IsBool.Add(new ComboboxItem { id = "1", text = "是" });
            return IsBool;
        }

        public List<ComboboxItem> DataSource()
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select distinct SID,CODE,NAME,REMARK from S_DATASOURCE");

            return _db.ExecuteList<_BaseTable>(_SqlCmd).ToComboboxItem();
        }
        public List<ComboboxItem> UIType()
        {
            return "textbox;文字方塊,Number;數字方塊,Combobox;下拉選單,Time;時間選單,Date;日期選單".ToConboboxItem();
        }

        public List<ComboboxItem> Route()
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            var _db = new DBHelper("MSDB");

            string _SqlCmd = string.Format("select distinct SID,CODE,NAME,REMARK from M_ROUTE");

            return _db.ExecuteList<_BaseTable>(_SqlCmd).ToComboboxItem();
        }

   
    }
}
