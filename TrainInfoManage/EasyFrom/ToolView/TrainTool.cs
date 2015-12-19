using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TrainView.ToolView
{
    public partial class TrainTool : UserControl
    {
        #region 变量
        /// <summary>
        /// 系统配置
        /// </summary>
        private static TrainToolSet frmSet;
        /// <summary>
        /// 系统信息
        /// </summary>
        private static TrainToolInfo frmTool;
        #endregion


        public TrainTool()
        {
            InitializeComponent();
            //默认呈现系统配置
            btn_Set_Click(null,null);
        }

        /// <summary>
        /// 点击系统配置
        /// </summary>
        private void btn_Set_Click(object sender, EventArgs e)
        {
            if (frmSet == null)
            {
                frmSet = new TrainToolSet();
            }
            frmSet.Dock = DockStyle.Fill;
            this.pl_Tool.Controls.Clear();
            this.pl_Tool.Controls.Add(frmSet);
            frmSet.Show();
        }

        /// <summary>
        /// 系统信息
        /// </summary>
        private void btn_Info_Click(object sender, EventArgs e)
        {
            if (frmTool == null)
            {
                frmTool = new TrainToolInfo();
            }
            frmTool.Dock = DockStyle.Fill;
            this.pl_Tool.Controls.Clear();
            this.pl_Tool.Controls.Add(frmTool);
            frmTool.Show();
        }

        /// <summary>
        /// 设置按钮选中字体颜色
        /// </summary>
        /// <param name="index">按钮索引</param>
        private void SetSelectColor(int index)
        {
            switch (index)
            {
                case 0:
                    btn_Set.ForeColor = Color.Blue;
                    btn_Info.ForeColor = Color.Black;
                    break;
                case 1:
                    btn_Set.ForeColor = Color.Black;
                    btn_Info.ForeColor = Color.Blue;
                    break;
                default:
                    break;
            }
        }
        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }
    }
}
