using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using TrainView.ChildFrom;

namespace TrainView.AlarmView
{
    /// <summary>
    /// 报警记录
    /// </summary>
    public partial class AlarmRecord : UserControl
    {
        //修改对话框
        private Dlg_DriverNote dlg_Rer;

        private Dlg_Tip dlg_Tip = null;
        private int currentPage = 1;
        private int pageSize = 15;
        private int count = 0;
        public AlarmRecord()
        {
            InitializeComponent();
            count = DBAction.GetRecordCount(ETableName.AlarmLog, "", new RParams());
            InitData();
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

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        private void InitData()
        {

            using (DataTable alt = DBAction.GetPageDT(ETableName.AlarmLog, "ID,CreateTime,AlarmItem,AlarmIntro", "", currentPage, pageSize, "CreateTime"))
            {
                if (alt.Rows.Count > 0)
                {
                    alt.TableName = "Alt";
                    dataGrid1.DataSource = alt;
                    dataGrid1.Select(0);
                }
            }
        }

        //上一页
        private void button3_Click_1(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dataGrid1.DataSource == null)
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
        private void button4_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dataGrid1.DataSource == null)
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
                    dlg_Tip.TipInfo = "当前已经是最后一页！";
                
                dlg_Tip.ShowDialog();
            }
        }

        //上一条
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dataGrid1.DataSource == null)
            {
                return;
            }
            int index = dataGrid1.CurrentCell.RowNumber;//获取当前选定的行
            if (index > 0)//判断是否为第一行
            {
                dataGrid1.UnSelect(index);//取消选定行
                dataGrid1.CurrentRowIndex = index - 1;
                dataGrid1.Select(index - 1);//选择上一条
            }
        }

        //下一条
        private void button2_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dataGrid1.DataSource == null)
            {
                return;
            }
            int index = dataGrid1.CurrentCell.RowNumber;//获取当前选定的行
            if (index < (((DataTable)(dataGrid1.DataSource)).Rows.Count - 1))//判断是否为最后一行
            {

                dataGrid1.UnSelect(index);//取消选定行
                dataGrid1.CurrentRowIndex = index + 1;
                dataGrid1.Select(index + 1);//选择下一条               
            }
        }
        #region 设置选定整行
        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGrid1.DataSource == null)
            {
                return;
            }
            int index = dataGrid1.CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                dataGrid1.Select(index);
            }
        }

        private void dataGrid1_GotFocus(object sender, EventArgs e)
        {
            if (dataGrid1.DataSource == null)
            {
                return;
            }
            int index = dataGrid1.CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                dataGrid1.Select(index);
            }
        }
        #endregion

        //private void btn_Amend_Click(object sender, EventArgs e)
        //{
        //    LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
        //    using (DataTable dt = (DataTable)dataGrid1.DataSource)
        //    {
        //        int index = dataGrid1.CurrentCell.RowNumber;
        //        if (dt.Rows.Count > 0)
        //        {
        //            if (dlg_Rer == null)
        //            {
        //                dlg_Rer = new Dlg_DriverNote();
        //                dlg_Rer.Closed += new EventHandler(GetNoteValue);
        //            }
        //            string id = dt.Rows[index][0].ToString();
        //            dlg_Rer.NoteID = Convert.ToInt32(id);
        //            dlg_Rer.ShowDialog();
        //        }
        //    }
        //}

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }

        
    }
}
