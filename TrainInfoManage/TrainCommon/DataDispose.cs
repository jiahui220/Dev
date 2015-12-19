using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Windows.Forms;

namespace TrainCommon
{
    public class DataDispose
    {
        //当前报警机车运行数据记录ID
        private static int WarnId = 0;
        private static List<int> wlist = new List<int>();

        #region 报警处理
        /// <summary>
        /// 报警信息处理
        /// 其中报警信息处理包含三部分①记录报警日志信息 ②语音播报报警内容 ③将报警信息提交至服务（此项在获取机车当时运行情况具体数据时同时提交，此处暂不处理）
        /// </summary>
        /// <param name="warnInfo">解析所得报警信息数组</param>
        public static void DisposeWarn(string[] warnInfo)
        {
            if (warnInfo.Length < 5)
            {
                return;
            }
            using (RParams param = new RParams())
            {
                //报警语音字符串
                string strItem = "";
                //查询报警项点信息
                string warnStr = warnInfo[1];
                string[] warnBrr = warnStr.Trim().Split(new char[] { ' ' });
                if (warnBrr.Length > 0)
                {

                    for (int i = 0; i < warnBrr.Length; i++)
                    {
                        string warnItem = warnBrr[i];
                        if (warnItem.Trim().Length > 0)
                        {
                            //获取报警项点详细信息
                            using (DataTable wt = DBAction.GetDTFromSQL("select * from " + ETableName.AlarmCfg.ToString() + " where ID=" + warnItem + " and IsOpen='1' "))
                            {
                                if (wt.Rows.Count > 0)
                                {
                                    //判断相同报警信息是否已记录且报警时间间隔超过30秒
                                    using (DataTable wl = DBAction.GetDTFromSQL("select CreateTime from " + ETableName.AlarmLog.ToString() + " where AlarmID=" + warnItem))
                                    {
                                        DateTime timeNow = DateTime.Now;
                                        if (LocoInfo.TrainInfo.CurrDateTime.Trim() != "")
                                        {
                                            timeNow = Convert.ToDateTime(LocoInfo.TrainInfo.CurrDateTime.Trim());
                                        }
                                        if (wl.Rows.Count > 0)
                                        {
                                            DateTime warnTime = Convert.ToDateTime(wl.Rows[wl.Rows.Count - 1][0].ToString());
                                            TimeSpan ts = timeNow - warnTime;
                                            double totals = ts.TotalSeconds;
                                            //大于30则为新的报警记录
                                            if (totals > 120)
                                            {
                                                if (WarnId == 0)
                                                {
                                                    //记录机车报警运行记录
                                                    param.Items.Clear();
                                                    param.Add("CreateTime", timeNow);
                                                    DBAction.Insert(ETableName.AlarmRunInfo, param);
                                                    using (DataTable iwt = DBAction.GetDTFromSQL("select ID from AlarmRunInfo "))
                                                    {
                                                        if (iwt.Rows.Count > 0)
                                                        {
                                                            WarnId = Convert.ToInt32(iwt.Rows[iwt.Rows.Count - 1][0]);
                                                            wlist.Add(WarnId);
                                                        }
                                                    }
                                                }

                                                //添加报警记录
                                                param.Items.Clear();
                                                param.Add("CreateTime", timeNow);
                                                param.Add("ARInfoId", WarnId);
                                                param.Add("AlarmID", warnItem);
                                                param.Add("AlarmItem", wt.Rows[0]["AlarmItem"].ToString());
                                                param.Add("AlarmIntro", wt.Rows[0]["AlarmIntro"].ToString());
                                                param.Add("AlarmNum", wt.Rows[0]["ItemNum"].ToString());
                                                DBAction.Insert(ETableName.AlarmLog, param);
                                                strItem += wt.Rows[0]["AlarmIntro"].ToString();//串联报警内容
                                            }
                                        }
                                        else
                                        {
                                            if (WarnId == 0)
                                            {
                                                //记录机车报警运行记录
                                                param.Items.Clear();
                                                param.Add("CreateTime", timeNow);
                                                DBAction.Insert(ETableName.AlarmRunInfo, param);
                                                using (DataTable iwt = DBAction.GetDTFromSQL("select ID from AlarmRunInfo "))
                                                {
                                                    if (iwt.Rows.Count > 0)
                                                    {
                                                        WarnId = Convert.ToInt32(iwt.Rows[iwt.Rows.Count - 1][0]);
                                                        wlist.Add(WarnId);
                                                    }
                                                }
                                            }

                                            //添加报警记录
                                            param.Items.Clear();
                                            param.Add("CreateTime", timeNow);
                                            param.Add("ARInfoId", WarnId);
                                            param.Add("AlarmID", warnItem);
                                            param.Add("AlarmItem", wt.Rows[0]["AlarmItem"].ToString());
                                            param.Add("AlarmIntro", wt.Rows[0]["AlarmIntro"].ToString());
                                            param.Add("AlarmNum", wt.Rows[0]["ItemNum"].ToString());
                                            DBAction.Insert(ETableName.AlarmLog, param);
                                            strItem += wt.Rows[0]["AlarmItem"].ToString();//串联报警内容
                                        }
                                    }

                                }
                            }

                        }
                    }
                    //报警语音播报
                    if (strItem != "" && LocoInfo.TrainInfo.IsVoice)
                    {
                        BaseVoice.TrainVoice.SpeekVioce(strItem);
                    }
                }
            }


        }
        #endregion

