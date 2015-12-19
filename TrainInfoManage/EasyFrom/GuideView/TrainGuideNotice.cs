using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.Xml;
using TrainView.ChildFrom;


namespace TrainView.GuideView
{
    /// <summary>
    /// 通知公告
    /// </summary>
    public partial class TrainGuideNotice : UserControl
    {
        //是否删除询问窗体
        private Dlg_Ask dlg_Ask = null;
        private Dlg_Tip dlg_Tip = null;
        //查看信息窗体
        private Dlg_Check_Notice dlg_Check_Notice = null;
        private int currentPage = 1;
        private int pageSize = 15;
        private int count = 0;
        public TrainGuideNotice()
        {
            InitializeComponent();
            count = DBAction.GetRecordCount(ETableName.Announcement, "", new RParams());
            InitData();
        }

        /// <summary>
        /// 查看信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable nt = (DataTable)(dgNotice.DataSource))
            {
                int index = dgNotice.CurrentCell.RowNumber;
                if (nt.Rows.Count > 0)
                {
                    if (dlg_Check_Notice == null)
                    {
                        dlg_Check_Notice = new Dlg_Check_Notice();
                    }
                    string id = nt.Rows[index][0].ToString();
                    dlg_Check_Notice.NoticeID = Convert.ToInt32(id);
                    dlg_Check_Notice.ShowDialog();
                }
            }
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable nt = (DataTable)(dgNotice.DataSource))
            {
                if (nt.Rows.Count == 0)
                {
                    return;
                }
                if (dlg_Ask == null)
                {
                    dlg_Ask = new Dlg_Ask();
                    dlg_Ask.AskInfo = "是否确认删除此通知公告？";
                    dlg_Ask.Closed += new EventHandler(GetAskValue);
                }
                dlg_Ask.ShowDialog();
            }
        }

        /// <summary>
        /// 初始化加载通知公告信息
        /// </summary>
        private void InitData()
        {
            
            using (DataTable nt = DBAction.GetPageDT(ETableName.Announcement, "*", "", currentPage, pageSize, "ReceTime"))
            {
                nt.TableName = "Notice";
                if (nt.Rows.Count > 0)
                {

                    dgNotice.DataSource = nt;
                }

                if (nt.Rows.Count > 0)
                {
                    dgNotice.Select(0);
                }              
            }

        }

        /// <summary>
        /// 处理确认删除对话框的结果
        /// </summary>
        private void GetAskValue(object sender, EventArgs e)
        {
            bool isDelete = (bool)((Dlg_Ask)sender).Tag;
            //确认删除
            if (isDelete)
            {
                int index = dgNotice.CurrentCell.RowNumber;
                using (DataTable nt = (DataTable)(dgNotice.DataSource))
                {
                    if (nt.Rows.Count > 0)
                    {
                        DBAction.Delete(ETableName.Announcement, "ID=" + nt.Rows[index][0].ToString());
                    }
                    InitData();
                }
            }
        }

        
        private void dgNotice_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgNotice.DataSource == null)
            {
                return;
            }
            int index = dgNotice.CurrentCell.RowNumber;
            if (((DataTable)(dgNotice.DataSource)).Rows.Count > 0)
            {
                dgNotice.Select(index);
            }
        }

        private void dgNotice_GotFocus(object sender, EventArgs e)
        {
            if (dgNotice.DataSource == null)
            {
                return;
            }
            int index = dgNotice.CurrentCell.RowNumber;
            if (((DataTable)(dgNotice.DataSource)).Rows.Count > 0)
            {
                dgNotice.Select(index);
            }
        }
       


        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Uppage_Click(object sender, EventArgs e)
        {
            BaseLibrary.SendRunInfo(LocoInfo.TrainInfo.SckTrains, "");
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNotice.DataSource == null)
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNotice.DataSource == null)
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

        //上一条
        private void btn_Up_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable nt = (DataTable)(dgNotice.DataSource))
            {
                if (nt.Rows.Count == 0)
                {
                    return;
                }
                int index = dgNotice.CurrentCell.RowNumber;//获取当前选定的行
                if (index > 0)//判断是否为第一行
                {
                    dgNotice.UnSelect(index);//取消选定行
                    dgNotice.CurrentRowIndex = index - 1;
                    dgNotice.Select(index - 1);//选择上一条
                }
            }
        }

        //下一条
        private void btn_Down_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable nt = (DataTable)(dgNotice.DataSource))
            {
                if (nt.Rows.Count == 0)
                {
                    return;
                }
                int index = dgNotice.CurrentCell.RowNumber;//获取当前选定的行
                if (index < (((DataTable)(dgNotice.DataSource)).Rows.Count - 1))//判断是否为最后一行
                {

                    dgNotice.UnSelect(index);//取消选定行
                    dgNotice.CurrentRowIndex = index + 1;
                    dgNotice.Select(index + 1);//选择下一条               
                }
            }
        }

        private void panel2_GotFocus(object sender, EventArgs e)
        {

        }

    }
}
