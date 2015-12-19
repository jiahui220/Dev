using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Net;
using System.Threading;


namespace TrainCommon
{
    public  class BaseLibrary
    {

        public static bool send = false;


        /// <summary>
        /// 判定IP地址是否合法
        /// </summary>
        /// <param name="IPAddress">IP地址</param>
        /// <returns></returns>
        public static bool CheckIPAddress(string IPAddress)
        {
            Regex rx = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            if (rx.IsMatch(IPAddress))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 根据司机号获取司机姓名
        /// </summary>
        /// <param name="DriverCode">司机号</param>
        /// <returns>司机姓名</returns>
        public static string GetDriverName(string DriverCode) {

            using (DataTable dt = DBAction.GetDTFromSQL("select sSJName from DriverDiction where iSJCode='" + DriverCode+"'"))
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return DriverCode;
                }
            }

        }

        /// <summary>
        /// 根据车型号获取机车车型车号
        /// </summary>
        /// <param name="TrainCode">机车型号编码</param>
        /// <returns>机车车型车号</returns>
        public static string GetTrainType(string TrainCode)
        {
            using (DataTable tt = DBAction.GetDTFromSQL("select sJXName from TrainTypeDiction where iJXCode='" + TrainCode+"'"))
            {
                if (tt.Rows.Count > 0)
                {
                    return tt.Rows[0][0].ToString();
                }
                else
                {
                    return TrainCode;
                }
            } 
        }

        /// <summary>
        /// 根据交路号和车站号获取车站名称
        /// </summary>
        /// <param name="iJLCode">交路号</param>
        /// <param name="iCZCode">车站号</param>
        /// <returns>车站名称</returns>
        public static string GetStationName(string iJLCode, string iCZCode)
        {
            using (DataTable st = DBAction.GetDTFromSQL("select sCZName from StationDiction where iJLCode='" + iJLCode + "' and iCZCode='" + iCZCode+"' "))
            {
                if (st.Rows.Count > 0)
                {
                    return st.Rows[0][0].ToString();
                }
                else
                {
                    return iCZCode;
                }
            }
        }

        //获取服务器地址
        public static void getUrl()
        {

            if (LocoInfo.TrainInfo.Url == "")
            {
                XmlAction xa = new XmlAction();
                XmlNode node = xa.GetNode("/RoboConfig/ServiceAddress", null, null);//服务地址
                XmlNode port = xa.GetNode("/RoboConfig/ServicePort", null, null);//服务端口
                LocoInfo.TrainInfo.Url = xa.GetText(node) + ":" + xa.GetText(port);//服务地址
            }
        }


        //获取服务器socket通信地址及端口
        public static void getSocketInfo() 
        {

            if (LocoInfo.TrainInfo.SocketPort==0)
            {
                XmlAction xa = new XmlAction();
                XmlNode node = xa.GetNode("/RoboConfig/ServiceAddress", null, null);//服务地址
                XmlNode port = xa.GetNode("/RoboConfig/SocketPort", null, null);//服务Socket监听端口
                LocoInfo.TrainInfo.IpAddress = xa.GetText(node);
                LocoInfo.TrainInfo.SocketPort = Convert.ToInt32(xa.GetText(port));
            }

        }


        #region 同步LKJ时间
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        [DllImport("CoreDll.DLL")]
        public static extern bool SetLocalTime(ref SYSTEMTIME time);

        public static void SyncTime(DateTime dateTime)
        {

            SYSTEMTIME st = new SYSTEMTIME();
            st.wYear = Convert.ToUInt16(dateTime.Year);
            st.wMonth = Convert.ToUInt16(dateTime.Month);
            st.wDay = Convert.ToUInt16(dateTime.Day);
            st.wHour = Convert.ToUInt16(dateTime.Hour);
            st.wMinute = Convert.ToUInt16(dateTime.Minute);
            st.wSecond = Convert.ToUInt16(dateTime.Second);
            try
            {
                bool re = SetLocalTime(ref st);
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
            }       
        }
        #endregion

