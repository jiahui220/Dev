using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TrainCommon
{
    /**
     * author:luojia
     */

    /// <summary>
    /// 数据应答处理类
    /// </summary>
    public class DataReply
    {
        //应答数据包
        public static string replyData = null;
        public static char[] fileContent = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataReply()
        {

        }

        #region 打包处理方法
        /// <summary>
        /// 计算并添加校验和
        /// </summary>
        public static void AddCheckCode()
        {
            if (!String.IsNullOrEmpty(replyData))
            {
                byte b = 0;
                for (int i = 0; i < replyData.Length; i++)
                {
                    b += (byte)replyData[i];
                }
                replyData += ((char)(0x100 - b)).ToString();
            }
        }

        /// <summary>
        /// CRC校验（校验码低字节在前、高字节在后）
        /// </summary>
        public static void AddCheckCodeCRC()
        {
            byte[] bData = new byte[replyData.Length];
            for (int i = 0; i < replyData.Length; i++)
            {
                bData[i] = (byte)replyData[i];
            }
            int b = 0;
            ushort crc = 0xFFFF;
            for (int i = 0; i < bData.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    b = ((bData[i] << j) & 0x80) ^ ((crc & 0x8000) >> 8);
                    crc <<= 1;
                    if (b != 0)
                    {
                        crc ^= 0x1021;
                    }
                }
            }
            crc = (ushort)~crc;
            replyData += ((char)((byte)(crc & 0x00FF))).ToString() + ((char)((byte)((crc & 0xFF00) >> 8))).ToString();
        }

        /// <summary>
        /// 添加源地址和目标地址
        /// </summary>
        public static void AddAddress()
        {
            if (!String.IsNullOrEmpty(replyData))
            {
                replyData = ((char)0x12).ToString() + ((char)0x11).ToString() + replyData;
            }
        }

        /// <summary>
        /// 转换帧内容:转换帧的内容，不包括帧起始和结束标识
        /// 0x7c -> 0x7d 0x5c ; 
        /// 0x7e -> 0x7d 0x5e ; 
        /// 0x7d -> 0x7d 0x5d
        /// </summary>
        public static void ConvertPacketContent()
        {
            if (String.IsNullOrEmpty(replyData))
            {
                return;
            }
            string replyDataTemp = String.Empty;
            for (int i = 0; i < replyData.Length; i++)
            {
                switch ((byte)replyData[i])
                {
                    case 0x7c:
                        replyDataTemp += ((char)0x7d).ToString() + ((char)0x5c).ToString();
                        break;
                    case 0x7e:
                        replyDataTemp += ((char)0x7d).ToString() + ((char)0x5e).ToString();
                        break;
                    case 0x7d:
                        replyDataTemp += ((char)0x7d).ToString() + ((char)0x5d).ToString();
                        break;
                    default:
                        replyDataTemp += replyData[i].ToString();
                        break;
                }
            }
            replyData = replyDataTemp;
        }

        /// <summary>
        /// 添加帧起始和结束标识
        /// </summary>
        public static void AddPacketStartAndEnd()
        {
            if (!String.IsNullOrEmpty(replyData))
            {
                replyData = ((char)0x7c).ToString() + replyData + ((char)0x7e).ToString();
            }
        }

        /// <summary>
        /// 打包数据
        /// </summary>
        public static void PackData()
        {
            //添加地址
            AddAddress();
            //添加校验和
            AddCheckCode();
            //数据转换
            ConvertPacketContent();
            //添加帧起始和结束位
            AddPacketStartAndEnd();
        }

        #endregion

        #region 应答处理方法
        /// <summary>
        /// 应答心跳或请求发送文件时，需要发送的文件数(1-15)
        /// </summary>
        /// <param name="data">0x01:(应答) 0xF*(*需要发送的文件数1-15)</param>
        /// <returns></returns>
        public static string ReplyHeartOrRequestSendFile(byte data)
        {
            replyData = "";
            //应答标识
            replyData += (char)0x01;
            //数据信息
            replyData += (char)data;
            //打包
            PackData();
            return replyData;
        
        }

        /// <summary>
        /// 获取文件内容总包数
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static int GetTotalPacket(string path)
        {
            fileContent = FileHelper.ReadFile(path);
            if (fileContent.Length < 1)
            {
                return 0;
            }
            return fileContent.Length / 1024 + (fileContent.Length % 1024 == 0 ? 0 : 1);
        }

        /// <summary>
        /// 发送文件头信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string[] ReplySendFileHeader(string fileName)
        {
            string[] data = new string[6];
            data[0] = "1";
            data[1] = fileContent.Length.ToString();
            data[2] ="1";
            data[3] ="1";
            data[4] = fileName.Length.ToString();
            data[5] = fileName;
            return data;
        }

        /// <summary>
        /// 应答文件内容
        /// </summary>
        /// <param name="currPacketNum"></param>
        /// <returns></returns>
        public static string[] ReplySendFileContent(int currPacketNum)
        {
            string[] data = new string[6];
            data[0] = "2";
            int len = 0;
            int totalPacket = fileContent.Length / 1024 + (fileContent.Length % 1024 == 0 ? 0 : 1);
            if (currPacketNum < totalPacket)
            {
                len = 1024;
            }
            else
            {
                len = fileContent.Length % 1024;
            }
            data[1] = fileContent.Length.ToString();
            data[2] = totalPacket.ToString();
            data[3] = currPacketNum.ToString();
            data[4] = len.ToString();
            data[5] = new string(fileContent).Substring((currPacketNum - 1) * 1024, len);
            return data;
        }

        /// <summary>
        /// 应答发送文件
        /// </summary>
        /// <param name="data">
        /// data[0]:数据标识 0x01（数据包为文件名）、0x02（数据包为文件内容）
        /// data[1]:文件总长度，整型
        /// data[2]:总包数，整型(从1开始)
        /// data[3]:当前包号，整型(从1开始)
        /// data[4]:数据包长度，整型（最大1024）
        /// data[5]:数据包内容（文件名），字符串类型
        /// </param>
        /// <returns></returns>
        public static char[] ReplySendFile(string[] data)
        {
            replyData = "";
            //应答标识
            replyData += (char)0x06;
            //数据标识
            replyData += (char)(Convert.ToByte("0x" + data[0], 16));
            //文件总长度（低字节在前）
            string fileLenTemp = Convert.ToUInt32(data[1]).ToString("X8");
            replyData += (char)(Convert.ToByte("0x" + fileLenTemp.Substring(6, 2), 16));
            replyData += (char)(Convert.ToByte("0x" + fileLenTemp.Substring(4, 2), 16));
            replyData += (char)(Convert.ToByte("0x" + fileLenTemp.Substring(2, 2), 16));
            replyData += (char)(Convert.ToByte("0x" + fileLenTemp.Substring(0, 2), 16));
            //总包数
            replyData += (char)(Convert.ToByte("0x" + data[2], 16));
            //当前包号
            replyData += (char)(Convert.ToByte("0x" + data[3], 16));
            //数据包长度（低字节在前）
            string dataLenTemp = Convert.ToUInt16(data[4]).ToString("X4");
            replyData += (char)(Convert.ToByte("0x" + dataLenTemp.Substring(2, 2), 16));
            replyData += (char)(Convert.ToByte("0x" + dataLenTemp.Substring(0, 2), 16));
            //数据包内容
            replyData += data[5];
            //添加地址
            AddAddress();
            //添加CRC校验（2个字节）
            AddCheckCodeCRC();
            //数据转换
            ConvertPacketContent();
            //添加帧的起始和结束位
            AddPacketStartAndEnd();
            return replyData.ToCharArray();
        }

        /// <summary>
        /// 应答收到违规项点
        /// </summary>
        /// <param name="data">
        /// data[0]:收到的应答识别码
        /// data[1]:收到的项点状态[例如：1 2 13 33 45 50]
        /// </param>
        /// <returns></returns>
        public static string ReplyItems(string[] data)
        {
            replyData = "";
            //应答标识
            replyData += (char)0x02;
            //应答标识码
            replyData += (char)(Convert.ToByte("0x" + data[0], 16));
            //项点状态(50个字节)
            string[] itemState = data[1].Split(' ');
            for (int i = 1; i < 51; i++)
            {
                int j = 0;
                for (; j < itemState.Length; j++)
                {
                    if (i == Convert.ToInt32(itemState[j]))
                    {
                        break;
                    }
                }
                if (j < itemState.Length)
                {
                    replyData += (char)0x01;
                }
                else
                {
                    replyData += (char)0x00;
                }
            }
            //打包
            PackData();
            return replyData;
        }

        /// <summary>
        /// 应答公告或文件
        /// </summary>
        /// <param name="data">
        /// data[0]:收到的应答识别码
        /// data[1]:0 记录信息接收完毕  1 重新请求记录信息
        /// data[2]:违规代码（低字节在前）
        /// </param>
        /// <returns></returns>
        public static string ReplyNoticeAndFile(string[] data)
        {
            replyData = "";
            //应答标识
            replyData += (char)0x05;
            //收到的应答识别码
            replyData += (char)(Convert.ToByte("0x" + data[0], 16));
            //数据信息
            replyData += (char)(Convert.ToByte("0x" + data[1], 16));
            //违规代码
            string breakCode = Convert.ToUInt16(data[2]).ToString("X4");
            replyData += (char)(Convert.ToByte("0x" + breakCode.Substring(2, 2), 16));
            replyData += (char)(Convert.ToByte("0x" + breakCode.Substring(0, 2), 16));
            //打包
            PackData();
            return replyData;
        }
        #endregion
    }
}
