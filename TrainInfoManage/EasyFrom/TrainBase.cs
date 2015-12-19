using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.Threading;
using System.Net.Sockets;
using TrainCommon.CanInfoCommon;
using TrainView.ChildFrom;
using TrainView.GuideView;
using TrainView.ToolView;
using System.Xml;
using System.IO.Ports;
using System.Drawing.Drawing2D;
using System.IO;

namespace EasyFrom
{
    public partial class TrainBase : UserControl
    {
        //包总长度
        int leng = 0;
        //包个数
        int pagsum = 0;
        //请求补发的次数
        int reissueSum = 0;

        //TrainBase trainBase = null;
        private TrainToolSet tts = null;
        int ask = 0;
        #region 属性
        /// <summary>
        /// 快捷跳转
        /// </summary>
        private bool isOneKey = false;
        /// <summary>
        /// 串口接收数据容器
        /// </summary>
        StringBuilder stb = new StringBuilder();
        /// <summary>
        /// 数据接收串口是否开启
        /// </summary>
        private bool isOpen = false;
        /// <summary>
        /// GPS数据接收串口是否开启
        /// </summary>
        private bool isGpsOpen = false;

        #region 线程变量
        /// <summary>
        /// 拨号线程
        /// </summary>
        private Thread dialThread = null;
        /// <summary>
        /// 数据处理线程
        /// </summary>
        private Thread fxThread = null;
        /// <summary>
        /// 检测程序更新线程
        /// </summary>
        private Thread updateThread = null;
        /// <summary>
        /// 时间校正线程
        /// </summary>
        private Thread SyncTimeTh;
        #endregion

        /// <summary>
        /// 数据分析线程是否开启
        /// </summary>
        private bool isFXStart = false;
        /// <summary>
        /// 限定数据发送间隔
        /// </summary>
        private int sendCount = 1;
        /// <summary>
        /// 记录重发等待时间
        /// </summary>
        private DateTime dtWait = DateTime.Now;
        /// <summary>
        /// 记录重发的数据包
        /// </summary>
        private string lastInfo = string.Empty;
        //拨号次数
        private int dailNum = 0;
        //时间输入对话框
        private Dlg_DateNeed dlg_DateNeed = null;
        //文本框索引
        private int dateIndex = 0;
        //485总线通信是否正常
        private bool rece485 = false;
        //分析485线程执行状态
        private bool pare485 = true;
        //刷新Can数据进出站信息创建报单
        private Thread CanReportTh;
        /// <summary>
        /// 报警ID存储容器
        /// </summary>
        private List<int> wlist = new List<int>();
        /// <summary>
        /// 是否同步时间
        /// </summary>
        private bool isSysTime = false;
        /// <summary>
        /// 通信Socket
        /// </summary>
        private SckTrains _socket = new SckTrains();
        /// <summary>
        /// 服务
        /// </summary>
        //private WebTrainService.TrainService trainService = null;
        /// <summary>
        /// 是否更新
        /// </summary>
        private bool isUpdate = true;
        /// <summary>
        /// 线程是否启动
        /// </summary>
        private bool thread = false;
        List<string> slist = new List<string>();
        private DataSet ds = null;
        //存放补发包的信息
        List<char> reissue = new List<char>();

        private Thread refData = null;

        #endregion


        public TrainBase()
        {

            //获取上一次开机时间
            LocoInfo.TrainInfo.OldOpenTime = BaseLibrary.getOpenTime();
            InitializeComponent();
            Init();
            //SetFullScreen();
            BaseLibrary.getSocketInfo();
            //获取服务器IP及端口
            _socket.IP = LocoInfo.TrainInfo.IpAddress;
            _socket.Port = LocoInfo.TrainInfo.SocketPort;
            LocoInfo.TrainInfo.SckTrains = _socket;
            getUrl();

            OpenPort();
            try
            {
                if (!isGpsOpen)
                {
                    sp_GPS.Open();
                    isGpsOpen = true;
                }
            }
            catch (Exception ex)
            {
            }

            if (!thread)
            {
                //*****分析数据线程 *****
                fxThread = new Thread(IsOverData);
                fxThread.IsBackground = true;
                fxThread.Start();

                //开启拨号线程
                dialThread = new Thread(ListenerNet);
                dialThread.IsBackground = true;
                dialThread.Start();

                //检测程序更新线程
                updateThread = new Thread(CheckProgrameUpdate);
                updateThread.IsBackground = true;

                thread = true;
            }
            BeginListen();
            //picNotice.Focus();//利用隐藏按钮转移焦点
            //btnNotice.WordText = "您有0条新的公告。";
            //picNotice.Image = imgNotice.Images[1];
            getUrl();
            //备份XML
            XmlHelper.InitXML();
        }

        //public TrainBase CreateInstant()
        //{
        //    if (this.trainBase == null)
        //    {
        //        this.trainBase = new TrainBase();

