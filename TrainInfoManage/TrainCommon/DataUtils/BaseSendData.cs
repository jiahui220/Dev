using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TrainCommon
{
    public class BaseSendData
    {
        //将文件读取为字节流
        public static byte[] FileToByte(string path) {

            byte[] bt = null;
            FileStream fs = null;
            if (File.Exists(path))
            {
                //打开现有文件以进行读取。
                fs = File.OpenRead(path);
                int b1;
                System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                while ((b1 = fs.ReadByte()) != -1)
                {
                    tempStream.WriteByte(((byte)b1));
                }
                bt = tempStream.ToArray();
            }
            return bt;
        }


         //应答数据包
        public static string replyData = null;
        public static char[] fileContent = null;
        public static byte[] fileByte = null;//文件内容
        public static byte[] nameByte = null;//文件名
        public static byte[] bData = null;//发送的内容
        public static byte[] currByte = null;//当前数据包自己
        public static byte[] bDataAndCrc = null;//除头尾外所有内容

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
        public static byte[] AddCheckCodeCRC()
        {
            byte[] by = new byte[2];
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
            by[0] = (byte)(crc & 0x00FF);
            by[1]=(byte)((crc & 0xFF00) >> 8);
            return by; 
        }

        /// <summary>
        /// 添加源地址和目标地址
        /// </summary>
        public static byte[] AddAddress()
        {
            byte[] add = new byte[2];
            add[0]=0x12;
            add[1] =0x11;
            return add;
        }

        /// <summary>
        /// 转换帧内容:转换帧的内容，不包括帧起始和结束标识
        /// 0x7c -> 0x7d 0x5c ; 
        /// 0x7e -> 0x7d 0x5e ; 
        /// 0x7d -> 0x7d 0x5d
        /// </summary>
        public static byte[] ConvertPacketContent()
        {
            int len = bDataAndCrc.Length;

            if (len<0)
            {
                return null;
            }
            int iPos = 1;
            for (int i = 0; i < len; ++i)
            {
                if (bDataAndCrc[i] == 0x7C)
                {
                    iPos++;
                }
                else if (bDataAndCrc[i] == 0x7E)
                {
                    iPos++;
                }
                else if (bDataAndCrc[i] == 0x7D)
                {
                    iPos++;
                }
                iPos++;
            }
            byte[] resultData = new byte[iPos + 1];
            
            //协议头
            resultData[0] = 0x7C;

            iPos = 1;
            for (int i = 0; i < len; i++)
            {
                if (bDataAndCrc[i] == 0x7C)
                {
                    resultData[iPos] = 0x7d;
                    iPos++;
                    resultData[iPos] = 0x5c;
                }
                else if (bDataAndCrc[i] == 0x7E)
                {
                    resultData[iPos] = 0x7d;
                    iPos++;
                    resultData[iPos] = 0x5e;
                }
                else if (bDataAndCrc[i] == 0x7D)
                {
                    resultData[iPos] = 0x7d;
                    iPos++;
                    resultData[iPos] = 0x5d;
                }
                else
                {
                    resultData[iPos] = bDataAndCrc[i];
                }
                iPos++;
            }

            // 协议尾
            resultData[iPos] = 0x7E;

            return resultData;

        }

        /// <summary>
        /// 添加帧起始和结束标识
        /// </summary>
        public static byte[] AddPacketStartAndEnd()
        {
            byte[] se = new byte[2];
            se[0] = Convert.ToByte("0x7c", 16);
            se[1] = Convert.ToByte("0x7e", 16);
            return se;
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
            fileByte = FileToByte(path);
            if (fileByte.Length < 1)
            {
                return 0;
            }
            return fileByte.Length / 1024 + (fileByte.Length % 1024 == 0 ? 0 : 1);
        }

        /// <summary>
        /// 发送文件头信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string[] ReplySendFileHeader(string fileName)
        {
            nameByte = Encoding.Default.GetBytes(fileName);
            string[] data = new string[5];
            data[0] = "1";
            data[1] = fileByte.Length.ToString();
            data[2] ="1";
            data[3] ="1";
            data[4] = nameByte.Length.ToString();
            return data;
        }

        /// <summary>
        /// 应答文件内容
        /// </summary>
        /// <param name="currPacketNum"></param>
        /// <returns></returns>
        public static string[] ReplySendFileContent(int currPacketNum)
        {
            string[] data = new string[5];
            data[0] = "2";
            int len = 0;
            int totalPacket = fileByte.Length / 1024 + (fileByte.Length % 1024 == 0 ? 0 : 1);
            if (currPacketNum < totalPacket)
            {
                len = 1024;
            }
            else
            {
                len = fileByte.Length % 1024;
            }
            data[1] = fileByte.Length.ToString();
            data[2] = totalPacket.ToString();
            data[3] = currPacketNum.ToString();
            data[4] = len.ToString();
            currByte=new byte[len];
            Array.Copy(fileByte, (currPacketNum - 1) * 1024, currByte, 0, len);
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
        public static byte[] ReplySendFile(string[] data,int type)
        {
            //协议首部分
            bData = null;
            bDataAndCrc=null;
            byte[] fristData = new byte[10];
            //应答标识
            fristData[0] = Convert.ToByte("0x06",16);
            //数据标识
            fristData[1] = Convert.ToByte("0x" + data[0], 16);
            //文件总长度（低字节在前）
            string fileLenTemp = Convert.ToUInt32(data[1]).ToString("X8");
            fristData[2] =Convert.ToByte("0x" + fileLenTemp.Substring(6, 2), 16);
            fristData[3] =Convert.ToByte("0x" + fileLenTemp.Substring(4, 2), 16);
            fristData[4] =Convert.ToByte("0x" + fileLenTemp.Substring(2, 2), 16);
            fristData[5] =Convert.ToByte("0x" + fileLenTemp.Substring(0, 2), 16);
            //总包数
            fristData[6] =Convert.ToByte("0x" + data[2], 16);
            //当前包号
            fristData[7] =Convert.ToByte("0x" + data[3], 16);
            //数据包长度（低字节在前）
            string dataLenTemp = Convert.ToUInt16(data[4]).ToString("X4");
            fristData[8] =Convert.ToByte("0x" + dataLenTemp.Substring(2, 2), 16);
            fristData[9] =Convert.ToByte("0x" + dataLenTemp.Substring(0, 2), 16);
            //添加地址
            byte[] addByte=AddAddress();
            //文件内容(数据包内容)
            switch (type)
            {
                case 0:
                    bData = new byte[12 + nameByte.Length];//除校验位，起始位，结束位外所有内容长度
                    addByte.CopyTo(bData, 0);
                    fristData.CopyTo(bData, addByte.Length);
                    nameByte.CopyTo(bData, fristData.Length + addByte.Length);
                    break;
                case 1:
                    //MessageBox.Show("1****");
                    bData = new byte[12 + currByte.Length];//除校验位，起始位，结束位外所有内容长度
                    //MessageBox.Show("2****" + bData.Length);
                    addByte.CopyTo(bData, 0);
                    //MessageBox.Show("3****" + fristData);
                    fristData.CopyTo(bData, addByte.Length);
                    //MessageBox.Show("4****" + currByte.Length);
                    currByte.CopyTo(bData, fristData.Length + addByte.Length);
                    break;
            }
            //添加CRC校验（2个字节）
            byte[] crc=AddCheckCodeCRC();
            bDataAndCrc = new byte[crc.Length + bData.Length];
            bData.CopyTo(bDataAndCrc, 0);
            crc.CopyTo(bDataAndCrc, bData.Length);
            //数据转换
            byte[] dataByte=ConvertPacketContent();
            return dataByte;
        }

        #endregion

    }
}
