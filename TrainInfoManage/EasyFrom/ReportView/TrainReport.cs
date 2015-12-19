using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using EasyFrom.ReportView;
using TrainCommon;
using System.Threading;
using TrainView.ChildFrom;

namespace EasyFrom
{
    public partial class TrainReport : UserControl
    {
        #region 变量
        /// <summary>
        /// 报单基础信息
        /// </summary>
        private static ReportBase frmBase;
        /// <summary>
        /// 出勤时分
        /// </summary>
        private static ReportWorkTime frmWork;
        /// <summary>
        /// 领取燃料
        /// </summary>
        private static ReportFuel frmFuel;
        /// <summary>
        /// 补机重联
        /// </summary>
        private static ReportConnect frmConnect;
        /// <summary>
        /// 运行编组
        /// </summary>
        private static ReportGroup frmGroup;
        /// <summary>
        /// 提示对话框
        /// </summary>
        public static Dlg_Tip dlg_Tip;
        /// <summary>
        /// 报单ID
        /// </summary>
        private int reportID = 0;
        /// <summary>
        /// 提交报单线程
        /// </summary>
        private Thread thSendReport = null;
        /// <summary>
        /// 是否点击发送
        /// </summary>
        public bool isSend = false;
        /// <summary>
        /// 报单重发次数
        /// </summary>
        public int sendCount = 0;
        #endregion

        public TrainReport()
        {
            InitializeComponent();
            //加载报单基础信息
            btn_Base_Click(null,null);
        }

        /// <summary>
        /// 加载报单基础信息
        /// </summary>
        private void btn_Base_Click(object sender, EventArgs e)
        {
            if (frmBase==null)
            {
                frmBase = new ReportBase();
            }
            //设置按钮点击字体颜色
            SetSelectColor(0);
            frmBase.Dock = DockStyle.Fill;
            this.pl_Report.Controls.Clear();
            this.pl_Report.Controls.Add(frmBase);
            frmBase.Show();
        }

        /// <summary>
        /// 加载出勤时分
        /// </summary>
        private void btn_Work_Click(object sender, EventArgs e)
        {
            if (frmWork == null)
            {
                frmWork = new ReportWorkTime();
            }
            //设置按钮点击字体颜色
            SetSelectColor(1);
            frmWork.Dock = DockStyle.Fill;
            this.pl_Report.Controls.Clear();
            this.pl_Report.Controls.Add(frmWork);
            frmWork.Show();
        }

        /// <summary>
        /// 加载领取燃料
        /// </summary>
        private void btn_Fuel_Click(object sender, EventArgs e)
        {
            if (frmFuel == null)
            {
                frmFuel = new ReportFuel();
            }
            //设置按钮点击字体颜色
            SetSelectColor(2);
            frmFuel.Dock = DockStyle.Fill;
            this.pl_Report.Controls.Clear();
            this.pl_Report.Controls.Add(frmFuel);
            frmFuel.Show();
        }

        /// <summary>
        ///加载补机重联
        /// </summary>
        private void btn_Conect_Click(object sender, EventArgs e)
        {
            if (frmConnect == null)
            {
                frmConnect = new ReportConnect();
            }
            //设置按钮点击字体颜色
            SetSelectColor(3);
            frmConnect.Dock = DockStyle.Fill;
            this.pl_Report.Controls.Clear();
            this.pl_Report.Controls.Add(frmConnect);
            frmConnect.Show();
        }

        /// <summary>
        /// 加载运行编组
        /// </summary>
        private void btn_Group_Click(object sender, EventArgs e)
        {
            if (frmGroup == null)
            {
                frmGroup = new ReportGroup();
            }
            //设置按钮点击字体颜色
            SetSelectColor(4);
            frmGroup.Dock = DockStyle.Fill;
            this.pl_Report.Controls.Clear();
            this.pl_Report.Controls.Add(frmGroup);
            frmGroup.Show();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            //string msg = "报单发送中，请勿进行其他操作！！！";
            LocoInfo.dataConnect = 1;
            //点击发送按钮
            isSend = true;
            //设置按钮不可点击
            //SetSelectColor(5);
            //获取报单ID
            if (LocoInfo.TrainInfo.ReportID == 0)
            {
                using (DataTable ht = DBAction.GetDTFromSQL("select ID from ReportHeader "))
                {
                    if (ht.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.ReportID = Convert.ToInt32(ht.Rows[ht.Rows.Count - 1][0]);
                    }
                }
            }
            //if (dlg_Tip == null)
            //{
            //    dlg_Tip = new Dlg_Tip();
            //}
            ////设置提示信息
            //dlg_Tip.TipInfo = msg;
            //dlg_Tip.ShowDialog(); 
            btn_Submit.Text = "提交中...";
            btn_Submit.ForeColor = Color.Red;
            btn_Submit.Enabled = false;
            //发送报单
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            thSendReport = new Thread(new ThreadStart(ManualSendReport));
            thSendReport.IsBackground = true;
            thSendReport.Start();
        }

