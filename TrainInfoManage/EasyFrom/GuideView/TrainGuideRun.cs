using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;

namespace TrainView.GuideView
{
    public partial class TrainGuideRun : UserControl
    {
        public TrainGuideRun()
        {
            InitializeComponent();
        }

        //上一条
        private void btn_Up_Click(object sender, EventArgs e)
        {
            //LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            //if (dgv_RunInfo.DataSource == null)
            //{
            //    return;
            //}
            //if (((DataTable)(dgv_RunInfo.DataSource)).Rows.Count > 0)
            //{
            //    int index = dgv_RunInfo.CurrentCell.RowNumber;//获取当前选定的行
            //    if (index > 0)//判断是否为第一行
            //    {
            //        dgv_RunInfo.UnSelect(index);//取消选定行
            //        dgv_RunInfo.CurrentRowIndex = index - 1;
            //        dgv_RunInfo.Select(index - 1);//选择上一条
            //    }
            //}
        }

        //下一条
        private void btn_Down_Click(object sender, EventArgs e)
        {
            //LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            //if (dgv_RunInfo.DataSource == null)
            //{
            //    return;
            //}
            //if (((DataTable)(dgv_RunInfo.DataSource)).Rows.Count > 0)
            //{
            //    int index = dgv_RunInfo.CurrentCell.RowNumber;//获取当前选定的行
            //    if (index < (((DataTable)(dgv_RunInfo.DataSource)).Rows.Count - 1))//判断是否为最后一行
            //    {

            //        dgv_RunInfo.UnSelect(index);//取消选定行
            //        dgv_RunInfo.CurrentRowIndex = index + 1;
            //        dgv_RunInfo.Select(index + 1);//选择下一条               
            //    }
            //}
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }
    }
}
