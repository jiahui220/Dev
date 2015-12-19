namespace EasyFrom
{
    partial class BaseFrom
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_One = new System.Windows.Forms.Button();
            this.btn_five = new System.Windows.Forms.Button();
            this.btn_Four = new System.Windows.Forms.Button();
            this.btn_Three = new System.Windows.Forms.Button();
            this.btn_Two = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tmrCurrTime = new System.Windows.Forms.Timer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl_title);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 40);
            this.panel1.GotFocus += new System.EventHandler(this.panel1_GotFocus);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 32);
            // 
            // lbl_title
            // 
            this.lbl_title.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular);
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(275, 8);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(250, 24);
            this.lbl_title.Text = "基础信息";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(547, 8);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(250, 24);
            this.lblTime.Text = "2014-03-01 12:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.btn_One);
            this.panel2.Controls.Add(this.btn_five);
            this.panel2.Controls.Add(this.btn_Four);
            this.panel2.Controls.Add(this.btn_Three);
            this.panel2.Controls.Add(this.btn_Two);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(111, 440);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 400);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(111, 40);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(482, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 20);
            this.label2.Text = "您有0条新公告。";
            // 
            // btn_One
            // 
            this.btn_One.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btn_One.ForeColor = System.Drawing.Color.Black;
            this.btn_One.Location = new System.Drawing.Point(13, 14);
            this.btn_One.Name = "btn_One";
            this.btn_One.Size = new System.Drawing.Size(79, 51);
            this.btn_One.TabIndex = 0;
            this.btn_One.Text = "基础信息";
            this.btn_One.Click += new System.EventHandler(this.btn_One_Click);
            // 
            // btn_five
            // 
            this.btn_five.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btn_five.ForeColor = System.Drawing.Color.Black;
            this.btn_five.Location = new System.Drawing.Point(13, 336);
            this.btn_five.Name = "btn_five";
            this.btn_five.Size = new System.Drawing.Size(79, 51);
            this.btn_five.TabIndex = 2;
            this.btn_five.Text = "系统工具";
            this.btn_five.Click += new System.EventHandler(this.btn_five_Click);
            // 
            // btn_Four
            // 
            this.btn_Four.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btn_Four.ForeColor = System.Drawing.Color.Black;
            this.btn_Four.Location = new System.Drawing.Point(13, 256);
            this.btn_Four.Name = "btn_Four";
            this.btn_Four.Size = new System.Drawing.Size(79, 51);
            this.btn_Four.TabIndex = 1;
            this.btn_Four.Text = "机车报警";
            this.btn_Four.Click += new System.EventHandler(this.btn_Four_Click);
            // 
            // btn_Three
            // 
            this.btn_Three.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btn_Three.ForeColor = System.Drawing.Color.Black;
            this.btn_Three.Location = new System.Drawing.Point(13, 176);
            this.btn_Three.Name = "btn_Three";
            this.btn_Three.Size = new System.Drawing.Size(79, 51);
            this.btn_Three.TabIndex = 2;
            this.btn_Three.Text = "行车指南";
            this.btn_Three.Click += new System.EventHandler(this.btn_Three_Click);
            // 
            // btn_Two
            // 
            this.btn_Two.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btn_Two.ForeColor = System.Drawing.Color.Black;
            this.btn_Two.Location = new System.Drawing.Point(13, 96);
            this.btn_Two.Name = "btn_Two";
            this.btn_Two.Size = new System.Drawing.Size(79, 51);
            this.btn_Two.TabIndex = 3;
            this.btn_Two.Text = "电子报单";
            this.btn_Two.Click += new System.EventHandler(this.btn_Two_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(111, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(689, 440);
            // 
            // tmrCurrTime
            // 
            this.tmrCurrTime.Enabled = true;
            this.tmrCurrTime.Interval = 1000;
            this.tmrCurrTime.Tick += new System.EventHandler(this.tmrCurrTime_Tick);
            // 
            // BaseFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BaseFrom";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BaseFrom_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Four;
        private System.Windows.Forms.Button btn_Three;
        private System.Windows.Forms.Button btn_Two;
        private System.Windows.Forms.Button btn_One;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btn_five;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrCurrTime;
        private System.Windows.Forms.Label label1;
    }
}

