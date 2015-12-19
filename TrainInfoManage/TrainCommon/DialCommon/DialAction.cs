using System;

using System.Collections.Generic;
using System.Text;

namespace TrainCommon
{
    public class DialAction
    {
        /// <summary>
        /// 拨号连接CDMA
        /// 设备网络连接名称:"cdma"
        /// 所拨的号码:"#777"
        /// 所属域:""
        /// 用户名:"card"
        /// 用户口令:"card"
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            return RasComm.RasDialRun(2);
        }

        /// <summary>
        /// 拨号连接CDMA
        /// </summary>
        /// <param name="linkName">设备网络连接名称</param>
        /// <param name="phoneNum">所拨的号码</param>
        /// <param name="domain">所属域</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">用户口令</param>
        /// <returns></returns>
        public static bool Connect(string linkName, string phoneName, string domain, string user, string pwd)
        {
            return RasComm.RasDialRun(2,linkName,phoneName,domain,user,pwd);
        }

        /// <summary>
        /// 获取网络连接状态
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            return RasComm.RasDialStatus();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public static bool Close()
        {
            return RasComm.RasDialHangUp();
        }

    }
}
