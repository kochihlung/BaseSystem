using SysService.objs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustService.Svc
{
   public static class BaseExt
    {
        public static void CreateMainDtl(this CustService.CustObjs.view.B_MainDtl obj)
        {
            var MainType = Type.GetType(obj.MainTable);

            //建立Main的Table物件
            foreach (var p in Type.GetType(obj.MainTable).GetProperties())
            {
                var attr = p.GetCustomAttributes(true).FirstOrDefault();
                if (attr != null)
                {
                    EasyUI_DataGrid_Attribute r = (EasyUI_DataGrid_Attribute)attr;
                    obj.Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = r.Name });
                }
                else
                {
                    obj.Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = p.Name });
                }
            }

            //建立Dtl的Table物件
            foreach (var p in Type.GetType(obj.MainTable + "DTL").GetProperties())
            {
                var attr = p.GetCustomAttributes(true).FirstOrDefault();
                if (attr != null)
                {
                    EasyUI_DataGrid_Attribute r = (EasyUI_DataGrid_Attribute)attr;
                    obj.Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = r.Name });
                }
                else
                {
                    obj.Main.Base.Col.Add(new ModlingSetupDtl { ColName = p.Name, text = p.Name });
                }
            }

        }
    }
}
