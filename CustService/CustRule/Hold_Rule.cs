using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DBHelpers;
using SysService.svc;

using SysService.objs;
using SysService.objs.tb;
using SysService.objs.View;
using CustService.CustObjs.tb;
using CustService.CustObjs.view;


namespace CustService.CustRule
{
    public class Hold_Rule
    {
        public EasyUI_DataGrid GetHoldControlData(string HOLD_SID)
        {
            EasyUI_DataGrid Result = new EasyUI_DataGrid();
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format(@"select a.*,b.NAME UserName,c.CODE ReasonCode,c.NAME ReasonName,d.CODE ReasonTypeCode,d.NAME ReasonTypeName
                                     from W_HOLDCONTROL a
                                     left join S_USERINFO b on a.UID = b.SID
                                     left join M_HOLDRESON c on a.M_HOLDRESON = c.SID
                                     left join M_HOLDTYPE d on c.M_HOLDTYPE = d.SID
                                     where a.HOLD_SID='{0}'", HOLD_SID);

            var data = db.ExecuteList<V_HoldControlInfo>(SqlCmd);

            Result = data.ToEasyUI_DataGrid<V_HoldControlInfo>();

            Result.columns[0].Find(o => o.field == "CODE").title = "鎖定項目";
            Result.columns[0].Find(o => o.field == "NAME").title = "鎖定類型";
            Result.columns[0].Find(o => o.field == "HOLD_SID").hidden = true;


            return Result;
        }

        public V_HoldControlInfo HoldBySID(string SID, string Reson, string Remark, string UID)
        {
            //1.取得Reason資料
            //2.依TypeCode取得指定物件的Status並填值
            //3.提交物件
            //4.修改Hold物件狀態
            //5.取得Hold資料
            //6.回傳

            //1.取得Reason資料
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select a.*,b.CODE TYPECODE,b.NAME TYPENAME from M_HOLDRESON a left join M_HOLDTYPE b on a.M_HOLDTYPE=b.SID where a.SID='{0}'", Reson);
            var Info = db.ExecuteObject<V_HoldResonInfo>(SqlCmd);

            //2.依TypeCode取得指定物件的Status並填值
            string FromStatus = string.Empty;
            W_HOLDCONTROL obj = new W_HOLDCONTROL();


            //依原因類型填入資料
            switch (Info.TYPECODE)
            {
                case "WorkOrder":
                    SqlCmd = string.Format("select * from W_WO where SID='{0}'", SID);
                    var r = db.ExecuteObject<W_WO>(SqlCmd);
                    obj.CODE = r.CODE;
                    obj.NAME = "WorkOrder Hold";

                    SqlCmd = string.Format(@"select a.*,b.NAME UserName,c.CODE ReasonCode,c.NAME ReasonName,d.CODE ReasonTypeCode,d.NAME ReasonTypeName
                                     from W_HOLDCONTROL a
                                     left join S_USERINFO b on a.UID = b.SID
                                     left join M_HOLDRESON c on a.M_HOLDRESON = c.SID
                                     left join M_HOLDTYPE d on c.M_HOLDTYPE = d.SID
                                     where a.HOLD_SID='{0}' and STATUS='Hold'", SID);

                    var data = db.ExecuteList<V_HoldControlInfo>(SqlCmd);

                    var _FromStatus = data.Select(o => new { o.FROMSTATUS }).Distinct().ToList();

                    switch (_FromStatus.Count())
                    {
                        case 0:
                            FromStatus = r.STATUS;
                            break;
                        case 1:
                            FromStatus = _FromStatus[0].FROMSTATUS;
                            break;
                        default:
                            throw new Exception("數據異常：物件來源狀態大於1種。");
                    }
                    break;
                case "Equipment":
                    break;
                case "Tools":
                    break;
                case "WIP":
                    break;
                default:
                    throw new Exception("原因類型錯誤!");
            }

            if (FromStatus == null || FromStatus == string.Empty) { throw new Exception("取得物件原始狀態失敗!"); };
            obj.HOLD_SID = SID;
            obj.M_HOLDRESON = Reson;
            obj.REMARK = Remark;
            obj.STATUS = "Hold";
            obj.FROMSTATUS = FromStatus;
            obj.UID = UID;

            //3.提交物件
            obj = BaseExt.CreateByObject<W_HOLDCONTROL>(obj);

            //4.修改Hold物件狀態
            //因原因類型修改物件狀態
            switch (Info.TYPECODE)
            {
                case "WorkOrder":
                    SqlCmd = string.Format("select * from W_WO where SID='{0}'", SID);
                    var r = db.ExecuteObject<W_WO>(SqlCmd);
                    r.STATUS = "Hold";
                    r.UID = UID;
                    r.REMARK = string.Format("Hold control ID:{0}", obj.SID);
                    r.UpdateBySID<W_WO>(TransType.Hold);
                    break;
                case "Equipment":
                    break;
                case "Tools":
                    break;
                case "WIP":
                    break;
                default:
                    throw new Exception("原因類型錯誤!");
            }

            //5.取得Hold資料
            SqlCmd = string.Format(@"select a.*,b.NAME UserName,c.CODE ReasonCode,c.NAME ReasonName,d.CODE ReasonTypeCode,d.NAME ReasonTypeName
                                     from W_HOLDCONTROL a
                                     left join S_USERINFO b on a.UID = b.SID
                                     left join M_HOLDRESON c on a.M_HOLDRESON = c.SID
                                     left join M_HOLDTYPE d on c.M_HOLDTYPE = d.SID
                                     where a.SID='{0}'", obj.SID);

            var Result = db.ExecuteObject<V_HoldControlInfo>(SqlCmd);

            //6.回傳
            return Result;
        }

        public V_HoldControlInfo UnHoldBySID(string SID, string UID)
        {
            //1.取得Hold資料
            //2.修改Hold Status & User ID
            //3.提交物件
            //4.新增Hold物件UnHold的History
            //5.檢查Hold物件是否可Release
            //6.取得Hold資料
            //7.回傳

            //1.取得Reason資料
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from W_HOLDCONTROL where SID='{0}'", SID);
            var Info = db.ExecuteObject<W_HOLDCONTROL>(SqlCmd);

            if (Info == null) { throw new Exception("找不到指定的Hold資料!"); };

            //2.依TypeCode取得指定物件的Status並填值
            Info.STATUS = "UnHold";
            Info.UID = UID;

            //3.提交物件
            BaseExt.UpdateBySID<W_HOLDCONTROL>(Info, TransType.UnHold);

            //4.新增Hold物件UnHold的History
            SqlCmd = string.Format("select a.*,b.CODE TYPECODE,b.NAME TYPENAME from M_HOLDRESON a left join M_HOLDTYPE b on a.M_HOLDTYPE=b.SID where a.SID='{0}'", Info.M_HOLDRESON);
            var ReasonInfo = db.ExecuteObject<V_HoldResonInfo>(SqlCmd);

            switch (ReasonInfo.TYPECODE)
            {
                case "WorkOrder":
                    SqlCmd = string.Format("select * from W_WO where SID='{0}'", Info.HOLD_SID);
                    var r = db.ExecuteObject<W_WO>(SqlCmd);

                    if (r == null) { new Exception("找不到指定的工單"); };

                    // 更新工單以新增History
                    r.UID = UID;
                    r.REMARK = string.Format("Unhold control ID:{0}", SID);
                    r.UpdateBySID<W_WO>(TransType.UnHold);

                    //若Hold已完全解除，則修改物件狀態
                    if (CheckRelease(Info))
                    {
                        r.UID = UID;
                        r.STATUS = Info.FROMSTATUS;
                        r.REMARK = r.REMARK + string.Format(@"\nSystem Action:Release");
                        r.UpdateBySID<W_WO>(TransType.HoldRelease);
                    }
                    break;
                case "Equipment":
                    break;
                case "Tools":
                    break;
                case "WIP":
                    break;
                default:
                    throw new Exception("原因類型錯誤!");
            }

            //4.取得資料
            SqlCmd = string.Format(@"select a.*,b.NAME UserName,c.CODE ReasonCode,c.NAME ReasonName,d.CODE ReasonTypeCode,d.NAME ReasonTypeName
                                     from W_HOLDCONTROL a
                                     left join S_USERINFO b on a.UID = b.SID
                                     left join M_HOLDRESON c on a.M_HOLDRESON = c.SID
                                     left join M_HOLDTYPE d on c.M_HOLDTYPE = d.SID
                                     where a.SID='{0}'", SID);

            var Result = db.ExecuteObject<V_HoldControlInfo>(SqlCmd);

            //5.回傳
            return Result;
        }

        public object UnHoldAll(string SID, string UID)
        {
            //解鎖僅更新狀態，供追蹤鎖定狀態
            //多筆更新若以物件提交方式將造成多次存取DB，固改用指令方式進行資料更新

            //1.建立交易區塊
            //2.By Hold_SID及Status取得資料修改清單
            //3.更新資料
            //4.產生HID並插入History
            //5.新增Hold物件UnHold的History
            //6.回傳結果

            //1.建立交易區塊
            var _db = new DBHelper("MSDB");
            var _Conn = _db.CreateConnection();
            _Conn.Open();//開啟連線
            var _Cmd = _Conn.CreateCommand();
            var _Tran = _Conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            _Cmd.Transaction = _Tran;
            try
            {
                //2.By Hold_SID及Status取得資料修改清單
                string SqlCmd = string.Format("select * from W_HOLDCONTROL where HOLD_SID='{0}' and STATUS = 'Hold'", SID);
                var ls = _db.ExecuteList<W_HOLDCONTROL>(SqlCmd);

                if (ls == null) { throw new Exception("找不到該物件的Hold資料!"); }

                //定義欄位
                List<string> _lsColumn = BaseExt.GetProperties<W_HOLDCONTROL>();

                //3.更新Data
                _Cmd.CommandText = string.Format(@"update W_HOLDCONTROL set STATUS='UnHold',UID='{1}',UDT='{2}' where HOLD_SID='{0}'", SID, UID, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                _Cmd.ExecuteNonQuery();

                //4.產生HID並插入History
                foreach (var item in ls)
                {
                    //插入History
                    _Cmd.CommandText = string.Format(@"INSERT INTO {1} ({0},TRANS,HID)
                                                SELECT {0},'{5}',{3}
                                                FROM {2} where SID='{4}'", string.Join(",", _lsColumn), "W_HOLDCONTROL_HT", "W_HOLDCONTROL", "NewID()", item.SID, "UnHold");
                    _Cmd.ExecuteNonQuery();
                }

                //5.新增Hold物件UnHold的History
                SqlCmd = string.Format("select a.*,b.CODE TYPECODE,b.NAME TYPENAME from M_HOLDRESON a left join M_HOLDTYPE b on a.M_HOLDTYPE=b.SID where a.SID='{0}'", ls[0].M_HOLDRESON);
                var ReasonInfo = _db.ExecuteObject<V_HoldResonInfo>(SqlCmd);

                switch (ReasonInfo.TYPECODE)
                {
                    case "WorkOrder":
                        SqlCmd = string.Format("select * from W_WO where SID='{0}'", ls[0].HOLD_SID);
                        var r = _db.ExecuteObject<W_WO>(SqlCmd);

                        if (r == null) { new Exception("找不到指定的工單"); };

                        // 更新工單以新增History
                        r.UID = UID;
                        r.REMARK = string.Format("Unhold control ID:{0}", SID);
                        r.UpdateBySID<W_WO>(TransType.UnHold);

                        //Hold已完全解除，修改物件狀態
                        r.UID = UID;
                        r.STATUS = ls[0].FROMSTATUS;
                        r.REMARK = r.REMARK + string.Format(@"\nSystem Action:Release");
                        r.UpdateBySID<W_WO>(TransType.HoldRelease);
                        break;
                    case "Equipment":
                        break;
                    case "Tools":
                        break;
                    case "WIP":
                        break;
                    default:
                        throw new Exception("原因類型錯誤!");
                }



                _Tran.Commit();
                _Conn.Close();
            }
            catch (Exception ex)
            {
                _Tran.Rollback();
                _Conn.Close();
                throw new Exception(ex.Message);
            }

            ////6.回傳
            return GetHoldControlData(SID).data;
        }

        private bool CheckRelease(W_HOLDCONTROL obj)
        {
            bool Result = false;
            var db = new DBHelper("MSDB");
            string SqlCmd = string.Format("select * from W_HOLDCONTROL where HOLD_SID='{0}' and STATUS='Hold'", obj.HOLD_SID);
            var ls = db.ExecuteList<W_HOLDCONTROL>(SqlCmd);

            if (ls.Count == 0)
            {
                Result = true;
            }

            return Result;
        }

    }
}
