using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon.CanInfoCommon
{
    /// <summary>
    /// Can数据实体类
    /// </summary>
    public class TrainCanInfo
    {
        #region 静态实例成员
        private static TrainCanInfo _TrainCan = null;

        /// <summary>
        /// 当前机车基本信息
        /// </summary>
        public static TrainCanInfo TrainCan
        {
            get
            {
                if (_TrainCan == null)
                    _TrainCan = new TrainCanInfo();
                return _TrainCan;
            }
        }
        #endregion

        #region 优先级50数据
        //速度
        private string _Speed = "0";

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        //限速
        private string _LimitSpeed = "0";

        /// <summary>
        /// 限速
        /// </summary>
        public string LimitSpeed
        {
            get { return _LimitSpeed; }
            set { _LimitSpeed = value; }
        }

        //时间
        private string _CanTime = "";

        /// <summary>
        /// 时间
        /// </summary>
        public string CanTime
        {
            get { return _CanTime; }
            set { _CanTime = value; }
        }

        //机车工况
        private string _WorkCond = "";

        /// <summary>
        /// 机车工况
        /// </summary>
        public string WorkCond
        {
            get { return _WorkCond; }
            set { _WorkCond = value; }
        }

        //机车信号
        private string _CabSignal = "";

        /// <summary>
        /// 机车信号
        /// </summary>
        public string CabSignal
        {
            get { return _CabSignal; }
            set { _CabSignal = value; }
        }

        //制动输出
        private string _BrOutPut = "";

        /// <summary>
        /// 制动输出
        /// </summary>
        public string BrOutPut
        {
            get { return _BrOutPut; }
            set { _BrOutPut = value; }
        }

        //公里标
        private string _KilometerPost = "";

        /// <summary>
        /// 公里标
        /// </summary>
        public string KilometerPost
        {
            get { return _KilometerPost; }
            set { _KilometerPost = value; }
        }

        //距离
        private string _Distance = "0";

        /// <summary>
        /// 距离
        /// </summary>
        public string Distance
        {
            get { return _Distance; }
            set { _Distance = value; }
        }

        //信号机编号
        private string _SignalNum = "";

        /// <summary>
        /// 信号机编号
        /// </summary>
        public string SignalNum
        {
            get { return _SignalNum; }
            set { _SignalNum = value; }
        }

        //信号机种类
        private string _SignalType = "";

        /// <summary>
        /// 信号机种类
        /// </summary>
        public string SignalType
        {
            get { return _SignalType; }
            set { _SignalType = value; }
        }

        //监控状态
        private string _MonitorType = "";

        /// <summary>
        /// 监控状态
        /// </summary>
        public string MonitorType
        {
            get { return _MonitorType; }
            set { _MonitorType = value; }
        }

        #endregion

        #region 优先级58数据
        //司机编号
        private string _DriverNum = "";

        /// <summary>
        /// 司机编号
        /// </summary>
        public string DriverNum
        {
            get { return _DriverNum; }
            set { _DriverNum = value; }
        }

        //副司机
        private string _AssDriverNum = "";

        /// <summary>
        /// 副司机
        /// </summary>
        public string AssDriverNum
        {
            get { return _AssDriverNum; }
            set { _AssDriverNum = value; }
        }
        
        //车站号
        private string _StatioNo = "";

        /// <summary>
        /// 车站号
        /// </summary>
        public string StatioNo
        {
            get { return _StatioNo; }
            set { _StatioNo = value; }
        }

        //总重
        private string _AllUp = "";

        /// <summary>
        /// 总重
        /// </summary>
        public string AllUp
        {
            get { return _AllUp; }
            set { _AllUp = value; }
        }

        //计长
        private string _Length = "";

        /// <summary>
        /// 计长
        /// </summary>
        public string Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        //辆数
        private string _NumberCar = "0";

        /// <summary>
        /// 辆数
        /// </summary>
        public string NumberCar
        {
            get { return _NumberCar; }
            set { _NumberCar = value; }
        }

        //交路号
        private string _CrossRoadNo = "";

        /// <summary>
        /// 交路号
        /// </summary>
        public string CrossRoadNo
        {
            get { return _CrossRoadNo; }
            set { _CrossRoadNo = value; }
        }

        //载重
        private string _CarryLoad = "";

        /// <summary>
        /// 载重
        /// </summary>
        public string CarryLoad
        {
            get { return _CarryLoad; }
            set { _CarryLoad = value; }
        }

        //客车
        private string _Carriage = "0";

        /// <summary>
        /// 客车
        /// </summary>
        public string Carriage
        {
            get { return _Carriage; }
            set { _Carriage = value; }
        }

        //重车
        private string _HeavyCar = "0";

        /// <summary>
        /// 重车
        /// </summary>
        public string HeavyCar
        {
            get { return _HeavyCar; }
            set { _HeavyCar = value; }
        }

        //空车
        private string _EmptyCar = "0";

        /// <summary>
        /// 空车
        /// </summary>
        public string EmptyCar
        {
            get { return _EmptyCar; }
            set { _EmptyCar = value; }
        }

        //非运用车
        private string _NonServCar = "0";

        /// <summary>
        /// 非运用车
        /// </summary>
        public string NonServCar
        {
            get { return _NonServCar; }
            set { _NonServCar = value; }
        }

        //代客车
        private string _GenPassCar = "0";

        /// <summary>
        /// 代客车
        /// </summary>
        public string GenPassCar
        {
            get { return _GenPassCar; }
            set { _GenPassCar = value; }
        }

        //守车
        private string _CabooseCar = "0";

        /// <summary>
        /// 守车
        /// </summary>
        public string CabooseCar
        {
            get { return _CabooseCar; }
            set { _CabooseCar = value; }
        }

        //机车号
        private string _TrainNum = "";

        /// <summary>
        /// 机车号
        /// </summary>
        public string TrainNum
        {
            get { return _TrainNum; }
            set { _TrainNum = value; }
        }

        //机车型号
        private string _TypeNum = "";

        /// <summary>
        /// 机车型号
        /// </summary>
        public string TypeNum
        {
            get { return _TypeNum; }
            set { _TypeNum = value; }
        }

        //日期
        private string _CanDate = "";

        /// <summary>
        /// 日期
        /// </summary>
        public string CanDate
        {
            get { return _CanDate; }
            set { _CanDate = value; }
        }

        //车次
        private string _VehicleTrips = "";

        /// <summary>
        /// 车次
        /// </summary>
        public string VehicleTrips
        {
            get { return _VehicleTrips; }
            set { _VehicleTrips = value; }
        }

        //本补客货
        private string _FillPassCargo = "";

        /// <summary>
        /// 本补客货
        /// </summary>
        public string FillPassCargo
        {
            get { return _FillPassCargo; }
            set { _FillPassCargo = value; }
        }

        #endregion

        #region 优先级59数据
        //管压0
        private string _TubeGageZero = "";

        /// <summary>
        /// 管压0
        /// </summary>
        public string TubeGageZero
        {
            get { return _TubeGageZero; }
            set { _TubeGageZero = value; }
        }

        //管压1
        private string _TubeGageOne = "";

        /// <summary>
        /// 管压1
        /// </summary>
        public string TubeGageOne
        {
            get { return _TubeGageOne; }
            set { _TubeGageOne = value; }
        }

        //管压2
        private string _TubeGageTwo = "";

        /// <summary>
        /// 管压2
        /// </summary>
        public string TubeGageTwo
        {
            get { return _TubeGageTwo; }
            set { _TubeGageTwo = value; }
        }

        //备用压力
        private string _TubeGageThree = "";

        /// <summary>
        /// 备用压力
        /// </summary>
        public string TubeGageThree
        {
            get { return _TubeGageThree; }
            set { _TubeGageThree = value; }
        }

        //柴油机转速
        private string _DieselSpeed = "";

        /// <summary>
        /// 柴油机转速
        /// </summary>
        public string DieselSpeed
        {
            get { return _DieselSpeed; }
            set { _DieselSpeed = value; }
        }

        #endregion

    }
}
