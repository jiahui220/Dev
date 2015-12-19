namespace TrainView.AlarmView
{
    partial class AlarmRecord
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
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.Alt = new System.Windows.Forms.DataGridTableStyle();
            this.ID = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AlarmItem = new System.Windows.Forms.DataGridTextBoxColumn();
            this.AlarmIntro = new System.Windows.Forms.DataGridTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Silver;
            this.button4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(566, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 30);
            this.button4.TabIndex = 7;
            this.button4.Text = "下一页";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Silver;
            this.button3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(462, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 30);
            this.button3.TabIndex = 6;
            this.button3.Text = "上一页";
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(129, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "下一条";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(25, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "上一条";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColor = System.Drawing.Color.White;
            this.dataGrid1.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.Location = new System.Drawing.Point(0, 51);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.SelectionBackColor = System.Drawing.Color.Blue;
            this.dataGrid1.Size = new System.Drawing.Size(679, 330);
            this.dataGrid1.TabIndex = 8;
            this.dataGrid1.TableStyles.Add(this.Alt);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // Alt
            // 
            this.Alt.GridColumnStyles.Add(this.ID);
            this.Alt.GridColumnStyles.Add(this.CreateTime);
            this.Alt.GridColumnStyles.Add(this.AlarmItem);
            this.Alt.GridColumnStyles.Add(this.AlarmIntro);
            this.Alt.MappingName = "Alt";
            // 
            // ID
            // 
            ////this.ID.Format = "";
            ////this.ID.FormatInfo = null;
            this.ID.HeaderText = "序号";
            this.ID.MappingName = "ID";
            this.ID.Width = 100;
            // 
            // CreateTime
            // 
            ////this.CreateTime.Format = "";
            ////this.CreateTime.FormatInfo = null;
            this.CreateTime.HeaderText = "创建时间";
            this.CreateTime.MappingName = "CreateTime";
            this.CreateTime.Width = 150;
            // 
            // AlarmItem
            // 
            //this.AlarmItem.format = "";
            //this.AlarmItem.FormatInfo = null;
            this.AlarmItem.HeaderText = "报警项目";
            this.AlarmItem.MappingName = "AlarmItem";
            this.AlarmItem.Width = 200;
            // 
            // AlarmIntro
            // 
            ////this.AlarmIntro.Format = "";
            ////this.AlarmIntro.FormatInfo = null;
            this.AlarmIntro.HeaderText = "报警介绍";
            this.AlarmIntro.MappingName = "AlarmIntro";
            this.AlarmIntro.Width = 200;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 51);
            this.panel1.GotFocus += new System.EventHandler(this.panel1_GotFocus);
            // 
            // AlarmRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Name = "AlarmRecord";
            this.Size = new System.Drawing.Size(679, 381);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridTableStyle Alt;
        private System.Windows.Forms.DataGridTextBoxColumn ID;
        private System.Windows.Forms.DataGridTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridTextBoxColumn AlarmItem;
        private System.Windows.Forms.DataGridTextBoxColumn AlarmIntro;
    }
}