        /// <summary>
        /// 提交报单
        /// </summary>
        public void ManualSendReport()
        {
            string msg = "";
            while (sendCount < 5 && !LocoInfo.TrainInfo.IsSendSucess)
            {
                SendReport.SendReportHead(LocoInfo.TrainInfo.SckTrains, "");
                sendCount++;
                thSendReport.Join(10000);
            }
            LocoInfo.dataConnect = 0;
            if (isSend)
            {
                //重发五次未成功，则返回提交失败
                if (sendCount == 5 && !LocoInfo.TrainInfo.IsSendSucess)
                {
                    //MessageBox.Show("报单提交失败，请确认联网是否正常");
                    msg = "报单提交失败，请确认联网是否正常";                  
                }
                else
                {
                    //MessageBox.Show("报单提交成功");
                    msg = "报单提交成功";
                }
                dlg_Tip = new Dlg_Tip();
                //设置提示信息
                dlg_Tip.TipInfo = msg;
                dlg_Tip.ShowDialog();
                //子线程调用主线程的方法来修改按钮的属性
                this.Invoke(new EventHandler(delegate
                {
                    //SetSelectColor(6);
                    btn_Submit.Text = "提交报单";
                    btn_Submit.ForeColor = Color.Black;
                    btn_Submit.Enabled = true;
                })); 
                sendCount = 0;
                LocoInfo.TrainInfo.IsSendSucess = false;
            }
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
                    btn_Base.ForeColor = Color.Blue;
                    btn_Work.ForeColor = Color.Black;
                    btn_Fuel.ForeColor = Color.Black;
                    btn_Conect.ForeColor = Color.Black;
                    btn_Group.ForeColor = Color.Black;
                    break;
                case 1:
                    btn_Base.ForeColor = Color.Black;
                    btn_Work.ForeColor = Color.Blue;
                    btn_Fuel.ForeColor = Color.Black;
                    btn_Conect.ForeColor = Color.Black;
                    btn_Group.ForeColor = Color.Black;
                    break;
                case 2:
                    btn_Base.ForeColor = Color.Black;
                    btn_Work.ForeColor = Color.Black;
                    btn_Fuel.ForeColor = Color.Blue;
                    btn_Conect.ForeColor = Color.Black;
                    btn_Group.ForeColor = Color.Black;
                    break;
                case 3:
                    btn_Base.ForeColor = Color.Black;
                    btn_Work.ForeColor = Color.Black;
                    btn_Fuel.ForeColor = Color.Black;
                    btn_Conect.ForeColor = Color.Blue;
                    btn_Group.ForeColor = Color.Black;
                    break;
                case 4:
                    btn_Base.ForeColor = Color.Black;
                    btn_Work.ForeColor = Color.Black;
                    btn_Fuel.ForeColor = Color.Black;
                    btn_Conect.ForeColor = Color.Black;
                    btn_Group.ForeColor = Color.Blue;
                    break;
                case 5:
                    btn_Submit.Enabled=false;
                    btn_Base.Enabled = false;
                    btn_Work.Enabled = false;
                    btn_Fuel.Enabled = false;
                    btn_Conect.Enabled = false;
                    btn_Group.Enabled = false;
                    break;
                case 6:
                    btn_Submit.Enabled = true;
                    btn_Base.Enabled = true;
                    btn_Work.Enabled = true;
                    btn_Fuel.Enabled = true;
                    btn_Conect.Enabled = true;
                    btn_Group.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void pl_Report_GotFocus(object sender, EventArgs e)
        {

        }
    }
}
