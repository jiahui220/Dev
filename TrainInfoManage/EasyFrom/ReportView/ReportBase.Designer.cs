namespace EasyFrom.ReportView
{
    partial class ReportBase
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
            this.txtNum = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtBelong = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtBrearu = new System.Windows.Forms.TextBox();
            this.tagTime = new System.Windows.Forms.Label();
            this.tagNum = new System.Windows.Forms.Label();
            this.tagType = new System.Windows.Forms.Label();
            this.tagBelong = new System.Windows.Forms.Label();
            this.tagBureau = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.tmr_CarInfo = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // txtNum
            // 
            this.txtNum.BackColor = System.Drawing.Color.White;
            this.txtNum.Enabled = false;
            this.txtNum.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtNum.ForeColor = System.Drawing.Color.Black;
            this.txtNum.Location = new System.Drawing.Point(241, 204);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(296, 28);
            this.txtNum.TabIndex = 41;
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.Enabled = false;
            this.txtType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtType.ForeColor = System.Drawing.Color.Black;
            this.txtType.Location = new System.Drawing.Point(241, 154);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(296, 28);
            this.txtType.TabIndex = 40;
            // 
            // txtBelong
            // 
            this.txtBelong.BackColor = System.Drawing.Color.White;
            this.txtBelong.Enabled = false;
            this.txtBelong.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtBelong.ForeColor = System.Drawing.Color.Black;
            this.txtBelong.Location = new System.Drawing.Point(241, 104);
            this.txtBelong.Name = "txtBelong";
            this.txtBelong.Size = new System.Drawing.Size(296, 28);
            this.txtBelong.TabIndex = 39;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.White;
            this.txtTime.Enabled = false;
            this.txtTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtTime.ForeColor = System.Drawing.Color.Black;
            this.txtTime.Location = new System.Drawing.Point(241, 254);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(296, 28);
            this.txtTime.TabIndex = 38;
            // 
            // txtBrearu
            // 
            this.txtBrearu.BackColor = System.Drawing.Color.White;
            this.txtBrearu.Enabled = false;
            this.txtBrearu.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtBrearu.ForeColor = System.Drawing.Color.Black;
            this.txtBrearu.Location = new System.Drawing.Point(241, 54);
            this.txtBrearu.Name = "txtBrearu";
            this.txtBrearu.Size = new System.Drawing.Size(296, 28);
            this.txtBrearu.TabIndex = 37;
            // 
            // tagTime
            // 
            this.tagTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagTime.ForeColor = System.Drawing.Color.Black;
            this.tagTime.Location = new System.Drawing.Point(138, 260);
            this.tagTime.Name = "tagTime";
            this.tagTime.Size = new System.Drawing.Size(97, 25);
            this.tagTime.Text = "报单日期:";
            // 
            // tagNum
            // 
            this.tagNum.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagNum.ForeColor = System.Drawing.Color.Black;
            this.tagNum.Location = new System.Drawing.Point(138, 210);
            this.tagNum.Name = "tagNum";
            this.tagNum.Size = new System.Drawing.Size(97, 25);
            this.tagNum.Text = "车号:";
            // 
            // tagType
            // 
            this.tagType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagType.ForeColor = System.Drawing.Color.Black;
            this.tagType.Location = new System.Drawing.Point(138, 160);
            this.tagType.Name = "tagType";
            this.tagType.Size = new System.Drawing.Size(97, 25);
            this.tagType.Text = "机型:";
            // 
            // tagBelong
            // 
            this.tagBelong.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagBelong.ForeColor = System.Drawing.Color.Black;
            this.tagBelong.Location = new System.Drawing.Point(138, 110);
            this.tagBelong.Name = "tagBelong";
            this.tagBelong.Size = new System.Drawing.Size(97, 25);
            this.tagBelong.Text = "机务段:";
            // 
            // tagBureau
            // 
            this.tagBureau.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagBureau.ForeColor = System.Drawing.Color.Black;
            this.tagBureau.Location = new System.Drawing.Point(138, 60);
            this.tagBureau.Name = "tagBureau";
            this.tagBureau.Size = new System.Drawing.Size(97, 25);
            this.tagBureau.Text = "铁路局:";
            // 
            // lbl_msg
            // 
            this.lbl_msg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.lbl_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_msg.Location = new System.Drawing.Point(138, 13);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(399, 20);
            this.lbl_msg.Text = "数据加载中，请稍后.....";
            // 
            // tmr_CarInfo
            // 
            this.tmr_CarInfo.Tick += new System.EventHandler(this.tmr_CarInfo_Tick);
            // 
            // ReportBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtBelong);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtBrearu);
            this.Controls.Add(this.tagTime);
            this.Controls.Add(this.tagNum);
            this.Controls.Add(this.tagType);
            this.Controls.Add(this.tagBelong);
            this.Controls.Add(this.tagBureau);
            this.Name = "ReportBase";
            this.Size = new System.Drawing.Size(674, 339);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtBelong;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtBrearu;
        private System.Windows.Forms.Label tagTime;
        private System.Windows.Forms.Label tagNum;
        private System.Windows.Forms.Label tagType;
        private System.Windows.Forms.Label tagBelong;
        private System.Windows.Forms.Label tagBureau;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Timer tmr_CarInfo;
    }
}
