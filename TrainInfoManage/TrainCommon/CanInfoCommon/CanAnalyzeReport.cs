using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Windows.Forms;

namespace TrainCommon.CanInfoCommon
{
    /// <summary>
    /// 处理Can数据报单信息
    /// </summary>
    public class CanAnalyzeReport
    {
        /// <summary>
        /// 创建报单
        /// </summary>
        public static void CreateReport() 
        {
            try
            {
               
                using (RParams param = new RParams())
                {
                    //MessageBox.Show("进入方法");
                    //司机信息不为空
                    if (TrainCanInfo.TrainCan.DriverNum.Trim().Length>0)
                    {
                        //进出站状态
                        string signalType =TrainCanInfo.TrainCan.SignalType;
                        //MessageBox.Show("进出站状态------->" + signalType);
                        //车型
                        string trainType = TrainCanInfo.TrainCan.TypeNum;
                        //车号
                        string trainNum = TrainCanInfo.TrainCan.TrainNum;
                        //司机编号
                        string dn = TrainCanInfo.TrainCan.DriverNum;
                        #region  创建报单，插入报单头信息及乘务员信息
                        //判断是否已创建报单
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
                        if (dn != drnum)
                        {
                            DataDispose.CreateReport(dn,TrainCanInfo.TrainCan.AssDriverNum, trainType, trainNum, rc, dp,TrainCanInfo.TrainCan.CanDate+" "+TrainCanInfo.TrainCan.CanTime);//创建报单
                        }
                        else
                        {
                            LocoInfo.TrainInfo.ReportID = Convert.ToInt32(reportID);
                        }
                        #endregion
                        //创建司机记录
                       DataDispose.CreateLog(dn);

                        #region 添加运行编组信息
                        if (signalType.Trim() == "进站" || signalType.Trim() == "进出站" || signalType.Trim() == "出站")
                        {
                            int speed = Convert.ToInt32(TrainCanInfo.TrainCan.Speed);//速度
                            if (speed == 0)
                            {
                                //站名
                                string sn = "";
                                //出站时间
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
                                if (TrainCanInfo.TrainCan.StatioNo == BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo))
                                {
                                    //请求站名更新
                                    UpdateBaseLibrary.UpdateStationName(LocoInfo.TrainInfo.SckTrains, TrainCanInfo.TrainCan.StatioNo, TrainCanInfo.TrainCan.CrossRoadNo);
                                }

                                if (sn.Trim() != BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo))
                                {
                                    param.Items.Clear();
                                    param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                    param.Add("TrainNum", TrainCanInfo.TrainCan.VehicleTrips);//车次
                                    param.Add("StationName", BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo));//站名
                                    param.Add("ArrivedTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);//到达时分
                                    //param.Add("ArrivedTime", DateTime.Now.ToString());
                                    param.Add("SetOutTime", "");//出发时分
                                    param.Add("TotalWeight", TrainCanInfo.TrainCan.AllUp);//牵引总重
                                    param.Add("LoadWeight", TrainCanInfo.TrainCan.CarryLoad);//牵引载重
                                    param.Add("BusNumSum", TrainCanInfo.TrainCan.Carriage);//客车数量合计
                                    param.Add("WeightTruck", TrainCanInfo.TrainCan.HeavyCar);//重车
                                    param.Add("EmptyTruck", TrainCanInfo.TrainCan.EmptyCar);//空车
                                    param.Add("NoUseTruck", TrainCanInfo.TrainCan.NonServCar);//非运用车
                                    param.Add("GetPassenger", TrainCanInfo.TrainCan.GenPassCar);//代客车 
                                    param.Add("Summation", TrainCanInfo.TrainCan.NumberCar);//合计
                                    param.Add("TrainChange", TrainCanInfo.TrainCan.Length);//列车换长
                                    bool result = DBAction.Insert(ETableName.RunAndGroup, param);
                                }
                                else
                                {
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
                                //修改进站记录的出发时分
                                using (DataTable ins = DBAction.GetDTFromSQL("select ID,ArrivedTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID + " and  StationName='" + BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo) + "'"))
                                {
                                    if (ins.Rows.Count > 0)
                                    {
                                        if (ins.Rows[0][1].ToString().Trim().Length > 0)
                                        {
                                            int rgID = Convert.ToInt32(ins.Rows[0][0]);
                                            DateTime aTime = Convert.ToDateTime(ins.Rows[0][1]);
                                            TimeSpan ts = Convert.ToDateTime(TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime) - aTime;
                                            int minues = Convert.ToInt32(ts.TotalMinutes);//停车时分
                                            string ms = "";
                                            if (minues > 0)
                                            {
                                                ms = minues + "分钟";
                                            }
                                            param.Items.Clear();
                                            param.Add("SetOutTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);//出站时间
                                            param.Add("StopTime", ms);//停车时间
                                            DBAction.Update(ETableName.RunAndGroup, "SetOutTime,StopTime", "RHId=" + LocoInfo.TrainInfo.ReportID + " and  StationName='" + BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo) + "'", param);
                                        }
                                    }
                                    else
                                    {
                                        param.Items.Clear();
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        param.Add("TrainNum",TrainCanInfo.TrainCan.VehicleTrips);//车次
                                        param.Add("StationName", BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo));//站名
                                        param.Add("ArrivedTime", "");//到达时间
                                        param.Add("SetOutTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);//出发时间
                                        param.Add("TotalWeight", TrainCanInfo.TrainCan.AllUp);//牵引总重
                                        param.Add("LoadWeight", TrainCanInfo.TrainCan.CarryLoad);//牵引载重
                                        param.Add("BusNumSum", TrainCanInfo.TrainCan.Carriage);//客车数量合计
                                        param.Add("WeightTruck", TrainCanInfo.TrainCan.HeavyCar);//重车
                                        param.Add("EmptyTruck", TrainCanInfo.TrainCan.EmptyCar);//空车
                                        param.Add("NoUseTruck", TrainCanInfo.TrainCan.NonServCar);//非运用车
                                        param.Add("GetPassenger", TrainCanInfo.TrainCan.GenPassCar);//代客车 
                                        param.Add("Summation", TrainCanInfo.TrainCan.NumberCar);//合计
                                        param.Add("TrainChange", TrainCanInfo.TrainCan.Length);//列车换长
                                        bool result = DBAction.Insert(ETableName.RunAndGroup, param);
                                    }
                                }

                            }
                        }
                        #endregion

                        #region 添加出入段信息
                        string monitorState = TrainCanInfo.TrainCan.MonitorType;//机车监控状态
                        monitorState = monitorState.Replace("入段", "< ").Replace("出段", "> ");
                        int li = monitorState.Split(new char[] { '<' }).Length;
                        int lo = monitorState.Split(new char[] { '>' }).Length;
                        using (DataTable tioDT = DBAction.GetDTFromSQL("select * from TrainInOut where RHId=" + LocoInfo.TrainInfo.ReportID))
                        {
                            int count = tioDT.Rows.Count; string station = BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo);//获取站名
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
                                            param.Add("InLocalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                            param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                            DBAction.Insert(ETableName.TrainInOut, param);
                                        }
                                        else
                                        {
                                            param.Items.Clear();
                                            param.Add("InLocalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
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
                                            param.Add("InExternalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                            param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                            DBAction.Insert(ETableName.TrainInOut, param);
                                        }
                                        else
                                        {
                                            param.Items.Clear();
                                            param.Add("InExternalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
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
                                        param.Add("OutLocalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        DBAction.Insert(ETableName.TrainInOut, param);
                                    }
                                    else
                                    {
                                        param.Items.Clear();
                                        param.Add("OutLocalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                        DBAction.Update(ETableName.TrainInOut, "OutLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                    }

                                }
                                else
                                {
                                    if (count == 0)
                                    {
                                        param.Items.Clear();
                                        param.Add("OutExternalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                                        DBAction.Insert(ETableName.TrainInOut, param);
                                    }
                                    else
                                    {
                                        param.Items.Clear();
                                        param.Add("OutExternalTime", TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime);
                                        DBAction.Update(ETableName.TrainInOut, "OutExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogDaily.logerr(ex.ToString());
            }
        
        
        
        }
    }
}
