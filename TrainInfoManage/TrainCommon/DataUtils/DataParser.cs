using System;
using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    /**
     * author:luojia
     */

    /// <summary>
    /// 数据解析器
    /// </summary>
    public class DataParser
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataParser()
        {
        }

        #region 数据转换操作
        /// <summary>
        /// 将字符串转换为字符数组
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>字符数组</returns>
        public static char[] Str2Char(string data)
        {
            return data.ToCharArray();
        }

        /// <summary>
        /// 将字符串转为为字节数组
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] Str2Byte(string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        /// <summary>
        /// 将字符串转换为十六进制字符串
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>十六进制字符串</returns>
        public static string Str2HexStr(string data)
        {
            string s = String.Empty;
            byte[] b = Str2Byte(data);
            for (int i = 0; i < b.Length; i++)
            {
                s += Convert.ToString(b[i], 16) + " ";
            }
            return s.Trim().ToUpper();
        }

        /// <summary>
        /// 多字符转为整数
        /// </summary>
        /// <param name="c1">低字节</param>
        /// <param name="c2">高字节</param>
        /// <returns></returns>
        public static int MultiChar2Int(char c1, char c2)
        {
            return ((byte)c2 << 8) + (byte)c1;
        }

        /// <summary>
        /// 字符转为整数
        /// </summary>
        /// <param name="c1">低字节</param>
        /// <param name="c2"></param>
        /// <param name="c3">高字节</param>
        /// <returns></returns>
        public static int MultiChar2Int(char c1, char c2, char c3)
        {
            return ((byte)c3 << 16) + MultiChar2Int(c1, c2);
        }

        /// <summary>
        /// 字符转为整数
        /// </summary>
        /// <param name="c1">低字节</param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="c4">高字节</param>
        /// <returns></returns>
        public static int MultiChar2Int(char c1, char c2, char c3, char c4)
        {
            return ((byte)c4 << 24) + MultiChar2Int(c1, c2, c3);
        }
        #endregion

        #region 获取数据包相关信息
        /// <summary>
        /// 获取帧内容
        /// </summary>
        /// <param name="data">整个数据包字符数组，包括帧起始，帧内容和结束位</param>
        /// <returns>帧内容字符数组，不包括帧起始和结束位</returns>
        public static char[] GetPacketContent(char[] data)
        {
            string s = new string(data);
            char[] temp = Str2Char(s.Substring(1, s.Length - 2));
            string res = String.Empty;
            for (int i = 0; i < temp.Length; i++)
            {
                if ((byte)temp[i] == 0x7d)
                {
                    if ((byte)temp[i + 1] == 0x5c)
                    {
                        res += ((char)0x7c).ToString();
                        i++;
                    }
                    else if ((byte)temp[i + 1] == 0x5e)
                    {
                        res += ((char)0x7e).ToString();
                        i++;
                    }
                    else if ((byte)temp[i + 1] == 0x5d)
                    {
                        res += ((char)0x7d).ToString();
                        i++;
                    }
                    else
                    {
                        res += temp[i].ToString();
                    }
                }
                else
                {
                    res += temp[i].ToString();
                }
            }
            return Str2Char(res);
        }


        /// <summary>
        /// 获取数据包协议类型
        /// </summary>
        /// <param name="data">帧内容字符数组，不包括帧起始和结束位</param>
        /// <returns></returns>
        public static byte GetDataType(char[] data)
        {
            return (byte)data[2];
        }

        /// <summary>
        /// 获取数据包的数据信息
        /// </summary>
        /// <param name="data">帧内容字符数组，不包括帧起始和结束位</param>
        /// <returns></returns>
        public static char[] GetData(char[] data)
        {
            string s = new string(data);
            string l = s.Length.ToString();
            return Str2Char(s.Substring(3, s.Length - 4));
        }
        public static char[] GetData1(char[] data)
        {
            string s = new string(data);
            string l = s.Length.ToString();
            return Str2Char(s.Substring(11, s.Length - 11));
        }
        public static char[] GetData1(char[] data, int n)
        {
            string s = new string(data);
            string l = s.Length.ToString();
            return Str2Char(s.Substring(n, 80));
        }
        #endregion


        #region 接受数据包相关协议处理方法
        /// <summary>
        /// 接收到心跳数据(0x01)
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns></returns>
        public static bool ReceiveHeart(char[] data)
        {
            if (data.Length == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 接收到请求文件发送数据(0x06)
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>文件号：0x01(文件从1开始，最大15个)、0xFF文件接受完、0xF0接受失败</returns>
        public static byte ReceiveRequestFile(char[] data)
        {
            if (data.Length == 1)
            {
                return (byte)data[0];
            }
            return 0xF0;
        }

        /// <summary>
        /// 接受到实时的违规项点数据(0x02)`
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>
        /// string[0]:违规项点编号，如1 4 22 34，代表第1、4、22、34项违规
        /// string[1]:司机号
        /// string[1]:车次号码
        /// </returns>
        public static string[] ReceiveBreakItems(char[] data)
        {
            string[] result = null;
            if (data.Length == 57)
            {
                result = new string[5];
                result[0] = ((byte)data[0]).ToString();
                string items = String.Empty;
                //每一个字节代表一个违规项点的状态(1-50)
                for (int i = 1; i < 51; i++)
                {
                    //0x01：违规    0x00：无违规    其它：预留
                    if ((byte)data[i] == 0x01)
                    {
                        items += i + " ";
                    }
                }
                result[1] = items.Trim();
                //主程序版本号
                result[2] = ((byte)data[51]).ToString("d2") + "" + ((byte)data[52]).ToString("d2");
                LocoInfo.TrainInfo.ProVersion = result[2];
                //外CAN库版本号
                result[3] = ((byte)data[53]).ToString("d2") + "" + ((byte)data[54]).ToString("d2");
                LocoInfo.TrainInfo.CanVersion = result[3];
                //配置文件版本号
                result[4] = ((byte)data[55]).ToString("d2") + "" + ((byte)data[56]).ToString("d2");
                LocoInfo.TrainInfo.ItemsVersion = result[4];
            }
            return result;
        }
        /// <summary>
        /// 接受到实时的违规项点数据(0x02)`
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns>
        /// string[0]:违规项点编号，如1 4 22 34，代表第1、4、22、34项违规
        /// string[1]:司机号
        /// string[1]:车次号码
        /// </returns>
        public static string[] ReceiveBreakItems1(char[] data)
        {
            string[] result = null;
            if (data.Length == 257)
            {
                result = new string[6];
                string timebj = string.Empty;
                result[0] = ((byte)data[0]).ToString();
                string items = String.Empty;
                //每一个字节代表一个违规项点的状态(1-50)
                for (int i = 1, j = 1; j < 251; i++)
                {
                    //0x01：违规    0x00：无违规    其它：预留
                    if ((byte)data[j] == 0x01)
                    {
                        items += i + " ";
                        string yearM = DateTime.Now.Year + "-" + (Convert.ToString(DateTime.Now.Month).Length == 1 ? "0" + Convert.ToString(DateTime.Now.Month) : Convert.ToString(DateTime.Now.Month)) + "-";
                        string dayStr = Convert.ToString((byte)data[j + 1]).Length == 1 ? "0" + Convert.ToString((byte)data[j + 1]) : Convert.ToString((byte)data[j + 1]);
                        string hourStr = Convert.ToString((byte)data[j + 2]).Length == 1 ? "0" + Convert.ToString((byte)data[j + 2]) : Convert.ToString((byte)data[j + 2]);
                        string minuStr = Convert.ToString((byte)data[j + 3]).Length == 1 ? "0" + Convert.ToString((byte)data[j + 3]) : Convert.ToString((byte)data[j + 3]);
                        string secStr = Convert.ToString((byte)data[j + 4]).Length == 1 ? "0" + Convert.ToString((byte)data[j + 4]) : Convert.ToString((byte)data[j + 4]);
                        if ((byte)data[j + 1] == 0x00)
                        {
                            timebj += DateTime.Now.ToString() + ",";
                        }
                        else
                        { 
                            timebj += yearM + dayStr + " " + hourStr + ":" + minuStr + ":" + secStr + ","; 
                        }
                    }
                    j = j + 5;
                }
                //存放了违规的顺序号
                result[1] = items.Trim();
                //主程序版本号
                result[2] = ((byte)data[251]).ToString("d2") + "" + ((byte)data[252]).ToString("d2");
                LocoInfo.TrainInfo.ProVersion = result[2];
                //外CAN库版本号
                result[3] = ((byte)data[253]).ToString("d2") + "" + ((byte)data[254]).ToString("d2");
                LocoInfo.TrainInfo.CanVersion = result[3];
                //配置文件版本号
                result[4] = ((byte)data[255]).ToString("d2") + "" + ((byte)data[256]).ToString("d2");
                LocoInfo.TrainInfo.ItemsVersion = result[4];
                //报警时间
                result[5] = timebj.Trim();
            }
            return result;
        }
        /// <summary>
        /// 接受到实时的公告数据(0x02)
        /// </summary>
        /// <param name="data">数据信息</param>
        /// <returns></returns>
        public static string[] ReceiveNotice(char[] data)
        {
            string[] result = null;
            if (data.Length == 71)
            {
                result = ParserNoticeAndFile(data);
            }
            return result;
        }

        /// <summary>
        /// 接受到实时的文件发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] ReceviceFile(char[] data)
        {
            string[] result = null;
            if (data.Length == 71)
            {
                result = ParserNoticeAndFile(data);
            }
            return result;
        }
        /// <summary>
        /// 解析补发信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] ParserNoticeAndFile1(char[] data)
        {
            string[] result = new string[37];
            result[0] = "";
            result[1] = "";

            string time = ((byte)data[0] + 2000) + "-" + (byte)data[1] + "-" + (byte)data[2] + " ";
            time += (byte)data[3] + ":" + (byte)data[4] + ":" + (byte)data[5];
            result[2] = time;

            //速度
            result[3] = MultiChar2Int(data[6], data[7]).ToString();
            //限速
            result[4] = MultiChar2Int(data[8], data[9]).ToString();
            //距离  平调/调车/出入段工作状态下为有符号数，其它为无符号
            result[5] = MultiChar2Int(data[10], data[11]).ToString();

            #region 信号机种类
            switch (((byte)data[12] & 0xE0) >> 4)
            {
                case 0:
                    result[6] = "备用";
                    break;
                case 1:
                    result[6] = "进出站";
                    break;
                case 2:
                    result[6] = "出站";
                    break;
                case 3:
                    result[6] = "进站";
                    break;
                case 4:
                    result[6] = "通过";
                    break;
                case 5:
                    result[6] = "预告";
                    break;
                case 6:
                    result[6] = "容许";
                    break;
                case 7:
                    result[6] = "分割";
                    break;
                case 8:
                    result[6] = "未定义";
                    break;
                case 9:
                    result[6] = "1预告";
                    break;
                case 10:
                    result[6] = "2预告";
                    break;
                default:
                    result[6] = String.Empty;
                    break;
            }
            #endregion

            //信号机编号
            result[7] = MultiChar2Int(data[13], data[14]).ToString();
            //公理标
            string trend = "趋势增";
            if (((byte)data[15] & 0x40) == 0)
            {
                trend = "趋势减";
            }
            //公理标的正负
            if (((byte)data[15] & 0x80) == 0)
            {
                result[8] = trend + " " + MultiChar2Int(data[17], data[16],(char)((byte)data[15] & 0x3F));
            }
            else
            {
                result[8] = trend + " " + MultiChar2Int(data[17], data[16],(char)((byte)data[15] & 0x3F)) * (-1);
            }

            #region 监控状态
            string monitorState = String.Empty;
            if (((byte)data[19] & 0x02) == 0x02)
            {
                monitorState += "ATP控制标志 ";
            }
            if (((byte)data[19] & 0x01) == 0x01)
            {
                monitorState += "TVM430控制 ";
            }
            if (((byte)data[18] & 0x80) == 0x80)
            {
                monitorState += "通常工作 ";
            }
            if (((byte)data[18] & 0x40) == 0x40)
            {
                monitorState += "调车控制 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[11]).ToString("x2") + ((byte)data[10]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[18] & 0x20) == 0x20)
            {
                monitorState += "平面调车 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[11]).ToString("x2") + ((byte)data[10]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[18] & 0x10) == 0x10)
            {
                monitorState += "降级工作 ";
            }
            if (((byte)data[18] & 0x08) == 0x08)
            {
                monitorState += "入段 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[11]).ToString("x2") + ((byte)data[10]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[18] & 0x04) == 0x04)
            {
                monitorState += "非本无控制 ";
            }
            if (((byte)data[18] & 0x02) == 0x02)
            {
                monitorState += "运行方向向后 ";
            }
            if (((byte)data[18] & 0x01) == 0x01)
            {
                monitorState += "出段/调监状态 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[11]).ToString("x2") + ((byte)data[10]).ToString("x2"), 16).ToString();
            }
            result[9] = monitorState.Trim();
            #endregion

            #region 制动状态
            result[10] = String.Empty;
            if (((byte)data[20] & 0x40) == 0x40)
            {
                result[10] += "紧急制动 ";
            }
            if (((byte)data[20] & 0x04) == 0x04)
            {
                result[10] += "常用关风 ";
            }
            if (((byte)data[20] & 0x02) == 0x02)
            {
                result[10] += "常用减压 ";
            }
            if (((byte)data[20] & 0x01) == 0x01)
            {
                result[10] += "卸载制动 ";
            }
            result[10] = result[10].Trim();
            #endregion

            #region 机车工况
            result[11] = String.Empty;
            if (((byte)data[21] & 0x10) == 0x10)
            {
                result[11] += "制动 ";
            }
            if (((byte)data[21] & 0x08) == 0x08)
            {
                result[11] += "牵引 ";
            }
            if (((byte)data[21] & 0x04) == 0x04)
            {
                result[11] += "向后 ";
            }
            if (((byte)data[21] & 0x02) == 0x02)
            {
                result[11] += "向前 ";
            }
            if (((byte)data[21] & 0x01) == 0x01)
            {
                result[11] += "零位 ";
            }
            result[11] = result[11].Trim();
            #endregion

            #region 机车信号机显示
            result[12] = String.Empty;
            if (((byte)data[23] & 0x20) == 0x20)
            {
                result[12] += "机车信号闪灯 ";
            }
            if (((byte)data[23] & 0x10) == 0x10)
            {
                result[12] += "绝缘节 ";
            }
            if (((byte)data[23] & 0x08) == 0x08)
            {
                result[12] += "制式 ";
            }
            if (((byte)data[23] & 0x04) == 0x04)
            {
                result[12] += "速度等级3 ";
            }
            if (((byte)data[23] & 0x02) == 0x02)
            {
                result[12] += "速度等级2 ";
            }
            if (((byte)data[23] & 0x01) == 0x01)
            {
                result[12] += "速度等级1 ";
            }
            if (((byte)data[22] & 0x80) == 0x80)
            {
                result[12] += "白 ";
            }
            if (((byte)data[22] & 0x40) == 0x40)
            {
                result[12] += "红 ";
            }
            if (((byte)data[22] & 0x20) == 0x20)
            {
                result[12] += "红黄 ";
            }
            if (((byte)data[22] & 0x10) == 0x10)
            {
                result[12] += "双黄 ";
            }
            if (((byte)data[22] & 0x08) == 0x08)
            {
                result[12] += "黄2 ";
            }
            if (((byte)data[22] & 0x04) == 0x04)
            {
                result[12] += "黄 ";
            }
            if (((byte)data[22] & 0x02) == 0x02)
            {
                result[12] += "绿黄 ";
            }
            if (((byte)data[22] & 0x01) == 0x01)
            {
                result[12] += "绿 ";
            }
            result[12] = result[12].Trim();
            #endregion

            //列车管压
            result[13] = MultiChar2Int(data[24], data[25]).ToString();
            //砸缸压力
            result[14] = MultiChar2Int(data[26], data[27]).ToString();
            //均缸1压力
            result[15] = MultiChar2Int(data[28], data[29]).ToString();
            //均缸2压力
            result[16] = MultiChar2Int(data[30], data[31]).ToString();
            //柴油机转速
            result[17] = MultiChar2Int(data[32], data[33]).ToString();

            #region 本补客货
            result[18] = string.Empty;
            if (((byte)data[34] & 0x40) == 1)
            {
                result[18] += "上下行反 ";

            }
            result[18] += "双机 ";
            if (((byte)data[34] & 0x20) == 1)
            {
                result[18] += "单机 ";

            }
            //通过三位二进制判断“000 ---- 111”
            string value = String.Empty;
            if (((byte)data[34] & 0x10) == 0x10)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            if (((byte)data[34] & 0x08) == 0x08)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            if (((byte)data[34] & 0x04) == 0x04)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            switch (Convert.ToByte(value, 2))
            {
                case 0:
                    result[18] += "货1 ";
                    break;
                case 1:
                    result[18] += "货2 ";
                    break;
                case 2:
                    result[18] += "货3 ";
                    break;
                case 3:
                    result[18] += "货4 ";
                    break;
                case 4:
                    result[18] += "客1 ";
                    break;
                case 5:
                    result[18] += "客2 ";
                    break;
                case 6:
                    result[18] += "客3 ";
                    break;
                case 7:
                    result[18] += "客4 ";
                    break;
                default:
                    break;
            }
            if (((byte)data[34] & 0x02) == 0x02)
            {
                result[18] += "非本务 ";
            }
            else
            {
                result[18] += "本务 ";
            }
            if (((byte)data[34] & 0x01) == 0x01)
            {
                result[18] += "客车 ";
            }
            else
            {
                result[18] += "货车 ";
            }
            result[18] = result[18].Trim();
            #endregion

            //机车型号
            result[19] = MultiChar2Int(data[35], data[36]).ToString();
            //机车号
            result[20] = MultiChar2Int(data[37], data[38]).ToString();
            //司机号
            result[21] = MultiChar2Int(data[39], data[40], data[41]).ToString();
            //副司机代码
            result[22] = MultiChar2Int(data[42], data[43], data[44]).ToString();
            //车种标识
            result[23] = data[45].ToString() + data[46].ToString() + data[47].ToString() + data[48].ToString();
            result[23] = result[23].Trim();
            //车次号码
            result[24] = MultiChar2Int(data[49], data[50], data[51]).ToString();
            //交路号
            result[25] = ((byte)data[52] & 0x1F).ToString();
            //车站号
            result[26] = MultiChar2Int(data[53], data[54]).ToString();
            //总重
            result[27] = MultiChar2Int(data[55], data[56]).ToString();
            //辆数
            result[28] = MultiChar2Int(data[57], data[58]).ToString();
            //计长
            result[29] = (MultiChar2Int(data[59], data[60]) * 0.1).ToString("0.0");
            //载重
            result[30] = MultiChar2Int(data[61], data[62]).ToString();
            //客车
            result[31] = ((byte)data[63]).ToString();
            //重车
            result[32] = ((byte)data[64]).ToString();
            //空车
            result[33] = ((byte)data[65]).ToString();
            //非运用车
            result[34] = ((byte)data[66]).ToString();
            //代客车
            result[35] = ((byte)data[67]).ToString();
            //守车
            result[36] = ((byte)data[68]).ToString();

            return result;

        }

        /// <summary>
        /// 取出出站时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ParseNoticeTime(char[] data)
        {
            string timeString = string.Empty;
            try
            {
                string realString = new string(data);
                realString = realString.Substring(18, 17);

                string[] lines = realString.Split(' ');

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        long year = Convert.ToInt64(lines[i], 16) + 2000;
                        timeString += year.ToString() + "-";
                    }

                    if (i == 1)
                    {
                        long month = Convert.ToInt64(lines[i], 16);
                        if (month < 10)
                        {
                            timeString += "0" + month.ToString();
                        }
                        else
                        {
                            timeString += month;
                        }
                        timeString += "-";
                    }

                    if (i == 2)
                    {
                        long day = Convert.ToInt64(lines[i], 16);
                        if (day < 10)
                        {
                            timeString += "0" + day;
                        }
                        else
                        {
                            timeString += day;
                        }
                        timeString += " ";
                    }

                    if (i == 3)
                    {
                        long hour = Convert.ToInt64(lines[i], 16);
                        if (hour < 10)
                        {
                            timeString += "0" + hour;
                        }
                        else
                        {
                            timeString += hour;
                        }
                        timeString += ":";
                    }

                    if (i == 4)
                    {
                        long mm = Convert.ToInt64(lines[i], 16);
                        if (mm < 10)
                        {
                            timeString += "0" + mm;
                        }
                        else
                        {
                            timeString += mm;
                        }
                        timeString += ":";
                    }

                    if (i == 5)
                    {
                        long ss = Convert.ToInt64(lines[i], 16);
                        if (ss < 10)
                        {
                            timeString += "0" + ss;
                        }
                        else
                        {
                            timeString += ss;
                        }
                    }

                }

                DateTime dt = DateTime.Parse(timeString);
                //DateTime dt = DateTime.ParseExact(timeString, "yyyy-MM-dd hh:mi:ss", null);
                //Convert.ToDateTime(timeString,"yyyymmddhhmiss",, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                timeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return timeString;
        }


        /// <summary>
        /// 解析公告或文件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] ParserNoticeAndFile(char[] data)
        {
            string[] result = new string[37];

            //文件记录总数，0x00：代表广播信息
            result[0] = ((byte)data[0]).ToString();
            //记录号，0x00：代表广播信息
            result[1] = ((byte)data[1]).ToString();

            //时间：2013-7-22 16:13:9
            string time = ParseNoticeTime(data);
            //string time = ((byte)data[2] + 2000) + "-" + (byte)data[3] + "-" + (byte)data[4] + " ";
            //time += (byte)data[5] + ":" + (byte)data[6] + ":" + (byte)data[7];
            result[2] = time;

            //速度
            result[3] = MultiChar2Int(data[8], data[9]).ToString();
            //限速
            result[4] = MultiChar2Int(data[10], data[11]).ToString();
            //距离  平调/调车/出入段工作状态下为有符号数，其它为无符号
            result[5] = MultiChar2Int(data[12], data[13]).ToString();

            #region 信号机种类
            switch (((byte)data[14] & 0xE0) >> 4)
            {
                case 0:
                    result[6] = "备用";
                    break;
                case 1:
                    result[6] = "进出站";
                    break;
                case 2:
                    result[6] = "出站";
                    break;
                case 3:
                    result[6] = "进站";
                    break;
                case 4:
                    result[6] = "通过";
                    break;
                case 5:
                    result[6] = "预告";
                    break;
                case 6:
                    result[6] = "容许";
                    break;
                case 7:
                    result[6] = "分割";
                    break;
                case 8:
                    result[6] = "未定义";
                    break;
                case 9:
                    result[6] = "1预告";
                    break;
                case 10:
                    result[6] = "2预告";
                    break;
                default:
                    result[6] = String.Empty;
                    break;
            }
            #endregion

            //信号机编号
            result[7] = MultiChar2Int(data[15], data[16]).ToString();
            //公理标
            string trend = "趋势增";
            if (((byte)data[17] & 0x40) == 0)
            {
                trend = "趋势减";
            }
            //公理标的正负
            if (((byte)data[17] & 0x80) == 0)
            {
                result[8] = trend + " " + MultiChar2Int(data[19], data[18], (char)((byte)data[17] & 0x3F));
            }
            else
            {
                result[8] = trend + " " + MultiChar2Int(data[19], data[18], (char)((byte)data[17] & 0x3F)) * (-1);
            }

            #region 监控状态
            string monitorState = String.Empty;
            if (((byte)data[21] & 0x02) == 0x02)
            {
                monitorState += "ATP控制标志 ";
            }
            if (((byte)data[21] & 0x01) == 0x01)
            {
                monitorState += "TVM430控制 ";
            }
            if (((byte)data[20] & 0x80) == 0x80)
            {
                monitorState += "通常工作 ";
            }
            if (((byte)data[20] & 0x40) == 0x40)
            {
                monitorState += "调车控制 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[13]).ToString("x2") + ((byte)data[12]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[20] & 0x20) == 0x20)
            {
                monitorState += "平面调车 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[13]).ToString("x2") + ((byte)data[12]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[20] & 0x10) == 0x10)
            {
                monitorState += "降级工作 ";
            }
            if (((byte)data[20] & 0x08) == 0x08)
            {
                monitorState += "入段 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[13]).ToString("x2") + ((byte)data[12]).ToString("x2"), 16).ToString();
            }
            if (((byte)data[20] & 0x04) == 0x04)
            {
                monitorState += "非本无控制 ";
            }
            if (((byte)data[20] & 0x02) == 0x02)
            {
                monitorState += "运行方向向后 ";
            }
            if (((byte)data[20] & 0x01) == 0x01)
            {
                monitorState += "出段/调监状态 ";
                //距离转为有符号数
                result[5] = Convert.ToInt16(((byte)data[13]).ToString("x2") + ((byte)data[12]).ToString("x2"), 16).ToString();
            }
            result[9] = monitorState.Trim();
            #endregion

            #region 制动状态
            result[10] = String.Empty;
            if (((byte)data[22] & 0x40) == 0x40)
            {
                result[10] += "紧急制动 ";
            }
            if (((byte)data[22] & 0x04) == 0x04)
            {
                result[10] += "常用关风 ";
            }
            if (((byte)data[22] & 0x02) == 0x02)
            {
                result[10] += "常用减压 ";
            }
            if (((byte)data[22] & 0x01) == 0x01)
            {
                result[10] += "卸载制动 ";
            }
            result[10] = result[10].Trim();
            #endregion

            #region 机车工况
            result[11] = String.Empty;
            if (((byte)data[23] & 0x10) == 0x10)
            {
                result[11] += "制动 ";
            }
            if (((byte)data[23] & 0x08) == 0x08)
            {
                result[11] += "牵引 ";
            }
            if (((byte)data[23] & 0x04) == 0x04)
            {
                result[11] += "向后 ";
            }
            if (((byte)data[23] & 0x02) == 0x02)
            {
                result[11] += "向前 ";
            }
            if (((byte)data[23] & 0x01) == 0x01)
            {
                result[11] += "零位 ";
            }
            result[11] = result[11].Trim();
            #endregion

            #region 机车信号机显示
            result[12] = String.Empty;
            if (((byte)data[25] & 0x20) == 0x20)
            {
                result[12] += "机车信号闪灯 ";
            }
            if (((byte)data[25] & 0x10) == 0x10)
            {
                result[12] += "绝缘节 ";
            }
            if (((byte)data[25] & 0x08) == 0x08)
            {
                result[12] += "制式 ";
            }
            if (((byte)data[25] & 0x04) == 0x04)
            {
                result[12] += "速度等级3 ";
            }
            if (((byte)data[25] & 0x02) == 0x02)
            {
                result[12] += "速度等级2 ";
            }
            if (((byte)data[25] & 0x01) == 0x01)
            {
                result[12] += "速度等级1 ";
            }
            if (((byte)data[24] & 0x80) == 0x80)
            {
                result[12] += "白 ";
            }
            if (((byte)data[24] & 0x40) == 0x40)
            {
                result[12] += "红 ";
            }
            if (((byte)data[24] & 0x20) == 0x20)
            {
                result[12] += "红黄 ";
            }
            if (((byte)data[24] & 0x10) == 0x10)
            {
                result[12] += "双黄 ";
            }
            if (((byte)data[24] & 0x08) == 0x08)
            {
                result[12] += "黄2 ";
            }
            if (((byte)data[24] & 0x04) == 0x04)
            {
                result[12] += "黄 ";
            }
            if (((byte)data[24] & 0x02) == 0x02)
            {
                result[12] += "绿黄 ";
            }
            if (((byte)data[24] & 0x01) == 0x01)
            {
                result[12] += "绿 ";
            }
            result[12] = result[12].Trim();
            #endregion

            //列车管压
            result[13] = MultiChar2Int(data[26], data[27]).ToString();
            //砸缸压力
            result[14] = MultiChar2Int(data[28], data[29]).ToString();
            //均缸1压力
            result[15] = MultiChar2Int(data[30], data[31]).ToString();
            //均缸2压力
            result[16] = MultiChar2Int(data[32], data[33]).ToString();
            //柴油机转速
            result[17] = MultiChar2Int(data[34], data[35]).ToString();

            #region 本补客货
            result[18] = string.Empty;
            if (((byte)data[36] & 0x40) == 1)
            {
                result[18] += "上下行反 ";

            }
            result[18] += "双机 ";
            if (((byte)data[36] & 0x20) == 1)
            {
                result[18] += "单机 ";

            }
            //通过三位二进制判断“000 ---- 111”
            string value = String.Empty;
            if (((byte)data[36] & 0x10) == 0x10)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            if (((byte)data[36] & 0x08) == 0x08)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            if (((byte)data[36] & 0x04) == 0x04)
            {
                value += "1";
            }
            else
            {
                value += "0";
            }
            switch (Convert.ToByte(value, 2))
            {
                case 0:
                    result[18] += "货1 ";
                    break;
                case 1:
                    result[18] += "货2 ";
                    break;
                case 2:
                    result[18] += "货3 ";
                    break;
                case 3:
                    result[18] += "货4 ";
                    break;
                case 4:
                    result[18] += "客1 ";
                    break;
                case 5:
                    result[18] += "客2 ";
                    break;
                case 6:
                    result[18] += "客3 ";
                    break;
                case 7:
                    result[18] += "客4 ";
                    break;
                default:
                    break;
            }
            if (((byte)data[36] & 0x02) == 0x02)
            {
                result[18] += "非本务 ";
            }
            else
            {
                result[18] += "本务 ";
            }
            if (((byte)data[36] & 0x01) == 0x01)
            {
                result[18] += "客车 ";
            }
            else
            {
                result[18] += "货车 ";
            }
            result[18] = result[18].Trim();
            #endregion

            //机车型号
            result[19] = MultiChar2Int(data[37], data[38]).ToString();
            //机车号
            result[20] = MultiChar2Int(data[39], data[40]).ToString();
            //司机号
            result[21] = MultiChar2Int(data[41], data[42], data[43]).ToString();
            //副司机代码
            result[22] = MultiChar2Int(data[44], data[45], data[46]).ToString();
            //车种标识
            result[23] = data[47].ToString() + data[48].ToString() + data[49].ToString() + data[50].ToString();
            result[23] = result[23].Trim();
            //车次号码
            result[24] = MultiChar2Int(data[51], data[52], data[53]).ToString();
            //交路号
            result[25] = ((byte)data[54] & 0x1F).ToString();
            //车站号
            result[26] = MultiChar2Int(data[55], data[56]).ToString();
            //总重
            result[27] = MultiChar2Int(data[57], data[58]).ToString();
            //辆数
            result[28] = MultiChar2Int(data[59], data[60]).ToString();
            //计长
            result[29] = (MultiChar2Int(data[61], data[62]) * 0.1).ToString("0.0");
            //载重
            result[30] = MultiChar2Int(data[63], data[64]).ToString();
            //客车
            result[31] = ((byte)data[65]).ToString();
            //重车
            result[32] = ((byte)data[66]).ToString();
            //空车
            result[33] = ((byte)data[67]).ToString();
            //非运用车
            result[34] = ((byte)data[68]).ToString();
            //代客车
            result[35] = ((byte)data[69]).ToString();
            //守车
            result[36] = ((byte)data[70]).ToString();
            ////车速等级
            //result[37] = MultiChar2Int(data[71], data[72]).ToString();
            return result;

        }
        #endregion

    }
}
