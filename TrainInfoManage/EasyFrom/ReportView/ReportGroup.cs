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
    public partial class ReportGroup : UserControl
    {
        //修改对话框
        private Dlg_DriverNote dlg_Rer;
        private Dlg_Tip dlg_Tip = null;

        //报单ID
        private int reportId = 0;
        /// <summary>
        /// 当前页
        /// </summary>
        private int currentPage = 1;
        /// <summary>
        /// 页条数
        /// </summary>
        private int pageSize = 15;
        /// <summary>
        /// 总条数
        /// </summary>
        private int count = 0;

        public ReportGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 上一条
        /// </summary>
        private void btn_Upitem_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            int index = dgv_RunGroup.CurrentCell.RowNumber;//获取当前选定的行
            if (index > 0)//判断是否为第一行
            {
                dgv_RunGroup.UnSelect(index);//取消选定行
                dgv_RunGroup.CurrentRowIndex = index - 1;
                dgv_RunGroup.Select(index - 1);//选择上一条
            }
        }

        /// <summary>
        /// 下一条
        /// </summary>
        private void btn_Downitem_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            int index = dgv_RunGroup.CurrentCell.RowNumber;//获取当前选定的行
            if (index < (((DataTable)(dgv_RunGroup.DataSource)).Rows.Count - 1))//判断是否为最后一行
            {
                dgv_RunGroup.UnSelect(index);//取消选定行
                dgv_RunGroup.CurrentRowIndex = index + 1;
                dgv_RunGroup.Select(index + 1);//选择下一条               
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void btn_Uppage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            if (currentPage > 1)
            {
                currentPage = currentPage - 1;
                InitData();
            }
            else
            {

                dlg_Tip = new Dlg_Tip();
                dlg_Tip.TipInfo = "当前已经是第一页！";

                dlg_Tip.ShowDialog();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            int total = pageSize * currentPage;
            if (count > total)
            {
                currentPage = currentPage + 1;
                InitData();
            }
            else
            {
                dlg_Tip = new Dlg_Tip();
                dlg_Tip.TipInfo = "当前已经是最后一页";
                dlg_Tip.ShowDialog();
            }
        }
        /// <summary>
        /// 获取新建或修改的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetNoteValue(object sender, EventArgs e)
        {
            InitData();//刷新列表  
        }

        //初始化运行编组信息
        public void InitData()
        {
            count = DBAction.GetRecordCount(" RunAndGroup ", "RHId=" + LocoInfo.TrainInfo.ReportID, new RParams());
            //获取报单ID
            if (LocoInfo.TrainInfo.ReportID == 0)
            {
                //XmlAction xa = new XmlAction();
                //XmlNode node = xa.GetNode("/RoboConfig/ReportID", null, null);//报单ID节点
                LocoInfo.TrainInfo.RoboConfig = DBAction.GetDTFromSQL("select * from RoboConfig");
                string rID = LocoInfo.TrainInfo.RoboConfig.Rows[0]["ReportID"].ToString().Trim();//报单ID
                LocoInfo.TrainInfo.ReportID = Convert.ToInt32(rID);
            }
            using (DataTable rgDT = DBAction.GetPageDT(" RunAndGroup ", "TrainNum,StationName,ArrivedTime,SetOutTime", " RHId=" + LocoInfo.TrainInfo.ReportID, currentPage, pageSize))
            {
                if (rgDT.Rows.Count > 0)
                {
                    #region 设置显示格式
                    rgDT.TableName = "RunAndGroup";
                    #endregion
                    dgv_RunGroup.DataSource = rgDT;
                    if (rgDT.Rows.Count > 0)
                    {
                        dgv_RunGroup.Select(0);
                    }

                }
            }
        }

        private void dgv_RunGroup_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            int index = dgv_RunGroup.CurrentCell.RowNumber;
            if (((DataTable)(dgv_RunGroup.DataSource)).Rows.Count > 0)
            {
                dgv_RunGroup.Select(index);
            }
        }
        private void dgv_RunGroup_GotFocus(object sender, EventArgs e)
        {
            if (dgv_RunGroup.DataSource == null)
            {
                return;
            }
            int index = dgv_RunGroup.CurrentCell.RowNumber;
            if (((DataTable)(dgv_RunGroup.DataSource)).Rows.Count > 0)
            {
                dgv_RunGroup.Select(index);
            }
        }


        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Amend_Click(object sender, EventArgs e)
        {
            try
            {
                LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
                using (DataTable dt = (DataTable)dgv_RunGroup.DataSource)
                {
                    int index = dgv_RunGroup.CurrentCell.RowNumber;
                    if (dt.Rows.Count > 0)
                    {
                        if (dlg_Rer == null)
                        {
                            dlg_Rer = new Dlg_DriverNote();
                            dlg_Rer.Closed += new EventHandler(GetNoteValue);
                        }
                        string id = dt.Rows[index][0].ToString();
                        dlg_Rer.NoteID = Convert.ToInt32(id);
                        dlg_Rer.NumID = 5;
                        dlg_Rer.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        ///// <summary>
        ///// 刷新列表数据
        ///// </summary>
        //private void trm_ref_Tick(object sender, EventArgs e)
        //{
        //    InitData();//刷新列表  
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            InitData();//刷新列表  
        }

        
    }
}
