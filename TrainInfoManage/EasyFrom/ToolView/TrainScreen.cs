using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Trainfo
{
    /// <summary>
    /// 屏保窗体
    /// </summary>
    public partial class TrainScreen : Form
    {
        /// <summary>
        /// 时间闪动效果
        /// </summary>
        private int count = 0;

        public TrainScreen()
        {
            InitializeComponent();
            //隐藏工具栏
            TrainForm.HideTaskBar();
            //设置全屏
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            pnlDate.Location = new Point((int)((this.Width - pnlDate.Width) / 2), (int)((this.Height - pnlDate.Height) / 2));
            Cursor.Hide();
        }

        /// <summary>
        /// 点击关闭屏保
        /// </summary>
        private void TrainScreen_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("关闭屏保");
            tmrTime.Enabled = false;
            tmrTime.Dispose();
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            LocoInfo.TrainInfo.IsScreenShow = false;
            this.Close();
        }

        /// <summary>
        /// 时间刷新
        /// </summary>
        private void tmrTime_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                lblTime.Text = DateTime.Now.ToString("HH:mm");
                count = 1;
            }
            else
            {
                lblTime.Text = DateTime.Now.ToString("HH mm");
                count = 0;
            }
            lblDate.Text = ShowDate();
        }

        /// <summary>
        /// 加载屏保
        /// </summary>
        private void TrainScreen_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblDate.Text = ShowDate();
            tmrTime.Enabled = true;
        }

        /// <summary>
        /// 时间格式转换
        /// </summary>
        private string ShowDate()
        {
            string s = DateTime.Now.ToString("MM月dd日 星期");
            switch (DateTime.Now.DayOfWeek)
            { 
                case DayOfWeek.Monday:
                    s += "一";
                    break;
                case DayOfWeek.Tuesday:
                    s += "二";
                    break;
                case DayOfWeek.Wednesday:
                    s += "三";
                    break;
                case DayOfWeek.Thursday:
                    s += "四";
                    break;
                case DayOfWeek.Friday:
                    s += "五";
                    break;
                case DayOfWeek.Saturday:
                    s += "六";
                    break;
                case DayOfWeek.Sunday:
                    s += "日";
                    break;
                default:
                    break;
            }
            return s;
        }
    }
}