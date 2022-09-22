using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysService.objs;

namespace SysService.objs.View
{
    public class B_MainDtl
    {

        public B_MainDtl(string tb_Main)
        {
            MainTable = tb_Main;
            Main = new B_Main();
            Dtl = new B_Dtl();
            Main.Base = new ModlingSetup();
            Dtl.Base = new ModlingSetup();
            System.Reflection.Assembly assy = System.Reflection.Assembly.LoadFrom(System.Web.HttpContext.Current.Server.MapPath("~/bin") + @"\CustService.dll");
            var MainType = assy.GetType("CustService.CustObjs.tb." + tb_Main);
            if (MainType == null) { MainType = Type.GetType("SysService.objs.tb." + tb_Main); };
            Main.Base.Name = tb_Main;
            Main.Base.text = tb_Main;
            //建立Main的Table物件
            foreach (var p in MainType.GetProperties())
            {
                var attr = p.GetCustomAttributes(true).FirstOrDefault();
                if (attr != null)
                {
                    EasyUI_DataGrid_Attribute r = (EasyUI_DataGrid_Attribute)attr;
                    Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = r.Name, Show = !r.Show, sort = r.OrderNbr });
                }
                else
                {
                    Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = p.Name, sort = 9999 });
                }
            }
            Main.Base.Col = Main.Base.Col.OrderBy(o => o.sort).ToList();

            var DtlType = assy.GetType("CustService.CustObjs.tb." + tb_Main + "DTL");
            if (DtlType == null) { DtlType = Type.GetType("SysService.objs.tb." + tb_Main + "DTL"); };
            if (DtlType == null) { return; };
            Dtl.Base.Name = tb_Main + "DTL";
            Dtl.Base.text = tb_Main + "DTL";
            //建立Dtl的Table物件
            foreach (var p in DtlType.GetProperties())
            {
                var attr = p.GetCustomAttributes(true).FirstOrDefault();
                if (attr != null)
                {
                    EasyUI_DataGrid_Attribute r = (EasyUI_DataGrid_Attribute)attr;
                    Dtl.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = r.Name, Show = !r.Show, sort = r.OrderNbr });
                }
                else
                {
                    Dtl.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = p.Name, sort = 9999 });
                }
            }

            Dtl.Base.Col = Dtl.Base.Col.OrderBy(o => o.sort).ToList();
        }
        public string MainTable { get; set; }
        public string Text { get; set; }
        public B_Main Main { get; set; }
        public B_Dtl Dtl { get; set; }

    }



    public class B_Main
    {
        public ModlingSetup Base { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
    public class B_Dtl
    {
        public ModlingSetup Base { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
