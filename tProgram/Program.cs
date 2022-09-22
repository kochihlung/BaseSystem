using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysService.Rule;
using SysService.svc;

using Newtonsoft.Json;
using SysService.objs.tb;
using System.Net;
using System.IO;


namespace tProgram
{
    class Program
    {
        static void Main(string[] args)
        {

            //var _db = new DBHelper("MSDB");
            //string SqlCmd = string.Format("select * from S_USERINFO", Name, orderby);
            //_db.ExecuteList<Dictionary<string, string>>("SqlCmd");



            //var assem = Type.GetType("tProgram");

            //return;
            //FileStream fsFile = new FileStream(@"M_TEST.cs", FileMode.Create);
            //var charDataValue = "This is test string".ToCharArray();
            //var byDataValue = new byte[charDataValue.Length];

            ////將字符數組轉換成字節數組
            //Encoder ec = Encoding.UTF8.GetEncoder();
            //ec.GetBytes(charDataValue, 0, charDataValue.Length, byDataValue, 0, true);

            ////將指針設定起始位置
            //fsFile.Seek(0, SeekOrigin.Begin);
            ////寫入文件
            //fsFile.Write(byDataValue, 0, byDataValue.Length);

            //fsFile.Close();

            //var a = 1;

            //return;

            //WIP_Rule r = new WIP_Rule();
            ////r.WIP_MoveByCode(new SysService.objs.Frm_WIPMove() { WIPID= "INST12240001K" });

            //string ss = HttpPost("http://localhost:44361/Public/GetControlles", "");


        }

    }

    public static class BaseExt
    {
        public static string To32Str(this int Nbr, int ft = 0)
        {
            string Result = "";

            return Result;
        }

    }
}
