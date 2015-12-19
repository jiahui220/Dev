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
    public partial class Dlg_Ask : Form
    {
        //提示内容
        private string _AskInfo;

        /// <summary>
        /// 提示内容
        /// </summary>
        public string AskInfo
        {
            get { return _AskInfo; }
            set { _AskInfo = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Dlg_Ask()
        {
            InitializeComponent();
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }
        #region 事件
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Tag = false;
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Tag = true;
            this.Close();
        }
        #endregion

        private void Dlg_Ask_Load(object sender, EventArgs e)
        {
            lblInfo.Text = AskInfo;
        }

        private void lblInfo_ParentChanged(object sender, EventArgs e)
        {

        }

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }

    }
}