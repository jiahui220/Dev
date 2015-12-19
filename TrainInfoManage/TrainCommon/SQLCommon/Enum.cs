using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    /// <summary>
    /// 数据库所有表名
    /// </summary>
    public enum ETableName
    {
        //报警配置表
        AlarmCfg,
        //报警日志表
        AlarmLog,
        //报警机车运行信息表
        AlarmRunInfo,
        //通知公告表
        Announcement,
        //司机日志表
        DriverLog,
        //补机重联和有动力附挂机车表
        Reconnection,
        //报单头部信息表
        ReportHeader,
        //报单统计表
        ReportOther,
        //列车臀型及编组情况表
        RunAndGroup,
        //乘务员信息表
        Steward,
        //机车领取油脂表
        TrainGetOil,
        //机车领取燃料信息表
        TrainGetFuel,
        //机车出入段时分表
        TrainInOut,
        /// <summary>
        /// 司机字典库
        /// </summary>
        DriverDiction,
        /// <summary>
        /// 实时运行数据
        /// </summary>
        TrainRuning,
        /// <summary>
        /// 站名库
        /// </summary>
        StationDiction,
        /// <summary>
        /// 车型字典库
        /// </summary>
        TrainTypeDiction,
        /// <summary>
        /// 书籍
        /// </summary>
        Book,
        /// <summary>
        /// 章节
        /// </summary>
        Chapter


    }

    /// <summary>
    /// 端口列表
    /// </summary>
    public enum TrainCom
    {
       
        ////语音模块串口配置
        //Voice_Port="Com2",
        //Voice_Baud =9600,
        ////Can模块串口配置
        //Can_Port="Com4",
        //Can_Baud= 115200,
        ////485模块串口配置
        //Can485_Port="Com5",
        //Can485_Baud=28800,
        ////Gps模块串口配置
        //Gps_Port="Com8",
        //Gps_Baud =115200
    }

    /// <summary>
    /// 数据格式验证
    /// </summary>
    public enum EVerification
    {
        /// <summary>
        /// 不需要验证
        /// </summary>
        No,
        /// <summary>
        /// 非空验证
        /// </summary>
        IsNotNull,
        /// <summary>
        /// 身份证长度验证
        /// </summary>
        IDcardLength,
        /// <summary>
        /// 整数验证
        /// </summary>
        Integer,
        /// <summary>
        /// 小数验证
        /// </summary>
        Decimal,
        /// <summary>
        /// 安全字符串验证、常用于账号和密码
        /// </summary>
        SafeString,
        /// <summary>
        /// Email格式验证
        /// </summary>
        EMail,
        /// <summary>
        /// 双字节字符验证（包含汉字）
        /// </summary>
        Chinese,
        /// <summary>
        /// 禁止注入字符，如：'、"、*、%、select、update、delete、insert
        /// </summary>
        DisBadChar
    }

}
