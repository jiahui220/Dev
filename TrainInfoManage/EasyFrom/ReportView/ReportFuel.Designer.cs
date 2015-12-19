namespace EasyFrom.ReportView
{
    partial class ReportFuel
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
            this.txtSubmit = new System.Windows.Forms.TextBox();
            this.tagSubmit = new System.Windows.Forms.Label();
            this.txtGet = new System.Windows.Forms.TextBox();
            this.tagGet = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSubmit
            // 
            this.txtSubmit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSubmit.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtSubmit.Location = new System.Drawing.Point(226, 191);
            this.txtSubmit.Name = "txtSubmit";
            this.txtSubmit.Size = new System.Drawing.Size(316, 28);
            this.txtSubmit.TabIndex = 45;
            // 
            // tagSubmit
            // 
            this.tagSubmit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tagSubmit.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagSubmit.ForeColor = System.Drawing.Color.Black;
            this.tagSubmit.Location = new System.Drawing.Point(133, 196);
            this.tagSubmit.Name = "tagSubmit";
            this.tagSubmit.Size = new System.Drawing.Size(87, 25);
            this.tagSubmit.Tag = "1";
            this.tagSubmit.Text = "交电量:";
            // 
            // txtGet
            // 
            this.txtGet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtGet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtGet.Location = new System.Drawing.Point(226, 117);
            this.txtGet.Name = "txtGet";
            this.txtGet.Size = new System.Drawing.Size(316, 28);
            this.txtGet.TabIndex = 44;
            // 
            // tagGet
            // 
            this.tagGet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tagGet.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagGet.ForeColor = System.Drawing.Color.Black;
            this.tagGet.Location = new System.Drawing.Point(133, 122);
            this.tagGet.Name = "tagGet";
            this.tagGet.Size = new System.Drawing.Size(87, 25);
            this.tagGet.Text = "接电量:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 30);
            this.button1.TabIndex = 48;
            this.button1.Text = "输入";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(556, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 30);
            this.button2.TabIndex = 49;
            this.button2.Text = "输入";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ReportFuel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSubmit);
            this.Controls.Add(this.tagSubmit);
            this.Controls.Add(this.txtGet);
            this.Controls.Add(this.tagGet);
            this.Name = "ReportFuel";
            this.Size = new System.Drawing.Size(674, 339);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSubmit;
        private System.Windows.Forms.Label tagSubmit;
        private System.Windows.Forms.TextBox txtGet;
        private System.Windows.Forms.Label tagGet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
