namespace EasyFrom
{
    partial class TrainReport
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
            this.btn_Submit = new System.Windows.Forms.Button();
            this.btn_Group = new System.Windows.Forms.Button();
            this.btn_Conect = new System.Windows.Forms.Button();
            this.btn_Fuel = new System.Windows.Forms.Button();
            this.btn_Work = new System.Windows.Forms.Button();
            this.btn_Base = new System.Windows.Forms.Button();
            this.pl_Report = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.btn_Submit);
            this.panel2.Controls.Add(this.btn_Group);
            this.panel2.Controls.Add(this.btn_Conect);
            this.panel2.Controls.Add(this.btn_Fuel);
            this.panel2.Controls.Add(this.btn_Work);
            this.panel2.Controls.Add(this.btn_Base);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 403);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(679, 40);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Submit.ForeColor = System.Drawing.Color.Black;
            this.btn_Submit.Location = new System.Drawing.Point(565, 5);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(88, 30);
            this.btn_Submit.TabIndex = 5;
            this.btn_Submit.Text = "提交报单";
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // btn_Group
            // 
            this.btn_Group.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Group.ForeColor = System.Drawing.Color.Black;
            this.btn_Group.Location = new System.Drawing.Point(457, 5);
            this.btn_Group.Name = "btn_Group";
            this.btn_Group.Size = new System.Drawing.Size(88, 30);
            this.btn_Group.TabIndex = 4;
            this.btn_Group.Text = "运行编组";
            this.btn_Group.Click += new System.EventHandler(this.btn_Group_Click);
            // 
            // btn_Conect
            // 
            this.btn_Conect.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Conect.ForeColor = System.Drawing.Color.Black;
            this.btn_Conect.Location = new System.Drawing.Point(349, 5);
            this.btn_Conect.Name = "btn_Conect";
            this.btn_Conect.Size = new System.Drawing.Size(88, 30);
            this.btn_Conect.TabIndex = 3;
            this.btn_Conect.Text = "补机重联";
            this.btn_Conect.Click += new System.EventHandler(this.btn_Conect_Click);
            // 
            // btn_Fuel
            // 
            this.btn_Fuel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Fuel.ForeColor = System.Drawing.Color.Black;
            this.btn_Fuel.Location = new System.Drawing.Point(241, 5);
            this.btn_Fuel.Name = "btn_Fuel";
            this.btn_Fuel.Size = new System.Drawing.Size(88, 30);
            this.btn_Fuel.TabIndex = 2;
            this.btn_Fuel.Text = "领取燃料";
            this.btn_Fuel.Click += new System.EventHandler(this.btn_Fuel_Click);
            // 
            // btn_Work
            // 
            this.btn_Work.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Work.ForeColor = System.Drawing.Color.Black;
            this.btn_Work.Location = new System.Drawing.Point(133, 5);
            this.btn_Work.Name = "btn_Work";
            this.btn_Work.Size = new System.Drawing.Size(88, 30);
            this.btn_Work.TabIndex = 1;
            this.btn_Work.Text = "出勤时分";
            this.btn_Work.Click += new System.EventHandler(this.btn_Work_Click);
            // 
            // btn_Base
            // 
            this.btn_Base.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Base.ForeColor = System.Drawing.Color.Black;
            this.btn_Base.Location = new System.Drawing.Point(25, 5);
            this.btn_Base.Name = "btn_Base";
            this.btn_Base.Size = new System.Drawing.Size(88, 30);
            this.btn_Base.TabIndex = 0;
            this.btn_Base.Text = "基础信息";
            this.btn_Base.Click += new System.EventHandler(this.btn_Base_Click);
            // 
            // pl_Report
            // 
            this.pl_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_Report.Location = new System.Drawing.Point(0, 0);
            this.pl_Report.Name = "pl_Report";
            this.pl_Report.Size = new System.Drawing.Size(679, 403);
            this.pl_Report.GotFocus += new System.EventHandler(this.pl_Report_GotFocus);
            // 
            // TrainReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.pl_Report);
            this.Controls.Add(this.panel2);
            this.Name = "TrainReport";
            this.Size = new System.Drawing.Size(679, 443);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Group;
        private System.Windows.Forms.Button btn_Conect;
        private System.Windows.Forms.Button btn_Fuel;
        private System.Windows.Forms.Button btn_Work;
        private System.Windows.Forms.Button btn_Base;
        private System.Windows.Forms.Panel pl_Report;
        public System.Windows.Forms.Button btn_Submit;
    }
}
