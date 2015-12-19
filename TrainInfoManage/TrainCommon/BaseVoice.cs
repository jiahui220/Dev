using System;

using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace TrainCommon
{
    /// <summary>
    /// 语音朗读类
    /// </summary>
    public  class BaseVoice
    {
        SerialPort sp = new SerialPort();

        private bool isOpen = false;

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }

        #region 静态实例成员
        private static BaseVoice _TrainVoice = null;
        /// <summary>
        ///实例语音
        /// </summary>
        public static BaseVoice TrainVoice
        {
            get
            {
                if (_TrainVoice == null)
                    _TrainVoice = new BaseVoice();
                return _TrainVoice;
            }
        }
        #endregion


        /// <summary>
        /// 语音朗读
        /// </summary>
        /// <param name="content">朗读内容</param>
        public  void SpeekVioce(string content)
        {
            if (!IsOpen)
            {
                if (sp.PortName != "COM2")
                {
                    sp.PortName = "COM2";//设置通信串口
                }
                sp.BaudRate = 9600;//设置串口波特率
                sp.DataBits = 8;//设置串口数据位
                sp.StopBits = StopBits.One;//设置停止位
                sp.Parity = Parity.None;//设置校验
                sp.WriteTimeout = 5000;//设置超时
                if (!sp.IsOpen)
                {
                    sp.Open(); 
                }
                isOpen = true;
            }
            byte[] send = GetSendByte(content);//发送数组
            sp.Write(send, 0, send.Length);//传送数据


        }

        //将语音字符串转化为语音合成指令
        public static byte[] GetSendByte(string content)
        {
            byte[] byteArray = Encoding.GetEncoding("UNICODE").GetBytes(content);//将要合成的语音转化为字节
            int l = byteArray.Length + 2;
            string cl = Convert.ToString(l, 16);
            if (cl.Length < 4)
            {
                int n = 4 - cl.Length;
                switch (n)
                {
                    case 1:
                        cl = "0 " + cl;
                        break;
                    case 2:
                        cl = "00 " + cl;
                        break;
                    case 3:
                        cl = "00 0" + cl;
                        break;
                }
            }
            string hexValues = "FD " + cl + " 01 03";
            byte[] headByte = UnHex(hexValues, "gb2312");
            byte[] allByte = copybyte(headByte, byteArray);
            return allByte;

        }

        //将字符串转换为字节数组
        public static  byte[] UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格 
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            return bytes;
        }

        //合并数组
        public static byte[] copybyte(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
    }
}
