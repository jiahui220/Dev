using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon.CanInfoCommon
{
    /// <summary>
    /// 解析Can数据包
    /// </summary>
    public class ParseCanData
    {
        /// <summary>
        /// 分析Can协议优先级50数据
        /// </summary>
        /// <param name="charData">Can数据</param>
        /// <param name="cFrame">帧号</param>
        /// <returns></returns>
        public static string[] ParseCan50(char[] charData, int cFrame)
        {
            string[] rData = new string[8];
            switch (cFrame)
            {
                //第0帧
                case 0:
                    //时速
                    rData[0] = (charData[6] & 0x01) * 256 + (byte)charData[5] + "";
                    TrainCanInfo.TrainCan.Speed = rData[0];
                    LocoInfo.TrainInfo.Speed = rData[0];
                    //限速
                    rData[1] = (charData[8] & 0x01) * 256 + (byte)charData[7] + "";
                    TrainCanInfo.TrainCan.LimitSpeed = rData[1];
                    //时
                    rData[2] = (charData[9] & 0x1F) + "";
                    //分
                    rData[3] = (charData[10] & 0x3F) + "";
                    //秒
                    rData[4] = (charData[11] & 0x3F) + "";
                    //百分秒
                    rData[5] = (charData[12] & 0x7F) + "";
                    TrainCanInfo.TrainCan.CanTime = rData[2] + ":" + rData[3] + ":" + rData[4];
                    break;
                //第1帧
                case 1:
                    //机车工况
                    if (((byte)charData[5] & 0x10) == 0x10)
                    {
                        rData[0] += "制动 ";
                    }
                    if (((byte)charData[5] & 0x08) == 0x08)
                    {
                        rData[0] += "牵引 ";
                    }
                    if (((byte)charData[5] & 0x04) == 0x04)
                    {
                        rData[0] += "向后 ";
                    }
                    if (((byte)charData[5] & 0x02) == 0x02)
                    {
                        rData[0] += "向前 ";
                    }
                    if (((byte)charData[5] & 0x01) == 0x01)
                    {
                        rData[0] += "零位 ";
                    }
                    TrainCanInfo.TrainCan.WorkCond = rData[0]; 
                    //工况自检
                    if ((charData[6] & 0x01) == 0x01)
                    {
                        rData[1] = "有效";
                    }
                    else
                    {
                        rData[1] = "故障";
                    }
                    //机车信号
                    if (((byte)charData[8] & 0x10) == 0x10)
                    {
                        rData[2] += "绝缘节 ";
                    }
                    if (((byte)charData[8] & 0x08) == 0x08)
                    {
                        rData[2] += "制式 ";
                    }
                    if (((byte)charData[8] & 0x04) == 0x04)
                    {
                        rData[2] += "速度等级3 ";
                    }
                    if (((byte)charData[8] & 0x02) == 0x02)
                    {
                        rData[2] += "速度等级2 ";
                    }
                    if (((byte)charData[8] & 0x01) == 0x01)
                    {
                        rData[2] += "速度等级1 ";
                    }
                    if (((byte)charData[7] & 0x80) == 0x80)
                    {
                        rData[2] += "白 ";
                    }
                    if (((byte)charData[7] & 0x40) == 0x40)
                    {
                        rData[2] += "红 ";
                    }
                    if (((byte)charData[7] & 0x20) == 0x20)
                    {
                        rData[2] += "红黄 ";
                    }
                    if (((byte)charData[7] & 0x10) == 0x10)
                    {
                        rData[2] += "双黄 ";
                    }
                    if (((byte)charData[7] & 0x08) == 0x08)
                    {
                        rData[2] += "黄2 ";
                    }
                    if (((byte)charData[7] & 0x04) == 0x04)
                    {
                        rData[2] += "黄 ";
                    }
                    if (((byte)charData[7] & 0x02) == 0x02)
                    {
                        rData[2] += "绿黄 ";
                    }
                    if (((byte)charData[7] & 0x01) == 0x01)
                    {
                        rData[2] += "绿 ";
                    }
                    TrainCanInfo.TrainCan.CabSignal = rData[2];
                    //信号自检
                    if (((byte)charData[9] & 0x01) == 0x01)
                    {
                        rData[3] = "有效";
                    }
                    else
                    {
                        rData[3] = "故障";
                    }
                    //制动输出
                    if (((byte)charData[11] & 0x40) == 0x40)
                    {
                        rData[4] += "紧急制动 ";
                    }
                    if (((byte)charData[11] & 0x04) == 0x04)
                    {
                        rData[4] += "常用关风 ";
                    }
                    if (((byte)charData[11] & 0x02) == 0x02)
                    {
                        rData[4] += "常用减压 ";
                    }
                    if (((byte)charData[11] & 0x01) == 0x01)
                    {
                        rData[4] += "卸载制动 ";
                    }
                    TrainCanInfo.TrainCan.BrOutPut = rData[4];
                    //信号自检
                    if (((byte)charData[12] & 0x01) == 0x01)
                    {
                        rData[5] = "有效";
                    }
                    else
                    {
                        rData[5] = "故障";
                    }
                    break;
                //第2帧
                case 2:
                    //公里标
                    if (((byte)charData[7] & 0x80) == 0x80)
                    {
                        rData[0] += "支线过机 ";
                    }
                    else
                    {
                        rData[0] += "普通过机 ";
                    }
                    rData[0] += (((byte)charData[7] & 0x3F) * 256 * 256 + ((byte)charData[6]) * 256 + (byte)charData[5]) + "";
                    TrainCanInfo.TrainCan.KilometerPost =rData[0];
                    LocoInfo.TrainInfo.Kilometer = BaseLibrary.KiloConversion(rData[0]);
                    //距离
                    rData[1] = ((byte)charData[9]) * 256 + ((byte)charData[8]) + "";
                    TrainCanInfo.TrainCan.Distance = rData[1];
                    //信号机编号
                    rData[2] = ((byte)charData[11]) * 256 + ((byte)charData[10]) + "";
                    TrainCanInfo.TrainCan.SignalNum = rData[2];
                    //信号机种类
                    int type = ((byte)charData[12] & 0xF0) >> 4;
                    switch (type)
                    {
                        case 0:
                            rData[3] = "备用";
                            break;
                        case 1:
                            rData[3] = "进出站";
                            break;
                        case 2:
                            rData[3] = "出站";
                            break;
                        case 3:
                            rData[3] = "进站";
                            break;
                        case 4:
                            rData[3] = "通过";
                            break;
                        case 5:
                            rData[3] = "预告";
                            break;
                        case 6:
                            rData[3] = "容许";
                            break;
                        case 7:
                            rData[3] = "分割";
                            break;
                        case 8:
                            rData[3] = "未定义";
                            break;
                        case 9:
                            rData[3] = "1预告";
                            break;
                        case 10:
                            rData[3] = "2预告";
                            break;
                    }
                    TrainCanInfo.TrainCan.SignalType = rData[3];
                    break;
                //第3帧
                case 3:
                    //监控状态
                    if (((byte)charData[12] & 0x80) == 0x80)
                    {
                        rData[0] += "通常工作 ";
                    }
                    if (((byte)charData[12] & 0x40) == 0x40)
                    {
                        rData[0] += "调车控制 ";
                    }
                    if (((byte)charData[12] & 0x20) == 0x20)
                    {
                        rData[0] += "平面调车 ";
                    }
                    if (((byte)charData[12] & 0x10) == 0x10)
                    {
                        rData[0] += "降级工作 ";
                    }
                    if (((byte)charData[12] & 0x08) == 0x08)
                    {
                        rData[0] += "入段 ";
                    }
                    if (((byte)charData[12] & 0x04) == 0x04)
                    {
                        rData[0] += "非本无控制 ";
                    }
                    if (((byte)charData[12] & 0x02) == 0x02)
                    {
                        rData[0] += "运行方向向后 ";
                    }
                    if (((byte)charData[12] & 0x01) == 0x01)
                    {
                        rData[0] += "出段/调监状态 ";
                    }
                    rData[0] = rData[0].Trim();
                    TrainCanInfo.TrainCan.MonitorType = rData[0];
                    break;
                //第4帧
                case 4:
                    //无必要信息，暂不处理
                    break;
                //第5帧
                case 5:
                    //无必要信息，暂不处理
                    break;
                //第6帧
                case 6:

                    break;
            }

            return rData;
        }

        /// <summary>
        /// 分析Can协议优先级58数据
        /// </summary>
        /// <param name="charData">Can数据</param>
        /// <param name="cFrame">帧号</param>
        /// <returns></returns>
        public static string[] ParseCan58(char[] charData, int cFrame)
        {
            string[] rData = new string[8];
            switch (cFrame)
            {
                //第0帧
                case 0:
                    //司机号
                    rData[0] = ((byte)charData[7]) * 256 * 256 + ((byte)charData[6]) * 256 + (byte)charData[5] + "";
                    LocoInfo.TrainInfo.DriverNum = rData[0];
                    LocoInfo.TrainInfo.DriverName = BaseLibrary.GetDriverName(rData[0]);
                    TrainCanInfo.TrainCan.DriverNum = rData[0];
                    //副司机号
                    rData[1] = ((byte)charData[10]) * 256 * 256 + ((byte)charData[9]) * 256 + (byte)charData[8] + "";
                    TrainCanInfo.TrainCan.AssDriverNum = rData[1];
                    //司机所属段号
                    rData[2] = ((byte)charData[12]) * 256 + (byte)charData[11] + "";
                    break;
                //第1帧
                case 1:
                    //输入交路号
                    rData[0] = ((byte)charData[5]).ToString();
                    //车站号
                    rData[1] = ((byte)charData[6]).ToString();
                    TrainCanInfo.TrainCan.StatioNo = rData[1];
                    //总重
                    rData[2] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    TrainCanInfo.TrainCan.AllUp = rData[2];
                    //计长
                    rData[3] = ((((byte)charData[10]) * 256 + ((byte)charData[9]))*0.1).ToString("0.0");
                    TrainCanInfo.TrainCan.Length = rData[3];
                    //辆数
                    rData[4] = ((byte)charData[11]) + "";
                    TrainCanInfo.TrainCan.NumberCar = rData[4];
                    //实际交路号
                    rData[5] = ((byte)charData[12]) + "";
                    TrainCanInfo.TrainCan.CrossRoadNo = rData[5];
                    break;
                //第2帧
                case 2:
                    //载重
                    rData[0] = ((byte)charData[6]) * 256 + ((byte)charData[5]) + "";
                    TrainCanInfo.TrainCan.CarryLoad = rData[0];
                    //客车
                    rData[1] = ((byte)charData[7]).ToString();
                    TrainCanInfo.TrainCan.Carriage = rData[1];
                    //重车
                    rData[2] = ((byte)charData[8]).ToString();
                    TrainCanInfo.TrainCan.HeavyCar = rData[2];
                    //空车
                    rData[3] = ((byte)charData[9]).ToString();
                    TrainCanInfo.TrainCan.EmptyCar = rData[3]; 
                    //非运用车
                    rData[4] = ((byte)charData[10]).ToString();
                    TrainCanInfo.TrainCan.NonServCar = rData[4];
                    //代客车
                    rData[5] = ((byte)charData[11]).ToString();
                    TrainCanInfo.TrainCan.GenPassCar = rData[5];
                    //守车
                    rData[6] = ((byte)charData[12]).ToString();
                    TrainCanInfo.TrainCan.CabooseCar = rData[6];
                    break;
                //第3帧
                case 3:
                    //轮径
                    rData[0] = ((byte)charData[6] & 0x7F) * 256 + ((byte)charData[5]) + "";
                    //机车号
                    rData[1] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    LocoInfo.TrainInfo.TrainNum = rData[1];
                    TrainCanInfo.TrainCan.TrainNum = rData[1];
                    //装置号
                    rData[2] = ((byte)charData[10]) * 256 + ((byte)charData[9]) + "";
                    //机车型号
                    rData[3] = ((byte)charData[12] * 0x0F) * 256 + ((byte)charData[11]) + "";
                    LocoInfo.TrainInfo.TypeNum =rData[3];
                    LocoInfo.TrainInfo.TrainType =BaseLibrary.GetTrainType(rData[3]);
                    TrainCanInfo.TrainCan.TypeNum = rData[3];
                    break;
                //第4帧
                case 4:
                    //最大重量
                    rData[0] = ((byte)charData[6]) * 256 + ((byte)charData[5]) + "";
                    //最大计长
                    rData[1] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    //最大辆数
                    rData[2] = ((byte)charData[9]).ToString();
                    //柴油机脉冲数
                    rData[3] = ((byte)charData[10]).ToString();
                    //双针表量程
                    rData[4] = ((byte)charData[11]).ToString();
                    //制动机型号
                    rData[5] = ((byte)charData[12]).ToString();
                    break;
                //第5帧
                case 5:
                    //年,月,日
                    int year = (2000 + (byte)charData[5]);
                    int month = ((byte)charData[6]);
                    int day = ((byte)charData[7]);
                    rData[0] = year + "-" + month + "-" + day;
                    TrainCanInfo.TrainCan.CanDate = rData[0];
                    break;
                //第6帧
                case 6:
                    //车次
                    rData[0] = MultiChar2Int(charData[5], charData[6], charData[7]).ToString();
                    LocoInfo.TrainInfo.VehicleTrip = rData[0];
                    TrainCanInfo.TrainCan.VehicleTrips = rData[0];
                    //车种标示
                    rData[1] = charData[11].ToString() + charData[10].ToString() + charData[9].ToString() + charData[8].ToString();
                    //本补客货
                    if (((byte)charData[12] & 0x40) == 1)
                    {
                        rData[2] += "上下行反 ";

                    }
                    if (((byte)charData[12] & 0x20) == 1)
                    {
                        rData[2] += "单机 ";

                    }
                    else
                    {
                        rData[2] += "双机 ";
                    }
                    //通过三位二进制判断“000 ---- 111”
                    string value = String.Empty;
                    if (((byte)charData[12] & 0x10) == 0x10)
                    {
                        value += "1";
                    }
                    else
                    {
                        value += "0";
                    }
                    if (((byte)charData[12] & 0x08) == 0x08)
                    {
                        value += "1";
                    }
                    else
                    {
                        value += "0";
                    }
                    if (((byte)charData[12] & 0x04) == 0x04)
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
                            rData[2] += "货1 ";
                            break;
                        case 1:
                            rData[2] += "货2 ";
                            break;
                        case 2:
                            rData[2] += "货3 ";
                            break;
                        case 3:
                            rData[2] += "货4 ";
                            break;
                        case 4:
                            rData[2] += "客1 ";
                            break;
                        case 5:
                            rData[2] += "客2 ";
                            break;
                        case 6:
                            rData[2] += "客3 ";
                            break;
                        case 7:
                            rData[2] += "客4 ";
                            break;
                        default:
                            break;
                    }
                    if (((byte)charData[12] & 0x02) == 0x02)
                    {
                        rData[2] += "非本务 ";
                    }
                    else
                    {
                        rData[2] += "本务 ";
                    }
                    if (((byte)charData[12] & 0x01) == 0x01)
                    {
                        rData[2] += "客车 ";
                    }
                    else
                    {
                        rData[2] += "货车 ";
                    }
                    rData[2] = rData[2].Trim();
                    TrainCanInfo.TrainCan.FillPassCargo = rData[2];
                    break;
                //第7帧
                case 7:
                    break;
            }
            return rData;
        }

        /// <summary>
        /// 分析Can协议优先级59数据
        /// </summary>
        /// <param name="charData">Can数据</param>
        /// <param name="cFrame">帧号</param>
        /// <returns></returns>
        public static string[] ParseCan59(char[] charData, int cFrame)
        {
            string[] rData = new string[8];
            switch (cFrame)
            {
                //第0帧
                case 0:
                    //管压0
                    rData[0] = ((byte)charData[6]) * 256 + ((byte)charData[5]) + "";
                    TrainCanInfo.TrainCan.TubeGageZero = rData[0];
                    //管压1
                    rData[1] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    TrainCanInfo.TrainCan.TubeGageOne = rData[1];
                    //管压2
                    rData[2] = ((byte)charData[10]) * 256 + ((byte)charData[9]) + "";
                    TrainCanInfo.TrainCan.TubeGageTwo = rData[2];
                    //备用压力
                    rData[3] = ((byte)charData[12]) * 256 + ((byte)charData[11]) + "";
                    TrainCanInfo.TrainCan.TubeGageThree = rData[3];
                    break;
                //第1帧
                case 1:
                    //原边电压
                    rData[0] = ((byte)charData[6]) * 256 + ((byte)charData[5]) + "";
                    //原边电流
                    rData[1] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    //加速度
                    rData[2] = ((byte)charData[10]) * 256 + ((byte)charData[9]) + "";
                    //柴油机转速
                    rData[3] = ((byte)charData[12]) * 256 + ((byte)charData[11]) + "";
                    TrainCanInfo.TrainCan.DieselSpeed = rData[3];
                    break;
                //第2帧
                case 2:
                    //速度0
                    rData[0] = ((byte)charData[6]) * 256 + ((byte)charData[5]) + "";
                    //速度1
                    rData[1] = ((byte)charData[8]) * 256 + ((byte)charData[7]) + "";
                    //速度2
                    rData[2] = ((byte)charData[10]) * 256 + ((byte)charData[9]) + "";
                    //原边功率
                    rData[3] = ((byte)charData[12]) * 256 + ((byte)charData[11]) + "";
                    break;
                //第3帧
                case 3:
                    break;
                //第4帧
                case 4:
                    break;
                //第5帧
                case 5:
                    break;
                //第6帧
                case 6:
                    break;
                //第7帧
                case 7:
                    break;
            }
            return new string[1];
        }

        // 取得指定位
        public static int getBit2Char(char c, int hBit, int lBit)
        {
            int leftSpace = 7 - hBit;
            int cTmp = c << leftSpace;
            return cTmp >> (leftSpace + lBit);
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

    }
}
