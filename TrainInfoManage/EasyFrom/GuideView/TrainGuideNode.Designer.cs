namespace TrainView.GuideView
{
    partial class TrainGuideNode
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
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.btn_Look = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Amend = new System.Windows.Forms.Button();
            this.btn_New = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgNote = new System.Windows.Forms.DataGrid();
            this.Note = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Down
            // 
            this.btn_Down.BackColor = System.Drawing.Color.Silver;
            this.btn_Down.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Down.ForeColor = System.Drawing.Color.Black;
            this.btn_Down.Location = new System.Drawing.Point(5, 189);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(40, 78);
            this.btn_Down.TabIndex = 120;
            this.btn_Down.Text = "下";
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.BackColor = System.Drawing.Color.Silver;
            this.btn_Up.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Up.ForeColor = System.Drawing.Color.Black;
            this.btn_Up.Location = new System.Drawing.Point(5, 71);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(40, 78);
            this.btn_Up.TabIndex = 119;
            this.btn_Up.Text = "上";
            this.btn_Up.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(547, 6);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(80, 30);
            this.btn_Downpage.TabIndex = 67;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(451, 6);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(80, 30);
            this.btn_Uppage.TabIndex = 68;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // btn_Look
            // 
            this.btn_Look.BackColor = System.Drawing.Color.Silver;
            this.btn_Look.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Look.ForeColor = System.Drawing.Color.Black;
            this.btn_Look.Location = new System.Drawing.Point(299, 6);
            this.btn_Look.Name = "btn_Look";
            this.btn_Look.Size = new System.Drawing.Size(80, 30);
            this.btn_Look.TabIndex = 69;
            this.btn_Look.Text = "查看";
            this.btn_Look.Click += new System.EventHandler(this.btn_Look_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Silver;
            this.btn_Delete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Delete.ForeColor = System.Drawing.Color.Black;
            this.btn_Delete.Location = new System.Drawing.Point(204, 6);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(80, 30);
            this.btn_Delete.TabIndex = 70;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Amend
            // 
            this.btn_Amend.BackColor = System.Drawing.Color.Silver;
            this.btn_Amend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Amend.ForeColor = System.Drawing.Color.Black;
            this.btn_Amend.Location = new System.Drawing.Point(110, 6);
            this.btn_Amend.Name = "btn_Amend";
            this.btn_Amend.Size = new System.Drawing.Size(80, 30);
            this.btn_Amend.TabIndex = 71;
            this.btn_Amend.Text = "修改";
            this.btn_Amend.Click += new System.EventHandler(this.btn_Amend_Click);
            // 
            // btn_New
            // 
            this.btn_New.BackColor = System.Drawing.Color.Silver;
            this.btn_New.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_New.ForeColor = System.Drawing.Color.Black;
            this.btn_New.Location = new System.Drawing.Point(15, 6);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(80, 30);
            this.btn_New.TabIndex = 67;
            this.btn_New.Text = "新建";
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgNote);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 340);
            // 
            // dgNote
            // 
            this.dgNote.BackgroundColor = System.Drawing.Color.White;
            this.dgNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNote.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dgNote.Location = new System.Drawing.Point(0, 0);
            this.dgNote.Name = "dgNote";
            this.dgNote.SelectionBackColor = System.Drawing.Color.Blue;
            this.dgNote.Size = new System.Drawing.Size(623, 340);
            this.dgNote.TabIndex = 0;
            this.dgNote.TableStyles.Add(this.Note);
            this.dgNote.CurrentCellChanged += new System.EventHandler(this.dgNote_CurrentCellChanged);
            // 
            // Note
            // 
            this.Note.GridColumnStyles.Add(this.ID);
            this.Note.GridColumnStyles.Add(this.Title);
            this.Note.GridColumnStyles.Add(this.Content);
            this.Note.GridColumnStyles.Add(this.CreateTime);
            this.Note.MappingName = "Note";
            // 
            // ID
            ////// 
            ////this.ID.Format = "";
            ////this.ID.FormatInfo = null;
            this.ID.HeaderText = "编号";
            this.ID.MappingName = "ID";
            this.ID.NullText = "";
            this.ID.Width = 100;
            // 
            // Title
            // 
            ////this.Title.Format = "";
            ////this.Title.FormatInfo = null;
            this.Title.HeaderText = "标题";
            this.Title.MappingName = "Title";
            this.Title.Width = 100;
            // 
            // Content
            // 
            ////this.Content.Format = "";
            ////this.Content.FormatInfo = null;
            this.Content.HeaderText = "内容";
            this.Content.MappingName = "Content";
            this.Content.Width = 200;
            // 
            // CreateTime
            // 
            ////this.CreateTime.Format = "";
            ////this.CreateTime.FormatInfo = null;
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.MappingName = "CreateTime";
            this.CreateTime.Width = 150;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btn_Amend);
            this.panel2.Controls.Add(this.btn_Downpage);
            this.panel2.Controls.Add(this.btn_New);
            this.panel2.Controls.Add(this.btn_Uppage);
            this.panel2.Controls.Add(this.btn_Look);
            this.panel2.Controls.Add(this.btn_Delete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(674, 42);
            this.panel2.GotFocus += new System.EventHandler(this.panel2_GotFocus);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.btn_Up);
            this.panel3.Controls.Add(this.btn_Down);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(623, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(51, 340);
            // 
            // TrainGuideNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "TrainGuideNode";
            this.Size = new System.Drawing.Size(674, 382);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Button btn_Look;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Amend;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGrid dgNote;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn Title;
        private System.Windows.Forms.DataGridTextBoxColumn Content;
        private System.Windows.Forms.DataGridTextBoxColumn CreateTime;
        public System.Windows.Forms.DataGridTableStyle Note;
    }
}
