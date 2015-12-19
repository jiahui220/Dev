namespace TrainView.GuideView
{
    partial class TrainGuideAudio
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.btn_Choice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(115, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 25);
            this.label1.Text = "音频名称:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(115, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 25);
            this.label2.Text = "音频文件:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtName.Location = new System.Drawing.Point(216, 76);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(252, 22);
            this.txtName.TabIndex = 57;
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFile.Enabled = false;
            this.txtFile.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.txtFile.Location = new System.Drawing.Point(216, 173);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(252, 22);
            this.txtFile.TabIndex = 52;
            // 
            // btn_Send
            // 
            this.btn_Send.BackColor = System.Drawing.Color.Silver;
            this.btn_Send.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Send.ForeColor = System.Drawing.Color.Black;
            this.btn_Send.Location = new System.Drawing.Point(314, 274);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(73, 30);
            this.btn_Send.TabIndex = 59;
            this.btn_Send.Text = "发 送";
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // btn_Choice
            // 
            this.btn_Choice.BackColor = System.Drawing.Color.Silver;
            this.btn_Choice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Choice.ForeColor = System.Drawing.Color.Black;
            this.btn_Choice.Location = new System.Drawing.Point(476, 169);
            this.btn_Choice.Name = "btn_Choice";
            this.btn_Choice.Size = new System.Drawing.Size(73, 30);
            this.btn_Choice.TabIndex = 60;
            this.btn_Choice.Text = "选 择";
            this.btn_Choice.Click += new System.EventHandler(this.btn_Choice_Click);
            // 
            // TrainGuideAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.btn_Choice);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TrainGuideAudio";
            this.Size = new System.Drawing.Size(674, 339);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.Button btn_Choice;
        private System.Windows.Forms.OpenFileDialog dlgFileSelect;
    }
}
