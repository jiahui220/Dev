using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net.Sockets;

namespace TrainCommon
{
    /// <summary>
    /// 当前设备基础信息
    /// </summary>
    public class LocoInfo
    {
        /// <summary>
        /// 流量统计
        /// </summary>
        private long flow = 0;

        /// <summary>
        /// 流量统计
        /// </summary>
        public long Flow
        {
            get { return flow; }
            set { flow = value; }
        }

        private string trainType = "";

        /// <summary>
        /// 机车车型
        /// </summary>
        public string TrainType
        {
            get { return trainType; }
            set { trainType = value; }
        }

        private string typeNum = "";

        /// <summary>
        /// 车型编码
        /// </summary>
        public string TypeNum
        {
            get { return typeNum; }
            set { typeNum = value; }
        }



        private string trainNum = "";

        /// <summary>
        /// 机车车号
        /// </summary>
        public string TrainNum
        {
            get { return trainNum; }
            set { trainNum = value; }
        }


        private string driverNum = "";

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverNum
        {
            get { return driverNum; }
            set { driverNum = value; }
        }

        private string subNum = "";

        /// <summary>
        /// 副司机号
        /// </summary>
        public string SubNum
        {
            get { return subNum; }
            set { subNum = value; }
        }

        private int trainID = 0;

        /// <summary>
        /// 机车编号
        /// </summary>
        public int TrainID
        {
            get { return trainID; }
            set { trainID = value; }
        }


        private string vehicleTrip = "";

        /// <summary>
        /// 机车车次
        /// </summary>
        public string VehicleTrip
        {
            get { return vehicleTrip; }
            set { vehicleTrip = value; }
        }


        private string driverName = "";

        /// <summary>
        ///司机姓名
        /// </summary>
        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }


