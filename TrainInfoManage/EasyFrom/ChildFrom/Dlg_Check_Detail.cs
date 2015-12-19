using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.IO;

namespace TrainView.ChildFrom
{
    public partial class Dlg_Check_Detail : Form
    {

        private double zoomFactor = 0.2;
        private double moveFactor = 0.2;
        public Dlg_Check_Detail()
        {
            InitializeComponent();
        }

        



        /// <summary>
        /// 图片集合
        /// </summary>
        private DataTable imgDt = new DataTable();

        public DataTable ImgDt
        {
            get { return imgDt; }
            set { imgDt = value; }
        }

        /// <summary>
        /// 当前图片在集合中的行索引
        /// </summary>
        private int imgIndex = -1;

        public int ImgIndex
        {
            get { return imgIndex; }
            set { imgIndex = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("hello");
        }

        private void InitPic()
        {
            if (imgDt != null && imgDt.Rows.Count > 0 && imgIndex > -1)
            {
                //MessageBox.Show("索引值"+imgIndex);
                string picPath = imgDt.Rows[imgIndex]["FilePath"].ToString();
                string picName = imgDt.Rows[imgIndex]["PhotoName"].ToString();
                //lblTitle.Text = picName;
                //string picPath = "images/alarm_down.png";
                Image currImage = GetImage(picPath);
                //MessageBox.Show("3");
                if (currImage == null || string.IsNullOrEmpty(picPath))
                {
                    //MessageBox.Show("图片未下载");

                    currImage = GetImage("TRAINFOERROR.JPG");
                }
                if (currImage != null)
                {
                    picBox.Size = currImage.Size;
                    picBox.Image = currImage;
                    PicCenter();
                }
            }
        }

        /// <summary>
        /// 根据图片名称获取图片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Bitmap GetImage(string name)
        {
            string detailDirectory = TrainForm.basePath + "\\Details\\";
            string filePath = detailDirectory + name;
            if (File.Exists(filePath))
            {
                //MessageBox.Show("1----->" + filePath);
                return new Bitmap(filePath);
            }
            else
            {
                //MessageBox.Show("2");
                return null;
            }

        }


        /// <summary>
        /// 图片居中
        /// </summary>
        private void PicCenter()
        {
            int x = 0;
            int y = 0;
            if (picBox.Width < pnlPic.Width)
            {
                x = (pnlPic.Width - picBox.Width) / 2;
            }
            if (picBox.Height < pnlPic.Height)
            {
                y = (pnlPic.Height - picBox.Height) / 2;
            }
            picBox.Location = new Point(x, y);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Width > pnlPic.Width * 3 || picBox.Height > pnlPic.Height * 3)
            {
                return;
            }
            picBox.Width = (int)(picBox.Width * (1 + zoomFactor));
            picBox.Height = (int)(picBox.Height * (1 + zoomFactor));
            PicCenter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Width < 50 || picBox.Height < 50)
            {
                return;
            }
            picBox.Width = (int)(picBox.Width * (1 - zoomFactor));
            picBox.Height = (int)(picBox.Height * (1 - zoomFactor));
            PicCenter();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Height < pnlPic.Height)
            {
                return;
            }
            int y = picBox.Location.Y - (int)(picBox.Height * moveFactor);
            if (y <= pnlPic.Height - picBox.Height)
            {
                y = pnlPic.Height - picBox.Height;
            }
            picBox.Location = new Point(picBox.Location.X, y);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Height < pnlPic.Height)
            {
                return;
            }
            int y = picBox.Location.Y + (int)(picBox.Height * moveFactor);
            if (y >= 0)
            {
                y = 0;
            }
            picBox.Location = new Point(picBox.Location.X, y);
        }

        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Width < pnlPic.Width)
            {
                return;
            }
            int x = picBox.Location.X - (int)(picBox.Width * moveFactor);
            if (x <= pnlPic.Width - picBox.Width)
            {
                x = pnlPic.Width - picBox.Width;
            }
            picBox.Location = new Point(x, picBox.Location.Y);
        }

        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (picBox.Width < pnlPic.Width)
            {
                return;
            }
            int x = picBox.Location.X + (int)(picBox.Width * moveFactor);
            if (x >= 0)
            {
                x = 0;
            }
            picBox.Location = new Point(x, picBox.Location.Y);
        }

        private void Dlg_Check_Detail_Load(object sender, EventArgs e)
        {
            InitPic();
            //窗体全屏
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            int x = 0;
            int y = 0;
            this.Location = new Point(x, y);
        }

        private void btn_dd_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            this.Close();
        }
    }
}