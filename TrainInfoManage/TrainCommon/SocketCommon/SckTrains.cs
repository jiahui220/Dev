using System;

using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace TrainCommon
{
    public class SckTrains
    {
        //定义socket变量
        public static Socket socket;
        //设定socket接收长度
        private static int size = 1024;
        private SckParams _PMS = null;

        /// <summary>
        /// 数据包集合
        /// </summary>
        public SckParams PMS
        {
            get { return _PMS; }
            set { _PMS = value; }
        }

        private string _IP = "";

        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }

        private int _Port = 10000;

        /// <summary>
        /// 服务器监听端口
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }


        private string _SendType = "TCP";

        /// <summary>
        /// 通信方式UDP,TCP
        /// </summary>
        public string SendType
        {
            get { return _SendType; }
            set { _SendType = value; }
        }

        public SckTrains() 
        { 
          
        }

        public  bool Send() 
        {
            return Send(_PMS, _IP, _Port, _SendType);
        }

        public  bool Send(SckParams pms) 
        {
            return Send(pms, _IP, _Port, _SendType);
        }

        public bool Send(SckParams pms, string SendType)
        {
            return Send(pms, _IP, _Port, SendType);
        }

        //判断是否已连接
        private bool isConnect = false;

        /// <summary>
        /// 是否已连接服务器
        /// </summary>
        public bool IsConnect
        {
            get { return isConnect; }
            set { isConnect = value; }
        }

        /// <summary>
        /// 发送数据包
        /// </summary>
        /// <param name="pms">数据包集合</param>
        /// <param name="IP">服务器地址</param>
        /// <param name="Port">服务器端口</param>
        /// <param name="SendType">发送形式UDP或TCP</param>
        /// <returns></returns>
        public  bool Send(SckParams pms, string IP, int Port, string SendType) 
        {
            try
            {
                //验证IP地址
                if (!BaseLibrary.CheckIPAddress(IP))
                {
                    return false;
                }
                if (!isConnect)
                {
                    IPEndPoint ServerIPEP = new IPEndPoint(IPAddress.Parse(IP), Port);
                    //建立连接
                    if (SendType.ToLower() == "UDP")
                    {
                        socket = new Socket(ServerIPEP.AddressFamily, SocketType.Stream, ProtocolType.Udp);
                    }
                    else
                    {
                        socket = new Socket(ServerIPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    }
                    //将Socket连接到服务器
                 
                    socket.Connect((EndPoint)ServerIPEP);
                    isConnect = true;
                }

                //判断数据包是否为空
                if (pms!=null&&pms.PackItems.Count>0)
                {

                    //获取发送数据包
                    List<string> packs = pms.PackItems;
                    for (int i = 0; i < packs.Count; i++)
                    {
                        //向服务器发送数据包
                        byte[] packBytes = System.Text.Encoding.Default.GetBytes(packs[i]);
                        socket.Send(packBytes);
                        if (packBytes.Length>0)
                        {
                            LocoInfo.TrainInfo.Flow += packBytes.Length;
                        }
                        LocoInfo.TrainInfo.ClientSocket = socket;
                        Thread.Sleep(500);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //日志记录
                LogDaily.logerr(ex.ToString());
                isConnect = false;
                return false;
            }
        }

        public string SendMsg(SckParams pms)
        {
            try
            {
                string result = string.Empty;
                //验证IP地址
                if (!BaseLibrary.CheckIPAddress(this.IP))
                {
                    return result;
                }
                if (!isConnect)
                {
                    IPEndPoint ServerIPEP = new IPEndPoint(IPAddress.Parse(this.IP), this.Port);
                    //建立连接
                    if (this.SendType.ToLower() == "UDP")
                    {
                        socket = new Socket(ServerIPEP.AddressFamily, SocketType.Stream, ProtocolType.Udp);
                    }
                    else
                    {
                        socket = new Socket(ServerIPEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    }
                    //将Socket连接到服务器
                    socket.Connect((EndPoint)ServerIPEP);
                    isConnect = true;
                }
                //判断数据包是否为空
                if (pms != null && pms.PackItems.Count > 0)
                {

                    //获取发送数据包
                    List<string> packs = pms.PackItems;
                    for (int i = 0; i < packs.Count; i++)
                    {

                        //向服务器发送数据包
                        byte[] packBytes = System.Text.Encoding.Default.GetBytes(packs[i]);
                        socket.Send(packBytes);
                        LocoInfo.TrainInfo.ClientSocket = socket;
                        Thread.Sleep(500);
                        byte[] data = new byte[size];
                        int ret = 1;
                        while (ret > 0)
                        {
                            ret = socket.Receive(data, data.Length, 0);
                            result += Encoding.Default.GetString(data, 0, ret);
                            if (ret < size && result.Trim().EndsWith("}#"))
                            {
                                return result;
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                return null;
            }
        }
    }
}