        #region 广播信息处理
        /// <summary>
        /// 广播信息处理
        /// </summary>
        /// <param name="warnInfo"></param>
        public static void DisposeBroadcast(string[] noticeInfo, string info)
        {
            try
            {
                using (RParams param = new RParams())
                {
                    if (noticeInfo.Length == 37)
                    {

                        string signalType = noticeInfo[6];//进出站状态
                        string trainType = noticeInfo[19];//车型
                        string trainNum = noticeInfo[20];//车号
                        string dn = noticeInfo[21];//司机编号
                        #region  创建报单，插入报单头信息及乘务员信息
                        //判断是否已创建报单
                        //if (LocoInfo.TrainInfo.ReportID == 0)
                        //{
                        //MessageBox.Show("开始创建报单");
                        XmlAction xa = new XmlAction();
                        string reportID = "0";//报单ID
                        string drnum = "0";//获取司机编号             
                        if (LocoInfo.TrainInfo.RoboConfig.Rows.Count > 0)
                        {
                            using (DataTable ct = LocoInfo.TrainInfo.RoboConfig)
                            {
                                reportID = ct.Rows[ct.Rows.Count - 1]["ReportID"].ToString();
                                drnum = ct.Rows[ct.Rows.Count - 1]["DriverNum"].ToString();
                            }

                        }
                        XmlNode rn = xa.GetNode("/RoboConfig/RailCorp", null, null);//铁路局节点
                        string rc = xa.GetText(rn);//获取铁路局
                        XmlNode dpn = xa.GetNode("/RoboConfig/Depot", null, null);//机务段节点
                        string dp = xa.GetText(dpn);//获取机务段
                        //判断是否已创建报单，等于0表示未创建报单，其他为已创建
                        //if (reportID == "0")
                        //{
                        //    CreateReport(dn, noticeInfo[22], trainType, trainNum, rc, dp, noticeInfo[2]);//创建报单
                        //}
                        //else
                        //{
                        //判定报单是否为该班当班司机报单,不是则重新创建
                        if (dn != drnum)
                        {
                            CreateReport(dn, noticeInfo[22], trainType, trainNum, rc, dp, noticeInfo[2]);//创建报单
                        }
                        else
                        {
                            LocoInfo.TrainInfo.ReportID = Convert.ToInt32(reportID);
                        }
                        //}
                        ////报单出勤时分
                        //using (DataTable st = DBAction.GetDTFromSQL("select DutyTime,ReceiveTime from Steward where RHId=" + LocoInfo.TrainInfo.ReportID))
                        //{
                        //    if (st.Rows.Count > 0)
                        //    {
                        //        MessageBox.Show("出勤时间----->" + st.Rows[0]["DutyTime"].ToString() + "*****" + st.Rows[0]["ReceiveTime"].ToString());
                        //    }
                        //}
                        //}
                        #endregion
                        //创建司机记录
                        CreateLog(dn);
                        //string trs = "";//车次
                        //string tw = "";//总重
                        //string tl = "";//载重
                        //string tbs = "";//客车数量合计
                        //string fc = "";//重车
                        //string ec = "";//空车
                        //string nuc = "";//非运用车
                        //string rcr = "";//代客车
                        //string tc = "";//合计
                        //string trl = "";//列车换长



                        #region  判断运行编组基础数据是否有改变

                        ////车次
                        //if (noticeInfo[24] != "" && noticeInfo[24] != BaseReportInfo.ReportInfo.Trains)
                        //{
                        //    trs = noticeInfo[24];
                        //    BaseReportInfo.ReportInfo.Trains = noticeInfo[24];//将报表车次替换为最新车次
                        //}
                        //else
                        //{
                        //    trs = "";//车次
                        //}

                        ////总重
                        //if (noticeInfo[27] != "" && noticeInfo[27] != "0" && noticeInfo[27] != BaseReportInfo.ReportInfo.TotalWeight)
                        //{
                        //    tw = noticeInfo[27];
                        //    BaseReportInfo.ReportInfo.TotalWeight = noticeInfo[27];//更新最新总重
                        //}
                        //else
                        //{
                        //    tw = "";
                        //}

                        ////载重
                        //if (noticeInfo[30] != "" && noticeInfo[30] != "0" && noticeInfo[30] != BaseReportInfo.ReportInfo.TotalLoad)
                        //{
                        //    tl = noticeInfo[30];
                        //    BaseReportInfo.ReportInfo.TotalLoad = noticeInfo[30];
                        //}
                        //else
                        //{
                        //    tl = "";
                        //}

                        ////客车合计
                        //if (noticeInfo[31] != "" && noticeInfo[31] != "0" && noticeInfo[31] != BaseReportInfo.ReportInfo.TotalBus)
                        //{
                        //    tbs = noticeInfo[31];
                        //    BaseReportInfo.ReportInfo.TotalBus = noticeInfo[31];//更新最新客车数
                        //}
                        //else
                        //{
                        //    tbs = "";//客车数量合计
                        //}

                        ////重车
                        //if (noticeInfo[32] != "" && noticeInfo[32] != "0" && noticeInfo[32] != BaseReportInfo.ReportInfo.FullCar)
                        //{
                        //    fc = noticeInfo[32];
                        //    BaseReportInfo.ReportInfo.FullCar = noticeInfo[32];//更新最新重车数量
                        //}
                        //else
                        //{
                        //    fc = "";//重车
                        //}

                        ////空车
                        //if (noticeInfo[33] != "" && noticeInfo[33] != "0" && noticeInfo[33] != BaseReportInfo.ReportInfo.EmptyCar)
                        //{
                        //    ec = noticeInfo[33];
                        //    BaseReportInfo.ReportInfo.EmptyCar = noticeInfo[33];
                        //}
                        //else
                        //{
                        //    ec = "";//空车
                        //}

                        ////非运用车
                        //if (noticeInfo[34] != "" && noticeInfo[34] != "0" && noticeInfo[34] != BaseReportInfo.ReportInfo.NoUseCar)
                        //{
                        //    nuc = noticeInfo[34];
                        //    BaseReportInfo.ReportInfo.NoUseCar = noticeInfo[34];
                        //}
                        //else
                        //{
                        //    nuc = "";//非运用车
                        //}

                        ////代客车
                        //if (noticeInfo[35] != "" && noticeInfo[35] != "0" && noticeInfo[35] != BaseReportInfo.ReportInfo.ReplaceCar)
                        //{
                        //    rcr = noticeInfo[35];
                        //    BaseReportInfo.ReportInfo.ReplaceCar = noticeInfo[35];
                        //}
                        //else
                        //{
                        //    rcr = "";//代客车 
                        //}

                        ////合计
                        //if (noticeInfo[28] != "" && noticeInfo[28] != "0" && noticeInfo[28] != BaseReportInfo.ReportInfo.TotalCar)
                        //{
                        //    tc = noticeInfo[28];
                        //    BaseReportInfo.ReportInfo.TotalCar = noticeInfo[28];
                        //}
                        //else
                        //{
                        //    tc = "";//合计
                        //}

                        ////列车
                        //if (noticeInfo[29] != "" && noticeInfo[29] != "0" && noticeInfo[29] != BaseReportInfo.ReportInfo.TotalLength)
                        //{
                        //    trl = noticeInfo[29];
                        //    BaseReportInfo.ReportInfo.TotalLength = noticeInfo[29];
                        //}
                        //else
                        //{
                        //    trl = "";//列车换长
                        //}

                        #endregion



                        #region 添加运行编组信息
                        if (signalType.Trim() == "进站" || signalType.Trim() == "进出站" || signalType.Trim() == "出站" )
                        {
                         //   MessageBox.Show("添加运行编组信息");

                            int speed = Convert.ToInt32(noticeInfo[3]);//速度
                            if (speed == 0)
                            {
                             //   MessageBox.Show("速度为0判断");

                                string sn = "";
                                string sOutTime = "";
                                int id = 0;
                                //判断进站信息是否已添加
                                using (DataTable rt = DBAction.GetDTFromSQL("select  StationName,ID,SetOutTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID))
                                {
                                    if (rt.Rows.Count > 0)
                                    {
                                        sn = rt.Rows[rt.Rows.Count - 1][0].ToString();
                                        sOutTime = rt.Rows[rt.Rows.Count - 1][2].ToString();
                                        id = Convert.ToInt32(rt.Rows[rt.Rows.Count - 1][1]);
                                    }
                                }
                                if (noticeInfo[26] == BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]))
                                {
                                    //请求站名更新
                                    UpdateBaseLibrary.UpdateStationName(LocoInfo.TrainInfo.SckTrains, noticeInfo[26], noticeInfo[25]);
                                }

                                if (sn.Trim() != BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]))
                                {
                               //     MessageBox.Show("站名不相同");

                                    param.Items.Clear();
                                    param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                    param.Add("TrainNum", noticeInfo[24]);//车次
                                    param.Add("StationName", BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]));//站名
                                    param.Add("ArrivedTime", noticeInfo[2]);//到达时分
                                    //param.Add("ArrivedTime", DateTime.Now.ToString());
                                    param.Add("SetOutTime", "");//出发时分
                                    param.Add("TotalWeight", noticeInfo[27]);//牵引总重
                                    param.Add("LoadWeight", noticeInfo[30]);//牵引载重
                                    param.Add("BusNumSum", noticeInfo[31]);//客车数量合计
                                    param.Add("WeightTruck", noticeInfo[32]);//重车
                                    param.Add("EmptyTruck", noticeInfo[33]);//空车
                                    param.Add("NoUseTruck", noticeInfo[34]);//非运用车
                                    param.Add("GetPassenger", noticeInfo[35]);//代客车 
                                    param.Add("Summation", noticeInfo[28]);//合计
                                    param.Add("TrainChange", noticeInfo[29]);//列车换长
                                    bool result = DBAction.Insert(ETableName.RunAndGroup, param);
                                }
                                else
                                {

                               //     MessageBox.Show("站名相同");

                                    if (sOutTime != "")
                                    {
                                        //判断是否多次进出站。（即在获取第一个信号机是速度不为0，先记录为出站时间，速度为0时记录进站时间，修改出站时间为空。）
                                        param.Items.Clear();
                                        param.Add("ArrivedTime", sOutTime);//到达时分
                                        //param.Add("ArrivedTime", DateTime.Now.ToString());
                                        param.Add("SetOutTime", "");//出发时分
                                        DBAction.Update("RunAndGroup", "ArrivedTime,SetOutTime", "ID=" + id, param);
                                    }
                                }
                            }
                            else
                            {
                             //   MessageBox.Show("速度不为0判断");

                                //修改进站记录的出发时分
                                using (DataTable ins = DBAction.GetDTFromSQL("select ID,ArrivedTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID + " and  StationName='" + BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]) + "'"))
                                {
                                    if (ins.Rows.Count > 0)
                                    {
                                     //   MessageBox.Show("RunAndGroup有数据");

                                        if (ins.Rows[0][1].ToString().Trim().Length > 0)
                                        {
                                            int rgID = Convert.ToInt32(ins.Rows[0][0]);
                                            DateTime aTime = Convert.ToDateTime(ins.Rows[0][1]);
                                            TimeSpan ts = Convert.ToDateTime(noticeInfo[2]) - aTime;
                                            int minues = Convert.ToInt32(ts.TotalMinutes);//停车时分
                                            string ms = "";
                                            if (minues > 0)
                                            {
                                                ms = minues + "分钟";
                                            }
                                            param.Items.Clear();
                                            param.Add("SetOutTime", noticeInfo[2]);//出站时间
                                            param.Add("StopTime", ms);//停车时间
                                            DBAction.Update(ETableName.RunAndGroup, "SetOutTime,StopTime", "RHId=" + LocoInfo.TrainInfo.ReportID + " and  StationName='" + BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]) + "'", param);
                                        }
                                    }
                                    else
                                    {
                                   //     MessageBox.Show("RunAndGroup无数据");

                                        param.Items.Clear();
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        param.Add("TrainNum", noticeInfo[24]);//车次
                                        param.Add("StationName", BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]));//站名
                                        param.Add("ArrivedTime", "");//到达时间
                                        param.Add("SetOutTime", noticeInfo[2]);//出发时间
                                        param.Add("TotalWeight", noticeInfo[27]);//牵引总重
                                        param.Add("LoadWeight", noticeInfo[30]);//牵引载重
                                        param.Add("BusNumSum", noticeInfo[31]);//客车数量合计
                                        param.Add("WeightTruck", noticeInfo[32]);//重车
                                        param.Add("EmptyTruck", noticeInfo[33]);//空车
                                        param.Add("NoUseTruck", noticeInfo[34]);//非运用车
                                        param.Add("GetPassenger", noticeInfo[35]);//代客车 
                                        param.Add("Summation", noticeInfo[28]);//合计
                                        param.Add("TrainChange", noticeInfo[29]);//列车换长
                                        bool result = DBAction.Insert(ETableName.RunAndGroup, param);

                                    }
                                }

                            }
                        }
                        #endregion

                        #region 添加出入段信息
                        string monitorState = noticeInfo[9];//机车监控状态
                        monitorState = monitorState.Replace("入段", "< ").Replace("出段", "> ");
                        int li = monitorState.Split(new char[] { '<' }).Length;
                        int lo = monitorState.Split(new char[] { '>' }).Length;
                        using (DataTable tioDT = DBAction.GetDTFromSQL("select * from TrainInOut where RHId=" + LocoInfo.TrainInfo.ReportID))
                        {
                            int count = tioDT.Rows.Count; string station = BaseLibrary.GetStationName(noticeInfo[25], noticeInfo[26]);//获取站名
                            if (li > 1)//监控状态为入段
                            {
                                //LocoInfo.TrainInfo.NowStation
                                string inEx = "";//入本段时分
                                string inLo = "";//入外段时分
                                if (count > 0)//是否存在出入段记录
                                {
                                    inLo = tioDT.Rows[0]["InLocalTime"].ToString();//入本段时分
                                    inEx = tioDT.Rows[0]["InExternalTime"].ToString();//入外段时分
                                }
                                //判断是否入本段
                                if (LocoInfo.TrainInfo.NowStation.StartsWith("武汉北") || LocoInfo.TrainInfo.NowStation.StartsWith("江岸") || station.StartsWith("武汉北") || station.StartsWith("江岸"))
                                {
                                    //入本段
                                    if (inLo.Trim().Length == 0)
                                    {
                                        if (count == 0)
                                        {
                                            param.Items.Clear();
                                            param.Add("InLocalTime", noticeInfo[2]);
                                            param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                            DBAction.Insert(ETableName.TrainInOut, param);
                                        }
                                        else
                                        {
                                            param.Items.Clear();
                                            param.Add("InLocalTime", noticeInfo[2]);
                                            DBAction.Update(ETableName.TrainInOut, "InLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                        }
                                    }
                                }
                                else
                                {
                                    //入外段
                                    if (inEx.Trim().Length == 0)
                                    {
                                        if (count == 0)
                                        {
                                            param.Items.Clear();
                                            param.Add("InExternalTime", noticeInfo[2]);
                                            param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                            DBAction.Insert(ETableName.TrainInOut, param);
                                        }
                                        else
                                        {
                                            param.Items.Clear();
                                            param.Add("InExternalTime", noticeInfo[2]);
                                            DBAction.Update(ETableName.TrainInOut, "InExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                        }
                                    }
                                }
                            }
                            if (lo > 1)//监控状态为出段
                            {
                                string outEx = "";//出外段时分
                                string outLo = "";//出本段时分
                                if (count > 0)
                                {
                                    outEx = tioDT.Rows[0]["OutExternalTime"].ToString();//出外段时分
                                    outLo = tioDT.Rows[0]["OutLocalTime"].ToString();//出本段时分
                                }
                                //判断是否出本段
                                if (LocoInfo.TrainInfo.NowStation.StartsWith("武汉北") || LocoInfo.TrainInfo.NowStation.StartsWith("江岸") || station.StartsWith("武汉北") || station.StartsWith("江岸"))
                                {
                                    if (count == 0)
                                    {
                                        param.Items.Clear();
                                        param.Add("OutLocalTime", noticeInfo[2]);
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        DBAction.Insert(ETableName.TrainInOut, param);
                                    }
                                    else
                                    {
                                        param.Items.Clear();
                                        param.Add("OutLocalTime", noticeInfo[2]);
                                        DBAction.Update(ETableName.TrainInOut, "OutLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                    }

                                }
                                else
                                {
                                    if (count == 0)
                                    {
                                        param.Items.Clear();
                                        param.Add("OutExternalTime", noticeInfo[2]);
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        DBAction.Insert(ETableName.TrainInOut, param);
                                    }
                                    else
                                    {
                                        param.Items.Clear();
                                        param.Add("OutExternalTime", noticeInfo[2]);
                                        DBAction.Update(ETableName.TrainInOut, "OutExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                    }
                                }
                            }
                        }




                        #endregion

                        #region 添加运行数据实时记录
                        if (LocoInfo.TrainInfo.LogTime != null)
                        {
                            TimeSpan logs = DateTime.Now - LocoInfo.TrainInfo.LogTime;
                            double ls = logs.Seconds;
                            //机车信息每一分钟记录一条
                            if (ls >= 30)
                            {
                                insertRunInfo(noticeInfo, info);
                                LocoInfo.TrainInfo.LogTime = DateTime.Now;
                            }
                        }
                        else
                        {
                            insertRunInfo(noticeInfo, info);
                            LocoInfo.TrainInfo.LogTime = DateTime.Now;
                        }

                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
            }

        }
        #endregion


        //添加运行记录
        public static void insertRunInfo(string[] noticeInfo, string info)
        {
            using (RParams param = new RParams())
            {

        //      MessageBox.Show(LocoInfo.TrainInfo.TrainID.ToString() + "----" + noticeInfo[19] + "----" + noticeInfo[20] + "----" + noticeInfo[2]+"----"+info);
                param.Items.Clear();
                LocoInfo.TrainInfo.LogTime = DateTime.Now;
                param.Add("TrainID", LocoInfo.TrainInfo.TrainID);
                param.Add("TrainType", BaseLibrary.GetTrainType(noticeInfo[19]));
                param.Add("TrainNum", noticeInfo[20]);
                param.Add("RecordTime", noticeInfo[2]);
                param.Add("TrainRunInfo", info);
                if (LocoInfo.TrainInfo.Longitude != "0.00" && LocoInfo.TrainInfo.Latitude != "0.00")
                {
                    string coors = LocoInfo.TrainInfo.Longitude + "," + LocoInfo.TrainInfo.Latitude;
                    param.Add("Coordinates", coors);
                }
                else
                {
                    param.Add("Coordinates", "0");
                }
                DBAction.Insert(ETableName.TrainRuning, param);
               // DBAction.Insert("TrainRuning", param);

            }
        }


        //添加车载信息发送记录
        public static void CreateLog(string driverNum)
        {
            int logId = LocoInfo.TrainInfo.LogId;
            using (RParams param = new RParams())
            {
                //判断当前司机是否有记录
                if (logId == 0)
                {
                    //MessageBox.Show("创建司机记录");
                    string strSql = "select ID,DriverNum from SendLog ";
                    using (DataTable dt = DBAction.GetDTFromSQL(strSql))
                    {
                        string oldDricer = "";
                        if (dt.Rows.Count > 0)
                        {
                            oldDricer = dt.Rows[dt.Rows.Count - 1]["DriverNum"].ToString();
                        }
                        //不是当前司机记录则新增一条
                        if (driverNum != oldDricer)
                        {
                            param.Items.Clear();
                            param.Add("DriverNum", driverNum);
                            param.Add("WarnSendID", 0);
                            param.Add("ReportSendID", LocoInfo.TrainInfo.ReportID);
                            param.Add("GroupSendID", 0);
                            DBAction.Insert("SendLog", param);
                            //MessageBox.Show("创建司机记录完成");
                        }
                        using (DataTable st = DBAction.GetDTFromSQL(strSql))
                        {
                            //获取当前司机的记录ID
                            LocoInfo.TrainInfo.LogId = Convert.ToInt32(st.Rows[st.Rows.Count - 1]["ID"]);
                            //MessageBox.Show("司机记录ID" + LocoInfo.TrainInfo.LogId);
                        }

                    }
                }
            }

        }



        #region 创建报单
        /// <summary>
        /// 创建报单
        /// </summary>
        /// <param name="dn">司机编号</param>
        /// <param name="sd">副司机编号</param>
        /// <param name="ty">机车类型</param>
        /// <param name="tn">机车编号</param>
        /// <param name="rc">铁路局</param>
        /// <param name="dp">机务段</param>
        public static void CreateReport(string dn, string sd, string ty, string tn, string rc, string dp, string dateTime)
        {
            XmlAction xla = new XmlAction();
            //创建报单
            using (RParams param = new RParams())
            {
                param.Add("CreateTime", dateTime);
                param.Add("RailCorp", rc);
                param.Add("Depot", dp);
                param.Add("TrainType", BaseLibrary.GetTrainType(ty));
                param.Add("TrainNum", tn);
                DBAction.Insert(ETableName.ReportHeader, param);

                using (DataTable ht = DBAction.GetDTFromSQL("select ID from ReportHeader"))
                {
                    if (ht.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.ReportID = Convert.ToInt32(ht.Rows[ht.Rows.Count - 1][0]);
                        //LocoInfo.TrainInfo.ReportID = LocoInfo.TrainInfo.ReportID;
                    }
                }

                //插入乘务员信息
                param.Items.Clear();
                string dName = BaseLibrary.GetDriverName(dn);
                string sName = BaseLibrary.GetDriverName(sd);
                param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                param.Add("DriverNum", dn);
                param.Add("DriverName", dName);
                param.Add("SubDriverNum", sd);
                param.Add("SubDriverName", sName);
                param.Add("DutyTime", dateTime);
                param.Add("ReceiveTime", dateTime);
                bool result = DBAction.Insert(ETableName.Steward, param);

                //修改节点值
                param.Items.Clear();
                param.Add("ReportID", LocoInfo.TrainInfo.ReportID);
                param.Add("DriverNum", dn);
                DBAction.Update("RoboConfig", "ReportID,DriverNum", "ID=" + LocoInfo.TrainInfo.RoboConfig.Rows[0]["ID"].ToString(), param);

                //更新数据集信息
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig order by ID");
            }




        }
        #endregion

    }
}
