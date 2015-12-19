namespace TrainView.ToolView
{
    partial class TrainTool
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
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_Info = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pl_Tool = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Set
            // 
            this.btn_Set.BackColor = System.Drawing.Color.Silver;
            this.btn_Set.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Set.ForeColor = System.Drawing.Color.Black;
            this.btn_Set.Location = new System.Drawing.Point(207, 5);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(93, 30);
            this.btn_Set.TabIndex = 10;
            this.btn_Set.Text = "系统设置";
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_Info
            // 
            this.btn_Info.BackColor = System.Drawing.Color.Silver;
            this.btn_Info.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Info.ForeColor = System.Drawing.Color.Black;
            this.btn_Info.Location = new System.Drawing.Point(373, 5);
            this.btn_Info.Name = "btn_Info";
            this.btn_Info.Size = new System.Drawing.Size(95, 30);
            this.btn_Info.TabIndex = 11;
            this.btn_Info.Text = "系统信息";
            this.btn_Info.Click += new System.EventHandler(this.btn_Info_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.btn_Info);
            this.panel1.Controls.Add(this.btn_Set);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 40);
            this.panel1.GotFocus += new System.EventHandler(this.panel1_GotFocus);
            // 
            // pl_Tool
            // 
            this.pl_Tool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_Tool.Location = new System.Drawing.Point(0, 0);
            this.pl_Tool.Name = "pl_Tool";
            this.pl_Tool.Size = new System.Drawing.Size(674, 384);
            // 
            // TrainTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pl_Tool);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "TrainTool";
            this.Size = new System.Drawing.Size(674, 424);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_Info;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pl_Tool;
    }
}
