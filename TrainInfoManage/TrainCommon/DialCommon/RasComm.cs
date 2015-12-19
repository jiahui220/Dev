using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TrainCommon
{
    /**
     * 自动拨号类
     */
    public class RasComm
    {
        public struct RASCONN
        {
            public int dwSize;
            public IntPtr hrasconn;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
            public string szEntryName;

            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            //public string szDeviceType;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
            //public string szDeviceName;
        }



        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RASEAPINFO
        {
            public uint dwSizeofEapInfo;
            //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=(int)RasFieldSizeConstants.RAS_MaxEntryName + 1)]
            public byte[] pbEapInfo;
        }

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

        public static IntPtr[] dialhandle = new IntPtr[1];

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


        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern int RasEnumConnections
            (
            ref RASCONN lprasconn, // buffer to receive connections data
            ref int lpcb, // size in bytes of buffer
            ref int lpcConnections // number of connections written to buffer
            );



        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasGetConnectStatus(
                IntPtr hRasConn,
                [In, Out]RASCONNSTATUS[] lprasconnstatus
                );


        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public extern static uint RasHangUp(
                IntPtr hrasconn
                );


        [DllImport("Coredll.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RasEnumEntries(IntPtr reserved, IntPtr lpszPhonebook,
            [In, Out] RASENTRYNAME[] lprasentryname, ref int lpcb, ref int lpcEntries);


        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasGetEntryDialParams(
            IntPtr lpszPhoneBook,
            [In, Out]RASDIALPARAMS[] lpRasDialParams,
            IntPtr lpfPassword
            );

        [DllImport("Coredll.dll", CharSet = CharSet.Auto)]
        public static extern uint RasDial(
            IntPtr dialExtensions,
            IntPtr phoneBookPath,
            [In]RASDIALPARAMS[] rasDialParam,
            uint NotifierType,
            IntPtr notifier,
            IntPtr[] pRasConn);
        //[In, Out] RASCONN[] pRasConn);




        /// <summary>
        /// GPRS dial
        /// </summary>
        /// <param name="dialhandle"></param>
        /// <returns></returns>
        public static bool RasDialRun()
        {
            RASDIALPARAMS[] entryNames = new RASDIALPARAMS[1];


            //GPRS拨号
            entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
            entryNames[0].szEntryName = "MyLink";           //设备网络连接名称      
            entryNames[0].szPhoneNumber = "*99***1#";       //所拨的号码
            entryNames[0].szDomain = "";                    //所属域
            entryNames[0].szUserName = "";					//用户名
            entryNames[0].szPassword = "";					//用户口令

            //RASCONN[] rescon= new RASCONN[1];

            //CDMA拨号
            /*			entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                        entryNames[0].szEntryName = "MyLink";           //设备网络连接名称      
                        entryNames[0].szPhoneNumber = "#777";			//所拨的号码
                        entryNames[0].szDomain = "";                    //所属域
                        entryNames[0].szUserName = "card";				//用户名
                        entryNames[0].szPassword = "card";		
            */

            uint nRet = RasDial(IntPtr.Zero, IntPtr.Zero, entryNames, 0, IntPtr.Zero, RasComm.dialhandle);
            if (nRet != 0) return false;
            //dialhandle = rescon[0].hrasconn;
            return true;
        }

        /// <summary>
        /// 根据类型进行拨号
        /// </summary>
        /// <param name="type">网络类型</param>
        /// <returns></returns>
        public static bool RasDialRun(int type)
        {
            RASDIALPARAMS[] entryNames = new RASDIALPARAMS[1];

            if (type == 1)
            {
                //GPRS拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = "MyLink";           //设备网络连接名称      
                entryNames[0].szPhoneNumber = "*99***1#";       //所拨的号码
                entryNames[0].szDomain = "";                    //所属域
                entryNames[0].szUserName = "";					//用户名
                entryNames[0].szPassword = "";					//用户口令
            }
            else if (type == 2)
            {
                //CDMA拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = "cdma";           //设备网络连接名称      
                entryNames[0].szPhoneNumber = "#777";			//所拨的号码
                entryNames[0].szDomain = "";                    //所属域
                entryNames[0].szUserName = "card";				//用户名
                entryNames[0].szPassword = "card";				//用户口令
            }
            uint nRet = RasDial(IntPtr.Zero, IntPtr.Zero, entryNames, 0, IntPtr.Zero, RasComm.dialhandle);
            if (nRet != 0) return false;
            //dialhandle = rescon[0].hrasconn;
            return true;
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        /// <param name="type"></param>
        /// <param name="linkName">设备网络连接名称</param>
        /// <param name="phoneNum">所拨的号码</param>
        /// <param name="domain">所属域</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">用户口令</param>
        /// <returns></returns>
        public static bool RasDialRun(int type,string linkName,string phoneNum,string domain,string user,string pwd)
        {
            RASDIALPARAMS[] entryNames = new RASDIALPARAMS[1];

            if (type == 1)
            {
                //GPRS拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = linkName;           //设备网络连接名称      
                entryNames[0].szPhoneNumber = phoneNum;       //所拨的号码
                entryNames[0].szDomain = domain;                    //所属域
                entryNames[0].szUserName = user;					//用户名
                entryNames[0].szPassword = pwd;					//用户口令
            }
            else if (type == 2)
            {
                //CDMA拨号
                entryNames[0].dwSize = Marshal.SizeOf(typeof(RASDIALPARAMS));
                entryNames[0].szEntryName = linkName;           //设备网络连接名称      
                entryNames[0].szPhoneNumber = phoneNum;			//所拨的号码
                entryNames[0].szDomain = domain;                    //所属域
                entryNames[0].szUserName = user;				//用户名
                entryNames[0].szPassword = pwd;				//用户口令
            }
            uint nRet = RasDial(IntPtr.Zero, IntPtr.Zero, entryNames, 0, IntPtr.Zero, RasComm.dialhandle);
            if (nRet != 0) return false;
            //dialhandle = rescon[0].hrasconn;
            return true;
        }

        /// <summary>
        /// terminated ras connnection by connection handle
        /// </summary>
        /// <param name="rescon"></param>
        /// <returns></returns>
        public static bool RasDialHangUp()
        {
            uint nRet = RasHangUp(RasComm.dialhandle[0]);
            if (nRet != 0) return false;
            return true;
        }

        /// <summary>
        /// check connction status
        /// </summary>
        /// <param name="rescon"></param>
        /// <returns></returns>
        public static bool RasDialStatus(IntPtr rescon)
        {

            RASCONNSTATUS[] constatus = new RASCONNSTATUS[1];
            if (RasGetConnectStatus(rescon, constatus) == 0)
            {
                if (constatus[0].rasconnstate == RASCS_Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// check connction status
        /// </summary>
        /// <param name="rescon"></param>
        /// <returns></returns>
        public static bool RasDialStatus()
        {

            RASCONNSTATUS[] constatus = new RASCONNSTATUS[1];
            if (RasGetConnectStatus(RasComm.dialhandle[0], constatus) == 0)
            {
                if (constatus[0].rasconnstate == RASCS_Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get ras entrynames
        /// </summary>
        /// <returns></returns>
        private static string[] EntryNames()
        {
            int cb = Marshal.SizeOf(typeof(RasComm.RASENTRYNAME)), entries = 0;
            RasComm.RASENTRYNAME[] entryNames = new RasComm.RASENTRYNAME[1];
            entryNames[0].dwSize = Marshal.SizeOf(typeof(RasComm.RASENTRYNAME));
            //Get entry number
            uint nRet = RasComm.RasEnumEntries(IntPtr.Zero, IntPtr.Zero, entryNames, ref cb, ref entries);
            if (entries == 0) return null;
            string[] _EntryNames = new string[entries];
            entryNames = new RasComm.RASENTRYNAME[entries];
            for (int i = 0; i < entries; i++)
            {
                entryNames[i].dwSize = Marshal.SizeOf(typeof(RasComm.RASENTRYNAME));
            }

            nRet = RasComm.RasEnumEntries(IntPtr.Zero, IntPtr.Zero, entryNames, ref cb, ref entries);

            for (int i = 0; i < entries; i++)
            {
                _EntryNames[i] = entryNames[i].szEntryName;
            }
            return _EntryNames;
        }

    }
}
