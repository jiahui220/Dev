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
    public partial class Dlg_Book : Form
    {
        //当前章节索引
        private int currChapterIndex = 0;
        //当前页
        private int currPage = 1;
        //文件路径
        DataTable dt = null;
        //总页数
        private int totalPage;
        //每页的最大长度
        private int pageMaxLength = 180;
        /// <summary>
        /// 书籍ID
        /// </summary>
        private string _BookId;


        /// <summary>
        /// 书籍ID
        /// </summary>
        public string BookId
        {
            get { return _BookId; }
            set { _BookId = value; }
        }

        public Dlg_Book()
        {
            InitializeComponent();
            //窗体居中
            //int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            //int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            //this.Location = new Point(x, y);
            //设置全屏
            SetFullScreen();
        }

        /// <summary>
        /// 设置全屏
        /// </summary>
        private void SetFullScreen()
        {
            FormCommon.HideTaskBar();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }

        #region 私有方法
        /// <summary>
        /// 读取文件内容
        /// </summary>
        private void ReadContent()
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            if (currChapterIndex > dt.Rows.Count - 1 || currChapterIndex < 0)
            {
                return;
            }
            string content = dt.Rows[currChapterIndex]["Content"].ToString().Trim();
            totalPage = content.Length / pageMaxLength + (content.Length % pageMaxLength == 0 ? 0 : 1);
            lblPage.Text = currPage + "/" + totalPage;
            if (currPage < totalPage)
            {
                txtContent.Text = content.Substring((currPage - 1) * pageMaxLength, pageMaxLength);
            }
            else
            {
                txtContent.Text = content.Substring((currPage - 1) * pageMaxLength, content.Length % pageMaxLength);
            }
        }
        #endregion

        /// <summary>
        /// 上一章
        /// </summary>
        private void btn_Up_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currChapterIndex > 0)
            {
                treeChapter.Nodes[currChapterIndex].BackColor = Color.White;
                currChapterIndex--;
                currPage = 1;
                treeChapter.Nodes[currChapterIndex].BackColor = Color.RoyalBlue;
                ReadContent();
            }
        }

        /// <summary>
        /// 下一章
        /// </summary>
        private void btn_Down_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currChapterIndex < treeChapter.Nodes.Count - 1)
            {
                treeChapter.Nodes[currChapterIndex].BackColor = Color.White;
                currChapterIndex++;
                currPage = 1;
                treeChapter.Nodes[currChapterIndex].BackColor = Color.RoyalBlue;
                ReadContent();
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void btn_prePage_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currPage > 1)
            {
                currPage--;
                ReadContent();
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void btn_nextPage_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                return;
            }
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currPage < totalPage)
            {
                currPage++;
                ReadContent();
            }
        }

        /// <summary>
        /// 章节切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeChapter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeChapter.SelectedNode != null)
            {
                currChapterIndex = treeChapter.SelectedNode.Index;
                for (int i = 0; i < treeChapter.Nodes.Count; i++)
                {
                    treeChapter.Nodes[i].BackColor = Color.White;
                }
                treeChapter.SelectedNode.BackColor = Color.RoyalBlue;
                currPage = 1;
                ReadContent();
            }
        }

        private void Dlg_Book_Load(object sender, EventArgs e)
        {
            treeChapter.Nodes.Clear();
            currChapterIndex = 0;
            currPage = 1;
            using (dt = DBAction.GetDTFromSQL("select * from Chapter where BookId = " + BookId + " and ChapterName is not null"))
            {
                if (dt.Rows.Count == 0)
                {
                    txtContent.Text = null;
                    lblPage.Text = null;
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Text = dt.Rows[i]["ChapterName"].ToString();
                    treeChapter.Nodes.Add(node);
                }
                if (treeChapter.Nodes.Count > 0)
                {
                    treeChapter.Nodes[0].BackColor = Color.RoyalBlue;
                }
                ReadContent();
            }
        }
    }
}