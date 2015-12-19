namespace TrainView.AlarmView
{
    partial class TrainAlarm
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
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_Record = new System.Windows.Forms.Button();
            this.pl_Alarm = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.btn_Set);
            this.panel2.Controls.Add(this.btn_Record);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 403);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(679, 40);
            // 
            // btn_Set
            // 
            this.btn_Set.BackColor = System.Drawing.Color.Silver;
            this.btn_Set.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Set.ForeColor = System.Drawing.Color.Black;
            this.btn_Set.Location = new System.Drawing.Point(383, 4);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(88, 30);
            this.btn_Set.TabIndex = 1;
            this.btn_Set.Text = "报警配置";
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_Record
            // 
            this.btn_Record.BackColor = System.Drawing.Color.Silver;
            this.btn_Record.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Record.ForeColor = System.Drawing.Color.Black;
            this.btn_Record.Location = new System.Drawing.Point(207, 4);
            this.btn_Record.Name = "btn_Record";
            this.btn_Record.Size = new System.Drawing.Size(88, 30);
            this.btn_Record.TabIndex = 0;
            this.btn_Record.Text = "报警记录";
            this.btn_Record.Click += new System.EventHandler(this.btn_Record_Click);
            // 
            // pl_Alarm
            // 
            this.pl_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_Alarm.Location = new System.Drawing.Point(0, 0);
            this.pl_Alarm.Name = "pl_Alarm";
            this.pl_Alarm.Size = new System.Drawing.Size(679, 403);
            // 
            // TrainAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pl_Alarm);
            this.Controls.Add(this.panel2);
            this.Name = "TrainAlarm";
            this.Size = new System.Drawing.Size(679, 443);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_Record;
        private System.Windows.Forms.Panel pl_Alarm;
    }
}
