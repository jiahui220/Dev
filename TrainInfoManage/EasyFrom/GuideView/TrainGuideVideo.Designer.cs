namespace TrainView.GuideView
{
    partial class TrainGuideVideo
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
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Play = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgMedia = new System.Windows.Forms.DataGrid();
            this.Video = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VideoId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VideoPath = new System.Windows.Forms.DataGridTextBoxColumn();
            this.VideoName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UploadUser = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UploadTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.Text = "序号：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.Text = "名称：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(221, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "类型：";
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(584, 57);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(80, 30);
            this.btn_Downpage.TabIndex = 76;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(584, 11);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(80, 30);
            this.btn_Uppage.TabIndex = 77;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.BackColor = System.Drawing.Color.Silver;
            this.btn_Down.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Down.ForeColor = System.Drawing.Color.Black;
            this.btn_Down.Location = new System.Drawing.Point(4, 135);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(35, 58);
            this.btn_Down.TabIndex = 105;
            this.btn_Down.Text = "下";
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.BackColor = System.Drawing.Color.Silver;
            this.btn_Up.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Up.ForeColor = System.Drawing.Color.Black;
            this.btn_Up.Location = new System.Drawing.Point(4, 48);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(35, 58);
            this.btn_Up.TabIndex = 104;
            this.btn_Up.Text = "上";
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblId.ForeColor = System.Drawing.Color.Black;
            this.lblId.Location = new System.Drawing.Point(87, 20);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(96, 20);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(87, 52);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(396, 20);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblType.ForeColor = System.Drawing.Color.Black;
            this.lblType.Location = new System.Drawing.Point(298, 20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(108, 20);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.btn_Play);
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn_Downpage);
            this.panel1.Controls.Add(this.btn_Uppage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 100);
            // 
            // btn_Play
            // 
            this.btn_Play.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Play.Location = new System.Drawing.Point(497, 20);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(72, 61);
            this.btn_Play.TabIndex = 78;
            this.btn_Play.Text = "播   放";
            this.btn_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btn_Up);
            this.panel2.Controls.Add(this.btn_Down);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(631, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(43, 239);
            // 
            // dgMedia
            // 
            this.dgMedia.BackgroundColor = System.Drawing.Color.White;
            this.dgMedia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMedia.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dgMedia.Location = new System.Drawing.Point(0, 100);
            this.dgMedia.Name = "dgMedia";
            this.dgMedia.SelectionBackColor = System.Drawing.Color.Blue;
            this.dgMedia.Size = new System.Drawing.Size(631, 239);
            this.dgMedia.TabIndex = 111;
            this.dgMedia.TableStyles.Add(this.Video);
            this.dgMedia.CurrentCellChanged += new System.EventHandler(this.dgMedia_CurrentCellChanged);
            this.dgMedia.GotFocus += new System.EventHandler(this.dgMedia_GotFocus);
            // 
            // Video
            // 
            this.Video.GridColumnStyles.Add(this.ID);
            this.Video.GridColumnStyles.Add(this.VideoId);
            this.Video.GridColumnStyles.Add(this.VideoPath);
            this.Video.GridColumnStyles.Add(this.VideoName);
            this.Video.GridColumnStyles.Add(this.UploadUser);
            this.Video.GridColumnStyles.Add(this.UploadTime);
            this.Video.MappingName = "Video";
            // 
            // ID
            // 
            ////this.ID.Format = "";
            ////this.ID.FormatInfo = null;
            this.ID.HeaderText = "编号";
            this.ID.MappingName = "ID";
            this.ID.Width = 100;
            // 
            // VideoId
            // 
            ////this.VideoId.Format = "";
            ////this.VideoId.FormatInfo = null;
            this.VideoId.MappingName = "VideoId";
            this.VideoId.Width = 0;
            // 
            // VideoPath
            // 
            ////this.VideoPath.Format = "";
            ////this.VideoPath.FormatInfo = null;
            this.VideoPath.MappingName = "VideoPath";
            this.VideoPath.Width = 0;
            // 
            // VideoName
            // 
            ////this.VideoName.Format = "";
            ////this.VideoName.FormatInfo = null;
            this.VideoName.HeaderText = "音频名";
            this.VideoName.MappingName = "VideoName";
            this.VideoName.Width = 100;
            // 
            // UploadUser
            ////// 
            ////this.UploadUser.Format = "";
            ////this.UploadUser.FormatInfo = null;
            this.UploadUser.HeaderText = "上传者";
            this.UploadUser.MappingName = "UploadUser";
            this.UploadUser.Width = 150;
            // 
            // UploadTime
            // 
            //this.UploadTime.Format = "";
            //this.UploadTime.FormatInfo = null;
            this.UploadTime.HeaderText = "上传时间";
            this.UploadTime.MappingName = "UploadTime";
            this.UploadTime.Width = 200;
            // 
            // TrainGuideVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dgMedia);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TrainGuideVideo";
            this.Size = new System.Drawing.Size(674, 339);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.OpenFileDialog dlgFileSelect;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGrid dgMedia;
        private System.Windows.Forms.DataGridTableStyle Video;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn VideoId;
        private System.Windows.Forms.DataGridTextBoxColumn VideoPath;
        private System.Windows.Forms.DataGridTextBoxColumn VideoName;
        private System.Windows.Forms.DataGridTextBoxColumn UploadUser;
        private System.Windows.Forms.DataGridTextBoxColumn UploadTime;
        private System.Windows.Forms.Button btn_Play;
    }
}
