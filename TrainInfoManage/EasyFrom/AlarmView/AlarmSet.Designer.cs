namespace TrainView.AlarmView
{
    partial class AlarmSet
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
            this.lblNum = new System.Windows.Forms.Label();
            this.tagNum = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.tagContent = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.tagName = new System.Windows.Forms.Label();
            this.tagState = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.tagId = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.AlSet = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AlarmItem = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AlarmIntro = new System.Windows.Forms.DataGridTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNum
            // 
            this.lblNum.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblNum.ForeColor = System.Drawing.Color.Black;
            this.lblNum.Location = new System.Drawing.Point(270, 16);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(85, 20);
            // 
            // tagNum
            // 
            this.tagNum.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagNum.ForeColor = System.Drawing.Color.Black;
            this.tagNum.Location = new System.Drawing.Point(204, 16);
            this.tagNum.Name = "tagNum";
            this.tagNum.Size = new System.Drawing.Size(75, 20);
            this.tagNum.Text = "编号:";
            // 
            // lblContent
            // 
            this.lblContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContent.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblContent.ForeColor = System.Drawing.Color.Black;
            this.lblContent.Location = new System.Drawing.Point(119, 81);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(464, 52);
            // 
            // tagContent
            // 
            this.tagContent.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagContent.ForeColor = System.Drawing.Color.Black;
            this.tagContent.Location = new System.Drawing.Point(31, 81);
            this.tagContent.Name = "tagContent";
            this.tagContent.Size = new System.Drawing.Size(71, 20);
            this.tagContent.Text = "内容:";
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(119, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(464, 20);
            // 
            // tagName
            // 
            this.tagName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagName.ForeColor = System.Drawing.Color.Black;
            this.tagName.Location = new System.Drawing.Point(31, 48);
            this.tagName.Name = "tagName";
            this.tagName.Size = new System.Drawing.Size(71, 20);
            this.tagName.Text = "名称:";
            // 
            // tagState
            // 
            this.tagState.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagState.ForeColor = System.Drawing.Color.Black;
            this.tagState.Location = new System.Drawing.Point(361, 16);
            this.tagState.Name = "tagState";
            this.tagState.Size = new System.Drawing.Size(213, 20);
            this.tagState.Text = "设置报警项点启用状态:";
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.lblId.ForeColor = System.Drawing.Color.Black;
            this.lblId.Location = new System.Drawing.Point(119, 16);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(79, 20);
            // 
            // tagId
            // 
            this.tagId.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagId.ForeColor = System.Drawing.Color.Black;
            this.tagId.Location = new System.Drawing.Point(31, 16);
            this.tagId.Name = "tagId";
            this.tagId.Size = new System.Drawing.Size(71, 20);
            this.tagId.Text = "序号:";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColor = System.Drawing.Color.White;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.ForeColor = System.Drawing.Color.Black;
            this.dataGrid1.Location = new System.Drawing.Point(0, 143);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.Blue;
            this.dataGrid1.Size = new System.Drawing.Size(629, 238);
            this.dataGrid1.TabIndex = 18;
            this.dataGrid1.TableStyles.Add(this.AlSet);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // AlSet
            // 
            this.AlSet.GridColumnStyles.Add(this.ID);
            this.AlSet.GridColumnStyles.Add(this.AlarmItem);
            this.AlSet.GridColumnStyles.Add(this.AlarmIntro);
            this.AlSet.MappingName = "AlSet";
            // 
            // ID
            // 
            //this.ID.Format = "";
            //this.ID.FormatInfo = null;
            this.ID.HeaderText = "序号";
            this.ID.MappingName = "ID";
            this.ID.Width = 100;
            // 
            // AlarmItem
            // 
            //this.AlarmItem.Format = "";
            //this.AlarmItem.FormatInfo = null;
            this.AlarmItem.HeaderText = "报警项目";
            this.AlarmItem.MappingName = "AlarmItem";
            this.AlarmItem.Width = 200;
            // 
            // AlarmIntro
            // 
            //this.AlarmIntro.Format = "";
            //this.AlarmIntro.FormatInfo = null;
            this.AlarmIntro.HeaderText = "报警介绍";
            this.AlarmIntro.MappingName = "AlarmIntro";
            this.AlarmIntro.Width = 200;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(8, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 71);
            this.button1.TabIndex = 19;
            this.button1.Text = "上";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(8, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 71);
            this.button2.TabIndex = 20;
            this.button2.Text = "下";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tagNum);
            this.panel1.Controls.Add(this.tagId);
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Controls.Add(this.tagState);
            this.panel1.Controls.Add(this.lblNum);
            this.panel1.Controls.Add(this.tagName);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblContent);
            this.panel1.Controls.Add(this.tagContent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 143);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(596, 102);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 30);
            this.button4.TabIndex = 11;
            this.button4.Text = "下一页";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(596, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 30);
            this.button3.TabIndex = 10;
            this.button3.Text = "上一页";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(581, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.Text = "开  启";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(629, 143);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(50, 238);
            // 
            // AlarmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AlarmSet";
            this.Size = new System.Drawing.Size(679, 381);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label tagNum;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label tagContent;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label tagName;
        private System.Windows.Forms.Label tagState;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label tagId;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridTableStyle AlSet;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn AlarmItem;
        private System.Windows.Forms.DataGridTextBoxColumn AlarmIntro;
    }
}
