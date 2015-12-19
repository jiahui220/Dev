using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TrainCommon;
using System.Threading;
using TrainView.ChildFrom;
using System.Reflection;

namespace TrainView.GuideView
{
    public partial class TrainGuideEbook : UserControl
    {
        private int pageBookNum = 8;
        private int barHeight = 59;
        //最大列数
        private int maxColNum = 4;
        //最大行数
        private int maxRowNum = 2;
        //书的总数量
        private int totalBook = 0;
        //当前页
        private int currPage = 1;
        //总页数
        private int totalPage;
        //当没有书时显示的提示图片
        private PictureBox picNoBook = null;
        //图书按钮
        private Button btnBook = null;
        //按钮宽度
        private int btnBookWidth = 46;
        //按钮高度
        private int btnBookHeight =70;
        //书名标签
        private Label lblBookName = null;
        //所有书籍编号
        private string[] bookId = null;
        //所有书籍名称
        private string[] bookName = null;
        //选择章节对话框
        private Dlg_Book dlg_Book = null;
        private Dlg_Tip dlg_Tip = null;
        //private WebTrainService.TrainService wts = null;
        
        private bool isQuery = false;
        private Thread updateThread = null;

        private Thread loadDataTh;

       
        public TrainGuideEbook()
        {
            InitializeComponent();
            lblPage.Text = "数据加载中......";
            lblPage.ForeColor = Color.Red;
            loadDataTh = new Thread(loadData);
            loadDataTh.Priority = ThreadPriority.Normal;
            loadDataTh.Start();
        }


        private void loadData()
        {
            //读取数据库文件电子书的信息
            ReadBook("select * from Book where BookName is not null");
            //加载书籍图标及文字
            LoadBook();
        }

        private void TrainGuideEbook_Click(object sender, EventArgs e)
        {

        }

        #region 数据加载
        /// <summary>
        /// 读取数据库文件电子书的信息
        /// </summary>
        private void ReadBook(string sql)
        {
            using (DataTable dt = DBAction.GetDTFromSQL(sql))
            {
                totalBook = dt.Rows.Count;
                if (totalBook == 0)
                {
                    return;
                }
                bookId = new string[totalBook];
                bookName = new string[totalBook];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bookId[i] = dt.Rows[i]["BookId"].ToString();
                    bookName[i] = dt.Rows[i]["BookName"].ToString();
                }
            }

        }
        #endregion

        #region 自定义事件函数
        /// <summary>
        /// 按钮点击事件--->弹出选择章节对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBook(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            string bookId = ((Button)sender).Tag.ToString();
            bookId = bookId.Substring(bookId.IndexOf('_') + 1);
            if (!DBAction.HasData("Book", "BookId=" + bookId, new RParams()))
            {
                dlg_Tip = new Dlg_Tip();
                dlg_Tip.TipInfo = "书籍内容正在更新中，请稍后阅览";
                dlg_Tip.ShowDialog();
                return;
            }
            if (dlg_Book == null)
            {
                dlg_Book = new Dlg_Book();
            }
            dlg_Book.BookId = bookId;
            btnQuery.Focus();
            dlg_Book.ShowDialog();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="i">索引</param>
        /// <param name="x">坐标x</param>
        /// <param name="y">坐标y</param>
        private void AddBtnBook(int i, int x, int y)
        {
            btnBook = new Button();
            btnBook.Size = new Size(btnBookWidth, btnBookHeight);
            btnBook.Tag = "book_" + bookId[(currPage - 1) * pageBookNum + i];
            btnBook.Text = "书籍";
            btnBook.Location = new Point(x, y);
            btnBook.Click += new System.EventHandler(OpenBook);
            pnlBook.Controls.Add(btnBook);
        }
        #endregion


        /// <summary>
        /// 获取嵌入资源的图片
        /// </summary>
        /// <param name="name">图片名</param>
        private Bitmap GetImage(string name)
        {
            string imageDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\images\\";
            return new Bitmap(imageDirectory + name);
        }

