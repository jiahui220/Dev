namespace TrainView.ChildFrom
{
    partial class Dlg_Connect
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBelong = new System.Windows.Forms.TextBox();
            this.tagBelong = new System.Windows.Forms.Label();
            this.txtRegionKilo = new System.Windows.Forms.TextBox();
            this.tagRegionKilo = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.tagType = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 37);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(120, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.Text = "补 机 重 联 信 息";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 242);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 41);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(300, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 30);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(208, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBelong
            // 
            this.txtBelong.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.txtBelong.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtBelong.Location = new System.Drawing.Point(109, 183);
            this.txtBelong.MaxLength = 50;
            this.txtBelong.Name = "txtBelong";
            this.txtBelong.Size = new System.Drawing.Size(267, 25);
            this.txtBelong.TabIndex = 19;
            // 
            // tagBelong
            // 
            this.tagBelong.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.tagBelong.ForeColor = System.Drawing.Color.Black;
            this.tagBelong.Location = new System.Drawing.Point(9, 188);
            this.tagBelong.Name = "tagBelong";
            this.tagBelong.Size = new System.Drawing.Size(115, 20);
            this.tagBelong.Text = "所属机务段:";
            // 
            // txtRegionKilo
            // 
            this.txtRegionKilo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.txtRegionKilo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtRegionKilo.Location = new System.Drawing.Point(109, 129);
            this.txtRegionKilo.MaxLength = 50;
            this.txtRegionKilo.Name = "txtRegionKilo";
            this.txtRegionKilo.Size = new System.Drawing.Size(267, 25);
            this.txtRegionKilo.TabIndex = 18;
            // 
            // tagRegionKilo
            // 
            this.tagRegionKilo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.tagRegionKilo.ForeColor = System.Drawing.Color.Black;
            this.tagRegionKilo.Location = new System.Drawing.Point(9, 134);
            this.tagRegionKilo.Name = "tagRegionKilo";
            this.tagRegionKilo.Size = new System.Drawing.Size(94, 20);
            this.tagRegionKilo.Text = "区间公里:";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.txtType.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtType.Location = new System.Drawing.Point(109, 75);
            this.txtType.MaxLength = 50;
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(267, 25);
            this.txtType.TabIndex = 17;
            // 
            // tagType
            // 
            this.tagType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.tagType.ForeColor = System.Drawing.Color.Black;
            this.tagType.Location = new System.Drawing.Point(9, 80);
            this.tagType.Name = "tagType";
            this.tagType.Size = new System.Drawing.Size(94, 20);
            this.tagType.Text = "车型号:";
            // 
            // Dlg_Connect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(385, 283);
            this.ControlBox = false;
            this.Controls.Add(this.txtBelong);
            this.Controls.Add(this.tagBelong);
            this.Controls.Add(this.txtRegionKilo);
            this.Controls.Add(this.tagRegionKilo);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.tagType);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dlg_Connect";
            this.Text = "Dlg_Connect";
            this.Load += new System.EventHandler(this.Dlg_Connect_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtBelong;
        private System.Windows.Forms.Label tagBelong;
        private System.Windows.Forms.TextBox txtRegionKilo;
        private System.Windows.Forms.Label tagRegionKilo;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label tagType;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}