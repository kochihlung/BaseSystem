using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using SysService.objs;
using SysService.objs.tb;
using SysService.objs.View;
using SysService.svc;

namespace SysService.Rule
{
    public class SN_Rule
    {
        public string GetWIPID()
        {
            //序號編碼為:INST1yyM00000
            //INS(固定碼:鷹士) + T1(區域碼:台南) + yy (年:後兩碼) + M(月) + 00000(流水碼)
            string Result = string.Empty;

            Result = string.Format(@"{0}{1}{2}{3}{4}",
                "INS",
                "T1",
                DateTime.Now.ToString("yy"),
                DateTime.Now.Month.To32Str(1),
                GetNextSEQUENCE("WIP_ID", null, DateTime.Now.ToString("yyyyMM")).To32Str(5)
                );

            return Result;
        }

        public string GetFormID()
        {
            //序號編碼為:INST1yyM00000
            //INS(固定碼:鷹士) + FM(表單固定碼) + yy (年:後兩碼) + M(月) + 00000(流水碼)
            string Result = string.Empty;

            Result = string.Format(@"{0}{1}{2}{3}{4}",
                "INS",
                "FM",
                DateTime.Now.ToString("yy"),
                DateTime.Now.Month.To32Str(1),
                GetNextSEQUENCE("Form_ID", null, DateTime.Now.ToString("yyyyMM")).To32Str(5)
                );

            return Result;
        }

        public List<string> GetBatchWIPID(int SNCount)
        {
            //序號編碼為:INST1yyM00000
            //INS(固定碼:鷹士) + T1(區域碼:台南) + yy (年:後兩碼) + M(月) + 00000(流水碼)
            List<string> Result = new List<string>();

            var ls = GetBatchSEQUENCE("WIP_ID", null, DateTime.Now.ToString("yyyyMM"), SNCount);

            foreach (var item in ls)
            {
                Result.Add(string.Format(@"{0}{1}{2}{3}{4}",
                "INS",
                "T1",
                DateTime.Now.ToString("yy"),
                DateTime.Now.Month.To32Str(1),
                item.To32Str(5)
                ));
            }
            return Result;
        }

        /// <summary>
        /// 取得下一個序號
        /// </summary>
        /// <param name="Code">命名代碼</param>
        /// <param name="Target">生成參考</param>
        /// <param name="Date">日期索引</param>
        /// <returns></returns>
        private int GetNextSEQUENCE(string Code, string Target, string Date)
        {
            int Result = 0;

            string WhereStr = string.Empty;

            if (Target != null)
            {
                WhereStr += string.Format("and INDEXTARGET='{0}'", Target);
            }

            if (Date != null)
            {
                WhereStr += string.Format("and INDEXDATE='{0}'", Date);
            }

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_SNCONTROL where 1=1 and CODE='{0}' {1}", Code, WhereStr);
            var obj = db.ExecuteObject<S_SNCONTROL>(SqlCmd);

            if (obj == null)
            {
                obj = new S_SNCONTROL();
                obj.CODE = Code;
                obj.INDEXTARGET = Target;
                obj.INDEXDATE = Date;
                obj.SEQUENCE = 1;
                obj.CreateByObject<S_SNCONTROL>();
                Result = obj.SEQUENCE;
            }
            else
            {
                obj.SEQUENCE = obj.SEQUENCE + 1;
                obj.UpdateBySID<S_SNCONTROL>(TransType.Update);
                Result = obj.SEQUENCE;
            }

            return Result;
        }

        /// <summary>
        /// 取得一批序號
        /// </summary>
        /// <param name="Code">命名代碼</param>
        /// <param name="Target">生成參考</param>
        /// <param name="Date">日期索引</param>
        /// <param name="Count">取號數</param>
        /// <returns></returns>
        private List<int> GetBatchSEQUENCE(string Code, string Target, string Date, int Count)
        {
            List<int> Result = new List<int>();

            string WhereStr = string.Empty;

            if (Target != null)
            {
                WhereStr += string.Format("and INDEXTARGET='{0}'", Target);
            }

            if (Date != null)
            {
                WhereStr += string.Format("and INDEXDATE='{0}'", Date);
            }

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_SNCONTROL where 1=1 and CODE='{0}' {1}", Code, WhereStr);
            var obj = db.ExecuteObject<S_SNCONTROL>(SqlCmd);

            if (obj == null)
            {
                obj = new S_SNCONTROL();
                obj.CODE = Code;
                obj.INDEXTARGET = Target;
                obj.INDEXDATE = Date;
                for (int i = 1; i < Count; i++)
                {
                    Result.Add(i + 1);
                }
                obj.SEQUENCE = Result.Max();
                obj.CreateByObject<S_SNCONTROL>();
            }
            else
            {
                for (int i = obj.SEQUENCE; i < obj.SEQUENCE + Count; i++)
                {
                    Result.Add(i + 1);
                }
                obj.SEQUENCE = Result.Max();
                obj.UpdateBySID<S_SNCONTROL>(TransType.Update);
            }

            return Result;
        }


    }
}