        /// <summary>
        /// 加载书籍
        /// </summary>
        private void LoadBook()
        {
            this.Invoke(new EventHandler(delegate {

                pnlBook.Controls.Clear();
                #region 无任何书籍
                if (totalBook == 0)
                {
                    picNoBook = new PictureBox();
                    picNoBook.Image = GetImage("book_no.png");
                    picNoBook.Size = new Size(105, 112);
                    //将图片置于中间
                    picNoBook.Location = new Point((pnlBook.Width - picNoBook.Width) / 2, (pnlBook.Height - picNoBook.Height) / 2);
                    pnlBook.Controls.Add(picNoBook);
                    //设置标签
                    lblBookName = new Label();
                    lblBookName.Size = new Size((pnlBook.Width - picNoBook.Width) / 2 + picNoBook.Width, 20);
                    lblBookName.Text = "当前还没有书籍哦~";
                    if (isQuery)
                    {
                        lblBookName.Text = "没有查询到相关的书籍哦~";
                    }
                    lblBookName.ForeColor = Color.FromArgb(150, 136, 128);
                    lblBookName.TextAlign = ContentAlignment.TopCenter;
                    int x = picNoBook.Location.X - (pnlBook.Width - picNoBook.Width) / 4;
                    lblBookName.Location = new Point(x, picNoBook.Location.Y + picNoBook.Height);
                    pnlBook.Controls.Add(lblBookName);
                    return;
                }
                #endregion
                totalPage = totalBook / pageBookNum + (totalBook % pageBookNum == 0 ? 0 : 1);
                if (totalPage > 1)
                {
                    //btnPrePage.Visible = true;
                    //btnNextPage.Visible = true;
                }
                //初始化图书按钮
                //设置页信息
                lblPage.Text = currPage + "/" + totalPage;
                //水平间距
                int horizontalDis = 0;
                //垂直间距
                int verticalDis = 0;
                if (currPage < totalPage)
                {
                    //2行4列，共8本书
                    horizontalDis = (pnlBook.Width - maxColNum * btnBookWidth) / (maxColNum + 1);
                    verticalDis = (pnlBook.Height - maxRowNum * btnBookHeight) / (maxRowNum + 1);
                    for (int i = 0; i < pageBookNum; i++)
                    {
                        //计算按钮的坐标
                        int x = (i % maxColNum + 1) * horizontalDis + (i % maxColNum) * btnBookWidth;
                        int y = (i / maxColNum + 1) * verticalDis + (i / maxColNum) * btnBookHeight;
                        //添加按钮
                        AddBtnBook(i, x, y);
                        //书名标签
                        AddLabelBookName(i, horizontalDis, x, y);
                    }
                }
                else if (currPage == totalPage)
                {
                    if (totalPage == 1)
                    {
                        //btnPrePage.Visible = false;
                        //btnNextPage.Visible = false;
                        lblPage.Text = "";
                    }
                    //最后一页剩余的书籍数量
                    int lastBookNum = totalBook - (totalPage - 1) * pageBookNum;
                    //书的数量小于等于3本时，只显示一排
                    if (lastBookNum <= 3)
                    {
                        //1行lastBookNum列，共lastBookNum本书
                        horizontalDis = (pnlBook.Width - lastBookNum * btnBookWidth) / (lastBookNum + 1);
                        verticalDis = (pnlBook.Height - btnBookHeight) / 2;
                        for (int i = 0; i < lastBookNum; i++)
                        {
                            //计算按钮的坐标
                            int x = (i % lastBookNum + 1) * horizontalDis + (i % lastBookNum) * btnBookWidth;
                            int y = verticalDis;
                            //添加按钮
                            AddBtnBook(i, x, y);
                            //添加书名标签
                            AddLabelBookName(i, horizontalDis, x, y);
                        }
                    }
                    else
                    {
                        //2行，第一行firstColNum列，第二行secondColNum列，共lastBookNum本书
                        int firstColNum = lastBookNum / 2 + (lastBookNum % 2 == 0 ? 0 : 1);
                        //int secondColNum = lastBookNum - firstColNum;
                        horizontalDis = (pnlBook.Width - firstColNum * btnBookWidth) / (firstColNum + 1);
                        verticalDis = (pnlBook.Height - maxRowNum * btnBookHeight) / (maxRowNum + 1);
                        for (int i = 0; i < lastBookNum; i++)
                        {
                            //计算按钮的坐标
                            int x = (i % firstColNum + 1) * horizontalDis + (i % firstColNum) * btnBookWidth;
                            int y = (i / firstColNum + 1) * verticalDis + (i / firstColNum) * btnBookHeight;
                            //添加按钮
                            AddBtnBook(i, x, y);
                            //添加书名标签
                            AddLabelBookName(i, horizontalDis, x, y);
                        }
                    }
                }
            
            }));

        }

        /// <summary>
        /// 添加书名标签
        /// </summary>
        /// <param name="i">循环索引</param>
        /// <param name="horizontalDis">按钮间的水平间距</param>
        /// <param name="x">按钮的坐标x</param>
        /// <param name="y">按钮的坐标y</param>
        private void AddLabelBookName(int i, int horizontalDis, int x, int y)
        {
            lblBookName = new Label();
            lblBookName.ForeColor = Color.Black;
            lblBookName.Text = bookName[(currPage - 1) * pageBookNum + i];
            lblBookName.TextAlign = ContentAlignment.TopCenter;
            lblBookName.Name = "name_" + bookId[(currPage - 1) * pageBookNum + i];
            lblBookName.Size = new Size(btnBookWidth + horizontalDis, 50);
            lblBookName.Location = new Point(x - horizontalDis / 2, y + btnBookHeight);
            pnlBook.Controls.Add(lblBookName);
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            //DoUpdate();
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            //btnQuery.Focus();
            if (String.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                return;
            }
            isQuery = true;
            //读取数据库文件电子书的信息
            string sql = "select DISTINCT Book.ID,Book.BookId,BookName,Book.Latest from Book,Chapter where Book.BookId=Chapter.BookId and (BookName LIKE '%" + txtKey.Text.Trim() + "%' OR ChapterName LIKE '%" + txtKey.Text.Trim() + "%' OR Content LIKE '%" + txtKey.Text.Trim() + "%')";
            ReadBook(sql);
            //加载书籍图标及文字
            LoadBook();
            //btnBack.Visible = true;
        }


        /// <summary>
        /// 返回主页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Return_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            isQuery = false;
            txtKey.Text = String.Empty;
            ReadBook("select * from Book where BookName is not null");
            //加载书籍图标及文字
            LoadBook();
            //btnBack.Visible = false;
            //btnQuery.Focus();
        }

        #region 翻页按钮
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Uppage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currPage == 1)
            {
                return;
            }
            currPage--;
            LoadBook();
        }
        #endregion

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (currPage == totalPage)
            {
                return;
            }
            currPage++;
            LoadBook();
        }

        private void DoUpdate()
        {
            LocoInfo.dataConnect = 1;
            UpdateBook.SendAllBookId(LocoInfo.TrainInfo.SckTrains);
            LocoInfo.dataConnect = 0;
            //Thread.Sleep(500);
        }

        //手动检测更新
        private void btn_Update_Click(object sender, EventArgs e)
        {
            updateThread = new Thread(new ThreadStart(DoUpdate));
            updateThread.IsBackground = true;
            updateThread.Start();
        }

        private void pnlBook_GotFocus(object sender, EventArgs e)
        {

        }
    }
}
