namespace TrainView.GuideView
{
    partial class TrainGuideNotice
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
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Look = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgNotice = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Silver;
            this.btn_Delete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Delete.ForeColor = System.Drawing.Color.Black;
            this.btn_Delete.Location = new System.Drawing.Point(14, 5);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(80, 30);
            this.btn_Delete.TabIndex = 74;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Look
            // 
            this.btn_Look.BackColor = System.Drawing.Color.Silver;
            this.btn_Look.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Look.ForeColor = System.Drawing.Color.Black;
            this.btn_Look.Location = new System.Drawing.Point(110, 5);
            this.btn_Look.Name = "btn_Look";
            this.btn_Look.Size = new System.Drawing.Size(80, 30);
            this.btn_Look.TabIndex = 78;
            this.btn_Look.Text = "查看";
            this.btn_Look.Click += new System.EventHandler(this.button7_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(450, 5);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(80, 30);
            this.btn_Uppage.TabIndex = 75;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(548, 5);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(80, 30);
            this.btn_Downpage.TabIndex = 73;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.BackColor = System.Drawing.Color.Silver;
            this.btn_Up.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Up.ForeColor = System.Drawing.Color.Black;
            this.btn_Up.Location = new System.Drawing.Point(5, 73);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(40, 73);
            this.btn_Up.TabIndex = 122;
            this.btn_Up.Text = "上";
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.BackColor = System.Drawing.Color.Silver;
            this.btn_Down.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Down.ForeColor = System.Drawing.Color.Black;
            this.btn_Down.Location = new System.Drawing.Point(5, 188);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(40, 73);
            this.btn_Down.TabIndex = 123;
            this.btn_Down.Text = "下";
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.btn_Uppage);
            this.panel2.Controls.Add(this.btn_Downpage);
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
            this.panel3.Controls.Add(this.btn_Down);
            this.panel3.Controls.Add(this.btn_Up);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(623, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(51, 340);
            // 
            // dgNotice
            // 
            this.dgNotice.BackgroundColor = System.Drawing.Color.White;
            this.dgNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNotice.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dgNotice.ForeColor = System.Drawing.Color.Black;
            this.dgNotice.Location = new System.Drawing.Point(0, 42);
            this.dgNotice.Name = "dgNotice";
            this.dgNotice.SelectionBackColor = System.Drawing.Color.Blue;
            this.dgNotice.Size = new System.Drawing.Size(623, 340);
            this.dgNotice.TabIndex = 127;
            this.dgNotice.TableStyles.Add(this.dataGridTableStyle1);
            this.dgNotice.CurrentCellChanged += new System.EventHandler(this.dgNotice_CurrentCellChanged);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.MappingName = "Notice";
            // 
            // dataGridTextBoxColumn1
            // 
            //this.dataGridTextBoxColumn1.Format = "";
            //this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "序号";
            this.dataGridTextBoxColumn1.MappingName = "ID";
            this.dataGridTextBoxColumn1.Width = 100;
            // 
            // dataGridTextBoxColumn2
            // 
            //this.dataGridTextBoxColumn2.Format = "";
            //this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.MappingName = "AID";
            this.dataGridTextBoxColumn2.Width = 0;
            // 
            // dataGridTextBoxColumn3
            // 
            //this.dataGridTextBoxColumn3.Format = "";
            //this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "标题";
            this.dataGridTextBoxColumn3.MappingName = "Title";
            this.dataGridTextBoxColumn3.Width = 100;
            // 
            // dataGridTextBoxColumn4
            // 
            //this.dataGridTextBoxColumn4.Format = "";
            //this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "内容";
            this.dataGridTextBoxColumn4.MappingName = "AnnoContent";
            this.dataGridTextBoxColumn4.Width = 150;
            // 
            // dataGridTextBoxColumn5
            // 
            //this.dataGridTextBoxColumn5.Format = "";
            //this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "发送人";
            this.dataGridTextBoxColumn5.MappingName = "SendPerson";
            this.dataGridTextBoxColumn5.Width = 100;
            // 
            // dataGridTextBoxColumn6
            // 
            //this.dataGridTextBoxColumn6.Format = "";
            //this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "发送时间";
            this.dataGridTextBoxColumn6.MappingName = "ReceTime";
            this.dataGridTextBoxColumn6.Width = 150;
            // 
            // TrainGuideNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dgNotice);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "TrainGuideNotice";
            this.Size = new System.Drawing.Size(674, 382);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Look;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGrid dgNotice;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
    }
}
