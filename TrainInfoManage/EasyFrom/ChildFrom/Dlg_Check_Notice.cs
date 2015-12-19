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
    public partial class Dlg_Check_Notice : Form
    {

        private Dlg_Tip dlg_Tip = null;
        //通知公告信息

        private int noticeID = 0;

        public int NoticeID
        {
            get { return noticeID; }
            set { noticeID = value; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public Dlg_Check_Notice()
        {
            InitializeComponent();
            //窗体全屏
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            int x = 0;
            int y = 0;
            this.Location = new Point(x, y);
        }

       
      
        private void Dlg_Check_Notice_Load(object sender, EventArgs e)
        {
            ShowText(noticeID.ToString());
        }

        //显示通知公告信息
        private void ShowText(string id)
        {
            using (DataTable noteInfo = DBAction.GetDTFromSQL("select * from Announcement where ID=" + id))
            {
                if (noteInfo.Rows.Count > 0)
                {
                    lblPeople.Text = noteInfo.Rows[0]["SendPerson"].ToString();
                    lblTime.Text = noteInfo.Rows[0]["ReceTime"].ToString();
                    lblTitle.Text = noteInfo.Rows[0]["Title"].ToString();
                    txtContent.Text = noteInfo.Rows[0]["AnnoContent"].ToString();
                }
            }

        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }

        //下一条
        private void button2_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable niDT = DBAction.GetDTFromSQL("select ID from Announcement where ID<" + noticeID))
            {
                if (niDT.Rows.Count > 0)
                {
                    ShowText(niDT.Rows[niDT.Rows.Count - 1][0].ToString());
                    noticeID = Convert.ToInt32(niDT.Rows[niDT.Rows.Count - 1][0].ToString());
                    //ShowText(noticeID.ToString());
                }
                else
                {

                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是最后一条！";

                    dlg_Tip.ShowDialog();
                }
            }
        }

        //上一条
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            using (DataTable niDT = DBAction.GetDTFromSQL("select ID from " + ETableName.Announcement.ToString() + "  where ID>" + noticeID))
            {
                if (niDT.Rows.Count > 0)
                {
                    ShowText(niDT.Rows[0][0].ToString());
                    noticeID = Convert.ToInt32(niDT.Rows[0][0].ToString());
                    //ShowText(noticeID.ToString());
                }
                else
                {

                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是第一条！";

                    dlg_Tip.ShowDialog();
                }
            }
        }

        private void lblPeople_ParentChanged(object sender, EventArgs e)
        {

        }
    }
}