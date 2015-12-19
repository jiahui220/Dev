namespace TrainView.GuideView
{
    partial class TrainGuideEbook
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Return = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBook = new System.Windows.Forms.Panel();
            this.lblPage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(0, -61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 40);
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(502, 6);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(73, 30);
            this.btn_Downpage.TabIndex = 66;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(421, 6);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(73, 30);
            this.btn_Uppage.TabIndex = 65;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Controls.Add(this.btn_Update);
            this.panel3.Controls.Add(this.btn_Downpage);
            this.panel3.Controls.Add(this.btn_Return);
            this.panel3.Controls.Add(this.btn_Uppage);
            this.panel3.Controls.Add(this.btnQuery);
            this.panel3.Controls.Add(this.txtKey);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(674, 44);
            // 
            // btn_Update
            // 
            this.btn_Update.BackColor = System.Drawing.Color.Silver;
            this.btn_Update.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Update.ForeColor = System.Drawing.Color.Black;
            this.btn_Update.Location = new System.Drawing.Point(583, 6);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(73, 30);
            this.btn_Update.TabIndex = 66;
            this.btn_Update.Text = "更新";
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Return
            // 
            this.btn_Return.BackColor = System.Drawing.Color.Silver;
            this.btn_Return.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Return.ForeColor = System.Drawing.Color.Black;
            this.btn_Return.Location = new System.Drawing.Point(340, 6);
            this.btn_Return.Name = "btn_Return";
            this.btn_Return.Size = new System.Drawing.Size(73, 30);
            this.btn_Return.TabIndex = 67;
            this.btn_Return.Text = "返回";
            this.btn_Return.Click += new System.EventHandler(this.btn_Return_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.Silver;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btnQuery.ForeColor = System.Drawing.Color.Black;
            this.btnQuery.Location = new System.Drawing.Point(259, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(73, 30);
            this.btnQuery.TabIndex = 68;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // txtKey
            // 
            this.txtKey.BackColor = System.Drawing.Color.White;
            this.txtKey.Location = new System.Drawing.Point(91, 10);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(149, 23);
            this.txtKey.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.Text = "关键字";
            // 
            // pnlBook
            // 
            this.pnlBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBook.Location = new System.Drawing.Point(0, 44);
            this.pnlBook.Name = "pnlBook";
            this.pnlBook.Size = new System.Drawing.Size(674, 310);
            this.pnlBook.GotFocus += new System.EventHandler(this.pnlBook_GotFocus);
            // 
            // lblPage
            // 
            this.lblPage.BackColor = System.Drawing.Color.Silver;
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPage.Location = new System.Drawing.Point(0, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(674, 26);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.lblPage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 354);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 26);
            // 
            // TrainGuideEbook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pnlBook);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(98)))), ((int)(((byte)(158)))));
            this.Name = "TrainGuideEbook";
            this.Size = new System.Drawing.Size(674, 380);
            this.Click += new System.EventHandler(this.TrainGuideEbook_Click);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Return;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Panel pnlBook;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Panel panel2;
    }
}
