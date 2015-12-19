using System;

using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace TrainCommon.CanInfoCommon
{
    /// <summary>
    /// 发送运行信息
    /// </summary>
    public class CanSendRunInfo
    {
        /// <summary>
        /// 发送Can运行信息
        /// </summary>
        public static void SendRunInfo(SckTrains sck) 
        {
            try
            {
                //运行信息内容
                string strConment = "";
                #region 基本信息
                //车型
                string typeName = BaseLibrary.GetTrainType(TrainCanInfo.TrainCan.TypeNum);
                string trainType = typeName == "" ? "%" : typeName;
                strConment += typeName + ",";
                //车号
                string trainNum = TrainCanInfo.TrainCan.TrainNum == "" ? "%" : TrainCanInfo.TrainCan.TrainNum;
                strConment += trainNum + ",";
                //时间
                string recTime = TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime;
                strConment += recTime + ",";
                //速度
                string speed = TrainCanInfo.TrainCan.Speed;
                strConment += speed + ",";
                //限速
                string limitSpeed = TrainCanInfo.TrainCan.LimitSpeed;
                strConment += limitSpeed + ",";
                //距离
                string distance = TrainCanInfo.TrainCan.Distance;
                strConment += distance + ",";
                //信号机种类
                string annuType = TrainCanInfo.TrainCan.SignalType == "" ? "%" : TrainCanInfo.TrainCan.SignalType;
                strConment += annuType + ",";
                //公里标
                string kilometer = TrainCanInfo.TrainCan.KilometerPost == "" ? "%" : TrainCanInfo.TrainCan.KilometerPost;
                strConment += kilometer + ",";
                //监控状态
                string monitStyle = "%";
                if (TrainCanInfo.TrainCan.MonitorType.Trim().Length > 0)
                {
                    monitStyle = TrainCanInfo.TrainCan.MonitorType.Trim();
                }
                //MessageBox.Show("监控状态--->" + monitStyle);
                strConment += monitStyle + ",";
                //制动状态
                string brakStyle = "%";
                try
                {
                    string bop = TrainCanInfo.TrainCan.BrOutPut;
                    if (bop.Trim().Length > 0)
                    {
                        brakStyle = bop.Trim();
                    }
                }
                catch (Exception)
                {
                    
                }
                strConment += brakStyle + ",";
                //机车工况
                string workCond = "%";
                if (TrainCanInfo.TrainCan.WorkCond.Trim().Length > 0)
                {
                    workCond = TrainCanInfo.TrainCan.WorkCond.Trim();
                }
                //MessageBox.Show("机车工况---->" + workCond);
                strConment += workCond + ",";
                //信号机
                string annunciator = TrainCanInfo.TrainCan.CabSignal == "" ? "%" : TrainCanInfo.TrainCan.CabSignal;
                strConment += annunciator + ",";
                //管压
                string hrs = TrainCanInfo.TrainCan.TubeGageZero == "" ? "%" : TrainCanInfo.TrainCan.TubeGageZero;
                strConment += hrs + ",";
                //闸缸压力
                string cylinPress = TrainCanInfo.TrainCan.TubeGageOne == "" ? "%" : TrainCanInfo.TrainCan.TubeGageOne;
                strConment += cylinPress + ",";
                //均缸压力1
                string avgCylinOne = TrainCanInfo.TrainCan.TubeGageTwo == "" ? "%" : TrainCanInfo.TrainCan.TubeGageTwo;
                strConment += avgCylinOne + ",";
                //均缸压力2
                string avgCylinTwo = TrainCanInfo.TrainCan.TubeGageThree == "" ? "%" : TrainCanInfo.TrainCan.TubeGageThree;
                strConment += avgCylinTwo + ",";
                //柴油机转速
                string engSpeed = TrainCanInfo.TrainCan.DieselSpeed == "" ? "%" : TrainCanInfo.TrainCan.DieselSpeed;
                strConment += engSpeed + ",";
                //本补客货
                string carOrTruck = TrainCanInfo.TrainCan.FillPassCargo == "" ? "%" : TrainCanInfo.TrainCan.FillPassCargo;
                strConment += carOrTruck + ",";
                //司机
                string driverName = BaseLibrary.GetDriverName(TrainCanInfo.TrainCan.DriverNum);
                driverName = driverName == "" ? "%" : driverName;
                strConment += driverName + ",";
                //副司机
                string assDriver = BaseLibrary.GetDriverName(TrainCanInfo.TrainCan.AssDriverNum);
                assDriver = assDriver == "" ? "%" : assDriver;
                strConment += assDriver + ",";
                //车次
                string vehicleTrips = TrainCanInfo.TrainCan.VehicleTrips == "" ? "%" : TrainCanInfo.TrainCan.VehicleTrips;
                strConment += vehicleTrips + ",";
                //站名
                string stationName = BaseLibrary.GetStationName(TrainCanInfo.TrainCan.CrossRoadNo, TrainCanInfo.TrainCan.StatioNo);
                stationName = stationName == "" ? "%" : stationName;
                strConment += stationName + ",";
                //总重
                string grossWeight = TrainCanInfo.TrainCan.AllUp == "" ? "%" : TrainCanInfo.TrainCan.AllUp;
                strConment += grossWeight + ",";
                //辆数
                string countCar = TrainCanInfo.TrainCan.NumberCar;
                strConment += countCar + ",";
                //换长
                string totalLength = TrainCanInfo.TrainCan.Length;
                strConment += totalLength + ",";
                //载重
                string totalLoad = TrainCanInfo.TrainCan.CarryLoad;
                strConment += totalLoad + ",";
                //客车
                string carriage = TrainCanInfo.TrainCan.Carriage;
                strConment += carriage + ",";
                //重车
                string heavyCar = TrainCanInfo.TrainCan.HeavyCar;
                strConment += heavyCar + ",";
                //空车
                string emptyCar = TrainCanInfo.TrainCan.EmptyCar;
                strConment += emptyCar + ",";
                //非运用车
                string nonServCar = TrainCanInfo.TrainCan.NonServCar;
                strConment += nonServCar + ",";
                //代客车
                string replaceCar = TrainCanInfo.TrainCan.GenPassCar;
                strConment += replaceCar + ",";
                //守车
                string caboose = TrainCanInfo.TrainCan.CabooseCar;
                strConment += caboose;
                #endregion
                //MessageBox.Show("运行信息内容------->" + strConment);
                #region 组包发送机车运行信息
                SckParams param = new SckParams();
                param.Tml = "Cmd,OpenTime,OnLineTime,OffLineTime,RunTime,Coordinates,Version,RunInfo";
                //命令标识
                param.Add("Cmd", "0101", false);//命令标识符

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


                //MessageBox.Show("组包------->" + strConment);

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
                //坐标
                if (LocoInfo.TrainInfo.Longitude != "0.00" && LocoInfo.TrainInfo.Latitude != "0.00")
                {
                    string coors = LocoInfo.TrainInfo.Longitude + "," + LocoInfo.TrainInfo.Latitude;
                    param.Add("Coordinates", coors, false);
                }
                else
                {
                    param.Add("Coordinates", "0", false);
                }
                //BaseVoice.TrainVoice.SpeekVioce("版本号" + FileUpdate.baseversion);
                param.Add("Version", FileUpdate.baseversion, false);//版本信息
                param.Add("RunInfo", strConment, true);//当前运行信息
                List<string> packs = param.CreatePacks();
                //MessageBox.Show("发送运行信息----->" + packs[0]);
                sck.Send(param, "UDP");
                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }



        }
    }
}
