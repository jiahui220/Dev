namespace Trainfo
{
    partial class TrainScreen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tmrTime = new System.Windows.Forms.Timer();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrTime
            // 
            this.tmrTime.Interval = 1000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.DimGray;
            this.lblTime.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular);
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.lblTime.Location = new System.Drawing.Point(7, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(187, 66);
            this.lblTime.Text = "00:00";
            // 
            // pnlDate
            // 
            this.pnlDate.BackColor = System.Drawing.Color.DimGray;
            this.pnlDate.Controls.Add(this.lblDate);
            this.pnlDate.Controls.Add(this.lblTime);
            this.pnlDate.Location = new System.Drawing.Point(300, 191);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(201, 99);
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.DimGray;
            this.lblDate.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            this.lblDate.Location = new System.Drawing.Point(3, 66);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(194, 30);
            this.lblDate.Text = "09月04日 星期三";
            // 
            // TrainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.ControlBox = false;
            this.Controls.Add(this.pnlDate);
            this.ForeColor = System.Drawing.Color.DimGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TrainScreen";
            this.Text = "TrainScreen";
            this.Load += new System.EventHandler(this.TrainScreen_Load);
            this.Click += new System.EventHandler(this.TrainScreen_Click);
            this.pnlDate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Label lblDate;
    }
}