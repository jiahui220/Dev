namespace TrainView.GuideView
{
    partial class TrainGuiDetail
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
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.btn_Downitem = new System.Windows.Forms.Button();
            this.btn_Upitem = new System.Windows.Forms.Button();
            this.btn_Look = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgPic = new System.Windows.Forms.DataGrid();
            this.Details = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ImgId = new System.Windows.Forms.DataGridTextBoxColumn();
            this.imgPath = new System.Windows.Forms.DataGridTextBoxColumn();
            this.imgName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UploadUser = new System.Windows.Forms.DataGridTextBoxColumn();
            this.UploadTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(588, 5);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(80, 30);
            this.btn_Downpage.TabIndex = 64;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(479, 5);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(80, 30);
            this.btn_Uppage.TabIndex = 65;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // btn_Downitem
            // 
            this.btn_Downitem.BackColor = System.Drawing.Color.Silver;
            this.btn_Downitem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Downitem.ForeColor = System.Drawing.Color.Black;
            this.btn_Downitem.Location = new System.Drawing.Point(120, 5);
            this.btn_Downitem.Name = "btn_Downitem";
            this.btn_Downitem.Size = new System.Drawing.Size(80, 30);
            this.btn_Downitem.TabIndex = 7;
            this.btn_Downitem.Text = "下一条";
            this.btn_Downitem.Click += new System.EventHandler(this.btn_Downitem_Click);
            // 
            // btn_Upitem
            // 
            this.btn_Upitem.BackColor = System.Drawing.Color.Silver;
            this.btn_Upitem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Upitem.ForeColor = System.Drawing.Color.Black;
            this.btn_Upitem.Location = new System.Drawing.Point(20, 5);
            this.btn_Upitem.Name = "btn_Upitem";
            this.btn_Upitem.Size = new System.Drawing.Size(80, 30);
            this.btn_Upitem.TabIndex = 6;
            this.btn_Upitem.Text = "上一条";
            this.btn_Upitem.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Look
            // 
            this.btn_Look.BackColor = System.Drawing.Color.Silver;
            this.btn_Look.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Look.ForeColor = System.Drawing.Color.Black;
            this.btn_Look.Location = new System.Drawing.Point(297, 5);
            this.btn_Look.Name = "btn_Look";
            this.btn_Look.Size = new System.Drawing.Size(105, 30);
            this.btn_Look.TabIndex = 66;
            this.btn_Look.Text = "查看";
            this.btn_Look.Click += new System.EventHandler(this.btn_Look_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btn_Uppage);
            this.panel2.Controls.Add(this.btn_Upitem);
            this.panel2.Controls.Add(this.btn_Look);
            this.panel2.Controls.Add(this.btn_Downitem);
            this.panel2.Controls.Add(this.btn_Downpage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 42);
            // 
            // dgPic
            // 
            this.dgPic.BackgroundColor = System.Drawing.Color.White;
            this.dgPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPic.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dgPic.Location = new System.Drawing.Point(0, 42);
            this.dgPic.Name = "dgPic";
            this.dgPic.SelectionBackColor = System.Drawing.Color.Blue;
            this.dgPic.Size = new System.Drawing.Size(680, 341);
            this.dgPic.TabIndex = 68;
            this.dgPic.TableStyles.Add(this.Details);
            this.dgPic.CurrentCellChanged += new System.EventHandler(this.dgPic_CurrentCellChanged);
            this.dgPic.GotFocus += new System.EventHandler(this.dgPic_GotFocus);
            // 
            // Details
            // 
            this.Details.GridColumnStyles.Add(this.ID);
            this.Details.GridColumnStyles.Add(this.ImgId);
            this.Details.GridColumnStyles.Add(this.imgPath);
            this.Details.GridColumnStyles.Add(this.imgName);
            this.Details.GridColumnStyles.Add(this.UploadUser);
            this.Details.GridColumnStyles.Add(this.UploadTime);
            this.Details.MappingName = "Details";
            // 
            // ID
            // 
            ////this.ID.Format = "";
            ////this.ID.FormatInfo = null;
            this.ID.HeaderText = "编号";
            this.ID.MappingName = "ID";
            this.ID.Width = 100;
            // 
            // ImgId
            // 
            ////this.ImgId.Format = "";
            ////this.ImgId.FormatInfo = null;
            this.ImgId.MappingName = "ImgId";
            this.ImgId.Width = 0;
            // 
            // imgPath
            // 
            //this.imgPath.Format = "";
            //this.imgPath.FormatInfo = null;
            this.imgPath.MappingName = "FilePath";
            this.imgPath.Width = 0;
            // 
            // imgName
            ////// 
            ////this.imgName.Format = "";
            ////this.imgName.FormatInfo = null;
            this.imgName.HeaderText = "施工图名称";
            this.imgName.MappingName = "PhotoName";
            this.imgName.Width = 150;
            // 
            // UploadUser
            // 
            //this.UploadUser.Format = "";
            //this.UploadUser.FormatInfo = null;
            this.UploadUser.HeaderText = "上传人";
            this.UploadUser.MappingName = "UploadUser";
            this.UploadUser.Width = 150;
            // 
            // UploadTime
            // 
            //this.UploadTime.Format = "";
            //this.UploadTime.FormatInfo = null;
            this.UploadTime.HeaderText = "上传时间";
            this.UploadTime.MappingName = "UploadTime";
            this.UploadTime.Width = 250;
            // 
            // TrainGuiDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dgPic);
            this.Controls.Add(this.panel2);
            this.Name = "TrainGuiDetail";
            this.Size = new System.Drawing.Size(680, 383);
            this.Click += new System.EventHandler(this.TrainGuiDetail_Click);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Button btn_Downitem;
        private System.Windows.Forms.Button btn_Upitem;
        private System.Windows.Forms.Button btn_Look;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGrid dgPic;
        private System.Windows.Forms.DataGridTableStyle Details;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn ImgId;
        private System.Windows.Forms.DataGridTextBoxColumn imgPath;
        private System.Windows.Forms.DataGridTextBoxColumn imgName;
        private System.Windows.Forms.DataGridTextBoxColumn UploadUser;
        private System.Windows.Forms.DataGridTextBoxColumn UploadTime;
    }
}
