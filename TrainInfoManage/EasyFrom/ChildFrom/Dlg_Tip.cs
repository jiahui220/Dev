using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using TrainView;
using System.Windows.Forms;

namespace TrainView.ChildFrom
{
    public partial class Dlg_Tip : Form
    {
        //提示内容
        private string tipInfo;
        /// <summary>
        /// 提示内容
        /// </summary>
        public string TipInfo
        {
            get { return tipInfo; }
            set { tipInfo = value; }
        }



        public Dlg_Tip()
        {
            InitializeComponent();
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 关闭提示框
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dlg_Tip_Load(object sender, EventArgs e)
        {
            this.label2.Text = TipInfo;
        }

        private void label2_ParentChanged(object sender, EventArgs e)
        {

        }

        private void panel2_GotFocus(object sender, EventArgs e)
        {

        }

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }
    }
}