        private string longitude = "0.00";

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }


        private string latitude = "0.00";

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }


        private string nowStation = "";

        /// <summary>
        /// 当前站
        /// </summary>
        public string NowStation
        {
            get { return nowStation; }
            set { nowStation = value; }
        }


        private string arrTime = "";

        /// <summary>
        /// 到达时分
        /// </summary>
        public string ArrTime
        {
            get { return arrTime; }
            set { arrTime = value; }
        }


        private string depTime = "";

        /// <summary>
        /// 出发时分
        /// </summary>
        public string DepTime
        {
            get { return depTime; }
            set { depTime = value; }
        }

        private string speed = "0";

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private string kilometer = "";

        /// <summary>
        /// 公里标
        /// </summary>
        public string Kilometer
        {
            get { return kilometer; }
            set { kilometer = value; }
        }



        private int reportID = 0;

        /// <summary>
        /// 报单ID
        /// </summary>
        public int ReportID
        {
            get { return reportID; }
            set { reportID = value; }
        }


        private bool isLine = false;

        /// <summary>
        /// 网络是否联通
        /// </summary>
        public bool IsLine
        {
            get { return isLine; }
            set { isLine = value; }
        }

        private string url = "";

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private bool isVoice = true;

        /// <summary>
        /// 是否开启语音播报报警信息
        /// </summary>
        public bool IsVoice
        {
            get { return isVoice; }
            set { isVoice = value; }
        }


        private DateTime logTime =new DateTime();
        
        /// <summary>
        /// 运行信息记录时间
        /// </summary>
        public DateTime LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }

        private int warnID = 0;

        //系统配置
        private DataTable roboConfig = null;

        public DataTable RoboConfig
        {
            get { return roboConfig; }
            set { roboConfig = value; }
        }

        //用于记录系统空闲操作时间
        private DateTime lastDateTime = DateTime.Now;

        public DateTime LastDateTime
        {
            get { return lastDateTime; }
            set { lastDateTime = value; }
        }

        //等待屏保的时间 分钟
        private int screenMinutes = 30;

        public int ScreenMinutes
        {
            get { return screenMinutes; }
            set { screenMinutes = value; }
        }

        //屏保是否已经显示
        private bool isScreenShow = false;

        public bool IsScreenShow
        {
            get { return isScreenShow; }
            set { isScreenShow = value; }
        }

        #region 静态实例成员
        private static LocoInfo _TrainInfo = null;

        /// <summary>
        /// 当前机车基本信息
        /// </summary>
        public static LocoInfo TrainInfo 
        {
            get
            {
                if (_TrainInfo == null)
                    _TrainInfo = new LocoInfo();
                return _TrainInfo;
            }
        }
        #endregion


        private int reconID = 0;

        /// <summary>
        /// 补机重联记录ID
        /// </summary>
        public int ReconID
        {
            get { return reconID; }
            set { reconID = value; }
        }

        private int runGroupID = 0;

        /// <summary>
        /// 运行编组记录ID
        /// </summary>
        public int RunGroupID
        {
            get { return runGroupID; }
            set { runGroupID = value; }
        }

        //是否发送文件
        private bool isSendFile = false;

        public bool IsSendFile
        {
            get { return isSendFile; }
            set { isSendFile = value; }
        }

        //文件路径
        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        
        }

        //LKJ时间记录
        private string currDateTime = "";

        /// <summary>
        /// LKJ时间记录
        /// </summary>
        public string CurrDateTime
        {
            get { return currDateTime; }
            set { currDateTime = value; }
        }

        private string isExistLKJFile = "0";

        /// <summary>
        /// 是否存在LKJ更新程序
        /// </summary>
        public string IsExistLKJFile
        {
            get { return isExistLKJFile; }
            set { isExistLKJFile = value; }
        }

        //主程序版本
        private string proVersion;

        public string ProVersion
        {
            get { return proVersion; }
            set { proVersion = value; }
        }

        //外can版本
        private string canVersion;

        public string CanVersion
        {
            get { return canVersion; }
            set { canVersion = value; }
        }

        //配置文件版本
        private string itemsVersion;

        public string ItemsVersion
        {
            get { return itemsVersion; }
            set { itemsVersion = value; }
        }

        private string currRunInfo = "";

        /// <summary>
        /// 当前运行
        /// </summary>
        public string CurrRunInfo
        {
            get { return currRunInfo; }
            set { currRunInfo = value; }
        }

        private int socketPort = 0;

        /// <summary>
        /// socket通信端口
        /// </summary>
        public int SocketPort 
        {
            get { return socketPort; }
            set { socketPort = value; }
        }


        private string ipAddress = "";

        /// <summary>
        /// socket通信服务器IP地址
        /// </summary>
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private DateTime runTime = new DateTime();

        /// <summary>
        /// 机车运行时间
        /// </summary>
        public DateTime RunTime
        {
            get { return runTime; }
            set { runTime = value; }
        }

        private int lstPort = 0;

        /// <summary>
        /// 车载监听端口
        /// </summary>
        public int LstPort
        {
            get { return lstPort; }
            set { lstPort = value; }
        }

        private int annCount = 0;

        /// <summary>
        /// 通知公告条数
        /// </summary>
        public int AnnCount
        {
            get { return annCount; }
            set { annCount = value; }
        }


        private Socket clientSocket = null;

        /// <summary>
        /// 客户端socket
        /// </summary>
        public Socket ClientSocket
        {
            get { return clientSocket; }
            set { clientSocket = value; }
        }

        private SckTrains _sckTrains;

        public SckTrains SckTrains
        {
            get { return _sckTrains; }
            set { _sckTrains = value; }
        }

        private int currWarnId = 0;
        /// <summary>
        /// 程序启动后第一次报警
        /// </summary>
        public int CurrWarnId
        {
            get { return currWarnId; }
            set { currWarnId = value; }
        }

        private int logId = 0;
        /// <summary>
        /// 车载信息发送记录ID
        /// </summary>
        public int LogId
        {
            get { return logId; }
            set { logId = value; }
        }


        private bool imgupdate = false;

        /// <summary>
        /// 施工图是否更新完成
        /// </summary>
        public bool Imgupdate
        {
            get { return imgupdate; }
            set { imgupdate = value; }
        }

        private bool videoUpdate = false;

        /// <summary>
        /// 视频是否更新完成
        /// </summary>
        public bool VideoUpdate
        {
            get { return videoUpdate; }
            set { videoUpdate = value; }
        }


        private bool isSendSucess = false;

        /// <summary>
        /// 报单是否发送成功
        /// </summary>
        public bool IsSendSucess
        {
            get { return isSendSucess; }
            set { isSendSucess = value; }
        }

        public static int dataConnect = 0;

        //关机前最后一条运行信息
        private string oldLogRuning = "";

        /// <summary>
        /// 关机最后一条运行信息
        /// </summary>
        public string OldLogRuning
        {
            get { return oldLogRuning; }
            set { oldLogRuning = value; }
        }

        //关机前最后一条运行信息记录的时间
        private string oldLogTime = "";

        /// <summary>
        /// 关机前最后一条运行信息记录时间
        /// </summary>
        public string OldLogTime
        {
            get { return oldLogTime; }
            set { oldLogTime = value; }
        }

        //关机前一条信息是否发送
        private bool oldIsSend = false;
       
        //关机前一条信息是否已发送
        public bool OldIsSend
        {
            get { return oldIsSend; }
            set { oldIsSend = value; }
        }

        //上一次开机时间
        private string oldOpenTime = "";

        /// <summary>
        /// 上一次开机时间
        /// </summary>
        public string OldOpenTime
        {
            get { return oldOpenTime; }
            set { oldOpenTime = value; }
        }



    }
}
