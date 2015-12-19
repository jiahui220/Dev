using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using TrainView.ChildFrom;
using TrainView.ToolView;

namespace EasyFrom.ReportView
{
    public partial class ReportWorkTime : UserControl
    {
        //提示框
        private Dlg_Tip dlg_t = null;
        private double noteCount = 600;
        string reson = "请输入";
        //时间输入对话框
        private Dlg_DateNeed dlg_DateTime = null;
        //文本框索引
        private int index = 0;
        public ReportWorkTime()
        {
            InitializeComponent();
            InitData();
            //timer1.Enabled = true;   
        }
        /// <summary>
        /// 显示对话框
        /// </summary>
        private void ShowDlgDt()
        {
            if (dlg_DateTime == null)
            {
                dlg_DateTime = new Dlg_DateNeed();
                dlg_DateTime.Closed += new EventHandler(GetDateTime);
            }
            btn_empty.Focus();//利用按钮转移焦点
            dlg_DateTime.ShowDialog();
        }

        /// <summary>
        /// 获取时间输入框的时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDateTime(object sender, EventArgs e)
        {
            string dt = (string)((Dlg_DateNeed)sender).Tag;
            if (String.IsNullOrEmpty(dt))
            {
                return;
            }
            using (RParams param = new RParams())
            {
                switch (index)
                {
                    case 0:
                        txtWorkTime.Text = dt;
                        if (txtWorkTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("DutyTime", txtWorkTime.Text);
                            DBAction.Update(ETableName.Steward, "DutyTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 1:
                        txtReceiveTime.Text = dt;
                        if (txtReceiveTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("ReceiveTime", txtReceiveTime.Text);
                            DBAction.Update(ETableName.Steward, "ReceiveTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 2:
                        txtSubmitTime.Text = dt;
                        if (txtSubmitTime.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("DeliverTime", txtSubmitTime.Text);
                            DBAction.Update(ETableName.Steward, "DeliverTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 3:
                        txtOutLocal.Text = dt;
                        if (txtOutLocal.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("OutLocalTime", txtOutLocal.Text.Trim());
                            DBAction.Update(ETableName.TrainInOut, "OutLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 4:
                        txtInOther.Text = dt;
                        if (txtInOther.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("InExternalTime", txtInOther.Text.Trim());
                            DBAction.Update(ETableName.TrainInOut, "InExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 5:
                        txtOutOther.Text = dt;
                        if (txtOutOther.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("OutExternalTime", txtOutOther.Text.Trim());
                            DBAction.Update(ETableName.TrainInOut, "OutExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    case 6:
                        txtInLocal.Text = dt;
                        if (txtInLocal.Text.Trim().Length > 0)
                        {
                            param.Items.Clear();
                            param.Add("InLocalTime", txtInLocal.Text.Trim());
                            DBAction.Update(ETableName.TrainInOut, "InLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                        }
                        break;
                    default:
                        break;
                }
                btn_empty.Focus();//利用按钮转移焦点
            }
        }

        /// <summary>
        /// 交车时分
        /// </summary>
        private void txtSubmitTime_GotFocus(object sender, EventArgs e)
        {
            index = 2;
            ShowDlgDt();
        }

        /// <summary>
        /// 接车时分
        /// </summary>
        private void txtReceiveTime_GotFocus(object sender, EventArgs e)
        {
            index = 1;
            ShowDlgDt();
        }

        /// <summary>
        /// 出勤时分
        /// </summary>
        private void txtWorkTime_GotFocus(object sender, EventArgs e)
        {
            index = 0;
            ShowDlgDt();
        }

        /// <summary>
        /// 入本段时分
        /// </summary>
        private void txtInLocal_GotFocus(object sender, EventArgs e)
        {
            index = 6;
            ShowDlgDt();
        }

        /// <summary>
        /// 出外段时分
        /// </summary>
        private void txtOutOther_GotFocus(object sender, EventArgs e)
        {
            index = 5;
            ShowDlgDt();
        }

        /// <summary>
        /// 入外段时分
        /// </summary>
        private void txtInOther_GotFocus(object sender, EventArgs e)
        {
            index = 4;
            ShowDlgDt();
        }

        /// <summary>
        /// 出本段时分
        /// </summary>
        private void txtOutLocal_GotFocus(object sender, EventArgs e)
        {
            index = 3;
            ShowDlgDt();
        }

        #region 绑定初始值
        /// <summary>
        /// 绑定初始值
        /// </summary>
        private void InitData()
        {
            if (LocoInfo.TrainInfo.ReportID == 0)
            {
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig");
                string rID = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ReportID"].ToString().Trim();//报单ID
                LocoInfo.TrainInfo.ReportID = Convert.ToInt32(rID);
            }

            using (DataTable st = DBAction.GetDTFromSQL("select * from Steward where RHId=" + LocoInfo.TrainInfo.ReportID))
            {
                if (st.Rows.Count > 0)
                {
                    txtWorkTime.Text = st.Rows[0]["DutyTime"].ToString();
                    txtReceiveTime.Text = st.Rows[0]["ReceiveTime"].ToString();
                    txtSubmitTime.Text = st.Rows[0]["DeliverTime"].ToString();
                }
            }

            using (DataTable ti = DBAction.GetDTFromSQL("select * from TrainInOut where RHId=" + LocoInfo.TrainInfo.ReportID))
            {
                if (ti.Rows.Count > 0)
                {
                    txtOutLocal.Text = ti.Rows[0]["OutLocalTime"].ToString();
                    txtInOther.Text = ti.Rows[0]["InExternalTime"].ToString();
                    txtOutOther.Text = ti.Rows[0]["OutExternalTime"].ToString();
                    txtInLocal.Text = ti.Rows[0]["InLocalTime"].ToString();
                }
                else
                {
                    //数据库未记录新建记录
                    using (RParams param = new RParams())
                    {
                        param.Add("RHId", LocoInfo.TrainInfo.ReportID);
                        DBAction.Insert("TrainInOut", param);
                    }
                }
            }

            if (noteCount==600)
            {
                noteCount = 0;
                if (txtWorkTime.Text == "")
                    reson += "出勤时间、";
                if (txtReceiveTime.Text == "")
                    reson += "接车时间\r\n";
                if (txtSubmitTime.Text == "")
                    reson += "交车时间、";
                if (txtOutLocal.Text == "")
                    reson += "出本段时间\r\n";
                if (txtInOther.Text == "")
                    reson += "入外段时间、";
                if (txtOutOther.Text == "")
                    reson += "出外段时间\r\n";
                if (txtInLocal.Text == "")
                    reson += "入本段时间.";
                if (dlg_t == null)
                {
                    dlg_t = new Dlg_Tip();
                    dlg_t.TipInfo = reson;
                    dlg_t.ShowDialog();
                }
            }



        }
        #endregion

        /// <summary>
        /// 清空过交车时分
        /// </summary>
        private void btn_empty_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubmitTime.Text = "";
                using (RParams param = new RParams())
                {
                    param.Add("DeliverTime", "");
                    DBAction.Update("Steward", "DeliverTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 清空出本段时分
        /// </summary>
        private void btn_empty1_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubmitTime.Text = "";
                using (RParams param = new RParams())
                {
                    param.Add("DeliverTime", "");
                    DBAction.Update("Steward", "DeliverTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 清空入外段时分
        /// </summary>
        private void btn_empty2_Click(object sender, EventArgs e)
        {
            try
            {
                txtInOther.Text = "";
                using (RParams param = new RParams())
                {
                    param.Add("InExternalTime", "");
                    DBAction.Update("TrainInOut", "InExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 清空出外段时分
        /// </summary>
        private void btn_empty3_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutOther.Text = "";
                using (RParams param = new RParams())
                {
                    param.Add("OutExternalTime", "");
                    DBAction.Update("TrainInOut", "OutExternalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 清空入本段时分
        /// </summary>
        private void btn_empty4_Click(object sender, EventArgs e)
        {
            try
            {
                txtInLocal.Text = "";
                using (RParams param = new RParams())
                {
                    param.Add("InLocalTime", "");
                    DBAction.Update("TrainInOut", "InLocalTime", "RHId=" + LocoInfo.TrainInfo.ReportID, param);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtWorkTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReportWorkTime_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void trm_ref_Tick(object sender, EventArgs e)
        {
            InitData();
        }

        
    }
}
