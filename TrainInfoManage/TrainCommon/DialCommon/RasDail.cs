using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TrainCommon
{
    public class RasDail
    {
        #region 系统参数
        const int MAX_PATH = 260;
        const int RAS_MaxEntryName = 20;
        const int RAS_MaxPhoneNumber = 128;
        const int RAS_MaxCallbackNumber = 48;
        const int UNLEN = 256;
        const int PWLEN = 256;
        const int DNLEN = 15;
        const int RAS_MaxDeviceType = 16;
        const int RAS_MaxDeviceName = 128;
        const int RASCS_Connected = 0x2000;

        //拨号
        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasDial(
            IntPtr dialExtensions,
            IntPtr phoneBookPath,
            [In]RASDIALPARAMS[] rasDialParam,
            uint NotifierType,
            IntPtr notifier,
            ref IntPtr pRasConn);

        //拨号参数
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RASDIALPARAMS
        {
            public int dwSize;// = Marshal.SizeOf(typeof(RASDIALPARAMS));

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string szEntryName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPhoneNumber + 1)]
            public string szPhoneNumber;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxCallbackNumber + 1)]
            public string szCallbackNumber;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = UNLEN + 1)]
            public string szUserName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PWLEN + 1)]
            public string szPassword;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DNLEN + 1)]
            public string szDomain;
            //public int dwSubEntry = 0;
            //public int dwCallbackId = 0;
            //#if   (WINVER   >=   0x401)  
            //public int dwSubEntry;
            //public uint dwCallbackId;  
        }

        //断开
        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public extern static uint RasHangUp(IntPtr hrasconn);

        //列出现有连接
        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        internal static extern UInt32 RasEnumConnections(
            [In, Out] RASCONN[] lprasconn, // buffer to receive connections data
            ref UInt32 lpcb, // size in bytes of buffer
            ref UInt32 lpcConnections // number of connections written to buffer
            );

        //现有连接的信息
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct RASCONN
        {
            private UInt32 m_size;
            private IntPtr m_hrasconn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = RAS_MaxEntryName + 1)]
            public char[] m_entryName;

            public IntPtr ConnectionHandle
            {
                get { return m_hrasconn; }
            }

            public string EntryName
            {
                get { return new string(m_entryName).TrimEnd(new char[] { '\0' }); }
            }

            public static RASCONN CreateStruct()
            {
                RASCONN obj = new RASCONN();
                obj.m_size = System.Convert.ToUInt32(Marshal.SizeOf(typeof(RASCONN)));
                obj.m_hrasconn = IntPtr.Zero;
                obj.m_entryName = new char[RAS_MaxEntryName + 1];
                return obj;
            }
        }

        //获取连接信息
        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasGetConnectStatus(
                IntPtr hRasConn,
                [In, Out]RASCONNSTATUS[] lprasconnstatus
                );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RASCONNSTATUS
        {
            public int dwSize;
            public int rasconnstate;
            public int dwError;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string szDeviceType;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string szDeviceName;
        }

        //获取连接参数
        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasGetEntryDialParams(
            IntPtr lpszPhoneBook,
            [In, Out]RASDIALPARAMS[] lpRasDialParams,
            IntPtr lpfPassword
            );

        //获取连接的名字
        [DllImport("Coredll.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RasEnumEntries(IntPtr reserved, IntPtr lpszPhonebook,
            [In, Out] RASENTRYNAME[] lprasentryname, ref int lpcb, ref int lpcEntries);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RASENTRYNAME
        {
            public int dwSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string szEntryName;
            //public int dwFlags;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH + 1)]
            //public string szPhonebook;
        }

        #endregion

        #region 拨号
        /// <summary>
        /// 根据类型进行拨号
        /// </summary>
        /// <param name="type">网络类型1-GPRS,2-CMDA</param>
        /// <returns></returns>
        public static bool RasDialRun(int type)
        {
            IntPtr RasConn = IntPtr.Zero;
            RASDIALPARAMS[] entryNames = new RASDIALPARAMS[1];

            if (type == 1)
            {
                //GPRS拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = "GPRS";           //设备网络连接名称     
                entryNames[0].szPhoneNumber = "*99***1#";       //所拨的号码
                entryNames[0].szDomain = "";                    //所属域
                entryNames[0].szUserName = "";     //用户名
                entryNames[0].szPassword = "";     //用户口令
            }
            else if (type == 2)
            {
                //CDMA拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = "cdma";   //设备网络连接名称     
                entryNames[0].szPhoneNumber = "#777"; //所拨的号码
                entryNames[0].szDomain = "";          //所属域
                entryNames[0].szUserName = "card";    //用户名
                entryNames[0].szPassword = "card";    //用户口令
            }
            uint nRet = RasDial(IntPtr.Zero, IntPtr.Zero, entryNames, 0, IntPtr.Zero, ref RasConn);
            if (nRet != 0) return false;
            //dialhandle = rescon[0].hrasconn;
            return true;
        }
        #endregion

        #region 检测连接状态
        /// <summary>
        /// 检测连接状态
        /// </summary>
        /// <param name="mEntryName"></param>
        /// <returns></returns>
        public static bool RasDialStatus(string mEntryName)
        {
            RASCONNSTATUS[] constatus = new RASCONNSTATUS[1];
            RASCONN[] ran = EnumerateConnections();
            bool result = false;
            for (int i = 0; i < ran.Length; i++)
            {
                if (ran[i].EntryName == mEntryName)
                {
                    if (RasGetConnectStatus(ran[i].ConnectionHandle, constatus) == 0)
                    {
                        if (constatus[0].rasconnstate == RASCS_Connected)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }

                }

            }
            return result;
        }
        #endregion

        #region 列举所有活动连接
        /// <summary>
        /// 列举所有活动连接
        /// </summary>
        /// <returns></returns>
        private static RASCONN[] EnumerateConnections()
        {
            RASCONN[] rasconn;
            UInt32 size = 0, noelements = 0;
            size = System.Convert.ToUInt32(Marshal.SizeOf((rasconn = new RASCONN[1])[0] = RASCONN.CreateStruct()));
            if (RasEnumConnections(rasconn, ref size, ref noelements) != 0x00)
            {
                // size = System.Convert.ToUInt32(Marshal.SizeOf((rasconn = new RASCONN[size / Marshal.SizeOf(typeof(RASCONN))])[0] = RASCONN.CreateStruct()) * rasconn.Length);


                rasconn = new RASCONN[size / Marshal.SizeOf(typeof(RASCONN))];
                rasconn[0] = RASCONN.CreateStruct();
                size = System.Convert.ToUInt32(Marshal.SizeOf(rasconn[0]) * rasconn.Length);


                if (RasEnumConnections(rasconn, ref size, ref noelements) != 0x00) { rasconn = null; }
            }
            return rasconn;
        }
        #endregion 

        #region 断开已建立的连接
        /// <summary>
        /// 断开已建立的连接
        /// </summary>
        /// <param name="mEntryName">已建立的活动连接的名称</param>
        /// <returns></returns>
        public static bool RasDialHangUp(string mEntryName)
        {
            RASCONN[] ran = EnumerateConnections();
            for (int i = 0; i < ran.Length; i++)
            {
                if (ran[i].EntryName == mEntryName)
                {
                    if (RasHangUp(ran[i].ConnectionHandle) == 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;   //表示没有此连接
        }
        #endregion

        #region 断开所有活动的连接
        /// <summary>
        /// 断开所有活动的连接
        /// </summary>
        public static void DisConnectAll()
        {
            RASCONN[] ran = EnumerateConnections();
            for (int i = 0; i < ran.Length; i++)
            {
                RasHangUp(ran[i].ConnectionHandle);
            }
        }
        #endregion
    }
}
