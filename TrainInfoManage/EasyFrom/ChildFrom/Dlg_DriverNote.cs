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
    public partial class Dlg_DriverNote : Form
    {

        //司机记事ID
        private int noteID = 0;
        public int NoteID
        {
            get { return noteID; }
            set { noteID = value; }
        }
        private int numID = 0;
        public int NumID
        {
            get { return numID; }
            set { numID = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Dlg_DriverNote()
        {
            InitializeComponent();
            //窗体全屏
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            int x = 0;
            int y = 0;
            this.Location = new Point(x, y);
        }
         /// <summary>
        /// 标题文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTitle_GotFocus(object sender, EventArgs e)
        {
            TrainForm.ShowInputPanel();
        }

        /// <summary>
        /// 内容文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContent_GotFocus(object sender, EventArgs e)
        {
            TrainForm.ShowInputPanel();
        }

         /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dlg_DriverNote_Load(object sender, EventArgs e)
        {
            if (numID == 5)
            {
                label1.Text = "车次：";
                label2.Text = "站名：";
                using (DataTable noDT = DBAction.GetDTFromSQL("select * from RunAndGroup where TrainNum=" + noteID))
                {
                    if (noDT.Rows.Count > 0)
                    {
                        txtTitle.Text = noDT.Rows[0]["TrainNum"].ToString();
                        txtContent.Text = noDT.Rows[0]["StationName"].ToString();
                    }
                    else
                    {
                        txtTitle.Text = String.Empty;
                        txtContent.Text = String.Empty;
                    }
                }
            }
            else
            {
                using (DataTable noDT = DBAction.GetDTFromSQL("select * from DriverLog where ID=" + noteID))
                {
                    if (noDT.Rows.Count > 0)
                    {
                        txtTitle.Text = noDT.Rows[0]["Title"].ToString();
                        txtContent.Text = noDT.Rows[0]["Content"].ToString();
                    }
                    else
                    {
                        txtTitle.Text = String.Empty;
                        txtContent.Text = String.Empty;
                    }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(txtTitle.Text.Trim()) || String.IsNullOrEmpty(txtContent.Text.Trim()))
            //{
            //    return;
            //}
            if (numID == 5)
            {
                LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
                using (RParams param = new RParams())
                {
                    param.Add("TrainNum", txtTitle.Text);
                    param.Add("StationName", txtContent.Text);
                    //修改
                    DBAction.Update(ETableName.RunAndGroup, "TrainNum,StationName", "TrainNum=" + noteID, param);
                    this.Close();
                }
            }
            else
            {
                LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
                using (RParams param = new RParams())
                {
                    param.Add("Title", txtTitle.Text);
                    param.Add("Content", txtContent.Text);
                    param.Add("CreateTime", DateTime.Now.ToString());
                    if (noteID == 0)
                    {
                        //添加司机记事
                        DBAction.Insert(ETableName.DriverLog, param);
                    }
                    else
                    {
                        //修改司机记事
                        DBAction.Update(ETableName.DriverLog, "Title,Content,CreateTime", "ID=" + noteID, param);
                    }
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
             LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }
    }
}