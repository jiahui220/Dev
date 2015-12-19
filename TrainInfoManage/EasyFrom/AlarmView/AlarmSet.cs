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
    /// 报警配置
    /// </summary>
    public partial class AlarmSet : UserControl
    {
        private Dlg_Tip dlg_Tip = null;
        private int currentPage = 1;
        private int pageSize = 15;
        private int count = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AlarmSet()
        {
            InitializeComponent();
            count = DBAction.GetRecordCount(" AlarmCfg ", "", new RParams());
            BindData();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            using (DataTable wt = DBAction.GetPageDT(" AlarmCfg ", "ID,AlarmItem,AlarmIntro", "", currentPage, pageSize))
            {
                wt.TableName = "AlSet";
                dataGrid1.DataSource = wt;

                //指定默认选择行
                if (wt.Rows.Count > 0)
                {
                    dataGrid1.Select(0);
                    ShowText(wt.Rows[0][0].ToString());
                }
            }
        }

        /// <summary>
        /// 显示选择的信息
        /// </summary>
        /// <param name="wID"></param>
        /// <summary>
        private void ShowText(string wID)
        {

            using (DataTable alInfo = DBAction.GetDTFromSQL(" select * from " + ETableName.AlarmCfg.ToString() + " where ID=" + wID))
            {
                if (alInfo.Rows.Count > 0)
                {
                    lblId.Text = alInfo.Rows[0]["ID"].ToString();
                    lblName.Text = alInfo.Rows[0]["AlarmItem"].ToString();
                    lblContent.Text = alInfo.Rows[0]["AlarmIntro"].ToString();
                    lblNum.Text = alInfo.Rows[0]["ItemNum"].ToString();
                    //if (alInfo.Rows[0]["IsOpen"].ToString() == "1")
                    //{
                    //    btnState.IsOn = true;
                    //}
                    //else
                    //{
                    //    btnState.IsOn = false;
                    //}
                }
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
                using (DataTable wt = DBAction.GetPageDT(" AlarmCfg ", "ID,AlarmItem,AlarmIntro", "", currentPage, pageSize)) 
                {
                    ShowText(wt.Rows[index + 1][0].ToString());
                }
                
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
                using (DataTable wt = DBAction.GetPageDT(" AlarmCfg ", "ID,AlarmItem,AlarmIntro", "", currentPage, pageSize))
                {
                    ShowText(wt.Rows[index - 1][0].ToString());
                }
            }
        }

        //上一页
        private void button3_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dataGrid1.DataSource == null)
            {
                return;
            }
            if (currentPage > 1)
            {
                currentPage = currentPage - 1;
                BindData();
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
                BindData();
            }
            else
            {
               
                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是最后一页！";
                
                dlg_Tip.ShowDialog();
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
                using (DataTable wt = DBAction.GetPageDT(" AlarmCfg ", "ID,AlarmItem,AlarmIntro", "", currentPage, pageSize))
                {
                    ShowText(wt.Rows[index][0].ToString());
                }
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
    }
}
