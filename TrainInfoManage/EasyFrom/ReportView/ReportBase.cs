using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.Threading;

namespace EasyFrom.ReportView
{

    /// <summary>
    /// 报单基础信息
    /// </summary>
    public partial class ReportBase : UserControl
    {
        private Thread loadData;

        public ReportBase()
        {
            InitializeComponent();
            InitData();
        }

        /// <summary>
        /// 启动线程加载数据
        /// </summary>
        public void InitData() 
        {
            //显示加载数据提示
            lbl_msg.Visible = true;
            loadData = new Thread(Init);
            //设置线程优先级
            loadData.Priority = ThreadPriority.BelowNormal;
            //线程启动
            loadData.Start();
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void Init()
        {
            this.Invoke(new EventHandler(delegate
            {

                if (LocoInfo.TrainInfo.RoboConfig == null)
                {
                    LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig order by ID");
                }
                //从数据库获取
                txtBrearu.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["RailCorp"].ToString();
                txtBelong.Text = LocoInfo.TrainInfo.RoboConfig.Rows[0]["Depot"].ToString();
                //报单创建日期
                if (LocoInfo.TrainInfo.ReportID != 0)
                {
                    using (DataTable rt = DBAction.GetDTFromSQL("select CreateTime from ReportHeader where ID=" + LocoInfo.TrainInfo.ReportID))
                    {
                        if (rt.Rows.Count > 0)
                        {
                            txtTime.Text = Convert.ToDateTime(rt.Rows[0]["CreateTime"]).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                    }
                }
                else
                {
                    txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                //车型
                txtType.Text = LocoInfo.TrainInfo.TrainType;
                //车号
                txtNum.Text = LocoInfo.TrainInfo.TrainNum;
                //数据加载完成取消数据加载提示
                lbl_msg.Visible = false;
            }));

        }

        private void tmr_CarInfo_Tick(object sender, EventArgs e)
        {
            //如果未获取机车信息，则刷新机车型号和车号
            if (txtType.Text.Trim() == "" || txtNum.Text.Trim() == "")
            {
                txtType.Refresh();
                txtNum.Refresh();
            }
            //获取后则释放time控件资源
            else
            {
                tmr_CarInfo.Dispose();
            }
        }
    }
}
