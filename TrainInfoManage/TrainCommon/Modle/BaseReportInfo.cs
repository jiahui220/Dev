using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    public class BaseReportInfo
    {
        private string trains = "";

        /// <summary>
        /// 车次
        /// </summary>
        public string Trains
        {
            get { return trains; }
            set { trains = value; }
        }

        private string totalWeight = "";

        /// <summary>
        /// 总重
        /// </summary>
        public string TotalWeight
        {
            get { return totalWeight; }
            set { totalWeight = value; }
        }

        private string totalLoad = "";

        /// <summary>
        /// 载重
        /// </summary>
        public string TotalLoad
        {
            get { return totalLoad; }
            set { totalLoad = value; }
        }

        private string fullCar = "";

        /// <summary>
        /// 重车
        /// </summary>
        public string FullCar
        {
            get { return fullCar; }
            set { fullCar = value; }
        }

        private string emptyCar = "";

        /// <summary>
        /// 空车
        /// </summary>
        public string EmptyCar
        {
            get { return emptyCar; }
            set { emptyCar = value; }
        }

        private string noUseCar = "";

        /// <summary>
        /// 非运用车
        /// </summary>
        public string NoUseCar
        {
            get { return noUseCar; }
            set { noUseCar = value; }
        }

        private string replaceCar = "";

        /// <summary>
        /// 代客车
        /// </summary>
        public string ReplaceCar
        {
            get { return replaceCar; }
            set { replaceCar = value; }
        }

        private string totalCar = "";

        /// <summary>
        /// 货车合计
        /// </summary>
        public string TotalCar
        {
            get { return totalCar; }
            set { totalCar = value; }
        }

        private string totalLength = "";

        /// <summary>
        /// 列车换长
        /// </summary>
        public string TotalLength
        {
            get { return totalLength; }
            set { totalLength = value; }
        }


        private string totalBus = "";

        /// <summary>
        /// 客车合计
        /// </summary>
        public string TotalBus
        {
            get { return totalBus; }
            set { totalBus = value; }
        }

        #region 静态实例成员
        private static BaseReportInfo _ReportInfo = null;

        /// <summary>
        /// 当前机车基本信息
        /// </summary>
        public static BaseReportInfo ReportInfo
        {
            get
            {
                if (_ReportInfo == null)
                    _ReportInfo = new BaseReportInfo();
                return _ReportInfo;
            }
        }
        #endregion

    }
}
