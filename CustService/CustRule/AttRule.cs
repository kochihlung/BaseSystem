using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using SysService.Rule;
using CustService.CustObjs.tb;
using CustService.CustObjs.view;
using SysService.svc;

namespace CustService.CustRule
{
    public class AttRule
    {
        public List<H_ATTREC_LOG> GetAttRecLog(string UID)
        {
            var Result = new List<H_ATTREC_LOG>();
            DBHelper db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"SELECT TOP (50) *  FROM H_ATTREC_LOG where S_USERINFO_ID='{0}'  order by udt desc", UID);
            Result = db.ExecuteList<H_ATTREC_LOG>(SqlCmd);

            return Result;
        }

        public List<H_ATTREC_LOG> SignAttRecLog(string Token)
        {
            var Result = new List<H_ATTREC_LOG>();

            var UserInfo = SysService.svc.BaseExt.GetUserInfo(Token);

            var rowData = new H_ATTREC_LOG() { S_USERINFO_ID = UserInfo.SID, DATASTATUS = "Create", NAME = UserInfo.NAME, CODE = UserInfo.TOKEN ,REMARK=UserInfo.LAT + "," + UserInfo.LON};

            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;
            try
            {
                rowData.CreateByObject<H_ATTREC_LOG>(_Cmd);

                _Tran.Commit();
                _Conn.Close();//關閉連線
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            return GetAttRecLog(UserInfo.SID);
        }
    }
}
