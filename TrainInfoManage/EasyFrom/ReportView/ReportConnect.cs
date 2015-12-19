using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using TrainView.ChildFrom;

namespace EasyFrom.ReportView
{
    public partial class ReportConnect : UserControl
    {
        /// <summary>
        /// 报表ID
        /// </summary>
        private int reportId = 0;
        /// <summary>
        /// 新增补机重联窗体
        /// </summary>
        private Dlg_Connect dlg_Connect = null;
        /// <summary>
        /// 提示框
        /// </summary>
        private Dlg_Ask dlg_Ask = null;
        /// <summary>
        /// 当前页
        /// </summary>
        private int currentPage = 1;
        /// <summary>
        /// 页行数
        /// </summary>
        private int pageSize = 15;
        private int count = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportConnect()
        {
            InitializeComponent();
            if (reportId == 0)
            {
                //XmlAction xa = new XmlAction();
                //XmlNode node = xa.GetNode("/RoboConfig/ReportID", null, null);//报单ID节点
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig");
                string rID = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ReportID"].ToString().Trim();//报单ID
                reportId = Convert.ToInt32(rID);
            }
            count = DBAction.GetRecordCount(" Reconnection ", "RHId=" + reportId, new RParams());
            InitData();
        }

        /// <summary>
        /// 新建运行编组
        /// </summary>
        private void btn_New_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable rt = DBAction.GetDTFromSQL("select ID from Reconnection where RHId=" + reportId))
            {
                int count = rt.Rows.Count;
                if (count >= 3)
                {
                    MessageBox.Show("补机重联不得超过三条");
                    return;
                }
            }
            if (dlg_Connect == null)
            {
                dlg_Connect = new Dlg_Connect();
                dlg_Connect.Closed += new EventHandler(GetStrConnect);
            }
            dlg_Connect.ReconID = 0;
            dlg_Connect.ReportID = reportId;
            dlg_Connect.ShowDialog();
        }

        /// <summary>
        /// 获取输入或修改的值
        /// </summary>
        public void GetStrConnect(object sender, EventArgs e)
        {
            InitData();
        }

        /// <summary>
        /// 为列表绑定初始值
        /// </summary>
        private void InitData()
        {

            if (reportId == 0)
            {
                //XmlAction xa = new XmlAction();
                //XmlNode node = xa.GetNode("/RoboConfig/ReportID", null, null);//报单ID节点
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig");
                string rID = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ReportID"].ToString().Trim();//报单ID
                reportId = Convert.ToInt32(rID);
            }
            using (DataTable rt = DBAction.GetPageDT(" Reconnection ", "*", "RHId=" + reportId, currentPage, pageSize))
            {
                rt.TableName = "Recon";
                dgv_Trc.DataSource = rt;

                if (rt.Rows.Count > 0)
                {
                    ShowText(((DataTable)rt).Rows[0][0].ToString());
                    dgv_Trc.Select(0);
                }
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="wID"></param>
        private void ShowText(string wID)
        {
            using (DataTable alInfo = DBAction.GetDTFromSQL("select * from " + ETableName.Reconnection.ToString() + " where ID=" + wID))
            {
                if (alInfo.Rows.Count > 0)
                {
                    lb_Carnum.Text = alInfo.Rows[0]["TrainType"].ToString();
                    lb_Km.Text = alInfo.Rows[0]["RegionDistance"].ToString();
                    lb_Section.Text = alInfo.Rows[0]["Belong"].ToString();
                }
            }

        }

        /// <summary>
        /// 修改运行编组
        /// </summary>
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            int index = dgv_Trc.CurrentCell.RowNumber;
            using (DataTable dt = (DataTable)dgv_Trc.DataSource)
            {
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    string reconID = dt.Rows[index][0].ToString();
                    if (dlg_Connect == null)
                    {
                        dlg_Connect = new Dlg_Connect();
                        dlg_Connect.Closed += new EventHandler(GetStrConnect);
                    }
                    dlg_Connect.ReportID = reportId;
                    dlg_Connect.ReconID = Convert.ToInt32(reconID);
                }
                dlg_Connect.ShowDialog();
            }
        }

        /// <summary>
        /// 删除运行编组
        /// </summary>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable dt = (DataTable)dgv_Trc.DataSource)
            {
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                if (dlg_Ask == null)
                {
                    dlg_Ask = new Dlg_Ask();
                    dlg_Ask.AskInfo = "是否确认删除此补机重联信息？";
                    dlg_Ask.Closed += new EventHandler(GetAskValue);
                }
                dlg_Ask.ShowDialog();
            }
        }

        /// <summary>
        /// 获取询问对话框的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAskValue(object sender, EventArgs e)
        {
            bool isDelete = (bool)((Dlg_Ask)sender).Tag;
            if (isDelete)
            {
                //确认删除
                int index = dgv_Trc.CurrentCell.RowNumber;
                using (DataTable dt = (DataTable)dgv_Trc.DataSource)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DBAction.Delete(ETableName.Reconnection, "ID=" + dt.Rows[index][0].ToString());
                    }
                    InitData();
                }
            }
        }

        /// <summary>
        /// 上一条
        /// </summary>
        private void btn_Up_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_Trc.DataSource == null)
            {
                return;
            }
            int index = dgv_Trc.CurrentCell.RowNumber;//获取当前选定的行
            if (index > 0)//判断是否为第一行
            {
                dgv_Trc.UnSelect(index);//取消选定行
                dgv_Trc.CurrentRowIndex = index - 1;
                dgv_Trc.Select(index - 1);//选择上一条
            }
        }

        /// <summary>
        /// 下一条
        /// </summary>
        private void btn_Down_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_Trc.DataSource == null)
            {
                return;
            }
            int index = dgv_Trc.CurrentCell.RowNumber;//获取当前选定的行
            if (index < (((DataTable)(dgv_Trc.DataSource)).Rows.Count - 1))//判断是否为最后一行
            {
                dgv_Trc.UnSelect(index);//取消选定行
                dgv_Trc.CurrentRowIndex = index + 1;
                dgv_Trc.Select(index + 1);//选择下一条               
            }
        }
        #region 设置选中整行
        private void dgv_Trc_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv_Trc.DataSource == null)
            {
                return;
            }
            int index = dgv_Trc.CurrentCell.RowNumber;
            if (((DataTable)(dgv_Trc.DataSource)).Rows.Count > 0)
            {
                ShowText(((DataTable)(dgv_Trc.DataSource)).Rows[index][0].ToString());
                dgv_Trc.Select(index);
            }
        }
        private void dgv_Trc_GotFocus(object sender, EventArgs e)
        {
            if (dgv_Trc.DataSource == null)
            {
                return;
            }
            int index = dgv_Trc.CurrentCell.RowNumber;
            if (((DataTable)(dgv_Trc.DataSource)).Rows.Count > 0)
            {
                dgv_Trc.Select(index);
            }
        }
        #endregion
    }
}
