using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace UpdateWizard
{
    public partial class LoadUpdate : Form
    {
        public string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        //private WebFileTransportService.FileTransportService fts = null;
        private ProcessStartInfo info = null;
        private DataTable dtConfig = null;
        private RParams param = null;
        private Thread th = null;


        const int EXSTYLE = -20;
        const int WS_EX_NOANIMATION = 0x04000000;
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern void SetWindowLong(IntPtr hWnd, int GetWindowLongParam, uint nValue);
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nItem);
        [DllImport("coredll.dll")]
        private static extern IntPtr GetCapture();

        public LoadUpdate()
        {
            InitializeComponent();
            NotShowInTaskbar();
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 系统更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadUpdate_Load(object sender, EventArgs e)
        {
            NotShowInTaskbar();
            th = new Thread(new ThreadStart(CheckUpdate));
            th.Start();
        }

        private void CheckUpdate()
        {
            using (dtConfig = DBAction.GetDTFromSQL("select * from RoboConfig order by ID"))
            {
                if (dtConfig.Rows[0]["IsFirst"].ToString().Equals("1"))
                {
                    SetAutoRun(true);
                }
                //本地有更新文件
                if (dtConfig.Rows[0]["MainVersion"].ToString() != dtConfig.Rows[0]["AssVersion"].ToString())
                {
                    //更新程序
                    FileHelper.CopyFiles(basePath + "\\UpdateTemp", basePath);
                    th.Join(30000);
                    Directory.Delete(basePath + "\\UpdateTemp\\", true);
                    Directory.CreateDirectory(basePath + "\\UpdateTemp");
                    //更新数据库
                    param = new RParams();
                    param.Add("MainVersion", dtConfig.Rows[0]["AssVersion"].ToString());
                    DBAction.Update("RoboConfig", "MainVersion", "ID=" + dtConfig.Rows[0]["ID"].ToString(), param);
                }
                //if (dtConfig.Rows[0]["IsAutoRun"].ToString().Equals("1"))
                //{
                //启动程序
                info = new ProcessStartInfo();
                info.FileName = basePath + "\\TrainView.exe";
                Process.Start(info);
                //}
                Application.Exit();
            }


        }

        /// <summary>
        /// 窗体不再任务栏显示
        /// </summary>
        private void NotShowInTaskbar()
        {
            Capture = true;
            IntPtr hwnd = GetCapture();
            Capture = false;
            uint style = GetWindowLong(hwnd, EXSTYLE);
            style |= WS_EX_NOANIMATION;
            SetWindowLong(hwnd, EXSTYLE, style);
        }

        #region 设置自启动方法
        /// <summary>
        /// 设置自启动
        /// </summary>
        /// <param name="isAutoRun"></param>
        /// <returns></returns>
        public bool SetAutoRun(bool isAutoRun)
        {
            RegistryKey rKey = null;
            try
            {
                //设置自启动-->80为启动优先级
                string keyName = "Launch80";
                string exePath = Assembly.GetExecutingAssembly().GetName().CodeBase;
                rKey = Registry.LocalMachine.OpenSubKey("init", true);
                if (rKey == null)
                {
                    rKey = Registry.LocalMachine.CreateSubKey("init");
                }
                rKey = Registry.LocalMachine.OpenSubKey("init", true);
                if (isAutoRun)
                {
                    rKey.SetValue(keyName, exePath);
                }
                else
                {
                    rKey.SetValue(keyName,"explorer.exe");
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (rKey != null)
                {
                    rKey.Close();
                }
            }
            return true;
        }
        #endregion

        private void lblInfo_ParentChanged(object sender, EventArgs e)
        {

        }

    }
}