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
    public partial class Dlg_Number : Form
    {
        public Dlg_Number()
        {
            InitializeComponent();
            txtNum.Text = String.Empty;
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 输入数字
        /// </summary>
        /// <param name="i"></param>
        private void InputNum(int i)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (txtNum.Text.Length == 1 && txtNum.Text.Equals("0"))
            {
                txtNum.Text = i.ToString();
                return;
            }
            txtNum.Text = txtNum.Text + i.ToString();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Tag = null;
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (String.IsNullOrEmpty(txtNum.Text.Trim()))
            {
                return;
            }
            if (txtNum.Text.Substring(txtNum.Text.Length - 1, 1).Equals("."))
            {
                txtNum.Text = txtNum.Text.Substring(0, txtNum.Text.Length - 1);
            }
            this.Tag = txtNum.Text;
            this.Close();
        }

        /// <summary>
        /// 输入1
        /// </summary>
        private void btn_One_Click(object sender, EventArgs e)
        {
            InputNum(1);
        }

        /// <summary>
        /// 输入2
        /// </summary>
        private void btn_Two_Click(object sender, EventArgs e)
        {
            InputNum(2);
        }

        /// <summary>
        /// 输入3
        /// </summary>
        private void btn_Three_Click(object sender, EventArgs e)
        {
            InputNum(3);
        }

        /// <summary>
        /// 输入4
        /// </summary>
        private void btn_Four_Click(object sender, EventArgs e)
        {
            InputNum(4);
        }

        /// <summary>
        /// 输入5
        /// </summary>
        private void btn_Five_Click(object sender, EventArgs e)
        {
            InputNum(5);
        }

        /// <summary>
        /// 输入6
        /// </summary>
        private void btn_Six_Click(object sender, EventArgs e)
        {
            InputNum(6);
        }

        /// <summary>
        /// 输入7
        /// </summary>
        private void btn_Seven_Click(object sender, EventArgs e)
        {
            InputNum(7);
        }

        /// <summary>
        /// 输入8
        /// </summary>
        private void btn_eight_Click(object sender, EventArgs e)
        {
            InputNum(8);
        }

        /// <summary>
        /// 输入9
        /// </summary>
        private void btn_night_Click(object sender, EventArgs e)
        {
            InputNum(9);
        }

        /// <summary>
        /// 输入0
        /// </summary>
        private void btn_zero_Click(object sender, EventArgs e)
        {
            InputNum(0);
        }

        /// <summary>
        /// 后退
        /// </summary>
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (txtNum.Text.Length == 0)
            {
                return;
            }
            txtNum.Text = txtNum.Text.Substring(0, txtNum.Text.Length - 1);
        }

        /// <summary>
        /// 输入点
        /// </summary>
        private void btn_point_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (txtNum.Text.IndexOf('.') != -1)
            {
                return;
            }
            if (String.IsNullOrEmpty(txtNum.Text))
            {
                txtNum.Text = "0.";
            }
            else
            {
                txtNum.Text = txtNum.Text + ".";
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        private void Dlg_Number_Load(object sender, EventArgs e)
        {
            txtNum.Text = String.Empty;
        }



    }
}