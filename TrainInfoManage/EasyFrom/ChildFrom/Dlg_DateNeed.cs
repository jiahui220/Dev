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
    public partial class Dlg_DateNeed : Form
    {
        private string _Tip;
        /// <summary>
        /// 标题
        /// </summary>
        public string Tip
        {
            get { return _Tip; }
            set { _Tip = value; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        private string _readTime;
        /// <summary>
        /// 填写时间
        /// </summary>
        public string ReadTime
        {
            get { return _readTime; }
            set { _readTime = value; }
        }

        //年
        private int year;
        //月
        private int month;
        //日
        private int day;

        public Dlg_DateNeed()
        {
            InitializeComponent();
            //窗体居中
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Location = new Point(x, y);
            //SetFullScreen();

        }

        /// <summary>
        /// 设置全屏
        /// </summary>
        private void SetFullScreen()
        {
            FormCommon.HideTaskBar();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        /// <summary>
        /// 判读当前年份是否为闰年
        /// </summary>
        /// <returns></returns>
        private bool IsLeapYear()
        {
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 年减
        /// </summary>
        private void btnYearDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            year = Convert.ToInt32(txtYear.Text) - 1;
            txtYear.Text = year.ToString();
            //如果不是闰年，2月的天由29-->28
            if (!IsLeapYear())
            {
                if (Convert.ToInt32(txtMonth.Text) == 2 && Convert.ToInt32(txtDay.Text) > 28)
                {
                    txtDay.Text = "28";
                }
            }
        }
        /// <summary>
        /// 年减
        /// </summary>
        private void btnYearInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            year = Convert.ToInt32(txtYear.Text) + 1;
            txtYear.Text = year.ToString();
            //如果不是闰年，2月的天由29-->28
            if (!IsLeapYear())
            {
                if (Convert.ToInt32(txtMonth.Text) == 2 && Convert.ToInt32(txtDay.Text) > 28)
                {
                    txtDay.Text = "28";
                }
            }
        }
        /// <summary>
        /// 小时减
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHourDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtHour.Text) == 0)
            {
                txtHour.Text = "23";
            }
            else
            {
                txtHour.Text = (Convert.ToInt32(txtHour.Text) - 1).ToString("d2");
            }
        }
        /// <summary>
        /// 小时加
        /// </summary>
        private void btnHourInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtHour.Text) == 23)
            {
                txtHour.Text = "00";
            }
            else
            {
                txtHour.Text = (Convert.ToInt32(txtHour.Text) + 1).ToString("d2");
            }
        }
        /// <summary>
        /// 减月份
        /// </summary>
        private void btnMonthDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            month = Convert.ToInt32(txtMonth.Text);
            if (month == 1)
            {
                month = 12;
            }
            else
            {
                month--;
            }
            txtMonth.Text = month.ToString("d2");
        }
        /// <summary>
        /// 加月份
        /// </summary>
        private void btnMonthInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            month = Convert.ToInt32(txtMonth.Text);
            if (month == 12)
            {
                month = 1;
            }
            else
            {
                month++;
            }
            txtMonth.Text = month.ToString("d2");
        }
        /// <summary>
        /// 分钟减
        /// </summary>
        private void btnMinuteDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtMinute.Text) == 0)
            {
                txtMinute.Text = "59";
            }
            else
            {
                txtMinute.Text = (Convert.ToInt32(txtMinute.Text) - 1).ToString("d2");
            }
        }

        /// <summary>
        /// 分钟加
        /// </summary>
        private void btnMinuteInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtMinute.Text) == 59)
            {
                txtMinute.Text = "00";
            }
            else
            {
                txtMinute.Text = (Convert.ToInt32(txtMinute.Text) + 1).ToString("d2");
            }
        }
        /// <summary>
        /// 日减
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDayDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            year = Convert.ToInt32(txtYear.Text);
            month = Convert.ToInt32(txtMonth.Text);
            day = Convert.ToInt32(txtDay.Text);
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (day == 1)
                    {
                        day = 31;
                    }
                    else
                    {
                        day--;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day == 1)
                    {
                        day = 30;
                    }
                    else
                    {
                        day--;
                    }
                    break;
                case 2:
                    if (IsLeapYear())
                    {
                        if (day == 1)
                        {
                            day = 29;
                        }
                        else
                        {
                            day--;
                        }
                    }
                    else
                    {
                        if (day == 1)
                        {
                            day = 28;
                        }
                        else
                        {
                            day--;
                        }
                    }
                    break;
            }
            txtDay.Text = day.ToString("d2");
        }
        /// <summary>
        /// 日加
        /// </summary>
        private void btnDayInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            year = Convert.ToInt32(txtYear.Text);
            month = Convert.ToInt32(txtMonth.Text);
            day = Convert.ToInt32(txtDay.Text);
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (day == 31)
                    {
                        day = 1;
                    }
                    else
                    {
                        day++;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day == 30)
                    {
                        day = 1;
                    }
                    else
                    {
                        day++;
                    }
                    break;
                case 2:
                    if (IsLeapYear())
                    {
                        if (day == 29)
                        {
                            day = 1;
                        }
                        else
                        {
                            day++;
                        }
                    }
                    else
                    {
                        if (day == 28)
                        {
                            day = 1;
                        }
                        else
                        {
                            day++;
                        }
                    }
                    break;
            }
            txtDay.Text = day.ToString("d2");
        }
        /// <summary>
        /// 减秒
        /// </summary>
        private void btnSecondDec_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtSecond.Text) == 0)
            {
                txtSecond.Text = "59";
            }
            else
            {
                txtSecond.Text = (Convert.ToInt32(txtSecond.Text) - 1).ToString("d2");
            }
        }
        /// <summary>
        /// 加秒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSecondInc_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (Convert.ToInt32(txtSecond.Text) == 59)
            {
                txtSecond.Text = "00";
            }
            else
            {
                txtSecond.Text = (Convert.ToInt32(txtSecond.Text) + 1).ToString("d2");
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            ReadTime = txtYear.Text + "-" + txtMonth.Text + "-" + txtDay.Text + " " + txtHour.Text + ":" + txtMinute.Text + ":" + txtSecond.Text;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}