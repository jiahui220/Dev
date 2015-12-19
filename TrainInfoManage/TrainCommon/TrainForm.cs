using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;

namespace TrainCommon
{
    /**
     * author:luojia
     */
    public class TrainForm
    {
        //程序的基础路径/bin/debug
        public static string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        private static Process proc = null;

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;

        [DllImport("CoreDll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("CoreDll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("coredll")]
        private static extern int AddFontResource([In, MarshalAs(UnmanagedType.LPWStr)]string fontSource);

        [DllImport("coredll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 显示任务栏
        /// </summary>
        public static void ShowTaskBar()
        {
            IntPtr lpClassName = FindWindow("HHTaskBar", null);
            ShowWindow(lpClassName, SW_SHOWNORMAL);
        }

        /// <summary>
        /// 隐藏任务栏
        public static void HideTaskBar()
        {
            IntPtr lpClassName = FindWindow("HHTaskBar", null);
            ShowWindow(lpClassName, SW_HIDE);
        }

        /// <summary>
        /// 添加字体
        /// </summary>
        /// <param name="path">字体文件名</param>
        /// <returns></returns>
        public static int AddFont(string fileName)
        {
            string fontDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\fonts\\";
            int installedFont = AddFontResource(fontDirectory + fileName);
            SendMessage((IntPtr)0xffff, 0x001d, IntPtr.Zero, IntPtr.Zero);
            return installedFont;
        }

        /// <summary>
        /// 显示输入面板
        /// </summary>
        public static void ShowInputPanel()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            //必须为此目录，不可更改
            info.FileName = @"NAND Flash\pn\Ppencegb.exe";
            //info.FileName = TrainForm.basePath+ "\\pn\\Ppencegb.exe";
            //MessageBox.Show(info.FileName);
            try
            {
                proc = Process.Start(info);
            }
            catch
            {
                //MessageBox.Show("找不到程序运行文件");
            }
        }
    }
}
