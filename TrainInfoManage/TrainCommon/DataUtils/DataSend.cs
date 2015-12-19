using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrainCommon
{
    public class DataSend
    {
        public static int reallyLen;

        public static ushort[] crc_table =
        {
                0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7, 
                0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef, 
                0x1231, 0x0210, 0x3273, 0x2252, 0x52b5, 0x4294, 0x72f7, 0x62d6, 
                0x9339, 0x8318, 0xb37b, 0xa35a, 0xd3bd, 0xc39c, 0xf3ff, 0xe3de, 
                0x2462, 0x3443, 0x0420, 0x1401, 0x64e6, 0x74c7, 0x44a4, 0x5485, 
                0xa56a, 0xb54b, 0x8528, 0x9509, 0xe5ee, 0xf5cf, 0xc5ac, 0xd58d, 
                0x3653, 0x2672, 0x1611, 0x0630, 0x76d7, 0x66f6, 0x5695, 0x46b4, 
                0xb75b, 0xa77a, 0x9719, 0x8738, 0xf7df, 0xe7fe, 0xd79d, 0xc7bc, 
                0x48c4, 0x58e5, 0x6886, 0x78a7, 0x0840, 0x1861, 0x2802, 0x3823, 
                0xc9cc, 0xd9ed, 0xe98e, 0xf9af, 0x8948, 0x9969, 0xa90a, 0xb92b, 
                0x5af5, 0x4ad4, 0x7ab7, 0x6a96, 0x1a71, 0x0a50, 0x3a33, 0x2a12, 
                0xdbfd, 0xcbdc, 0xfbbf, 0xeb9e, 0x9b79, 0x8b58, 0xbb3b, 0xab1a, 
                0x6ca6, 0x7c87, 0x4ce4, 0x5cc5, 0x2c22, 0x3c03, 0x0c60, 0x1c41, 
                0xedae, 0xfd8f, 0xcdec, 0xddcd, 0xad2a, 0xbd0b, 0x8d68, 0x9d49, 
                0x7e97, 0x6eb6, 0x5ed5, 0x4ef4, 0x3e13, 0x2e32, 0x1e51, 0x0e70, 
                0xff9f, 0xefbe, 0xdfdd, 0xcffc, 0xbf1b, 0xaf3a, 0x9f59, 0x8f78, 
                0x9188, 0x81a9, 0xb1ca, 0xa1eb, 0xd10c, 0xc12d, 0xf14e, 0xe16f, 
                0x1080, 0x00a1, 0x30c2, 0x20e3, 0x5004, 0x4025, 0x7046, 0x6067, 
                0x83b9, 0x9398, 0xa3fb, 0xb3da, 0xc33d, 0xd31c, 0xe37f, 0xf35e, 
                0x02b1, 0x1290, 0x22f3, 0x32d2, 0x4235, 0x5214, 0x6277, 0x7256, 
                0xb5ea, 0xa5cb, 0x95a8, 0x8589, 0xf56e, 0xe54f, 0xd52c, 0xc50d, 
                0x34e2, 0x24c3, 0x14a0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405, 
                0xa7db, 0xb7fa, 0x8799, 0x97b8, 0xe75f, 0xf77e, 0xc71d, 0xd73c, 
                0x26d3, 0x36f2, 0x0691, 0x16b0, 0x6657, 0x7676, 0x4615, 0x5634, 
                0xd94c, 0xc96d, 0xf90e, 0xe92f, 0x99c8, 0x89e9, 0xb98a, 0xa9ab, 
                0x5844, 0x4865, 0x7806, 0x6827, 0x18c0, 0x08e1, 0x3882, 0x28a3, 
                0xcb7d, 0xdb5c, 0xeb3f, 0xfb1e, 0x8bf9, 0x9bd8, 0xabbb, 0xbb9a, 
                0x4a75, 0x5a54, 0x6a37, 0x7a16, 0x0af1, 0x1ad0, 0x2ab3, 0x3a92, 
                0xfd2e, 0xed0f, 0xdd6c, 0xcd4d, 0xbdaa, 0xad8b, 0x9de8, 0x8dc9, 
                0x7c26, 0x6c07, 0x5c64, 0x4c45, 0x3ca2, 0x2c83, 0x1ce0, 0x0cc1, 
                0xef1f, 0xff3e, 0xcf5d, 0xdf7c, 0xaf9b, 0xbfba, 0x8fd9, 0x9ff8, 
                0x6e17, 0x7e36, 0x4e55, 0x5e74, 0x2e93, 0x3eb2, 0x0ed1, 0x1ef0 
        };

        /// <summary>
        /// 取得和校验码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte GetSumCheck(byte[] data, int len)
        {
            byte sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += (byte)data[i];
            }
            return (byte)(~sum + 1);
        }

        /// <summary>
        /// 取得CRC校验码
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static ushort GetCRCCheck(byte[] data, int len)
        {
            byte hbit = 0;
            ushort crc = 0;

            for (int i = 0; i < len; i++)
            {
                hbit = (byte)((crc & 0xff00) >> 8);
                crc <<= 8;
                crc ^= crc_table[hbit ^ data[i]];
            }
            byte low = (byte)((crc >> 8) & 0x00FF);
            byte high = (byte)(crc & 0x00FF);
            crc = (ushort)((high << 8) + low);
            return crc;
        }

        /// <summary>
        /// 获取加上帧起始标志位的数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetSendDate(byte[] data)
        {
            byte[] b = new byte[200000];
            b[0] = 0x7C;
            reallyLen = 1;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0x7C)
                {
                    b[reallyLen] = 0x7d;
                    reallyLen++;
                    b[reallyLen] = 0x5c;
                }
                else if (data[i] == 0x7E)
                {
                    b[reallyLen] = 0x7d;
                    reallyLen++;
                    b[reallyLen] = 0x5e;
                }
                else if (data[i] == 0x7D)
                {
                    b[reallyLen] = 0x7d;
                    reallyLen++;
                    b[reallyLen] = 0x5d;
                }
                else
                {
                    b[reallyLen] = data[i];
                }
                reallyLen++;
            }
            b[reallyLen] = 0x7E;
            reallyLen++;
            return b;
        }

        /// <summary>
        /// 请求信息补发
        /// </summary>
        /// <returns></returns>
        public static byte[] AskReissue()
        {
            //获取当前的时间，转换为对应的十六进制
            DateTime now = DateTime.Now;
            DateTime toSave = DateTime.Now.AddHours(-1);
            int endyea = now.Year;
            endyea = endyea - 2000;
            byte endyea1 = Convert.ToByte(endyea);
            int endmon = now.Month;
            byte endmon1 = Convert.ToByte(endmon);
            int endday = now.Day;
            byte endday1 = Convert.ToByte(endday);
            int endhou = now.Hour;
            byte endhou1 = Convert.ToByte(endhou);
            int endmin = now.Minute;
            byte endmin1 = Convert.ToByte(endmin);
            int endsec = now.Second;
            byte endsec1 = Convert.ToByte(endsec);
            if (LocoInfo.TrainInfo.OldLogTime != "")
            {
                DateTime offtime = Convert.ToDateTime(LocoInfo.TrainInfo.OldLogTime);
                int stayea = offtime.Year;
                stayea = stayea - 2000;
                byte stayea1 = Convert.ToByte(stayea);
                int stamon = offtime.Month;
                byte stamon1 = Convert.ToByte(stamon);
                int staday = offtime.Day;
                byte staday1 = Convert.ToByte(staday);
                int stahou = offtime.Hour;
                byte stahou1 = Convert.ToByte(stahou);
                int stamin = offtime.Minute;
                byte stamin1 = Convert.ToByte(stamin);
                int stasec = offtime.Second;
                byte sstaec1 = Convert.ToByte(stasec);
                byte[] data = new byte[17] { 0x12, 0x11, 0x01, 0x03, stayea1, stamon1, staday1, stahou1, stamin1, sstaec1, endyea1, endmon1, endday1, endhou1, endmin1, endsec1, 0xDB, };
                data[16] = GetSumCheck(data, 16);
                return GetSendDate(data);
            }
            else
            {
                int stayea = toSave.Year;
                stayea = stayea - 2000;
                byte stayea1 = Convert.ToByte(stayea);
                int stamon = toSave.Month;
                byte stamon1 = Convert.ToByte(stamon);
                int staday = toSave.Day;
                byte staday1 = Convert.ToByte(staday);
                int stahou = toSave.Hour;
                byte stahou1 = Convert.ToByte(stahou);
                int stamin = toSave.Minute;
                byte stamin1 = Convert.ToByte(stamin);
                int stasec = toSave.Second;
                byte sstaec1 = Convert.ToByte(stasec);
                byte[] data = new byte[17] { 0x12, 0x11, 0x01, 0x03, stayea1, stamon1, staday1, stahou1, stamin1, sstaec1, endyea1, endmon1, endday1, endhou1, endmin1, endsec1, 0xDB, };
                data[16] = GetSumCheck(data, 16);
                return GetSendDate(data);
            }          
        }
        /// <summary>
        /// 应答心跳
        /// </summary>
        /// <returns></returns>
        public static byte[] SendHeart()
        {
            byte[] data = new byte[5] { 0x12, 0x11, 0x01, 0x01, 0xDB, };
            data[4] = GetSumCheck(data, 4);
            return GetSendDate(data);

        }
        //十六进制配上0x
        public static string ad(string a)
        {
            string result = "";
            if (a == "0" || a == "1" || a == "2" || a == "3" || a == "4" || a == "5" || a == "6" || a == "7" || a == "8" || a == "9" || a == "a" || a == "b" || a == "c" || a == "d" || a == "e" || a == "f")
                a = "0" + a;
            result += "0x" + a;
            return result;
        }
        /// 把十六进制字符串转换成字节型
        public static byte[] StringToByte(string InString)
        {
            string[] ByteStrings;
            ByteStrings = InString.Split(" ".ToCharArray());
            byte[] ByteOut;
            ByteOut = new byte[ByteStrings.Length - 1];
            for (int i = 0; i == ByteStrings.Length - 1; i++)
            {
                ByteOut[i] = Convert.ToByte(("0x" + ByteStrings[i]));
            }
            return ByteOut;
        }
        /// <summary>
        /// 应答发送文件请求
        /// </summary>
        /// <param name="fileNum">表示需要发送文件数1-15</param>
        /// <returns></returns>
        public static byte[] SendFileCount()
        {
            byte[] data = new byte[5] { 0x12, 0x11, 0x01, 0xF1, 0xEB };
            data[4] = GetSumCheck(data, 4);
            return GetSendDate(data);
        }

        /// <summary>
        /// 项点应答
        /// </summary>
        /// <param name="cID">收到的应答识别码</param>
        /// <param name="itemState"></param>
        /// <returns></returns>
        public static byte[] SendItems(byte cID, byte[] itemState)
        {
            byte[] data = new byte[55];
            data[0] = 0x12;
            data[1] = 0x11;
            data[2] = 0x02;
            data[3] = cID;
            for (int i = 0; i < itemState.Length; i++)
            {
                data[i + 4] = itemState[i];
            }
            data[54] = GetSumCheck(data, 54);
            return GetSendDate(data);
        }

        /// <summary>
        /// 发送文件回应消息
        /// </summary>
        /// <param name="itemCode">收到的应答识别码</param>
        /// <param name="isFinish">0 记录信息接收完毕 1重新请求记录信息</param>
        /// <returns></returns>
        public static byte[] SendFileReply(ushort itemCode, bool isFinish)
        {
            byte[] data = new byte[7] { 0x12, 0x11, 0x05, 0x00, 0x00, 0x00, 0x00 };
            data[3] = (byte)(isFinish ? 0x00 : 0x01);
            data[4] = (byte)(itemCode & 0x00FF);
            data[5] = (byte)((itemCode & 0xFF00) >> 8);
            data[6] = GetSumCheck(data, 6);
            return GetSendDate(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cType">数据标示（0x01表示文件名，0x02表示文件内容）</param>
        /// <param name="fileLen">文件总长度</param>
        /// <param name="totalPacket">总包数</param>
        /// <param name="currentPacket">当前包号</param>
        /// <param name="content">数据包内容</param>
        /// <returns></returns>
        public static byte[] SendFile(byte cType, int fileLen, byte totalPacket, byte currentPacket, byte[] content)
        {
            byte[] data = new byte[14 + content.Length];
            data[0] = 0x12;
            data[1] = 0x11;
            data[2] = 0x06;
            data[3] = cType;
            //文件总长度
            data[4] = (byte)(fileLen & 0x000000FF);
            data[5] = (byte)((fileLen & 0x0000FF00) >> 8);
            data[6] = (byte)((fileLen & 0x00FF0000) >> 16);
            data[7] = (byte)((fileLen & 0xFF000000) >> 24);
            //总包数
            data[8] = totalPacket;
            data[9] = currentPacket;
            //数据包长度
            data[10] = (byte)(content.Length & 0x00FF);
            data[11] = (byte)((content.Length & 0xFF00) >> 8);
            int i = 12;
            for (; i < data.Length - 2; i++)
            {
                data[i] = content[i - 12];
            }

            ushort crc = GetCRCCheck(data, 12 + content.Length);
            data[i] = (byte)(crc & 0x00FF);
            data[i + 1] = (byte)((crc & 0xFF00) >> 8);
            return GetSendDate(data);
        }
    }
}
