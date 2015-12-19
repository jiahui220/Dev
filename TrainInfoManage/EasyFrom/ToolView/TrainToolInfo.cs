using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using TrainView.ChildFrom;
using System.Threading;

namespace TrainView.ToolView
{
    public partial class TrainToolInfo : UserControl
    {
        /// <summary>
        /// 提示框
        /// </summary>
        private Dlg_Tip dlg_tip = null;
        public TrainToolInfo()
        {
            InitializeComponent();
            StartThread();
        }

        /// <summary>
        /// 数据绑定线程
        /// </summary>
        private void StartThread()
        {
            Thread newThread = new Thread(new ThreadStart(ToBindData));
            newThread.IsBackground = true;
            newThread.Priority = ThreadPriority.BelowNormal;
            newThread.Start();
        }

        //绑定数据到控件线程委托
        private delegate void BindDataToControlDelegate();
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void ToBindData() {
            if (LocoInfo.TrainInfo.RoboConfig==null)
            {
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig order by ID");
            }
            BindDataToControlDelegate bindDataToControl = new BindDataToControlDelegate(BindDataToControl);
            Invoke(bindDataToControl);
        }
        /// <summary>
        /// 绑定数据到控件
        /// </summary>
        private void BindDataToControl() 
        {
            txtState.Text = "初始化成功";
            txtNum.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["DeviceNum"].ToString();
            txtTrainNum.Text = LocoInfo.TrainInfo.TrainNum;
            txtType.Text = LocoInfo.TrainInfo.TrainType;
            txtVersion.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["MainVersion"].ToString();
            txtAddress.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ServiceAddress"].ToString();
            txtPort.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ServicePort"].ToString();
            txtProVersion.Text = LocoInfo.TrainInfo.ProVersion;
            txtCanVersion.Text = LocoInfo.TrainInfo.CanVersion;
            txtCfgVersion.Text = LocoInfo.TrainInfo.ItemsVersion;
            if (LocoInfo.TrainInfo.IsLine)
                textBox7.Text = "上线";
            else
                textBox7.Text = "离线";

        }

        /// <summary>
        /// 监测更新
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dlg_tip == null)
            {
                dlg_tip = new Dlg_Tip();
            }
            if (LocoInfo.TrainInfo.RoboConfig.Rows[0]["MainVersion"].ToString() != LocoInfo.TrainInfo.RoboConfig.Rows[0]["AssVersion"].ToString())
            {
                dlg_tip.TipInfo = "已检测到新版本，系统将在下次启动的时候自动安装更新";
            }
            else
            {
                dlg_tip.TipInfo = "当前已是最新版本";
            }
            dlg_tip.ShowDialog();
        }

        private void TrainToolInfo_Click(object sender, EventArgs e)
        {

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //FileUpdate.RequestUpdate(LocoInfo.TrainInfo.SckTrains);
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dlg_tip == null)
            {
                dlg_tip = new Dlg_Tip();
            }
            if (LocoInfo.TrainInfo.RoboConfig.Rows[0]["MainVersion"].ToString() != LocoInfo.TrainInfo.RoboConfig.Rows[0]["AssVersion"].ToString())
            {
                dlg_tip.TipInfo = "已检测到新版本，系统将在下次启动的时候自动安装更新";
            }
            else
            {
                dlg_tip.TipInfo = "当前已是最新版本";
            }
            dlg_tip.ShowDialog();
        }


    }
}
