using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrainView.GuideView;
using TrainView.AlarmView;
using TrainView.ToolView;
using TrainCommon;
using Trainfo;
using System.IO;
using System.Reflection;

namespace EasyFrom
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class BaseFrom : Form
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        private static TrainBase frmBase;
        /// <summary>
        /// 报单信息
        /// </summary>
        private static TrainReport frmReport;
        /// <summary>
        /// 行车指南
        /// </summary>
        private static TrainGuide frmGuide;
        /// <summary>
        /// 机车报警
        /// </summary>
        private static TrainAlarm frmAlarm;
        /// <summary>
        /// 系统配置
        /// </summary>
        private static TrainTool frmTool;
        /// <summary>
        /// 屏保窗体
        /// </summary>
        public static TrainScreen screen;

        public BaseFrom()
        {
            InitializeComponent();
            //初始化程序配置
            if (LocoInfo.TrainInfo.RoboConfig == null)
            {
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig order by ID");
            }
            //初始基本信息窗体
            btn_One_Click(null,null);
            //设置全屏
            SetFullScreen();
        }


        /// <summary>
        /// 加载基础信息窗体
        /// </summary>
        private void btn_One_Click(object sender, EventArgs e)
        {
            if (frmBase == null)
            {
                frmBase = new TrainBase();
            }

            //设置按钮字体颜色
            SetSelectColor(0);
            lbl_title.Text = "基础信息";
            frmBase.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmBase);
            frmBase.btn_Notice.Click += new EventHandler(btn_Notice_Click);
            frmBase.Show();
        }

        void btn_Notice_Click(object sender, EventArgs e)
        {
            //通知公告置0
            LocoInfo.TrainInfo.AnnCount = 0;
            if (frmGuide == null)
            {
                frmGuide = new TrainGuide();
            }
            //设置按钮字体颜色
            SetSelectColor(2);
            lbl_title.Text = "行车指南";
            frmGuide.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmGuide);
            frmGuide.btn_Notice_Click(null, null);
            frmGuide.Show();
        }

        /// <summary>
        /// 加载报单窗体
        /// </summary>
        private void btn_Two_Click(object sender, EventArgs e)
        {
            if (frmReport == null)
            {
                frmReport = new TrainReport();
            }
            //设置按钮字体颜色
            SetSelectColor(1);
            lbl_title.Text = "电子报单";
            frmReport.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmReport);
            frmReport.Show();
        }

        /// <summary>
        /// 加载行车指南窗体
        /// </summary>
        private void btn_Three_Click(object sender, EventArgs e)
        {
            if (frmGuide == null)
            {
                frmGuide = new TrainGuide();
            }
            //设置按钮字体颜色
            SetSelectColor(2);
            lbl_title.Text = "行车指南";
            frmGuide.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmGuide);
            frmGuide.Show();
        }

        /// <summary>
        /// 加载报警窗体
        /// </summary>
        private void btn_Four_Click(object sender, EventArgs e)
        {
            if (frmAlarm == null)
            {
                frmAlarm = new TrainAlarm();
            }
            //设置按钮字体颜色
            SetSelectColor(3);
            lbl_title.Text = "机车报警";
            frmAlarm.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmAlarm);
            frmAlarm.Show();
        }

        /// <summary>
        /// 加载系统配置窗体
        /// </summary>
        private void btn_five_Click(object sender, EventArgs e)
        {
            if (frmTool == null)
            {
                frmTool = new TrainTool();
            }
            //设置按钮字体颜色
            SetSelectColor(4);
            lbl_title.Text = "系统工具";
            frmTool.Dock = DockStyle.Fill;
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(frmTool);
            frmTool.Show();
        }

        private void BaseFrom_Load(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;

            //启动定时上传数据程序
          //  StartUpLoadProcess();

        }

        /// <summary>
        /// //启动外部程序 
        /// </summary>
        private void StartUpLoadProcess()
        {
            try
            {

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\CZSBDevice.exe";
                //启动外部程序 
                System.Diagnostics.Process.Start(path,"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        /// <summary>
        /// 设置全屏
        /// </summary>
        private void SetFullScreen()
        {
            FormCommon.HideTaskBar();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        /// <summary>
        /// 设置按钮选中字体颜色
        /// </summary>
        /// <param name="index">按钮索引</param>
        private void SetSelectColor(int index)
        {

            switch (index)
            {
                case 0:
                    btn_One.ForeColor = Color.Blue;
                    btn_Two.ForeColor = Color.Black;
                    btn_Three.ForeColor = Color.Black;
                    btn_Four.ForeColor = Color.Black;
                    btn_five.ForeColor = Color.Black;
                    break;
                case 1:
                    btn_One.ForeColor = Color.Black;
                    btn_Two.ForeColor = Color.Blue;
                    btn_Three.ForeColor = Color.Black;
                    btn_Four.ForeColor = Color.Black;
                    btn_five.ForeColor = Color.Black;
                    break;
                case 2:
                    btn_One.ForeColor = Color.Black;
                    btn_Two.ForeColor = Color.Black;
                    btn_Three.ForeColor = Color.Blue;
                    btn_Four.ForeColor = Color.Black;
                    btn_five.ForeColor = Color.Black;
                    break;
                case 3:
                    btn_One.ForeColor = Color.Black;
                    btn_Two.ForeColor = Color.Black;
                    btn_Three.ForeColor = Color.Black;
                    btn_Four.ForeColor = Color.Blue;
                    btn_five.ForeColor = Color.Black;
                    break;
                case 4:
                    btn_One.ForeColor = Color.Black;
                    btn_Two.ForeColor = Color.Black;
                    btn_Three.ForeColor = Color.Black;
                    btn_Four.ForeColor = Color.Black;
                    btn_five.ForeColor = Color.Blue;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 时间显示
        /// </summary>
        private void tmrCurrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //picSignal.Image = imgSignal.Images[0];
            //if (LocoInfo.TrainInfo.IsLine)
            //{
            //    picSignal.Image = imgSignal.Images[1];
            //}

            if (DateTime.Now.Subtract(LocoInfo.TrainInfo.LastDateTime).TotalMinutes >= LocoInfo.TrainInfo.ScreenMinutes && !LocoInfo.TrainInfo.IsScreenShow)
            {
                LocoInfo.TrainInfo.IsScreenShow = true;
                screen = new TrainScreen();
                screen.Show();
                //操作空闲时清理数据库数据
                BaseLibrary.delHistoryLog();
            }
            if (LocoInfo.TrainInfo.IsScreenShow)
            {
                LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            }
            if (LocoInfo.TrainInfo.IsLine)
                label1.Text = "3G信号正常";
            else
                label1.Text = "无信号";

            //刷新报单
            //frmReport.Refresh();
            //if(frmReport.)
        }

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }


    }
}