namespace TrainView.GuideView
{
    partial class TrainGuide
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Pic = new System.Windows.Forms.Button();
            this.btn_Medio = new System.Windows.Forms.Button();
            this.btn_Note = new System.Windows.Forms.Button();
            this.btn_Notice = new System.Windows.Forms.Button();
            this.btn_Book = new System.Windows.Forms.Button();
            this.btn_Run = new System.Windows.Forms.Button();
            this.pl_Guide = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.btn_Pic);
            this.panel2.Controls.Add(this.btn_Medio);
            this.panel2.Controls.Add(this.btn_Note);
            this.panel2.Controls.Add(this.btn_Notice);
            this.panel2.Controls.Add(this.btn_Book);
            this.panel2.Controls.Add(this.btn_Run);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 403);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(679, 40);
            // 
            // btn_Pic
            // 
            this.btn_Pic.BackColor = System.Drawing.Color.Silver;
            this.btn_Pic.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Pic.ForeColor = System.Drawing.Color.Black;
            this.btn_Pic.Location = new System.Drawing.Point(568, 4);
            this.btn_Pic.Name = "btn_Pic";
            this.btn_Pic.Size = new System.Drawing.Size(92, 30);
            this.btn_Pic.TabIndex = 6;
            this.btn_Pic.Text = "施工明细图";
            this.btn_Pic.Click += new System.EventHandler(this.btn_Pic_Click);
            // 
            // btn_Medio
            // 
            this.btn_Medio.BackColor = System.Drawing.Color.Silver;
            this.btn_Medio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Medio.ForeColor = System.Drawing.Color.Black;
            this.btn_Medio.Location = new System.Drawing.Point(456, 4);
            this.btn_Medio.Name = "btn_Medio";
            this.btn_Medio.Size = new System.Drawing.Size(92, 30);
            this.btn_Medio.TabIndex = 4;
            this.btn_Medio.Text = "媒体资料";
            this.btn_Medio.Click += new System.EventHandler(this.btn_Medio_Click);
            // 
            // btn_Note
            // 
            this.btn_Note.BackColor = System.Drawing.Color.Silver;
            this.btn_Note.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Note.ForeColor = System.Drawing.Color.Black;
            this.btn_Note.Location = new System.Drawing.Point(348, 4);
            this.btn_Note.Name = "btn_Note";
            this.btn_Note.Size = new System.Drawing.Size(92, 30);
            this.btn_Note.TabIndex = 3;
            this.btn_Note.Text = "司机计事";
            this.btn_Note.Click += new System.EventHandler(this.btn_Note_Click);
            // 
            // btn_Notice
            // 
            this.btn_Notice.BackColor = System.Drawing.Color.Silver;
            this.btn_Notice.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Notice.ForeColor = System.Drawing.Color.Black;
            this.btn_Notice.Location = new System.Drawing.Point(240, 4);
            this.btn_Notice.Name = "btn_Notice";
            this.btn_Notice.Size = new System.Drawing.Size(92, 30);
            this.btn_Notice.TabIndex = 2;
            this.btn_Notice.Text = "通知公告";
            this.btn_Notice.Click += new System.EventHandler(this.btn_Notice_Click);
            // 
            // btn_Book
            // 
            this.btn_Book.BackColor = System.Drawing.Color.Silver;
            this.btn_Book.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Book.ForeColor = System.Drawing.Color.Black;
            this.btn_Book.Location = new System.Drawing.Point(132, 4);
            this.btn_Book.Name = "btn_Book";
            this.btn_Book.Size = new System.Drawing.Size(92, 30);
            this.btn_Book.TabIndex = 1;
            this.btn_Book.Text = "电子手册";
            this.btn_Book.Click += new System.EventHandler(this.btn_Book_Click);
            // 
            // btn_Run
            // 
            this.btn_Run.BackColor = System.Drawing.Color.Silver;
            this.btn_Run.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Run.ForeColor = System.Drawing.Color.Black;
            this.btn_Run.Location = new System.Drawing.Point(20, 4);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(92, 30);
            this.btn_Run.TabIndex = 0;
            this.btn_Run.Text = "运行揭示";
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // pl_Guide
            // 
            this.pl_Guide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_Guide.Location = new System.Drawing.Point(0, 0);
            this.pl_Guide.Name = "pl_Guide";
            this.pl_Guide.Size = new System.Drawing.Size(679, 403);
            // 
            // TrainGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pl_Guide);
            this.Controls.Add(this.panel2);
            this.Name = "TrainGuide";
            this.Size = new System.Drawing.Size(679, 443);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Medio;
        private System.Windows.Forms.Button btn_Note;
        public  System.Windows.Forms.Button btn_Notice;
        private System.Windows.Forms.Button btn_Book;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.Panel pl_Guide;
        private System.Windows.Forms.Button btn_Pic;
    }
}
