using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TrainView.AlarmView
{
    public partial class TrainAlarm : UserControl
    {
        #region 窗体变量
        /// <summary>
        /// 报警记录
        /// </summary>
        private static AlarmRecord frmRecord;
        /// <summary>
        /// 报警配置
        /// </summary>
        private static AlarmSet frmSet;
        #endregion



        public TrainAlarm()
        {
            InitializeComponent();
            btn_Record_Click(null,null);
        }

        #region 按钮事件
        /// <summary>
        /// 点击查看报警记录
        /// </summary>
        private void btn_Record_Click(object sender, EventArgs e)
        {
            if (frmRecord == null)
            {
                frmRecord = new AlarmRecord();
            }
            //设置按钮字体颜色
            SetSelectColor(0);
            frmRecord.Dock = DockStyle.Fill;
            this.pl_Alarm.Controls.Clear();
            this.pl_Alarm.Controls.Add(frmRecord);
            frmRecord.Show();
        }

        /// <summary>
        /// 点击查看项点配置
        /// </summary>
        private void btn_Set_Click(object sender, EventArgs e)
        {
            if (frmSet == null)
            {
                frmSet = new AlarmSet();
            }
            //设置按钮字体颜色
            SetSelectColor(1);
            frmSet.Dock = DockStyle.Fill;
            this.pl_Alarm.Controls.Clear();
            this.pl_Alarm.Controls.Add(frmSet);
            frmSet.Show();
        }
        #endregion


        /// <summary>
        /// 设置按钮选中字体颜色
        /// </summary>
        /// <param name="index">按钮索引</param>
        private void SetSelectColor(int index)
        {

            switch (index)
            {
                case 0:
                    btn_Record.ForeColor = Color.Blue;
                    btn_Set.ForeColor = Color.Black;
                    break;
                case 1:
                    btn_Record.ForeColor = Color.Black;
                    btn_Set.ForeColor = Color.Blue;
                    break;
                default:
                    break;
            }
        }

    }
}
