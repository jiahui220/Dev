using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrainCommon;

namespace TrainView.ChildFrom
{
    public partial class Dlg_Connect : Form
    {
        private Dlg_Number dlg_Number = null;
        /// <summary>
        /// 补机重联ID
        /// </summary>
        private int _ReconID = 0;

        /// <summary>
        /// 补机重联ID
        /// </summary>
        public int ReconID
        {
            get { return _ReconID; }
            set { _ReconID = value; }
        }

        /// <summary>
        /// 报单ID
        /// </summary>
        private int _ReportID = 0;

        /// <summary>
        /// 报单ID
        /// </summary>
        public int ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }

        public Dlg_Connect()
        {
            InitializeComponent();
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 所属机务段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBelong_GotFocus(object sender, EventArgs e)
        {
            TrainForm.ShowInputPanel();
        }

        /// <summary>
        /// 区间公里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRegionKilo_GotFocus(object sender, EventArgs e)
        {
            if (dlg_Number == null)
            {
                dlg_Number = new Dlg_Number();
                dlg_Number.Closed += new EventHandler(GetNumber);
            }
            txtRegionKilo.Enabled = false;
            dlg_Number.ShowDialog();
        }

        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetNumber(object sender, EventArgs e)
        {
            txtRegionKilo.Enabled = true;
            string num = (string)((Dlg_Number)sender).Tag;
            if (String.IsNullOrEmpty(num))
            {
                return;
            }
            txtRegionKilo.Text = num;
        }

        /// <summary>
        /// 车型号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtType_GotFocus(object sender, EventArgs e)
        {
            TrainForm.ShowInputPanel();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dlg_Connect_Load(object sender, EventArgs e)
        {
            using (DataTable trcDT = DBAction.GetDTFromSQL("select TrainType,RegionDistance,Belong from Reconnection where ID=" + ReconID))
            {
                if (trcDT.Rows.Count > 0)
                {
                    txtType.Text = trcDT.Rows[0]["TrainType"].ToString();
                    txtRegionKilo.Text = trcDT.Rows[0]["RegionDistance"].ToString();
                    txtBelong.Text = trcDT.Rows[0]["Belong"].ToString();
                }
                else
                {
                    txtType.Text = String.Empty;
                    txtRegionKilo.Text = String.Empty;
                    txtBelong.Text = String.Empty;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Tag = null;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(txtType.Text.Trim()) || String.IsNullOrEmpty(txtRegionKilo.Text.Trim()) || String.IsNullOrEmpty(txtBelong.Text.Trim()))
            //{
            //    return;
            //}
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (RParams param = new RParams())
            {
                if (ReconID == 0)
                {
                    param.Items.Clear();
                    param.Add("RHId", ReportID);
                    param.Add("TrainType", txtType.Text);
                    param.Add("RegionDistance", txtRegionKilo.Text);
                    param.Add("Belong", txtBelong.Text);
                    DBAction.Insert(ETableName.Reconnection, param);
                }
                else
                {
                    param.Items.Clear();
                    param.Add("TrainType", txtType.Text);
                    param.Add("RegionDistance", txtRegionKilo.Text);
                    param.Add("Belong", txtBelong.Text);
                    DBAction.Update(ETableName.Reconnection, "TrainType,RegionDistance,Belong", "ID=" + ReconID, param);
                }
                this.Close();
            }

        }
    }
}