        //    }
        //    return this.trainBase;
        //}
        #region 程序更新事件
        /// <summary>
        /// 检测程序更新，只执行一次
        /// </summary>
        public void CheckProgrameUpdate()
        {
            if (LocoInfo.TrainInfo.IsLine)
            {
                FileUpdate.RequestUpdate(_socket);
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            lblSpeed.Font = new Font("Digiface", 32, FontStyle.Regular);
            LocoInfo.TrainInfo.LogTime = DateTime.Now;

        }
        #endregion

        #region LKJ分析器监听端口信息

        /// <summary>
        /// 打开分析器数据串口
        /// </summary>
        public void OpenPort()
        {
            try
            {
                if (!isOpen)
                {

                    sp_Lkj.PortName = "COM5";
                    sp_Lkj.BaudRate = int.Parse("28800");
                    sp_Lkj.DataBits = int.Parse("8");
                    sp_Lkj.StopBits = (StopBits)int.Parse("1");
                    sp_Lkj.Open();
                    //***避免串口死锁***
                    //写超时，如果底层串口驱动效率问题，能有效的避免死锁。
                    sp_Lkj.WriteTimeout = 1000;
                    //读超时，同上。
                    sp_Lkj.ReadTimeout = 1000;
                    //***避免串口死锁***
                    isOpen = true;
                    //MessageBox.Show("开启接收");
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void ClosePort()
        {
            //安全关闭当前串口。
            //***避免串口死锁***
            //注销串口中断接收事件，避免下次再执行进来，造成死锁。
            sp_Lkj.DataReceived -= this.sp_Lkj_DataReceived;
            //现在没有死锁，可以关闭串口。
            sp_Lkj.Close();
            //***避免串口死锁***
        }
        #endregion

        private int itemUpCount = 0;

        #region 更新项点配置
        private void GetItemInfo()
        {
            try
            {
                if (itemUpCount < 5)
                {
                    UpdateWarnItem.SendWarnItems(_socket);
                    itemUpCount++;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        #region 重启系统
        private bool isStartCount = false;
        private Dlg_Tip dlg_Restart = null;
        private void RestartSystem()
        {
            //Thread.Sleep(60000);
            //TrainToolSet.Restart();//重启系统
            dialThread.Join(30 * 60000);
        }
        #endregion


        #region 线程方法->监听网络状态
        /// <summary>
        /// 监听网络
        /// 每一秒检测一次网络状态，如果断开，进行拨号操作
        /// </summary>
        private void ListenerNet()
        {
            bool isOnLine = false;
            RasDailAction.DisAllContect();
            isOnLine = DialAction.Connect();
            LocoInfo.TrainInfo.IsLine = true;
            dialThread.Join(8000);
            while (true)
            {
                try
                {
                    //拨号次数
                    if (dailNum >= 5)
                    {
                        dialThread.Join(1000 * 60 * 15);
                        //断开所有拨号
                        RasDailAction.DisAllContect();
                        dailNum = 0;
                    }
                    isOnLine = DialAction.IsConnected();
                    LocoInfo.TrainInfo.IsLine = isOnLine;
                    if (!isOnLine)
                    {
                        LocoInfo.dataConnect = 0;
                        _socket.IsConnect = false;
                        //修改机车下线时间
                        XmlNode node = XmlHelper.GetNode("/RoboConfig/OffLineTime", null, null);
                        XmlHelper.SetNodeText(node, String.Format("{0:yyMMddHHmmss}", DateTime.Now));
                        DialAction.Close();
                        isOnLine = DialAction.Connect();
                        dailNum++;//累积拨号次数
                        if (isOnLine)
                        {
                            //修改机车上线时间
                            XmlNode onLineNode = XmlHelper.GetNode("/RoboConfig/OnLineTime", null, null);
                            XmlHelper.SetNodeText(onLineNode, String.Format("{0:yyMMddHHmmss}", DateTime.Now));
                        }
                        LocoInfo.TrainInfo.IsLine = isOnLine;
                        dialThread.Join(2000);
                    }
                    else
                    {
                        if (dailNum > 0)
                        {
                            XmlNode node = XmlHelper.GetNode("/RoboConfig/RestartNum", null, null);
                            XmlHelper.SetNodeText(node, "0");
                            dailNum = 0;
                        }
                        //if (LocoInfo.dataConnect==0)
                        //{
                        //机车运行信息，网络联通状态下，每1分钟发送一次
                        if (sendCount == 2 || sendCount == 4 || sendCount == 6 || sendCount == 8)
                        {
                            //LocoInfo.dataConnect = 1;
                            //发送报警
                            SendWarn.SendWarnItems(LocoInfo.TrainInfo.SckTrains, "");
                            //LocoInfo.dataConnect = 0;
                        }
                        else if (sendCount >= 10)
                        {
                            //自动提交报单，每5分钟发送一次
                            //BaseLibrary.SendReport(_socket,"");
                            //LocoInfo.dataConnect = 1;
                            SendReport.SendReportHead(_socket, "");
                            //LocoInfo.dataConnect = 0;
                            updateBaseDis();
                            sendCount = 1;
                        }
                        else
                        {
                            //MessageBox.Show("执行发送命令");
                            //发送运行信息，每30秒送一次
                            //LocoInfo.dataConnect = 1;
                            if (pare485)
                            {
                                BaseLibrary.SendRunInfo(_socket, "");
                            }
                            else
                            {
                                //MessageBox.Show("发送Can运行信息");
                                CanSendRunInfo.SendRunInfo(_socket);
                            }
                            //LocoInfo.dataConnect = 0;
                        }
                        sendCount++;
                        GetItemInfo();
                        //}
                        //上一次关机运行记录信息未发送则发送
                        if (!LocoInfo.TrainInfo.OldIsSend)
                        {
                            BaseLibrary.sendLastRunInfo(_socket);
                        }
                        dialThread.Join(30000);
                        //检测程序更新
                        if (isUpdate)
                        {
                            updateThread.Start();
                            isUpdate = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    LocoInfo.dataConnect = 0;
                    _socket.IsConnect = false;
                    dailNum++;//累积拨号次数
                    //修改机车下线时间
                    XmlNode node = XmlHelper.GetNode("/RoboConfig/OffLineTime", null, null);
                    XmlHelper.SetNodeText(node, String.Format("{0:yyMMddHHmmss}", DateTime.Now));
                    LocoInfo.TrainInfo.IsLine = false;
                    dialThread.Join(20000);
                }
            }
        }
        #endregion


        /// <summary>
        /// 接车时间
        /// </summary>
        private void txtReceiveTime_GotFocus(object sender, EventArgs e)
        {
            dateIndex = 1;
            ShowDlgDt("接车时分");
        }

        /// <summary>
        /// 出勤时间
        /// </summary>
        private void txtWorkTime_GotFocus(object sender, EventArgs e)
        {
            dateIndex = 0;
            ShowDlgDt("出勤时分");
        }

        /// <summary>
        /// 交车时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSubmitTime_GotFocus(object sender, EventArgs e)
        {
            if (Convert.ToInt32(LocoInfo.TrainInfo.Speed) == 0)
            {
                dateIndex = 2;
                ShowDlgDt("交车时分");
            }
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        private void ShowDlgDt(string tip)
        {
            if (dlg_DateNeed == null)
            {
                dlg_DateNeed = new Dlg_DateNeed();
                dlg_DateNeed.Closed += new EventHandler(GetDateTime);
            }
            dlg_DateNeed.Tip = tip;
            btn_Notice.Focus();
            dlg_DateNeed.ShowDialog();
        }

        /// <summary>
        /// 获取时间输入框的时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDateTime(object sender, EventArgs e)
        {
            //MessageBox.Show("111");
            btn_Notice.Focus();
            string dt = (string)((Dlg_DateNeed)sender).ReadTime;
            if (String.IsNullOrEmpty(dt))
            {
                return;
            }
            using (RParams param = new RParams())
            {
                switch (dateIndex)
                {
                    case 0:
                        txtWorkTime.Text = dt;
                        if (txtWorkTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("DutyTime", txtWorkTime.Text);
                            DBAction.Update(ETableName.Steward, "DutyTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 1:
                        txtReceiveTime.Text = dt;
                        if (txtReceiveTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("ReceiveTime", txtReceiveTime.Text);
                            DBAction.Update(ETableName.Steward, "ReceiveTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 2:
                        txtSubmitTime.Text = dt;
                        if (txtSubmitTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("DeliverTime", txtSubmitTime.Text);
                            DBAction.Update(ETableName.Steward, "DeliverTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                }
            }

        }


        #region LKJ分析器数据接收
        /// <summary>
        /// LKJ分析器接收事件
        /// </summary>
        private void sp_Lkj_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //读入收到的数据。
                int len = sp_Lkj.BytesToRead;
                if (len > 1)
                {
                    byte[] data = new byte[len];
                    sp_Lkj.Read(data, 0, len);

                    //数据转十六进制字符串。
                    string Hex = string.Empty;
                    for (int i = 0; i < data.Length; i++)
                    {
                        string tempstr = Convert.ToString(data[i], 16);
                        if (tempstr.Length < 2)
                        {
                            tempstr = '0' + tempstr;
                        }
                        Hex += tempstr + ' ';
                    }
                    Hex = Hex.ToUpper();
                    if (Hex.Length > 0)
                    {
                        slist.Add(Hex);
                    }

                }
            }
            catch (Exception ex)
            {
                //LogDaily.logerr(ex.ToString());
                //数据接收异常
                //MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region  数据分析
        /**
         * 判断接收数据是否完整及处理数量完整命令数据
         * 其中广播信息及报警处理包含①数据实时记录 ②数据实时显示 ③数据实时上传
         * 另报警信息处理还包含：报警项点的语音播报
         * */
        public void IsOverData()
        {


            //MessageBox.Show("开启分析");
            //int cts = 14400;
            fxThread.Join(10000);
            //MessageBox.Show("分析线程启动！");
            if (slist.Count == 0)
            {
                //485总线10s内无数据则默认，485通信错误
                pare485 = false;
                //启动Can数据接收与解析
                OpenCanPort();
                canFixTh = new Thread(new ThreadStart(FixCan));
                canFixTh.IsBackground = true;
                canFixTh.Start();
                //MessageBox.Show("接收485总线数据");

                //处理Can数据报单信息
                CanReportTh = new Thread(new ThreadStart(RefCanReport));
                CanReportTh.IsBackground = true;
                CanReportTh.Start();
            }
            ////slist.Clear();
            //MessageBox.Show("------>"+pare485);
            while (pare485)
            {
                try
                {
                    string reStr = "";
                    if (slist.Count > 0)
                    {
                        //接收数据处理
                        string str = stb.ToString().Trim() + " " + slist[0].Replace("7E", "7E-").Trim(); //替换命令结束标识，加入特殊字符
                        slist.RemoveAt(0);//删除已提取串口数据
                        if (str.Trim().Length > 0 && str != null)
                        {
                            string[] strData = str.Trim().Split(new char[] { '-' });//根据替换的特殊字符，拆分接收到的串口数据
                            int cmbCount = 0;//接收数据中含有的命令条数
                            int length = strData.Length;//拆分数组长度
                            string lastData = strData[length - 1];//获取拆分数组的最后一个值
                            //判断最后一个值是否为有效值，且为一条完整命令
                            if (lastData.Trim().Length > 0 && lastData.Trim().EndsWith("7E") && lastData.Trim().StartsWith("7C"))
                            {
                                cmbCount = length;
                            }
                            else
                            {
                                cmbCount = length - 1;
                                if (lastData.Trim().StartsWith("7C"))
                                {
                                    reStr = lastData;
                                }
                            }
                            //接收数据处理
                            if (stb.Length > 0)
                            {
                                stb.Remove(0, stb.Length);
                            }
                            //if (String.IsNullOrEmpty(reStr))
                            //{
                            //    stb.Append(reStr);
                            //}
                            stb.Append(reStr);

                            //循环解析接收到的命令
                            for (int i = 0; i < cmbCount; i++)
                            {
                                //将命令转化为字符数组
                                if (strData[i].Trim().EndsWith("7E") && strData[i].Trim().StartsWith("7C"))
                                {
                                    string[] idata = strData[i].Trim().Split(new char[] { ' ' });

                                    //string sssss="";
                                    //for (int jjj = 0; jjj < idata.Length; jjj++)
                                    //{
                                    //    sssss+=idata[jjj];
                                    //}
                                    //MessageBox.Show(sssss);


                                    char[] charData = new char[idata.Length];
                                    for (int j = 0; j < idata.Length; j++)
                                    {
                                        byte c = Convert.ToByte("0x" + idata[j].Trim(), 16);
                                        char cc = (char)c;
                                        charData[j] = cc;
                                    }
                                    char[] ssss = DataParser.GetPacketContent(charData);
                                    char[] sc = DataParser.GetData(ssss);

                                    if (idata.Length > 0)
                                    {
                                        string type = idata[3].Trim();
                                        switch (type)
                                        {
                                            case "01":
                                                if (LocoInfo.TrainInfo.IsSendFile)
                                                {
                                                    slist.Clear();//清除串口历史数据
                                                    byte[] data = DataSend.SendFileCount();
                                                    sp_Lkj.Write(data, 0, DataSend.reallyLen);
                                                }
                                                else
                                                {
                                                    if (ask == 0)
                                                    {
                                                        ask = 1;
                                                        byte[] data = DataSend.AskReissue();
                                                        //WriteData(data, DataSend.reallyLen);
                                                        sp_Lkj.Write(data, 0, DataSend.reallyLen);
                                                    }
                                                    else
                                                    {
                                                        byte[] data = DataSend.SendHeart();
                                                        //WriteData(data, DataSend.reallyLen);
                                                        sp_Lkj.Write(data, 0, DataSend.reallyLen);
                                                    }
                                                }
                                                break;
                                            case "02":
                                                if (sc.Length == 57)
                                                {
                                                    //对数据进行分析，ws数组存放违规、司机号、车次号码、版本号
                                                    string[] ws = DataParser.ReceiveBreakItems(sc);
                                                    DisposeWarn(ws);
                                                    byte[] witems = new byte[50];
                                                    //witems存放五十个项点
                                                    for (int w = 1; w < 51; w++)
                                                    {
                                                        witems[w - 1] = (byte)sc[w];
                                                    }
                                                    //项点应答
                                                    byte[] bData = DataSend.SendItems((byte)sc[0], witems);
                                                    sp_Lkj.Write(bData, 0, DataSend.reallyLen);
                                                    //WriteData(bData, DataSend.reallyLen);
                                                    fxThread.Join(300);
                                                    if (slist.Count > 8)
                                                    {
                                                        slist.RemoveRange(0, 5);
                                                    }
                                                }
                                                else if (sc.Length == 257)
                                                {
                                                    //string test2 = "";
                                                    //for (int tet = 0; tet < sc.Length; tet++)
                                                    //{
                                                    //    test2 += (byte)sc[tet] + "";
                                                    //}
                                                    //MessageBox.Show(test2);

                                                    // 对数据进行分析，ws数组存放违规、司机号、车次号码、版本号、违规时间
                                                    string[] ws = DataParser.ReceiveBreakItems1(sc);
                                                    // 报警信息处理
                                                    DisposeWarn(ws);
                                                    byte[] witems = new byte[50];
                                                    // witems存放五十个项点
                                                    for (int w = 1, m = 1; w < 51; w++)
                                                    {
                                                        witems[w - 1] = (byte)sc[m];
                                                        //bcd += witems[w - 1] + " ";
                                                        m = m + 5;
                                                    }
                                                    //  MessageBox.Show(bcd);
                                                    // 项点应答
                                                    byte[] bData = DataSend.SendItems((byte)sc[0], witems);
                                                    sp_Lkj.Write(bData, 0, DataSend.reallyLen);
                                                    // WriteData(bData, DataSend.reallyLen);
                                                    fxThread.Join(300);
                                                    //if (slist.Count > 8)
                                                    //{
                                                    //    slist.RemoveRange(0, 5);
                                                    //}
                                                }
                                                break;
                                            case "04":
                                                //处理广播
                                                if (sc.Length == 73)
                                                {
                                                    string[] gs = DataParser.ParserNoticeAndFile(sc);
                                                    
                                                    //string gs2 = "";
                                                    //for (int gs1 = 0; gs1 < 37;gs1++ )
                                                    //{
                                                    //    gs2+=gs[gs1]+" ";
                                                    //}
                                                    //MessageBox.Show(gs2);

                                                    //LKJ时间记录
                                                    LocoInfo.TrainInfo.CurrDateTime = gs[2];
                                                    #region  同步CE时间
                                                    if (!isSysTime)
                                                    {
                                                        DateTime lkjTime = Convert.ToDateTime(gs[2]);
                                                        TimeSpan ts = DateTime.Now - lkjTime;
                                                        //判定时间间隔是否相差3分钟
                                                        if (Math.Abs(ts.TotalMinutes) > 3)
                                                        {
                                                            LocoInfo.TrainInfo.LastDateTime = lkjTime;
                                                            SyncTimeTh = new Thread(SyncTime);
                                                            SyncTimeTh.IsBackground = true;
                                                            SyncTimeTh.Start();
                                                            //BaseLibrary.SyncTime(lkjTime);
                                                            //initWorkDateTime(LocoInfo.TrainInfo.CurrDateTime);
                                                        }
                                                        else
                                                        {
                                                            isSysTime = true;
                                                            //initWorkDateTime(LocoInfo.TrainInfo.CurrDateTime);
                                                        }
                                                        //修改开机时间
                                                        XmlNode node = XmlHelper.GetNode("/RoboConfig/OpenTime", null, null);
                                                        XmlHelper.SetNodeText(node, String.Format("{0:yyMMddHHmmss}", DateTime.Now.AddMinutes(-1)));
                                                    }
                                                    #endregion
                                                    DataDispose.DisposeBroadcast(gs, strData[i]);
                                                    setText(gs);
                                                    LocoInfo.TrainInfo.CurrRunInfo = strData[i];
                                                    fxThread.Join(300);
                                                    //if (slist.Count > 8)
                                                    //{
                                                    //    slist.RemoveRange(0, 5);
                                                    //}
                                                }
                                                break;
                                            case "06":
                                                if (sc.Length == 1)
                                                {
                                                    //MessageBox.Show("回复文件应答06");
                                                    if ((byte)sc[0] == 0xFF)
                                                    {
                                                        LocoInfo.TrainInfo.FilePath = null;
                                                        LocoInfo.TrainInfo.IsSendFile = false;
                                                        if (tts == null)
                                                        {
                                                            tts = new TrainToolSet();
                                                        }
                                                        MessageBox.Show("文件发送成功");
                                                        tts.btn_Send.ForeColor = Color.Black;
                                                        tts.btn_Send.ForeColor = System.Drawing.Color.Red;
                                                        slist.Clear();//清除串口历史数据                                      
                                                    }
                                                    else if ((byte)sc[0] == 0xF0)
                                                    {
                                                        MessageBox.Show("文件发送失败");
                                                    }
                                                    else
                                                    {
                                                        ReplySendFile();
                                                    }
                                                    //else if ((byte)sc[0] == 0x01)
                                                }
                                                break;
                                            case "07":
                                                if (sc.Length == 11)
                                                {
                                                    //  MessageBox.Show("无补发信息！！！");
                                                }
                                                else
                                                {
                                                    pagsum++;
                                                    int b = 0;
                                                    //ss存放补发的包信息
                                                    string bb = "";
                                                    char[] ss = DataParser.GetData1(sc);

                                                    //string test2 = "";
                                                    //for (int tet = 0; tet < ss.Length; tet++)
                                                    //{
                                                    //    test2 += (byte)ss[tet] + " ";
                                                    //}
                                                    //MessageBox.Show(test2);

                                                    //leng存放拼接之后的包的总长度
                                                    leng = leng + ss.Length;
                                                    //reissue存放所有补发的信息
                                                    for (int u = 0; u < ss.Length; u++)
                                                    {
                                                        reissue.Add(ss[u]);
                                                    }
                                                    //所有的包信息整合完毕
                                                    //MessageBox.Show(((byte)sc[5]).ToString() + ((byte)sc[6]).ToString() + ((byte)sc[7]).ToString() + ((byte)sc[8]).ToString());
                                                    if (sc[5] == sc[7] && sc[6] == sc[8])
                                                    {
                                                        //将reissue中的数据转存到数组reissueAll中
                                                        char[] reissueAll = new char[reissue.Count];
                                                        for (int li = 0; li < reissue.Count; li++)
                                                        {
                                                            reissueAll[li] = reissue[li];
                                                        }
                                                        //MessageBox.Show((((byte)sc[6] << 8) + (byte)sc[5]).ToString() + "   " + pagsum.ToString());
                                                        if ((((byte)sc[6] << 8) + (byte)sc[5]).ToString() == pagsum.ToString())
                                                        {
                                                            //逐个分析包
                                                            for (int q = 0, g = 0; q < leng / 80; q++)
                                                            {
                                                                char[] ss1 = DataParser.GetData1(reissueAll, g);
                                                                //dange存储补发协议中单个广播信息
                                                                string dange = "7c 11 12 04 00 00 ";
                                                                string among = "";
                                                                for (int qq = 0; qq < ss1.Length-8; qq++)
                                                                {
                                                                    among = Convert.ToString((byte)ss1[qq], 16);
                                                                    if (among.Length == 1)
                                                                    {
                                                                        among = "0" + among;
                                                                    }
                                                                    dange += among + " ";
                                                                }
                                                                dange += "7e";
                                                                string[] resul = DataParser.ParserNoticeAndFile1(ss1);
                                                                //对比数据库，并且更新数据
                                                                DataDispose.DisposeBroadcast(resul, dange);

                                                                //for (int k = 0; k < resul.Length; k++)
                                                                //{
                                                                //    bb += resul[k] + " ";
                                                                //}
                                                                //MessageBox.Show(bb);
                                                                g = g + 80;
                                                            }
                                                            slist.Clear();//清除串口历史数据
                                                            reissue.Clear();
                                                        }
                                                        else
                                                        {
                                                            //请求三次后发送数据仍不全，取消请求。
                                                            if (reissueSum < 3)
                                                            {
                                                                //reissue清空，重新存储补发信息
                                                                reissue.Clear();
                                                                //补发一次，补发次数加1
                                                                reissueSum++;
                                                                //接收到的包总长度清零
                                                                leng = 0;
                                                                //接收到的包数清零
                                                                pagsum = 0;
                                                                //由于数据发送不全，请求重新发送一次
                                                                byte[] data = DataSend.AskReissue();
                                                                sp_Lkj.Write(data, 0, DataSend.reallyLen);
                                                            }
                                                            else
                                                            {
                                                                //MessageBox.Show("请求补发三次后，数据仍出错，取消请求补发。");
                                                                slist.Clear();//清除串口历史数据
                                                            }

                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //fxThread.Join(100);
                }
                catch (Exception ex)
                {
                    LogDaily.logerr(ex.ToString());
                    //MessageBox.Show(ex.ToString());
                    fxThread.Join(2000);
                    if (slist.Count > 30)
                    {
                        slist.RemoveRange(0, 20);
                    }
                    continue;
                }
            }


        }
        #endregion

        #region 根据缓冲区发送内容

        /// <summary>
        /// 应答发送文件
        /// </summary>
        public void ReplySendFile()
        {
            string filePath = LocoInfo.TrainInfo.FilePath;
            if (String.IsNullOrEmpty(filePath))
            {
                return;
            }
            //读取文件内容
            byte[] content = FileHelper.ReadFileBinary(filePath);
            //文件内容长度
            int len = content.Length;
            //文件内容总包数
            int totalPacket = len / 1024 + (len % 1024 == 0 ? 0 : 1);
            //文件名
            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1).ToLower();
            //将文件名转换为字节数组
            byte[] fNByte = Encoding.ASCII.GetBytes(fileName);
            //组成完整的文件名应答字节
            byte[] data = DataSend.SendFile(0x01, len, Convert.ToByte(1), Convert.ToByte(1), fNByte);
            //发送文件名字节数组
            WriteData(data, DataSend.reallyLen);
            Thread.Sleep(100);
            //循环发送文件内容字节
            for (byte i = 1; i <= totalPacket; i++)//发送文件内容
            {
                int length = 1024;
                if (i == totalPacket)
                {
                    length = len - (i - 1) * 1024;
                }
                byte[] curr = new byte[length];
                for (int j = 0; j < curr.Length; j++)
                {
                    curr[j] = content[(i - 1) * 1024 + j];
                }
                data = DataSend.SendFile(0x02, len, (byte)totalPacket, i, curr);
                WriteData(data, DataSend.reallyLen);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 根据缓冲区大小，发送内容
        /// </summary>
        /// <param name="data"></param>
        /// <param name="reallyLen"></param>
        public void WriteData(byte[] data, int reallyLen)
        {
            int remainLen = reallyLen;
            int start = 0;
            while (remainLen > 0)
            {
                int outSize = sp_Lkj.BytesToWrite;
                int MaxOutSize = sp_Lkj.WriteBufferSize;
                if (remainLen > MaxOutSize - outSize)
                {
                    sp_Lkj.Write(data, start, MaxOutSize - outSize);
                    remainLen -= MaxOutSize - outSize;
                    start += MaxOutSize - outSize;
                }
                else
                {
                    sp_Lkj.Write(data, start, remainLen);
                    remainLen -= remainLen;
                    start += remainLen;
                }
            }
        }
        #endregion

        #region 报警处理
        /// <summary>
        /// 报警信息处理
        /// 其中报警信息处理包含三部分①记录报警日志信息 ②语音播报报警内容 ③将报警信息提交至服务（此项在获取机车当时运行情况具体数据时同时提交，此处暂不处理）
        /// </summary>
        /// <param name="warnInfo">解析所得报警信息数组</param>
        public void DisposeWarn(string[] warnInfo)
        {
            try
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
                    string wartime = warnInfo[5];

                    //MessageBox.Show("拆分之前：" + wartime);

                    string[] warnBrr = warnStr.Trim().Split(new char[] { ' ' });
                    string[] wartimeStr = wartime.Trim().Split(new char[] { ',' });

                    //string test2 = "";
                    //MessageBox.Show(warnBrr.Length.ToString());
                    //MessageBox.Show(wartimeStr.Length.ToString());
                    //for (int test = 0; test < wartimeStr.Length; test++)
                    //{
                    //    test2 += wartimeStr[test] + "__";
                    //    MessageBox.Show("拆分之后：" + test2);
                    //}



                    //报警运行信息记录ID
                    int WarnId = 0;
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
                                                TimeSpan ts = Convert.ToDateTime(wartimeStr[i]) - warnTime;
                                                double totals = ts.TotalSeconds;
                                                //大于30则为新的报警记录
                                                if (totals > 120)
                                                {
                                                    if (WarnId == 0)
                                                    {
                                                        //记录机车报警运行记录
                                                        param.Items.Clear();
                                                        param.Add("CreateTime", Convert.ToDateTime(wartimeStr[i]));//报警时间
                                                        param.Add("TypeNum", LocoInfo.TrainInfo.TypeNum);//车型编号
                                                        param.Add("TrainId", LocoInfo.TrainInfo.TrainNum);//机车号
                                                        param.Add("DriverNum", LocoInfo.TrainInfo.DriverNum);//司机编号
                                                        param.Add("SubDriverNum", LocoInfo.TrainInfo.SubNum);//副司机号
                                                        param.Add("WarnItems", warnStr);//报警项点
                                                        DBAction.Insert(ETableName.AlarmRunInfo, param);
                                                        using (DataTable iwt = DBAction.GetDTFromSQL("select ID from AlarmRunInfo "))
                                                        {
                                                            if (iwt.Rows.Count > 0)
                                                            {
                                                                WarnId = Convert.ToInt32(iwt.Rows[iwt.Rows.Count - 1][0]);
                                                                LocoInfo.TrainInfo.CurrWarnId = WarnId;
                                                            }
                                                        }
                                                    }

                                                    //添加报警记录
                                                    param.Items.Clear();
                                                    param.Add("CreateTime", Convert.ToDateTime(wartimeStr[i]));
                                                    param.Add("ARInfoId", WarnId);
                                                    param.Add("AlarmID", warnItem);
                                                    param.Add("AlarmItem", wt.Rows[0]["AlarmItem"].ToString());
                                                    param.Add("AlarmIntro", wt.Rows[0]["AlarmIntro"].ToString());
                                                    param.Add("AlarmNum", wt.Rows[0]["ItemNum"].ToString());
                                                    DBAction.Insert(ETableName.AlarmLog, param);
                                                    WarnId = 0;
                                                    strItem += wt.Rows[0]["AlarmIntro"].ToString();//串联报警内容
                                                }
                                            }
                                            else
                                            {
                                                if (WarnId == 0)
                                                {
                                                    //记录机车报警运行记录
                                                    param.Items.Clear();
                                                    param.Add("CreateTime", Convert.ToDateTime(wartimeStr[i]));//报警时间
                                                    param.Add("TypeNum", LocoInfo.TrainInfo.TypeNum);//车型编号
                                                    param.Add("TrainId", LocoInfo.TrainInfo.TrainNum);//机车号
                                                    param.Add("DriverNum", LocoInfo.TrainInfo.DriverNum);//司机编号
                                                    param.Add("SubDriverNum", LocoInfo.TrainInfo.SubNum);//副司机号
                                                    param.Add("WarnItems", warnStr);//报警项点
                                                    DBAction.Insert(ETableName.AlarmRunInfo, param);
                                                    using (DataTable iwt = DBAction.GetDTFromSQL("select ID from AlarmRunInfo "))
                                                    {
                                                        if (iwt.Rows.Count > 0)
                                                        {
                                                            WarnId = Convert.ToInt32(iwt.Rows[iwt.Rows.Count - 1][0]);
                                                            LocoInfo.TrainInfo.CurrWarnId = WarnId;
                                                        }
                                                    }
                                                }

                                                //添加报警记录
                                                param.Items.Clear();
                                                param.Add("CreateTime", Convert.ToDateTime(wartimeStr[i]));
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
                        WarnId = 0;
                        //报警语音播报
                        //if (strItem != "" && LocoInfo.TrainInfo.IsVoice)
                        //{
                        //    BaseVoice.TrainVoice.SpeekVioce(strItem);
                        //}
                    }
                }

            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                return;
            }
        }
        #endregion

        #region 外Can数据处理
        private bool IsReceving = false;
        //Can数据存储器
        List<string> canList = new List<string>();
        //接收Can数据
        private void sp_Can_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //***正在接收状态指示。
                //IsReceving = true;
                //读入收到的数据。
                if (!IsReceving)
                {
                    int Len = sp_Can.BytesToRead;
                    if (Len < 1)
                    {
                        //***接收完成状态指示。
                        IsReceving = false;
                        return;
                    }
                    byte[] data = new byte[Len];
                    sp_Can.Read(data, 0, Len);
                    //IsReceving = true;
                    //数据转十六进制字符串。
                    string Hex = string.Empty;
                    for (int i = 0; i < data.Length; i++)
                    {
                        string tempstr = Convert.ToString(data[i], 16);
                        if (tempstr.Length < 2)
                        {
                            tempstr = '0' + tempstr;
                        }
                        Hex += tempstr + ' ';
                    }
                    Hex = Hex.ToUpper().Trim();
                    IsReceving = true;
                    canList.Add(Hex);
                }

            }
            catch (Exception ex)
            {

            }
        }

        List<string> rlist = new List<string>();
        //外Can数据处理
        private void subStr(string s)
        {
            try
            {
                if (s.Length >= 41)
                {
                    int index = s.IndexOf("0F");
                    string a = s.Substring(39 + index, 2);
                    if (a.Trim() != "0D")
                    {
                        s = s.Substring(s.IndexOf("0F", 2)).Trim();
                        subStr(s);
                    }
                    else
                    {
                        string data = s.Substring(index, 41);
                        rlist.Add(data);
                        s = s.Substring(41);
                        subStr(s);
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        #region 接收数据解析

        //can数据分析线程
        private Thread canFixTh;
        /// <summary>
        /// 接收Can数据
        /// </summary>
        private void FixCan()
        {
            while (true)
            {
                try
                {
                    //每解析一条数据休息200毫秒
                    canFixTh.Join(200);
                    //判断是否存在Can数据
                    if (canList.Count > 0)
                    {
                        string item = canList[0];
                        rlist.Clear();
                        subStr(item);
                        if (rlist.Count == 0)
                        {
                            continue;
                        }
                        int len = rlist.Count;
                        for (int i = 0; i < len; i++)
                        {
                            string olditem = rlist[i];

                            //MessageBox.Show(olditem);

                            //判断文本内容是否为空
                            if (olditem.Trim().Length > 0 && olditem.StartsWith("0F") && olditem.EndsWith("0D"))
                            {
                                try
                                {
                                    string[] idata = olditem.Trim().Split(new char[] { ' ' });
                                    char[] charData = new char[idata.Length];
                                    for (int j = 0; j < idata.Length; j++)
                                    {
                                        byte c = Convert.ToByte("0x" + idata[j].Trim(), 16);
                                        char cc = (char)c;
                                        charData[j] = cc;
                                    }
                                    //转化
                                    char id0 = charData[3];
                                    char id1 = charData[4];

                                    int t1 = ((byte)id0 & 0x07) << 5;
                                    int t2 = id1 >> 3;
                                    int t3 = t1 + t2;
                                    int fram = id1 & 0x07;

                                    string test7 = "";
                                    for (int testdata = 0; testdata < charData.Length; testdata++)
                                    {
                                        test7 += (byte)charData[testdata] + " ";

                                    }
                                    switch (t3)
                                    {
                                        case 80:
                                            ParseCanData.ParseCan50(charData, fram);
                                            break;
                                        case 88:
                                            ParseCanData.ParseCan58(charData, fram);
                                            break;
                                        case 89:
                                            ParseCanData.ParseCan59(charData, fram);
                                            break;
                                    }
                                    //填充窗口信息字段
                                    setTextCan();
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                        //分析完一条数据，删除一条
                        canList.RemoveAt(0);
                        IsReceving = false;
                    }
                }
                catch (Exception)
                {

                }
            }


        }

        #endregion

        /// <summary>
        /// 打开串口
        /// </summary>
        private void OpenCanPort()
        {
            //***避免串口死锁***
            //写超时，如果底层串口驱动效率问题，能有效的避免死锁。
            sp_Can.WriteTimeout = 1000;
            //读超时，同上。
            sp_Can.ReadTimeout = 1000;
            //回车换行。
            sp_Can.NewLine = "\r\n";
            //***避免串口死锁***
            if (!sp_Can.IsOpen)
            {
                sp_Can.PortName = "COM4";
                sp_Can.BaudRate = int.Parse("115200");
                sp_Can.DataBits = int.Parse("8");
                sp_Can.StopBits = (StopBits)int.Parse("1");
                sp_Can.Open();
            }

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void CloseCanPort()
        {
            //安全关闭当前串口。
            //***避免串口死锁***
            //注销串口中断接收事件，避免下次再执行进来，造成死锁。
            sp_Can.DataReceived -= this.sp_Can_DataReceived;
            while (IsReceving)
            {
                //处理串口接收事件及其它系统消息。
                Application.DoEvents();
            }
            //现在没有死锁，可以关闭串口。
            sp_Can.Close();
            //***避免串口死锁***
        }

        /// <summary>
        /// 定时刷新报单信息
        /// </summary>
        private void RefCanReport()
        {
            while (true)
            {
                CanAnalyzeReport.CreateReport();
                //到站时间
                if (LocoInfo.TrainInfo.ReportID != 0)
                {
                    using (DataTable st = DBAction.GetDTFromSQL("select StationName,ArrivedTime,SetOutTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID))
                    {
                        if (st.Rows.Count > 0)
                        {
                            LocoInfo.TrainInfo.NowStation = st.Rows[st.Rows.Count - 1]["StationName"].ToString();
                            LocoInfo.TrainInfo.ArrTime = st.Rows[st.Rows.Count - 1]["ArrivedTime"].ToString();
                            LocoInfo.TrainInfo.DepTime = st.Rows[st.Rows.Count - 1]["SetOutTime"].ToString();
                        }
                        else
                        {
                            LocoInfo.TrainInfo.NowStation = "";
                            LocoInfo.TrainInfo.ArrTime = "";
                            LocoInfo.TrainInfo.DepTime = "";
                        }
                    }

                }
                #region  同步CE时间
                if (!isSysTime)
                {
                    if (TrainCanInfo.TrainCan.CanTime != "" && TrainCanInfo.TrainCan.CanDate != "")
                    {
                        string canTime = TrainCanInfo.TrainCan.CanDate + " " + TrainCanInfo.TrainCan.CanTime;
                        DateTime lkjTime = Convert.ToDateTime(canTime);
                        TimeSpan ts = DateTime.Now - lkjTime;
                        //判定时间间隔是否相差3分钟
                        if (Math.Abs(ts.TotalMinutes) > 3)
                        {
                            LocoInfo.TrainInfo.LastDateTime = lkjTime;
                            LocoInfo.TrainInfo.CurrDateTime = canTime;
                            SyncTimeTh = new Thread(SyncTime);
                            SyncTimeTh.IsBackground = true;
                            SyncTimeTh.Start();
                            //BaseLibrary.SyncTime(lkjTime);
                            LogDaily.logerr("同步时间");
                            //initWorkDateTime(LocoInfo.TrainInfo.CurrDateTime);
                        }
                        else
                        {
                            isSysTime = true;
                            //initWorkDateTime(LocoInfo.TrainInfo.CurrDateTime);
                        }
                        //修改开机时间
                        XmlNode node = XmlHelper.GetNode("/RoboConfig/OpenTime", null, null);
                        XmlHelper.SetNodeText(node, String.Format("{0:yyMMddHHmmss}", DateTime.Now.AddMinutes(-1)));
                    }

                }
                #endregion
                CanReportTh.Join(5000);
            }
        }


        #endregion

        #region 更新司机及车型
        /// <summary>
        /// 更新司机及车型
        /// </summary>
        public void updateBaseDis()
        {
            if (LocoInfo.TrainInfo.DriverNum != "" && LocoInfo.TrainInfo.SubNum != "")
            {
                //请求更新司机库
                if (LocoInfo.TrainInfo.DriverNum == LocoInfo.TrainInfo.DriverName)
                {
                    UpdateBaseLibrary.UpdateDriverName(_socket, LocoInfo.TrainInfo.DriverNum);
                }
                if (LocoInfo.TrainInfo.SubNum == BaseLibrary.GetDriverName(LocoInfo.TrainInfo.SubNum))
                {
                    UpdateBaseLibrary.UpdateDriverName(_socket, LocoInfo.TrainInfo.SubNum);
                }
                //请求更新车型
                if (LocoInfo.TrainInfo.TypeNum == LocoInfo.TrainInfo.TrainType)
                {
                    UpdateBaseLibrary.UpdateTrainType(_socket, LocoInfo.TrainInfo.TypeNum);
                }
            }
        }
        #endregion

        #region GPS数据处理事件
        /// <summary>
        /// 接收gps数据
        /// </summary>
        private void sp_GPS_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                int len = sp_GPS.BytesToRead;
                if (len > 1)
                {
                    string result = "";
                    result = sp_GPS.ReadLine();
                    if (result.StartsWith("$GPGGA"))
                    {
                        string[] temp = result.Split(',');
                        double d1 = 0, d2 = 0, dd = 0;
                        string s = null;
                        //纬度
                        if (!String.IsNullOrEmpty(temp[2]))
                        {
                            s = temp[2].ToString().Trim();
                            d1 = Convert.ToDouble(s.Substring(0, 2));
                            d2 = Convert.ToDouble(s.Substring(2, s.Length - 2));
                            dd = d1 + d2 / 60;
                            result = dd.ToString(".0000000000") + ",";
                        }
                        //经度
                        if (!String.IsNullOrEmpty(temp[4]))
                        {
                            s = temp[4].ToString().Trim();
                            d1 = Convert.ToDouble(s.Substring(0, 3));
                            d2 = Convert.ToDouble(s.Substring(3, s.Length - 3));
                            dd = d1 + d2 / 60;
                            result += dd.ToString(".0000000000");
                        }
                        string[] stemp = result.Split(',');
                        if (stemp.Length == 2)
                        {

                            LocoInfo.TrainInfo.Longitude = stemp[1];
                            LocoInfo.TrainInfo.Latitude = stemp[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogDaily.logerr(ex.ToString());
            }
        }
        /// <summary>
        /// 关闭GPS端口
        /// </summary>
        public void CloseGPSPort()
        {
            if (sp_GPS.IsOpen)
            {
                sp_GPS.Close();
            }
        }
        #endregion

        #region 线程监听服务器Socket请求
        /// <summary>
        /// 监听socket
        /// </summary>
        private Socket LstSocket;
        /// <summary>
        /// 接收的数据
        /// </summary>
        private string Recstr = "";
        /// <summary>
        /// socket通信线程
        /// </summary>
        private Thread LstThread;
        /// <summary>
        /// 监听Socket
        /// </summary>
        public void Listen()
        {
            LstThread.Join(40000);
            while (true)
            {
                try
                {
                    LstThread.Join(1000);
                    LstSocket = LocoInfo.TrainInfo.ClientSocket;
                    if (LstSocket != null)
                    {
                        //设置接收数据缓存区
                        byte[] byteMessage = new byte[LstSocket.Available];
                        int iBytes = LstSocket.Receive(byteMessage, byteMessage.Length, 0);
                        Recstr += Encoding.Default.GetString(byteMessage, 0, iBytes);
                        //数据是否接收完成
                        if (LstSocket.Available == 0 && Recstr.Length > 0 && Recstr.Trim().EndsWith("}#"))
                        {
                            string[] strItems = Recstr.Split(new char[] { '}' });
                            for (int i = 0; i < strItems.Length; i++)
                            {
                                string item = strItems[i].Replace("{", "").Replace("#", "");
                                if (item.Trim().Length > 0)
                                {
                                    string type = BaseLibrary.GetSocketType(item);
                                    if (!string.IsNullOrEmpty(lastInfo) && DateTime.Now.Subtract(dtWait).Seconds > 10 && !UpdateBook.IsEnd)
                                    {
                                        UpdateBook.isRequest = true;
                                        UpdateBook.AnalyzeBook(_socket, lastInfo);
                                        dtWait = DateTime.Now;
                                    }
                                    switch (type)
                                    {
                                        case "0100":
                                            //地面请求最新运行信息
                                            if (!rece485)
                                            {
                                                BaseLibrary.SendRunInfo(_socket, item);
                                            }
                                            else
                                            {
                                                CanSendRunInfo.SendRunInfo(_socket);
                                            }
                                            break;
                                        case "0200":
                                            SendReport.AnalyzeReport(_socket, item);
                                            //BaseLibrary.SendReport(_socket,item);
                                            break;
                                        case "0300":
                                            SendWarn.AnalyzeWarn(_socket, item);
                                            break;
                                        case "0400":
                                            UpdateBook.AnalyzeBook(_socket, item);
                                            UpdateBook.isRequest = false;
                                            if (UpdateBook.type != "31")
                                            {
                                                lastInfo = Recstr;
                                            }
                                            dtWait = DateTime.Now;
                                            break;
                                        case "0501":
                                            UpdateMedia.AnalyzeVideoInfo(item);
                                            break;
                                        case "0502":
                                            UpdateMedia.AnalyzeImgInfo(item);
                                            break;
                                        case "0601":
                                            ReceSocketDataParser.AnalyzeAnnInfo(item);
                                            break;
                                        case "0800":
                                            FileUpdate.RevceFile(_socket, item);
                                            break;
                                        case "0900":
                                            UpdateWarnItem.AnalyzeItemsInfo(item);
                                            break;
                                        case "1000":
                                            UpdateBaseLibrary.AnalyzeTrainType(item);
                                            break;
                                        case "1100":
                                            UpdateBaseLibrary.AnalyzeDriverName(item);
                                            break;
                                        case "1200":
                                            UpdateBaseLibrary.AnalyzeStationName(item);
                                            break;
                                        case "1300":
                                            LocoInfo.TrainInfo.OldIsSend = true;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            Recstr = "";
                            LstThread.Join(2000);
                            continue;
                        }
                    }
                }
                catch (SocketException ex)
                {
                    //关闭监听端口
                    LstThread.Join(10000);
                }
            }
        }
        /// <summary>
        /// 通过线程开始监听
        /// </summary>
        public void BeginListen()
        {
            try
            {
                LstThread = new Thread(new ThreadStart(Listen));
                LstThread.IsBackground = true;
                LstThread.Start();
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
            }
        }
        /// <summary>
        /// 关闭无用连接
        /// </summary>
        public void EndListen()
        {
            try
            {
                //关闭监听端口
                LstSocket.Close();
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region 同步时间
        public void SyncTime()
        {
            try
            {
                BaseLibrary.SyncTime(Convert.ToDateTime(LocoInfo.TrainInfo.CurrDateTime));
                isSysTime = true;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 更新基本信息
        //获取实体类值
        private void setText(string[] info)
        {
            //公里标
            LocoInfo.TrainInfo.Kilometer = BaseLibrary.KiloConversion(info[8]);

            //车号
            if (LocoInfo.TrainInfo.TrainNum == "")
            {
                LocoInfo.TrainInfo.TrainNum = info[20];
            }

            //车型
            if (LocoInfo.TrainInfo.TrainType == "")
            {
                LocoInfo.TrainInfo.TypeNum = info[19];
                LocoInfo.TrainInfo.TrainType = BaseLibrary.GetTrainType(info[19]);
            }

            LocoInfo.TrainInfo.Speed = info[3];

            //到站时间
            if (LocoInfo.TrainInfo.ReportID != 0)
            {
                using (DataTable st = DBAction.GetDTFromSQL("select StationName,ArrivedTime,SetOutTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID))
                {
                    if (st.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.NowStation = st.Rows[st.Rows.Count - 1]["StationName"].ToString();
                        LocoInfo.TrainInfo.ArrTime = st.Rows[st.Rows.Count - 1]["ArrivedTime"].ToString();
                        LocoInfo.TrainInfo.DepTime = st.Rows[st.Rows.Count - 1]["SetOutTime"].ToString();
                    }
                    else
                    {
                        LocoInfo.TrainInfo.NowStation = "";
                        LocoInfo.TrainInfo.ArrTime = "";
                        LocoInfo.TrainInfo.DepTime = "";
                    }
                }

            }

            LocoInfo.TrainInfo.VehicleTrip = info[24];

            LocoInfo.TrainInfo.DriverName = BaseLibrary.GetDriverName(info[21]);
            //司机号副司机号
            LocoInfo.TrainInfo.DriverNum = info[21];
            LocoInfo.TrainInfo.SubNum = info[22];

            this.Invoke(new EventHandler(delegate
            {

                //if (LocoInfo.TrainInfo.AnnCount == 0)
                //{
                //    picNotice.Image = imgNotice.Images[1];
                //}
                //btnNotice.WordText = "您有" + LocoInfo.TrainInfo.AnnCount + "条新的公告。";

                //车号、车型、速度
                lblId.Text = LocoInfo.TrainInfo.TrainNum;
                lblType.Text = LocoInfo.TrainInfo.TrainType;
                //lblSpeed.Text = LocoInfo.TrainInfo.Speed.Trim();
                int speed = 0;
                try
                {
                    speed = Convert.ToInt32(LocoInfo.TrainInfo.Speed.Trim());
                }
                catch (Exception ex)
                {
                    LogDaily.logerr(ex.ToString());
                    speed = 0;
                }
                lblSpeed.Text = speed.ToString("0000.00");

                //当前站
                lblCurrStation.Text = LocoInfo.TrainInfo.NowStation;
                //到站时间
                lblArriveTime.Text = LocoInfo.TrainInfo.ArrTime;
                //出发时间
                lblStartTime.Text = LocoInfo.TrainInfo.DepTime;

                lblDriver.Text = LocoInfo.TrainInfo.DriverName;
                lblNum.Text = LocoInfo.TrainInfo.VehicleTrip;

                ////坐标
                lblLatitude.Text = LocoInfo.TrainInfo.Latitude;
                lblLongitude.Text = LocoInfo.TrainInfo.Longitude;
                //更新通知公告图标
                //if (LocoInfo.TrainInfo.AnnCount == 0)
                //{
                //    picNotice.Image = imgNotice.Images[1];
                //}
                //else
                //{
                //    picNotice.Image = imgNotice.Images[0];
                //}
                //公里标
                //lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;

                if (LocoInfo.TrainInfo.Kilometer.Trim().Length > 0)
                {
                    string[] kilos = LocoInfo.TrainInfo.Kilometer.Split(new char[] { ' ' });
                    if (kilos.Length > 1)
                    {
                        lblKilometer.Text = kilos[0];
                        lblKiloNum.Text = kilos[1];
                    }
                    else
                    {
                        //公里标
                        lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;
                    }
                }
                else
                {
                    //公里标
                    lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;
                }

                if (isSysTime)
                {
                    initWorkDateTime(DateTime.Now.ToString());
                }

            }));

        }
        #endregion
        #region 通过CAN更新基本信息
        //获取实体类值
        private void setTextCan()
        {
            //到站时间
            if (LocoInfo.TrainInfo.ReportID != 0)
            {
                using (DataTable st = DBAction.GetDTFromSQL("select StationName,ArrivedTime,SetOutTime from RunAndGroup where RHId=" + LocoInfo.TrainInfo.ReportID))
                {
                    if (st.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.NowStation = st.Rows[st.Rows.Count - 1]["StationName"].ToString();
                        LocoInfo.TrainInfo.ArrTime = st.Rows[st.Rows.Count - 1]["ArrivedTime"].ToString();
                        LocoInfo.TrainInfo.DepTime = st.Rows[st.Rows.Count - 1]["SetOutTime"].ToString();
                    }
                    else
                    {
                        LocoInfo.TrainInfo.NowStation = "";
                        LocoInfo.TrainInfo.ArrTime = "";
                        LocoInfo.TrainInfo.DepTime = "";
                    }
                }

            }
            this.Invoke(new EventHandler(delegate
            {

                //if (LocoInfo.TrainInfo.AnnCount == 0)
                //{
                //    picNotice.Image = imgNotice.Images[1];
                //}
                //btnNotice.WordText = "您有" + LocoInfo.TrainInfo.AnnCount + "条新的公告。";

                //车号、车型、速度
                lblId.Text = LocoInfo.TrainInfo.TrainNum;
                lblType.Text = LocoInfo.TrainInfo.TrainType;
                //lblSpeed.Text = LocoInfo.TrainInfo.Speed.Trim();
                int speed = 0;
                try
                {
                    speed = Convert.ToInt32(LocoInfo.TrainInfo.Speed.Trim());
                }
                catch (Exception ex)
                {
                    LogDaily.logerr(ex.ToString());
                    speed = 0;
                }
                lblSpeed.Text = speed.ToString("0000.00");

                //当前站
                lblCurrStation.Text = LocoInfo.TrainInfo.NowStation;
                //到站时间
                lblArriveTime.Text = LocoInfo.TrainInfo.ArrTime;
                //出发时间
                lblStartTime.Text = LocoInfo.TrainInfo.DepTime;

                lblDriver.Text = LocoInfo.TrainInfo.DriverName;
                lblNum.Text = LocoInfo.TrainInfo.VehicleTrip;

                ////坐标
                lblLatitude.Text = LocoInfo.TrainInfo.Latitude;
                lblLongitude.Text = LocoInfo.TrainInfo.Longitude;
                //更新通知公告图标
                //if (LocoInfo.TrainInfo.AnnCount == 0)
                //{
                //    picNotice.Image = imgNotice.Images[1];
                //}
                //else
                //{
                //    picNotice.Image = imgNotice.Images[0];
                //}
                //公里标
                //lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;

                if (LocoInfo.TrainInfo.Kilometer.Trim().Length > 0)
                {
                    string[] kilos = LocoInfo.TrainInfo.Kilometer.Split(new char[] { ' ' });
                    if (kilos.Length > 1)
                    {
                        lblKilometer.Text = kilos[0];
                        lblKiloNum.Text = kilos[1];
                    }
                    else
                    {
                        //公里标
                        lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;
                    }
                }
                else
                {
                    //公里标
                    lblKilometer.Text = LocoInfo.TrainInfo.Kilometer;
                }

                if (isSysTime)
                {
                    initWorkDateTime(DateTime.Now.ToString());
                }

            }));

        }
        #endregion
        /// <summary>
        /// 初始化出勤时间，接车时间
        /// </summary>
        /// <param name="time"></param>
        public void initWorkDateTime(string time)
        {
            if (LocoInfo.TrainInfo.ReportID == 0)
            {
                txtWorkTime.Text = time;
                txtReceiveTime.Text = time;
            }
            else
            {
                //查询出勤时间值，并附初始值
                using (DataTable dt = DBAction.GetDTFromSQL("select DutyTime,ReceiveTime,DeliverTime from Steward where RHId=" + LocoInfo.TrainInfo.ReportID))
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtWorkTime.Text = dt.Rows[dt.Rows.Count - 1]["DutyTime"].ToString();
                        txtReceiveTime.Text = dt.Rows[dt.Rows.Count - 1]["ReceiveTime"].ToString();
                        txtSubmitTime.Text = dt.Rows[dt.Rows.Count - 1]["DeliverTime"].ToString();
                        lbl_Notice.Text = " 您有" + LocoInfo.TrainInfo.AnnCount + "条新公告。";

                    }
                }
            }
        }


        #region 初始化地址
        private void getUrl()
        {

            if (LocoInfo.TrainInfo.Url == "")
            {
                XmlAction xa = new XmlAction();
                XmlNode node = xa.GetNode("/RoboConfig/ServiceAddress", null, null);//服务地址
                XmlNode port = xa.GetNode("/RoboConfig/ServicePort", null, null);//服务端口
                LocoInfo.TrainInfo.Url = xa.GetText(node) + ":" + xa.GetText(port);//服务地址
            }
        }
        #endregion

        /// <summary>
        /// 清除交车时间
        /// </summary>
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txtSubmitTime.Text = "";
            try
            {
                if (LocoInfo.TrainInfo.ReportID != 0)
                {
                    using (RParams pram = new RParams())
                    {
                        pram.Add("DeliverTime", "");
                        DBAction.Update(ETableName.Steward, "DeliverTime", "RHId=" + LocoInfo.TrainInfo.ReportID, pram);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void lblLatitude_ParentChanged(object sender, EventArgs e)
        {

        }

        private void lblKilometer_ParentChanged(object sender, EventArgs e)
        {

        }

        private void TrainBase_Click(object sender, EventArgs e)
        {

        }

        private void btn_Notice_Click(object sender, EventArgs e)
        {
        }

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }

        private void txtSubmitTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Notice_ParentChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void TrainBase_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
