using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;

namespace TrainView.GuideView
{
    public partial class TrainGuide : UserControl
    {
        #region 窗体变量
        /// <summary>
        /// 运行揭示
        /// </summary>
        private static TrainGuideRun frmRun;
        /// <summary>
        /// 电子书
        /// </summary>
        private static TrainGuideEbook frmBook;
        /// <summary>
        /// 通知公告
        /// </summary>
        private static TrainGuideNotice frmNotice;
        /// <summary>
        /// 司机记事
        /// </summary>
        private static TrainGuideNode frmNode;
        /// <summary>
        /// 媒体资料
        /// </summary>
        private static TrainGuideVideo frmMedio;
        /// <summary>
        /// 施工图
        /// </summary>
        private static TrainGuiDetail frmDetail;
        #endregion

        public TrainGuide()
        {
            InitializeComponent();
            btn_Run_Click(null, null);
        }

        #region 按钮事件
        /// <summary>
        /// 点击查看运行揭示
        /// </summary>
        private void btn_Run_Click(object sender, EventArgs e)
        {
            if (frmRun == null)
            {
                frmRun = new TrainGuideRun();
            }
            //设置按钮字体
            SetSelectColor(0);
            frmRun.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmRun);
            frmRun.Show();
        }

        /// <summary>
        /// 点击查看电子书
        /// </summary>
        private void btn_Book_Click(object sender, EventArgs e)
        {
            if (frmBook == null)
            {
                frmBook = new TrainGuideEbook();
            }
            //设置按钮字体
            SetSelectColor(1);
            frmBook.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmBook);
            frmBook.Show();
        }

        /// <summary>
        /// 点击查看通知公告
        /// </summary>
        public void btn_Notice_Click(object sender, EventArgs e)
        {
            BaseLibrary.SendRunInfo(LocoInfo.TrainInfo.SckTrains, "");
            if (frmNotice == null)
            {
                frmNotice = new TrainGuideNotice();
            }
            //设置按钮字体
            SetSelectColor(2);
            frmNotice.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmNotice);
            frmNotice.Show();
        }

        /// <summary>
        /// 点击查看司机记事
        /// </summary>
        private void btn_Note_Click(object sender, EventArgs e)
        {
            if (frmNode == null)
            {
                frmNode = new TrainGuideNode();
            }
            //设置按钮字体
            SetSelectColor(3);
            frmNode.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmNode);
            frmNode.Show();
        }

        /// <summary>
        /// 点击查看媒体资料
        /// </summary>
        private void btn_Medio_Click(object sender, EventArgs e)
        {
            if (frmMedio == null)
            {
                frmMedio = new TrainGuideVideo();
            }
            //设置按钮字体
            SetSelectColor(4);
            frmMedio.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmMedio);
            frmMedio.Show();
        }

        /// <summary>
        /// 点击查看施工图
        /// </summary>
        private void btn_Pic_Click(object sender, EventArgs e)
        {
            if (frmDetail == null)
            {
                frmDetail = new TrainGuiDetail();
            }
            //设置按钮字体
            SetSelectColor(5);
            frmDetail.Dock = DockStyle.Fill;
            this.pl_Guide.Controls.Clear();
            this.pl_Guide.Controls.Add(frmDetail);
            frmDetail.Show();

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
                    btn_Run.ForeColor = Color.Blue;
                    btn_Book.ForeColor = Color.Black;
                    btn_Notice.ForeColor = Color.Black;
                    btn_Note.ForeColor = Color.Black;
                    btn_Medio.ForeColor = Color.Black;
                    btn_Pic.ForeColor = Color.Black;
                    break;
                case 1:
                    btn_Run.ForeColor = Color.Black;
                    btn_Book.ForeColor = Color.Blue;
                    btn_Notice.ForeColor = Color.Black;
                    btn_Note.ForeColor = Color.Black;
                    btn_Medio.ForeColor = Color.Black;
                    btn_Pic.ForeColor = Color.Black;
                    break;
                case 2:
                    btn_Run.ForeColor = Color.Black;
                    btn_Book.ForeColor = Color.Black;
                    btn_Notice.ForeColor = Color.Blue;
                    btn_Note.ForeColor = Color.Black;
                    btn_Medio.ForeColor = Color.Black;
                    btn_Pic.ForeColor = Color.Black;
                    break;
                case 3:
                    btn_Run.ForeColor = Color.Black;
                    btn_Book.ForeColor = Color.Black;
                    btn_Notice.ForeColor = Color.Black;
                    btn_Note.ForeColor = Color.Blue;
                    btn_Medio.ForeColor = Color.Black;
                    btn_Pic.ForeColor = Color.Black;
                    break;
                case 4:
                    btn_Run.ForeColor = Color.Black;
                    btn_Book.ForeColor = Color.Black;
                    btn_Notice.ForeColor = Color.Black;
                    btn_Note.ForeColor = Color.Black;
                    btn_Medio.ForeColor = Color.Blue;
                    btn_Pic.ForeColor = Color.Black;
                    break;
                case 5:
                    btn_Run.ForeColor = Color.Black;
                    btn_Book.ForeColor = Color.Black;
                    btn_Notice.ForeColor = Color.Black;
                    btn_Note.ForeColor = Color.Black;
                    btn_Medio.ForeColor = Color.Black;
                    btn_Pic.ForeColor = Color.Blue;
                    break;
                default:
                    break;
            }
        }

    }
}