        /// <summary>
        /// 清除历史记录数据，保持记录数据在200之内
        /// </summary>
        public static void delHistoryLog() {
           //司机日志记录超出1000条时，删除历史记录数据
            delLogByCount("DriverLog", 1000);
           //通知记录超出1000条时，删除历史记录数据
            delLogByCount("Announcement", 1000);
            //报警记录超出1000条时,删除历史记录数据
            delLogByCount("AlarmLog", 1000);
            //报警运行信息记录超出1000条时，删除历史记录数据
            delLogByCount("AlarmRunInfo", 1000);
            //运行记录数据大于2000条时，删除历史记录数据 
            delLogByCount("TrainRuning", 2000);
            //获取报表记录
            using (DataTable rt = DBAction.GetDTFromSQL("select ID from ReportHeader order by ID "))
            {
                if (rt.Rows.Count > 200)
                {
                    int id = Convert.ToInt32(rt.Rows[99][0]);
                    using (DataTable st = DBAction.GetDTFromSQL("select ID from ReportHeader where ID<" + id))
                    {
                        if (st.Rows.Count > 0)
                        {
                            for (int i = 0; i < st.Rows.Count; i++)
                            {
                                //删除该报表相关联的的所有信息
                                DBAction.Delete("ReportHeader", "ID=" + id);//删除报表头信息
                                DBAction.Delete("Steward", "RHId=" + id);//删除乘务员信息
                                DBAction.Delete("TrainInOut", "RHId=" + id);//删除出入段信息
                                DBAction.Delete("TrainGetFuel", "RHId=" + id);//删除报单领取燃料信息
                                DBAction.Delete("Reconnection", "RHId=" + id);//删除报单补机重联信息
                                DBAction.Delete("RunAndGroup", "RHId=" + id);//删除报单运行编组信息
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除数据表记录大于额定数的历史数据
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="count">额定数据条数</param>
        public static void delLogByCount(string table,int count) {

            //判断记录是否超出200条
            using (DataTable dtLog = DBAction.GetDTFromSQL("select ID from " + table + " order by ID "))
            {
                //获取已记录条数
                int logCount = dtLog.Rows.Count;
                int id = 0;
                //判定已记录条数是否超出保留条数上限
                if (logCount > count)
                {
                    
                    //超出上限，则删除部分信息，保留上限值一半的条数
                    int halfCount = count / 2;
                    //获取记录一半的条数的ID值
                    id = Convert.ToInt32(dtLog.Rows[logCount - halfCount][0]);
                    //删除小于该ID的所有数据
                    DBAction.Delete(table, "ID<" + id);
                }
            }
        }


        /// <summary>
        /// 公里标换算除以1000
        /// </summary>
        /// <param name="kilo"></param>
        /// <returns></returns>
        public static string KiloConversion(string kilo)
        {
            try
            {
                //公里标换算
                string[] kl = kilo.Split(new char[] { ' ' });
                if (kl.Length == 2)
                {
                    double k = Convert.ToDouble(kl[1]) / 1000;
                    kilo = kl[0] + " " + k;
                }
                return kilo;
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                return kilo;
            }

        }

        #region  发送运行信息
        /// <summary>
        /// 发送运行信息
        /// </summary>
        /// <param name="socket"></param>
        public static void SendRunInfo(SckTrains socket, string item) 
        {
            SckParams param = new SckParams();
            try
            {
                //MessageBox.Show("运行信息请求");
                //包模板
                param.Tml = "Cmd,OpenTime,OnLineTime,OffLineTime,RunTime,Coordinates,Version,RunInfo";
                //命令标识
                param.Add("Cmd", "0102", false);//命令标识符


                #region 获取记录时间
                //开机时间
                XmlNode openTimeNode = XmlHelper.GetNode("/RoboConfig/OpenTime", null, null);
                string openTime = XmlHelper.GetNodeText(openTimeNode);
                //上线时间
                XmlNode onLineTimeNode = XmlHelper.GetNode("/RoboConfig/OnLineTime", null, null);
                string onLineTime = XmlHelper.GetNodeText(onLineTimeNode);
                //下线时间
                XmlNode offLineTimeNode = XmlHelper.GetNode("/RoboConfig/OffLineTime", null, null);
                string offLineTime = XmlHelper.GetNodeText(offLineTimeNode);

                #region 判定机车时间记录是否为空值
                openTime = openTime == "" ? "%" : openTime;
                onLineTime = onLineTime == "" ? "%" : onLineTime;
                offLineTime = offLineTime == "" ? "%" : offLineTime;
                #endregion


                #endregion

                param.Add("OpenTime", openTime, false);//开机时间，默认为软件启动时间
                param.Add("OnLineTime", onLineTime, false);//上线时间
                param.Add("OffLineTime", offLineTime, false);//下线时间
                if (openTime != "%")
                {
                    TimeSpan ts = DateTime.Now - Convert.ToDateTime(BaseLibrary.ConversionTime(openTime, 0));
                    param.Add("RunTime", Convert.ToInt32(ts.Minutes) + "", false);//运行时间
                }
                else
                {
                    param.Add("RunTime", "0", false);//运行时间
                }
                if (LocoInfo.TrainInfo.Longitude != "0.00" && LocoInfo.TrainInfo.Latitude != "0.00")
                {
                    string coors = LocoInfo.TrainInfo.Longitude + "," + LocoInfo.TrainInfo.Latitude;
                    param.Add("Coordinates", coors, false);
                }
                else
                {
                    param.Add("Coordinates", "0", false);
                }
                param.Add("Version",FileUpdate.baseversion, false);
                param.Add("RunInfo", LocoInfo.TrainInfo.CurrRunInfo, true);//当前运行信息
                List<string> packs = param.CreatePacks();
                //MessageBox.Show("发送运行信息----->" + packs[0]); 
                //MessageBox.Show("开始发送");
                socket.Send(param, "UDP");
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show("发送异常"+ex.ToString());
            }
        }
        #endregion

        #region 获取开机时间
        public static string getOpenTime() 
        {
            string openTime = "";
            try
            {
                //开机时间
                XmlNode openTimeNode = XmlHelper.GetNode("/RoboConfig/OpenTime", null, null);
                openTime = XmlHelper.GetNodeText(openTimeNode);
                openTime = openTime == "" ? "%" : openTime;
            }
            catch (Exception ex)
            {
                openTime = "%"; 
            }
            return openTime;
        } 
        #endregion




        #region 报警向服务实时发送报警信息
        /// <summary>
        /// 报警向服务实时发送报警信息
        /// </summary>
        public static void SendWarn(SckTrains socket,string item)
        {
            try
            {
                //数据库查询语句
                string strSql = "";
                //获取已发送报警ID
                int sid = 0;
                string logNum = "";
                DataTable lt =null;
                int logID = 0;
                //当前是否含有报警信息
                if (LocoInfo.TrainInfo.CurrWarnId!=0)
                {
                    //读取已发送的报警ID


                    strSql = "select DriverNum,WarnSendID from SendLog where ID="+LocoInfo.TrainInfo.LogId;
                    using (lt = DBAction.GetDTFromSQL(strSql))
                    {
                        if (lt.Rows.Count > 0)
                        {
                            sid = Convert.ToInt32(lt.Rows[0]["WarnSendID"]);
                            //判定当前司机报警是否为第一条报警
                            if (sid == 0)
                            {
                                sid = LocoInfo.TrainInfo.CurrWarnId - 1;
                            }
                            logNum = lt.Rows[0]["DriverNum"].ToString();
                            logID = LocoInfo.TrainInfo.LogId;
                        }
                    }
                }
                else
                {
                    //当前未报警则查询以往未发送报警信息
                    strSql = "select ID,DriverNum,WarnSendID from SendLog where WarnSendID!='0'";
                    using (lt = DBAction.GetDTFromSQL(strSql))
                    {
                        if (lt.Rows.Count > 0)
                        {
                            sid = Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["WarnSendID"]);
                            logID = Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["ID"]);
                            logNum = lt.Rows[0]["DriverNum"].ToString();
                        }
                    }

                }
                //MessageBox.Show("司机编号-------->"+logNum);
                //获取未发送的报警
                using (DataTable wt = DBAction.GetDTFromSQL("select * from AlarmRunInfo where DriverNum='" + logNum + "' and ID>" + sid))
                {
                    //判断是否存在未发送报警信息
                    if (wt.Rows.Count > 0)
                    {
                        //报警时间
                        string createTime = wt.Rows[0]["CreateTime"].ToString();
                        string typeNum = wt.Rows[0]["TypeNum"].ToString();
                        string trainNum = wt.Rows[0]["TrainId"].ToString();
                        string driverNum = wt.Rows[0]["DriverNum"].ToString();
                        string subDriverNum = wt.Rows[0]["SubDriverNum"].ToString();
                        string warnItems = wt.Rows[0]["WarnItems"].ToString();
                        //最后一条历史报警记录
                        string lastWarnID = wt.Rows[wt.Rows.Count - 1]["WarnItems"].ToString();
                        //当前发送的报警记录
                        string sendWarnID = wt.Rows[0]["ID"].ToString();
                        DateTime currDate = Convert.ToDateTime(createTime);
                        SckParams param = new SckParams();
                        //发送报警头信息
                        param.Tml = "Cmd,Style,Type,CreateTime,TypeNum,TrainId,DriverNum,SubDriverNum,WarnID,WarnItems";
                        param.Add("Cmd", "0301", false);
                        param.Add("Style", "00", false);
                        param.Add("Type", "00", false);//报警项点信息
                        param.Add("CreateTime", createTime, false);
                        param.Add("TypeNum", typeNum, false);
                        param.Add("TrainId", trainNum, false);
                        param.Add("DriverNum", driverNum, false);
                        param.Add("SubDriverNum", subDriverNum, false);
                        param.Add("WarnID", sendWarnID, false);
                        param.Add("WarnItems", warnItems.Trim(), true);
                        param.CreatePacks();
                        socket.Send(param);
                        strSql = "select TrainRunInfo from TrainRuning where RecordTime between '" + currDate.AddMinutes(-10) + "' and '" + currDate.AddMinutes(10) + "' ";
                        using (DataTable wrt = DBAction.GetDTFromSQL(strSql))
                        {
                            if (wrt.Rows.Count > 0)
                            {
                                string[] runlogs = new string[wrt.Rows.Count];
                                for (int i = 0; i < wrt.Rows.Count; i++)
                                {
                                    runlogs[i] = wrt.Rows[0][0].ToString();
                                }
                                //发送报警运行信息
                                param.Clear();
                                param.Tml = "Cmd,Style,CreateTime,TypeNum,TrainId,DriverNum,SubDriverNum,WarnRun";
                                param.Add("Cmd", "0302", false);
                                param.Add("Style", "00", false);
                                param.Add("CreateTime", createTime, false);
                                param.Add("TypeNum", typeNum, false);
                                param.Add("TrainId", trainNum, false);
                                param.Add("DriverNum", driverNum, false);
                                param.Add("SubDriverNum", subDriverNum, false);
                                param.Add("WarnRun", runlogs, true);
                                param.CreatePacks();
                                socket.Send(param);
                            }
                            //判断历史未发送记录是否发送完成
                            if (lastWarnID == sendWarnID && logID != LocoInfo.TrainInfo.LogId)
                            {
                                //发送完成则修改为0
                                upSendWarnID("0", logID);
                            }
                            else
                            {
                                //修改
                                upSendWarnID(wt.Rows[0]["ID"].ToString(), logID);
                            }
                        }
                        }
                    }
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show("异常"+ex.ToString());
            }
        }
        #endregion

        #region 自动提交报单信息
        public static void SendReport(SckTrains socket, string item)
        {
            SckParams param = new SckParams();
            bool result = false;
            string serverGroup = "";
            try
            {
                //报单ID
                int rid = LocoInfo.TrainInfo.ReportID;
                //获取报单头信息
                using (DataTable rh = DBAction.GetDTFromSQL("select CreateTime,RailCorp,Depot,TrainType,TrainNum from ReportHeader where ID=" + rid))
                //乘务员信息
                using (DataTable st = DBAction.GetDTFromSQL("select DriverNum,SubDriverNum,DutyTime,ReceiveTime,DeliverTime from Steward where RHId=" + rid))
                //出入段信息
                using (DataTable ot = DBAction.GetDTFromSQL("select OutLocalTime,InExternalTime,OutExternalTime,InLocalTime from TrainInOut where  RHId=" + rid))
                //领取燃料
                using (DataTable ft = DBAction.GetDTFromSQL("select Receive,Deliver from TrainGetFuel where RHId=" + rid))
                {
                    //组合报表头内容
                    string strConment = "";
                    if (rh.Rows.Count > 0)
                    {
                        strConment += rh.Rows[0]["CreateTime"].ToString() == "" ? "%," : rh.Rows[0]["CreateTime"].ToString() + ",";
                        strConment += rh.Rows[0]["RailCorp"].ToString() == "" ? "%," : rh.Rows[0]["RailCorp"].ToString() + ",";
                        strConment += rh.Rows[0]["Depot"].ToString() == "" ? "%," : rh.Rows[0]["Depot"].ToString() + ",";
                        strConment += rh.Rows[0]["TrainType"].ToString() == "" ? "%," : rh.Rows[0]["TrainType"].ToString() + ",";
                        strConment += rh.Rows[0]["TrainNum"].ToString() == "" ? "%," : rh.Rows[0]["TrainNum"].ToString() + ",";
                        //MessageBox.Show("1");
                    }
                    else
                    {
                        strConment += "%,%,%,%,%,";
                        //MessageBox.Show("1");
                    }

                    if (st.Rows.Count > 0)
                    {
                        strConment += st.Rows[0]["DriverNum"].ToString() == "" ? "%," : st.Rows[0]["DriverNum"].ToString() + ",";
                        strConment += st.Rows[0]["SubDriverNum"].ToString() == "" ? "%," : st.Rows[0]["SubDriverNum"].ToString() + ",";
                        strConment += st.Rows[0]["DutyTime"].ToString() == "" ? "%," : st.Rows[0]["DutyTime"].ToString() + ",";
                        strConment += st.Rows[0]["ReceiveTime"].ToString() == "" ? "%," : st.Rows[0]["ReceiveTime"].ToString() + ",";
                        strConment += st.Rows[0]["DeliverTime"].ToString() == "" ? "%," : st.Rows[0]["DeliverTime"].ToString() + ",";
                        //MessageBox.Show("2");
                    }
                    else
                    {
                        strConment += "%,%,%,%,%,";
                        //MessageBox.Show("2");
                    }

                    if (ot.Rows.Count > 0)
                    {
                        strConment += ot.Rows[0]["OutLocalTime"].ToString() == "" ? "%," : ot.Rows[0]["OutLocalTime"].ToString() + ",";
                        strConment += ot.Rows[0]["InExternalTime"].ToString() == "" ? "%," : ot.Rows[0]["InExternalTime"].ToString() + ",";
                        strConment += ot.Rows[0]["OutExternalTime"].ToString() == "" ? "%," : ot.Rows[0]["OutExternalTime"].ToString() + ",";
                        strConment += ot.Rows[0]["InLocalTime"].ToString() == "" ? "%," : ot.Rows[0]["InLocalTime"].ToString() + ",";
                        //MessageBox.Show("3");
                    }
                    else
                    {
                        //MessageBox.Show("3");
                        strConment += "%,%,%,%,";
                    }
                    if (ft.Rows.Count > 0)
                    {
                        //MessageBox.Show("4");
                        strConment += ft.Rows[0]["Receive"].ToString() == "" ? "%," : ft.Rows[0]["Receive"].ToString() + ",";
                        strConment += ft.Rows[0]["Deliver"].ToString() == "" ? "%" : ft.Rows[0]["Deliver"].ToString();
                    }
                    else
                    {
                        //MessageBox.Show("4");
                        strConment += "%,%";
                    }
                    //MessageBox.Show("内容---------------->"+strConment);
                    //车型
                    string typeNum = LocoInfo.TrainInfo.TypeNum;
                    //车号
                    string trainNum = LocoInfo.TrainInfo.TrainNum;
                    //创建时间
                    string createTime = rh.Rows[0]["CreateTime"].ToString();

                    //发送报单头
                    param.Tml = "Cmd,Style,TypeNum,TrainNum,CreateTime,Conment";
                    param.Add("Cmd", "0201", false);
                    param.Add("Style", "00", false);
                    param.Add("TypeNum", typeNum, false);
                    param.Add("TrainNum", trainNum, false);
                    param.Add("CreateTime", createTime, false);
                    param.Add("Conment", strConment, true);
                    param.CreatePacks();
                    send = true;
                    result = socket.Send(param);

                    #region  查询未发送的报单编组
                    //已发送运行编组ID
                    int gid = 0;
                    using (DataTable rb = DBAction.GetDTFromSQL("select RunGroupID from RoboConfig "))
                    {
                        if (rb.Rows.Count > 0)
                        {
                            string rg = rb.Rows[0]["RunGroupID"].ToString().Trim();
                            gid = rg == "" ? 0 : Convert.ToInt32(rb.Rows[0]["RunGroupID"]);
                        }
                        string strSql = "select * from RunAndGroup where RHId=" + rid + " and ID>" + (gid - 1);
                        if (serverGroup.Trim().Length > 0)
                        {
                            strSql += " and ID not in (" + serverGroup.Trim() + ") ";
                        }
                        using (DataTable gt = DBAction.GetDTFromSQL(strSql))
                        {

                            for (int i = 0; i < gt.Rows.Count; i++)
                            {
                                string g0 = gt.Rows[i]["TrainNum"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TrainNum"].ToString().Trim();
                                string g1 = gt.Rows[i]["StationName"].ToString().Trim() == "" ? "%" : gt.Rows[i]["StationName"].ToString().Trim();
                                string g2 = gt.Rows[i]["ArrivedTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["ArrivedTime"].ToString().Trim();
                                string g3 = gt.Rows[i]["SetOutTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SetOutTime"].ToString().Trim();
                                string g4 = gt.Rows[i]["StopTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["StopTime"].ToString().Trim();
                                string g5 = gt.Rows[i]["SwitchTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SwitchTime"].ToString().Trim();
                                string g6 = gt.Rows[i]["OutStopTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["OutStopTime"].ToString().Trim();
                                string g7 = gt.Rows[i]["EarlyTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["EarlyTime"].ToString().Trim();
                                string g8 = gt.Rows[i]["LateTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["LateTime"].ToString().Trim();
                                string g9 = gt.Rows[i]["Reason"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Reason"].ToString().Trim();
                                string g10 = gt.Rows[i]["WorkMark"].ToString().Trim() == "" ? "%" : gt.Rows[i]["WorkMark"].ToString().Trim();
                                string g11 = gt.Rows[i]["RegionCode"].ToString().Trim() == "" ? "%" : gt.Rows[i]["RegionCode"].ToString().Trim();
                                string g12 = gt.Rows[i]["RegionDistance"].ToString().Trim() == "" ? "%" : gt.Rows[i]["RegionDistance"].ToString().Trim();
                                string g13 = gt.Rows[i]["TotalWeight"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TotalWeight"].ToString().Trim();
                                string g14 = gt.Rows[i]["LoadWeight"].ToString().Trim() == "" ? "%" : gt.Rows[i]["LoadWeight"].ToString().Trim();
                                string g15 = gt.Rows[i]["BusNumSum"].ToString().Trim() == "" ? "%" : gt.Rows[i]["BusNumSum"].ToString().Trim();
                                string g16 = gt.Rows[i]["Supplementtary"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Supplementtary"].ToString().Trim();
                                string g17 = gt.Rows[i]["SuppOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SuppOffice"].ToString().Trim();
                                string g18 = gt.Rows[i]["WeightTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["WeightTruck"].ToString().Trim();
                                string g19 = gt.Rows[i]["EmptyTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["EmptyTruck"].ToString().Trim();
                                string g20 = gt.Rows[i]["NoUseTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["NoUseTruck"].ToString().Trim();
                                string g21 = gt.Rows[i]["GetPassenger"].ToString().Trim() == "" ? "%" : gt.Rows[i]["GetPassenger"].ToString().Trim();
                                string g22 = gt.Rows[i]["Other"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Other"].ToString().Trim();
                                string g23 = gt.Rows[i]["Summation"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Summation"].ToString().Trim();
                                string g24 = gt.Rows[i]["TrainChange"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TrainChange"].ToString().Trim();
                                string g25 = gt.Rows[i]["BelongOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["BelongOffice"].ToString().Trim();
                                string g26 = gt.Rows[i]["UndertakeOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["UndertakeOffice"].ToString().Trim();
                                string g27 = gt.Rows[i]["UndertakeOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["UndertakeOffice"].ToString().Trim();

                                string group = g0 + "," + g1 + "," + g2 + "," + g3 + "," + g4 + "," + g5 + "," + g6 + "," + g7 + "," + g8 + "," + g9 + "," + g10 + "," + g11 + "," + g12 + "," + g13 + "," + g14 + "," + g15 + "," + g16 + "," + g17 + "," + g18 + "," + g19 + "," + g20 + "," + g21 + "," + g22 + "," + g23 + "," + g24 + "," + g25 + "," + g26 + "," + g27 + "," + gt.Rows[i]["ID"].ToString().Trim();
                                //发送报单运行编组
                                param = new SckParams();
                                param.Clear();
                                param.Tml = "Cmd,Style,TypeNum,TrainNum,CreateTime,Group";
                                param.Add("Cmd", "0202", false);
                                param.Add("Style", "00", false);
                                param.Add("TypeNum", typeNum, false);
                                param.Add("TrainNum", trainNum, false);
                                param.Add("CreateTime", createTime, false);
                                param.Add("Group", group, true);
                                param.CreatePacks();
                                result = socket.Send(param);
                                send = false;
                                //设置已发送报单运行编组ID
                                upSendGroupID(gt.Rows[0]["ID"].ToString());
                                //Thread.Sleep(500);
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //return false;
                //MessageBox.Show("发送报单异常"+ex.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 更新已提交编组ID
        /// </summary>
        /// <param name="id"></param>
        public static void upSendGroupID(string id)
        {
            using (RParams param = new RParams())
            {
                param.Add("RunGroupID", id);
                DBAction.Update("RoboConfig", "RunGroupID", "ID=1", param);
            }

        }

        /// <summary>
        /// 更新已提交报警信息
        /// </summary>
        /// <param name="id"></param>
        public static void upSendWarnID(string id,int logID)
        {
            using (RParams param = new RParams())
            {
                param.Add("WarnSendID", id);
                DBAction.Update("SendLog", "WarnSendID", "ID=" + logID, param);
            }
        } 

        #region 车载监听

        /// <summary>
        /// 获取消息类型
        /// </summary>
        /// <param name="receinfo">接收到的完整数据</param>
        /// <returns></returns>
        public static string GetSocketType(string receinfo)
        {
            //除去首尾字符
            receinfo = receinfo.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = receinfo.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取命令行
            string cmdType = headItems[0];
            return cmdType;
        }

        #endregion


        /// <summary>
        /// 换算时间
        /// </summary>
        /// <param name="strTime">yyMMddHHmmss时间格式字符串</param>
        /// <param name="returnType">返回类型,0：完整时间 1：年月日 2： 时分秒 3：年 4：月 5：日
        /// 6：时 7：分 8：秒</param>
        /// <returns></returns>
        public static string ConversionTime(string strTime, int returnType)
        {
            //错误时间字符串，返回空
            if (strTime.Length != 12)
            {
                return "";
            }
            //获取年部分
            string year = strTime.Substring(0, 2);
            //获取月部分
            string month = strTime.Substring(2, 2);
            //获取日期部分
            string day = strTime.Substring(4, 2);
            //获取小时
            string hour = strTime.Substring(6, 2);
            //获取分钟
            string minutes = strTime.Substring(8, 2);
            //获取秒
            string second = strTime.Substring(10, 2);

            string date = "";

            switch (returnType)
            {
                case 0:
                    date = "20" + year + "-" + month + "-" + day + " " + hour + ":" + minutes + ":" + second;
                    break;
                case 1:
                    date = "20" + year + "-" + month + "-" + day;
                    break;
                case 2:
                    date = hour + ":" + minutes + ":" + second;
                    break;
                case 3:
                    date = year;
                    break;
                case 4:
                    date = month;
                    break;
                case 5:
                    date = day;
                    break;
                case 6:
                    date = hour;
                    break;
                case 7:
                    date = minutes;
                    break;
                case 8:
                    date = second;
                    break;
                default:
                    break;
            }
            return date;
        }

        /// <summary>
        /// 获取关机前最后一条运行记录信息
        /// </summary>
        public static void getLastRunInfo() 
        {
            using (DataTable rt=DBAction.GetDTFromSQL("select TrainRunInfo,RecordTime from TrainRuning "))
            {
                int count=rt.Rows.Count;
                if (count>0)
                {
                    LocoInfo.TrainInfo.OldLogRuning = rt.Rows[count - 1]["TrainRunInfo"].ToString();
                    LocoInfo.TrainInfo.OldLogTime = rt.Rows[count - 1]["RecordTime"].ToString();
                }
            }
        }

        /// <summary>
        /// 发送上一次关机前的最后一条运行信息
        /// </summary>
        public static void sendLastRunInfo(SckTrains sck) 
        {
            try
            {
                if (LocoInfo.TrainInfo.OldLogTime != "" && LocoInfo.TrainInfo.OldLogRuning != "" && LocoInfo.TrainInfo.CurrDateTime.Trim().Length > 0)
                {
                    TimeSpan ts = Convert.ToDateTime(LocoInfo.TrainInfo.CurrDateTime) - Convert.ToDateTime(LocoInfo.TrainInfo.OldLogTime);
                    if (ts.TotalMinutes > 5)
                    {
                        SckParams param = new SckParams();
                        //数据包格式
                        param.Tml = "Cmd,LogTime,LogOpenTime,RunContent";
                        param.Add("Cmd", "1300", false);
                        param.Add("LogTime", LocoInfo.TrainInfo.OldLogTime, false);
                        param.Add("LogOpenTime", LocoInfo.TrainInfo.OldOpenTime, false);
                        param.Add("RunContent", LocoInfo.TrainInfo.OldLogRuning, true);
                        List<string> packs = param.CreatePacks();
                        sck.Send(param);
                    }
                    else
                    {
                        LocoInfo.TrainInfo.OldIsSend = true;
                    }

                }
            }
            catch (Exception ex)
            {
                
            }            
        }
    }
}
