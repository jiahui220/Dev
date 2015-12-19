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
    public partial class Dlg_Check_Note : Form
    {
        private Dlg_Tip dlg_Tip = null;
        
        //司机记事ID
        private int noteID = 0;
        public int NoteID
        {
            get { return noteID; }
            set { noteID = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Dlg_Check_Note()
        {
            InitializeComponent();
            //窗体全屏
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            int x = 0;
            int y = 0;
            this.Location = new Point(x, y);
        }

        //显示司机记事信息
        private void ShowText(string id)
        {
            using (DataTable noteInfo = DBAction.GetDTFromSQL("select * from DriverLog where ID=" + id))
            {
                if (noteInfo.Rows.Count > 0)
                {
                    lblTitle.Text = noteInfo.Rows[0]["Title"].ToString();
                    txtContent.Text = noteInfo.Rows[0]["Content"].ToString();
                    lblTime.Text = noteInfo.Rows[0]["CreateTime"].ToString();
                }
            }
        }



        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }

        //上一条
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable niDT = DBAction.GetDTFromSQL("select * from DriverLog  where ID<" + noteID + " order by ID desc"))
            {
                if (niDT.Rows.Count > 0)
                {
                    ShowText(niDT.Rows[0][0].ToString());
                    noteID = Convert.ToInt32(niDT.Rows[0][0].ToString());
                    ShowText(noteID.ToString());
                }
                else
                {

                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是第一条！";

                    dlg_Tip.ShowDialog();
                }
            }
        }

        //下一条
        private void button3_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable niDT = DBAction.GetDTFromSQL("select * from DriverLog where ID>" + noteID))
            {
                if (niDT.Rows.Count > 0)
                {
                    ShowText(niDT.Rows[0][0].ToString());
                    noteID = Convert.ToInt32(niDT.Rows[0][0].ToString());
                    ShowText(noteID.ToString());
                }
                else
                {

                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是最后一条！";

                    dlg_Tip.ShowDialog();
                }
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dlg_Check_Note_Load_1(object sender, EventArgs e)
        {
            ShowText(noteID.ToString());
        }

        private void lblTime_ParentChanged(object sender, EventArgs e)
        {

        }

       
    }
}