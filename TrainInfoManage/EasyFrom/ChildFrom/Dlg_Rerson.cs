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
    public partial class Dlg_Rerson : Form
    {
        //司机记事ID
        private int noteID = 0;
        public int NoteID
        {
            get { return noteID; }
            set { noteID = value; }
        }
        //构造函数
        public Dlg_Rerson()
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
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dlg_Rerson_Load(object sender, EventArgs e)
        {
            using (DataTable noDT = DBAction.GetDTFromSQL("select * from AlarmLog where ID=" + noteID))
            {
                if (noDT.Rows.Count > 0)
                {
                    txtTitle.Text = noDT.Rows[0]["AlarmItem"].ToString();
                    txtContent.Text = noDT.Rows[0]["AlarmIntro"].ToString();
                }
                else
                {
                    txtTitle.Text = String.Empty;
                    txtContent.Text = String.Empty;
                }
            }
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
        //确定添加
        private void button1_Click_1(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (RParams param = new RParams())
            {
                param.Add("AlarmItem", txtTitle.Text);
                param.Add("AlarmIntro", txtContent.Text);
                param.Add("CreateTime", DateTime.Now.ToString());
                //修改司机记事
                DBAction.Update(ETableName.AlarmLog, "CreateTime,AlarmItem,AlarmIntro", "ID=" + noteID, param);
                this.Close();
            }
        }
        //取消操作
        private void button2_Click_1(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
     
        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContent_TextChanged_1(object sender, EventArgs e)
        {

        }

        
    }
}