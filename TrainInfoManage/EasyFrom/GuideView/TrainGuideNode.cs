using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using TrainView.ChildFrom;

namespace TrainView.GuideView
{
    public partial class TrainGuideNode : UserControl
    {
        //新建或修改对话框
        private Dlg_DriverNote dlg_Note;
        //删除询问窗体
        private Dlg_Ask dlg_Ask = null;
        private Dlg_Tip dlg_Tip = null;
        private int currentPage = 1;
        private int pageSize = 15;
        private int count = 0;
        //查看对话框
        private Dlg_Check_Note dlg_Check_Note = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TrainGuideNode()
        {
            InitializeComponent();
            count = DBAction.GetRecordCount(ETableName.DriverLog, "", new RParams());
            InitData();
        }

        //上一条
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNote.DataSource == null)
            {
                return;
            }
            using (DataTable nt = (DataTable)(dgNote.DataSource))
            {
                if (nt.Rows.Count == 0)
                {
                    return;
                }
                int index = dgNote.CurrentCell.RowNumber;//获取当前选定的行
                if (index > 0)//判断是否为第一行
                {
                    dgNote.UnSelect(index);//取消选定行
                    dgNote.CurrentRowIndex = index - 1;
                    dgNote.Select(index - 1);//选择上一条
                }
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

        //***初始化加载数据列表
        private void InitData()
        {

            using (DataTable noDt = DBAction.GetPageDT(ETableName.DriverLog, "*", "", currentPage, pageSize, "CreateTime"))
            {
                if (noDt.Rows.Count > 0)
                {
                    noDt.TableName = "Note";
                    dgNote.DataSource = noDt;

                    if (noDt.Rows.Count > 0)
                    {
                        dgNote.Select(0);
                    }
                }
            }
        }

        private void dgNote_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgNote.DataSource == null)
            {
                return;
            }
            int index = dgNote.CurrentCell.RowNumber;
            if (((DataTable)(dgNote.DataSource)).Rows.Count > 0)
            {
                dgNote.Select(index);
            }
        }
        private void dgNote_GotFocus(object sender, EventArgs e)
        {
            if (dgNote.DataSource == null)
            {
                return;
            }
            int index = dgNote.CurrentCell.RowNumber;
            if (((DataTable)(dgNote.DataSource)).Rows.Count > 0)
            {
                dgNote.Select(index);
            }
        }

        /// <summary>
        /// 新建司机记事
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_New_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dlg_Note == null)
            {
                dlg_Note = new Dlg_DriverNote();
                dlg_Note.Closed += new EventHandler(GetNoteValue);
            }
            dlg_Note.NoteID = 0;
            dlg_Note.ShowDialog();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Amend_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable dt = (DataTable)dgNote.DataSource)
            {
                int index = dgNote.CurrentCell.RowNumber;
                if (dt.Rows.Count > 0)
                {
                    if (dlg_Note == null)
                    {
                        dlg_Note = new Dlg_DriverNote();
                        dlg_Note.Closed += new EventHandler(GetNoteValue);
                    }
                    string id = dt.Rows[index][0].ToString();
                    dlg_Note.NoteID = Convert.ToInt32(id);
                    dlg_Note.ShowDialog();
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
                int index = dgNote.CurrentCell.RowNumber;
                using (DataTable nt = (DataTable)(dgNote.DataSource))
                {
                    if (nt.Rows.Count > 0)
                    {
                        DBAction.Delete(ETableName.DriverLog, "ID=" + nt.Rows[index][0].ToString());
                    }
                    InitData();
                }
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable dt = (DataTable)dgNote.DataSource)
            {
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                if (dlg_Ask == null)
                {
                    dlg_Ask = new Dlg_Ask();
                    dlg_Ask.AskInfo = "是否确认删除选择的司机记事信息？";
                    dlg_Ask.Closed += new EventHandler(GetAskValue);
                }
                dlg_Ask.ShowDialog();
            }
        }

        private void btn_Look_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable nt = (DataTable)(dgNote.DataSource))
            {
                int index = dgNote.CurrentCell.RowNumber;
                if (nt.Rows.Count > 0)
                {
                    if (dlg_Check_Note == null)
                    {
                        dlg_Check_Note = new Dlg_Check_Note();
                    }
                    string id = nt.Rows[index][0].ToString();
                    dlg_Check_Note.NoteID = Convert.ToInt32(id);
                    dlg_Check_Note.ShowDialog();
                }
            }
        }

        //上一页
        private void btn_Uppage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNote.DataSource == null)
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

        //下一页
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNote.DataSource == null)
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

        //下一条
        private void btn_Down_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgNote.DataSource == null)
            {
                return;
            }
            using (DataTable nt = (DataTable)(dgNote.DataSource))
            {
                if (nt.Rows.Count == 0)
                {
                    return;
                }
                int index = dgNote.CurrentCell.RowNumber;//获取当前选定的行
                if (index < (((DataTable)(dgNote.DataSource)).Rows.Count - 1))//判断是否为最后一行
                {

                    dgNote.UnSelect(index);//取消选定行
                    dgNote.CurrentRowIndex = index + 1;
                    dgNote.Select(index + 1);//选择下一条               
                }
            }
        }

        private void panel2_GotFocus(object sender, EventArgs e)
        {

        }

    }
}
