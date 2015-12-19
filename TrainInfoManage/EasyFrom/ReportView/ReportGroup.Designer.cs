namespace EasyFrom.ReportView
{
    partial class ReportGroup
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
            this.btn_Upitem = new System.Windows.Forms.Button();
            this.btn_Downitem = new System.Windows.Forms.Button();
            this.btn_Uppage = new System.Windows.Forms.Button();
            this.btn_Downpage = new System.Windows.Forms.Button();
            this.dgv_RunGroup = new System.Windows.Forms.DataGrid();
            this.RunAndGroup = new System.Windows.Forms.DataGridTableStyle();
            this.TrainNum = new System.Windows.Forms.DataGridTextBoxColumn();
            this.StationName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ArrivedTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.SetOutTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Amend = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Upitem
            // 
            this.btn_Upitem.BackColor = System.Drawing.Color.Silver;
            this.btn_Upitem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Upitem.ForeColor = System.Drawing.Color.Black;
            this.btn_Upitem.Location = new System.Drawing.Point(23, 2);
            this.btn_Upitem.Name = "btn_Upitem";
            this.btn_Upitem.Size = new System.Drawing.Size(80, 37);
            this.btn_Upitem.TabIndex = 0;
            this.btn_Upitem.Text = "上一条";
            this.btn_Upitem.Click += new System.EventHandler(this.btn_Upitem_Click);
            // 
            // btn_Downitem
            // 
            this.btn_Downitem.BackColor = System.Drawing.Color.Silver;
            this.btn_Downitem.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Downitem.ForeColor = System.Drawing.Color.Black;
            this.btn_Downitem.Location = new System.Drawing.Point(127, 2);
            this.btn_Downitem.Name = "btn_Downitem";
            this.btn_Downitem.Size = new System.Drawing.Size(80, 37);
            this.btn_Downitem.TabIndex = 1;
            this.btn_Downitem.Text = "下一条";
            this.btn_Downitem.Click += new System.EventHandler(this.btn_Downitem_Click);
            // 
            // btn_Uppage
            // 
            this.btn_Uppage.BackColor = System.Drawing.Color.Silver;
            this.btn_Uppage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Uppage.ForeColor = System.Drawing.Color.Black;
            this.btn_Uppage.Location = new System.Drawing.Point(460, 2);
            this.btn_Uppage.Name = "btn_Uppage";
            this.btn_Uppage.Size = new System.Drawing.Size(80, 37);
            this.btn_Uppage.TabIndex = 2;
            this.btn_Uppage.Text = "上一页";
            this.btn_Uppage.Click += new System.EventHandler(this.btn_Uppage_Click);
            // 
            // btn_Downpage
            // 
            this.btn_Downpage.BackColor = System.Drawing.Color.Silver;
            this.btn_Downpage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Downpage.ForeColor = System.Drawing.Color.Black;
            this.btn_Downpage.Location = new System.Drawing.Point(564, 2);
            this.btn_Downpage.Name = "btn_Downpage";
            this.btn_Downpage.Size = new System.Drawing.Size(80, 37);
            this.btn_Downpage.TabIndex = 3;
            this.btn_Downpage.Text = "下一页";
            this.btn_Downpage.Click += new System.EventHandler(this.btn_Downpage_Click);
            // 
            // dgv_RunGroup
            // 
            this.dgv_RunGroup.BackColor = System.Drawing.Color.White;
            this.dgv_RunGroup.BackgroundColor = System.Drawing.Color.White;
            this.dgv_RunGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_RunGroup.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dgv_RunGroup.Location = new System.Drawing.Point(0, 39);
            this.dgv_RunGroup.Name = "dgv_RunGroup";
            this.dgv_RunGroup.SelectionBackColor = System.Drawing.Color.Blue;
            this.dgv_RunGroup.Size = new System.Drawing.Size(674, 300);
            this.dgv_RunGroup.TabIndex = 4;
            this.dgv_RunGroup.TableStyles.Add(this.RunAndGroup);
            this.dgv_RunGroup.CurrentCellChanged += new System.EventHandler(this.dgv_RunGroup_CurrentCellChanged);
            // 
            // RunAndGroup
            // 
            this.RunAndGroup.GridColumnStyles.Add(this.TrainNum);
            this.RunAndGroup.GridColumnStyles.Add(this.StationName);
            this.RunAndGroup.GridColumnStyles.Add(this.ArrivedTime);
            this.RunAndGroup.GridColumnStyles.Add(this.SetOutTime);
            this.RunAndGroup.MappingName = "RunAndGroup";
            // 
            // TrainNum
            // 
            //this.TrainNum.Format = "";
            //this.TrainNum.FormatInfo = null;
            this.TrainNum.HeaderText = "车次";
            this.TrainNum.MappingName = "TrainNum";
            this.TrainNum.Width = 80;
            // 
            // StationName
            // 
            ////this.StationName.Format = "";
            ////this.StationName.FormatInfo = null;
            this.StationName.HeaderText = "站名";
            this.StationName.MappingName = "StationName";
            this.StationName.Width = 150;
            // 
            // ArrivedTime
            // 
            //this.ArrivedTime.Format = "";
            //this.ArrivedTime.FormatInfo = null;
            this.ArrivedTime.HeaderText = "到达时分";
            this.ArrivedTime.MappingName = "ArrivedTime";
            this.ArrivedTime.Width = 200;
            // 
            // SetOutTime
            // 
            //this.SetOutTime.Format = "";
            //this.SetOutTime.FormatInfo = null;
            this.SetOutTime.HeaderText = "出发时分";
            this.SetOutTime.MappingName = "SetOutTime";
            this.SetOutTime.Width = 200;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_Amend);
            this.panel1.Controls.Add(this.btn_Uppage);
            this.panel1.Controls.Add(this.btn_Upitem);
            this.panel1.Controls.Add(this.btn_Downpage);
            this.panel1.Controls.Add(this.btn_Downitem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 39);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "刷新";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Amend
            // 
            this.btn_Amend.BackColor = System.Drawing.Color.Silver;
            this.btn_Amend.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Amend.ForeColor = System.Drawing.Color.Black;
            this.btn_Amend.Location = new System.Drawing.Point(244, 2);
            this.btn_Amend.Name = "btn_Amend";
            this.btn_Amend.Size = new System.Drawing.Size(80, 37);
            this.btn_Amend.TabIndex = 4;
            this.btn_Amend.Text = "修改";
            this.btn_Amend.Click += new System.EventHandler(this.btn_Amend_Click);
            // 
            // ReportGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dgv_RunGroup);
            this.Controls.Add(this.panel1);
            this.Name = "ReportGroup";
            this.Size = new System.Drawing.Size(674, 339);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Upitem;
        private System.Windows.Forms.Button btn_Downitem;
        private System.Windows.Forms.Button btn_Uppage;
        private System.Windows.Forms.Button btn_Downpage;
        private System.Windows.Forms.DataGrid dgv_RunGroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Amend;
        private System.Windows.Forms.DataGridTableStyle RunAndGroup;
        private System.Windows.Forms.DataGridTextBoxColumn TrainNum;
        private System.Windows.Forms.DataGridTextBoxColumn StationName;
        private System.Windows.Forms.DataGridTextBoxColumn ArrivedTime;
        private System.Windows.Forms.DataGridTextBoxColumn SetOutTime;
        private System.Windows.Forms.Button button1;
    }
}
