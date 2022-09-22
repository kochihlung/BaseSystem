using SysService.objs;
using SysService.objs.tb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using System.Data.Common;

namespace SysService.svc
{
    public static class BaseExt
    {
        public static string AdminToken = string.Empty;
        public static  bool IsExisted(this DBHelper db, string SqlCmd)
        {
            bool Result = false;
          
            var dr = db.ExecuteReader(SqlCmd);
            Result = dr.HasRows;

            return Result;
        }
        public static List<Dictionary<string, object>> SelectDictionary(this DBHelper db, string SqlCmd)
        {
            List<Dictionary<string, object>> Result = new List<Dictionary<string, object>>();
            try
            {
                
                var dr = db.ExecuteReader(SqlCmd);
                List<string> Columns = new List<string>();

                for (int c = 0; c < dr.FieldCount; c++)
                {
                    Columns.Add(dr.GetName(c));
                }

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        foreach (var col in Columns)
                        {
                            row.Add(col, dr[col]);
                        }
                        Result.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + string.Format("[{0}]", SqlCmd));
            }

            return Result;
        }

        public static S_USERINFO GetUserInfo(string Token)
        {
            if (AdminToken == Token)
            {
                return new S_USERINFO()
                {
                    NAME = "系統管理者",
                    CODE = "Admin",
                    TOKEN = Token,
                    EXPTIME = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd HH:mm:ss")
                };
            }

            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from S_USERINFO where TOKEN='{0}'", Token);
            var Result = db.ExecuteObject<S_USERINFO>(SqlCmd);
            if (Result == null)
            {
                throw new Exception("找不到對應的授權用戶，請確認是否登入取得授權!");
            }
            DateTime ExpTime = DateTime.Parse(Result.EXPTIME);

            if (ExpTime < DateTime.Now)
            {
                throw new Exception("授權已過期，請重新登入!");
            }

            return Result;
        }
        /// <summary>
        /// 將物件新增至Table中，使用傳入的Cmd
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="obj">新增物件</param>
        /// <returns></returns>
        public static T CreateByObject<T>(this T obj, DbCommand _Cmd) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);
            List<string> _lsColumn = new List<string>();

            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            //填入SID
            string _SID = BaseExt.GetSystemGUID();

            Result.GetType().GetProperty("SID").SetValue(Result, _SID);

            var _Type = typeof(T);
            List<string> col = new List<string>();
            List<string> Val = new List<string>();

            var _Properite = _Type.GetProperties();

            foreach (var item in _Properite)
            {
                col.Add(item.Name);
                var v = _Type.GetProperty(item.Name).GetValue(obj);
                if (v != null)
                {
                    Val.Add(v.ToString());
                }
                else
                {
                    Val.Add("");
                }
            }

            _Cmd.CommandText = string.Format("insert into {0}({1}) values('{2}')", _type.Name, string.Join(",", col), string.Join("','", Val));
            _Cmd.ExecuteNonQuery();



            _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Create','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, BaseExt.GetSystemGUID(), _SID);
            _Cmd.ExecuteNonQuery();

            return Result;
        }
        /// <summary>
        /// 將物件新增至Table中
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="obj">新增物件</param>
        /// <returns></returns>
        public static T CreateByObject<T>(this T obj) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);
            List<string> _lsColumn = new List<string>();

            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                //建立交易物件

                //填入SID
                string _SID = BaseExt.GetSystemGUID();

                Result.GetType().GetProperty("SID").SetValue(Result, _SID);

                try
                {
                    var _Type = typeof(T);
                    List<string> col = new List<string>();
                    List<string> Val = new List<string>();

                    var _Properite = _Type.GetProperties();

                    foreach (var item in _Properite)
                    {
                        col.Add(item.Name);
                        var v = _Type.GetProperty(item.Name).GetValue(obj);
                        if (v != null)
                        {
                            Val.Add(v.ToString());
                        }
                        else
                        {
                            Val.Add("");
                        }
                    }

                    _Cmd.CommandText = string.Format("insert into {0}({1}) values('{2}')", _type.Name, string.Join(",", col), string.Join("','", Val));
                    _Cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    var _Msg = ex.Message + string.Format("[{0}]", _Cmd.CommandText);
                    _Conn.Close();
                    throw new Exception(_Msg);
                }

                _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Create','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, BaseExt.GetSystemGUID(), _SID);
                _Cmd.ExecuteNonQuery();

                _Tran.Commit();
                _Conn.Close();//關閉連線
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return Result;
        }

        /// <summary>
        /// 批次將物件新增至Table中
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="obj">新增物件</param>
        /// <returns></returns>
        public static void CreateByList(this List<object> ls)
        {
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                foreach (var r in ls)
                {
                    var obj = r.GetType();
                    List<string> col = new List<string>();
                    List<string> Val = new List<string>();

                    string _SID = string.Empty;
                    if (obj.GetProperty("SID").GetValue(r) == null)
                    {
                        _SID = BaseExt.GetSystemGUID();
                        obj.GetProperty("SID").SetValue(r, _SID);
                    }
                    else
                    {
                        _SID = obj.GetProperty("SID").GetValue(r).ToString();
                    }

                    foreach (var p in obj.GetProperties())
                    {
                        col.Add(p.Name);

                        var v = obj.GetProperty(p.Name).GetValue(r);
                        if (v != null)
                        {
                            Val.Add(v.ToString());
                        }
                        else
                        {
                            Val.Add("");
                        }
                    }


                    _Cmd.CommandText = string.Format("insert into {0}({1}) values('{2}')", obj.Name, string.Join(",", col), string.Join("','", Val));
                    _Cmd.ExecuteNonQuery();

                    _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'Create','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", col), obj.Name + "_HT", obj.Name, BaseExt.GetSystemGUID(), _SID);
                    _Cmd.ExecuteNonQuery();
                }
                _Tran.Commit();
                _Conn.Close();//關閉連線
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }


        }
        /// <summary>
        /// 更新物件，使用傳入的Cmd
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="obj">物件</param>
        /// <param name="Tran">交易名稱</param>
        /// <returns></returns>
        public static T UpdateBySID<T>(this T obj, DbCommand _Cmd, TransType Tran, out string HID) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);

            List<string> _lsColumn = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }


            string _SID = obj.GetType().GetProperty("SID").GetValue(obj).ToString();

            obj.GetType().GetProperty("UDT").SetValue(obj, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            List<string> _ls = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                if (item.GetValue(obj) != null)
                {
                    _ls.Add(string.Format("{0}='{1}'", item.Name, item.GetValue(obj)));
                }
            }
            _ls.Remove(_ls.Find(o => o == "SID"));
            _Cmd.CommandText = string.Format(@"update {0} set {1} where SID='{2}'", _type.Name, string.Join(",", _ls), _SID);
            _Cmd.ExecuteNonQuery();
            HID = BaseExt.GetSystemGUID();
            //插入History
            _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'{5}','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, HID, _SID, Tran);
            _Cmd.ExecuteNonQuery();


            return Result;
        }
        /// <summary>
        /// 更新物件
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="obj">物件</param>
        /// <param name="Tran">交易名稱</param>
        /// <returns></returns>
        public static T UpdateBySID<T>(this T obj, TransType Tran) where T : new()
        {
            T Result = obj;

            Type _type = typeof(T);

            List<string> _lsColumn = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            //建立交易物件
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;

            try
            {
                string _SID = obj.GetType().GetProperty("SID").GetValue(obj).ToString();

                obj.GetType().GetProperty("UDT").SetValue(obj, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                List<string> _ls = new List<string>();
                foreach (var item in _type.GetProperties())
                {
                    if (item.GetValue(obj) != null)
                    {
                        _ls.Add(string.Format("{0}='{1}'", item.Name, item.GetValue(obj)));
                    }
                }
                _ls.Remove(_ls.Find(o => o == "SID"));
                _Cmd.CommandText = string.Format(@"update {0} set {1} where SID='{2}'", _type.Name, string.Join(",", _ls), _SID);
                _Cmd.ExecuteNonQuery();

                //插入History
                _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'{5}','{3}'
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), _type.Name + "_HT", _type.Name, BaseExt.GetSystemGUID(), _SID, Tran);
                _Cmd.ExecuteNonQuery();

                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return Result;
        }

        /// <summary>
        /// 取得物件的參數轉為字串集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<string> GetProperties<T>()
        {
            Type _type = typeof(T);

            List<string> _lsColumn = new List<string>();
            foreach (var item in _type.GetProperties())
            {
                _lsColumn.Add(item.Name);
            }

            return _lsColumn;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密資料</param>
        /// <param name="key">8位字元的金鑰字串</param>
        /// <param name="iv">8位字元的初始化向量字串</param>
        /// <returns></returns>
        public static string DESEncrypt(this string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["DES_Key"].ToString());
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["DES_iv"].ToString());

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密資料</param>
        /// <param name="key">8位字元的金鑰字串(需要和加密時相同)</param>
        /// <param name="iv">8位字元的初始化向量字串(需要和加密時相同)</param>
        /// <returns></returns>
        public static string DESDecrypt(this string data)
        {
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["DES_Key"].ToString());
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(ConfigurationManager.AppSettings["DES_iv"].ToString());

            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
            }
            catch
            {
                return null;
            }

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            return sr.ReadToEnd();
        }

        public static bool Tobool(this string r)
        {
            if (r == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ToIntStr(this bool r)
        {
            if (r)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static int GetSequence(SequenceType SequenceName)
        {
            int Result = 0;

            var _db = new DBHelper("MSDB");

            string CmdStr = string.Format("select next value for dbo.{0}", SequenceName);
            int.TryParse(_db.ExecuteScalar<string>(CmdStr), out Result);

            return Result;
        }

        public static string GetSystemGUID()
        {
            string Result = string.Empty;

            var _db = new DBHelper("MSDB");

            string CmdStr = string.Format("select NewID()");

            Result = _db.ExecuteScalar<string>(CmdStr);

            return Result;
        }


        public static List<ComboboxItem> ToConboboxItem(this string data)
        {
            List<ComboboxItem> Result = new List<ComboboxItem>();
            var a = data.Split(',');
            foreach (var d in a)
            {
                ComboboxItem Item = new ComboboxItem();
                var b = d.Split(';');
                if (b.Length > 1)
                {
                    Item.id = b[0];
                    Item.text = b[1];
                }
                else
                {
                    Item.id = b[0];
                    Item.text = b[0];
                }
                Result.Add(Item);
            }
            return Result;
        }

        public static string To32Str(this int Nbr, int ft = 0)
        {
            string Result = "";

            #region datas
            List<To32Str> ls = new List<To32Str>();
            ls.Add(new To32Str() { SQE = 0, STR = "0" });
            ls.Add(new To32Str() { SQE = 1, STR = "1" });
            ls.Add(new To32Str() { SQE = 2, STR = "2" });
            ls.Add(new To32Str() { SQE = 3, STR = "3" });
            ls.Add(new To32Str() { SQE = 4, STR = "4" });
            ls.Add(new To32Str() { SQE = 5, STR = "5" });
            ls.Add(new To32Str() { SQE = 6, STR = "6" });
            ls.Add(new To32Str() { SQE = 7, STR = "7" });
            ls.Add(new To32Str() { SQE = 8, STR = "8" });
            ls.Add(new To32Str() { SQE = 9, STR = "9" });
            ls.Add(new To32Str() { SQE = 10, STR = "A" });
            ls.Add(new To32Str() { SQE = 11, STR = "B" });
            ls.Add(new To32Str() { SQE = 12, STR = "C" });
            ls.Add(new To32Str() { SQE = 13, STR = "D" });
            ls.Add(new To32Str() { SQE = 14, STR = "E" });
            ls.Add(new To32Str() { SQE = 15, STR = "F" });
            ls.Add(new To32Str() { SQE = 16, STR = "G" });
            ls.Add(new To32Str() { SQE = 17, STR = "H" });
            ls.Add(new To32Str() { SQE = 18, STR = "J" });
            ls.Add(new To32Str() { SQE = 19, STR = "K" });
            ls.Add(new To32Str() { SQE = 20, STR = "L" });
            ls.Add(new To32Str() { SQE = 21, STR = "M" });
            ls.Add(new To32Str() { SQE = 22, STR = "N" });
            ls.Add(new To32Str() { SQE = 23, STR = "P" });
            ls.Add(new To32Str() { SQE = 24, STR = "Q" });
            ls.Add(new To32Str() { SQE = 25, STR = "R" });
            ls.Add(new To32Str() { SQE = 26, STR = "S" });
            ls.Add(new To32Str() { SQE = 27, STR = "T" });
            ls.Add(new To32Str() { SQE = 28, STR = "V" });
            ls.Add(new To32Str() { SQE = 29, STR = "W" });
            ls.Add(new To32Str() { SQE = 30, STR = "X" });
            ls.Add(new To32Str() { SQE = 31, STR = "Y" });
            #endregion

            var _Qeury = ls;
            //var _Qeury = db.Select<S_NBRTOSTR>(string.Format("select * from s_nbrtostr where rule_name='{0}'", "To32"));

            if (Nbr < _Qeury.Count)
            {
                var obj = _Qeury.Find(o => o.SQE == Nbr);
                Result = obj.STR;//若小於進位數直接轉字串回傳
            }
            else
            {
                int _Number = Nbr;
                do
                {
                    Result = _Qeury.Find(o => o.SQE == (_Number % _Qeury.Count)).STR + Result;
                    _Number = _Number / _Qeury.Count;
                } while (_Number > _Qeury.Count);

                Result = _Qeury.Find(o => o.SQE == _Number).STR + Result;

            }

            Result = Result.PadLeft(ft, '0');


            return Result;
        }
    }

    class S_NBRTOSTR
    {
        public int SID { get; set; }
        public string RULE_NAME { get; set; }
        public int SQE { get; set; }
        public string STR { get; set; }

    }

    class To32Str
    {
        public int SQE { get; set; }
        public string STR { get; set; }

    }
}
