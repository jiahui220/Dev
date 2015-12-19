namespace TrainView.ChildFrom
{
    partial class Dlg_Book
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_nextPage = new System.Windows.Forms.Button();
            this.btn_prePage = new System.Windows.Forms.Button();
            this.btn_Down = new System.Windows.Forms.Button();
            this.btn_Up = new System.Windows.Forms.Button();
            this.treeChapter = new System.Windows.Forms.TreeView();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblPage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 40);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(290, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 24);
            this.label1.Text = "书 籍 信 息";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Controls.Add(this.btn_nextPage);
            this.panel2.Controls.Add(this.btn_prePage);
            this.panel2.Controls.Add(this.btn_Down);
            this.panel2.Controls.Add(this.btn_Up);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 371);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(700, 49);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Silver;
            this.btn_Close.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Close.ForeColor = System.Drawing.Color.Black;
            this.btn_Close.Location = new System.Drawing.Point(606, 8);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 30);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "关闭";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_nextPage
            // 
            this.btn_nextPage.BackColor = System.Drawing.Color.Silver;
            this.btn_nextPage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_nextPage.ForeColor = System.Drawing.Color.Black;
            this.btn_nextPage.Location = new System.Drawing.Point(295, 8);
            this.btn_nextPage.Name = "btn_nextPage";
            this.btn_nextPage.Size = new System.Drawing.Size(75, 30);
            this.btn_nextPage.TabIndex = 3;
            this.btn_nextPage.Text = "下一页";
            this.btn_nextPage.Click += new System.EventHandler(this.btn_nextPage_Click);
            // 
            // btn_prePage
            // 
            this.btn_prePage.BackColor = System.Drawing.Color.Silver;
            this.btn_prePage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_prePage.ForeColor = System.Drawing.Color.Black;
            this.btn_prePage.Location = new System.Drawing.Point(201, 8);
            this.btn_prePage.Name = "btn_prePage";
            this.btn_prePage.Size = new System.Drawing.Size(75, 30);
            this.btn_prePage.TabIndex = 2;
            this.btn_prePage.Text = "上一页";
            this.btn_prePage.Click += new System.EventHandler(this.btn_prePage_Click);
            // 
            // btn_Down
            // 
            this.btn_Down.BackColor = System.Drawing.Color.Silver;
            this.btn_Down.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Down.ForeColor = System.Drawing.Color.Black;
            this.btn_Down.Location = new System.Drawing.Point(107, 8);
            this.btn_Down.Name = "btn_Down";
            this.btn_Down.Size = new System.Drawing.Size(75, 30);
            this.btn_Down.TabIndex = 1;
            this.btn_Down.Text = "下一章";
            this.btn_Down.Click += new System.EventHandler(this.btn_Down_Click);
            // 
            // btn_Up
            // 
            this.btn_Up.BackColor = System.Drawing.Color.Silver;
            this.btn_Up.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.btn_Up.ForeColor = System.Drawing.Color.Black;
            this.btn_Up.Location = new System.Drawing.Point(12, 8);
            this.btn_Up.Name = "btn_Up";
            this.btn_Up.Size = new System.Drawing.Size(75, 30);
            this.btn_Up.TabIndex = 0;
            this.btn_Up.Text = "上一章";
            this.btn_Up.Click += new System.EventHandler(this.btn_Up_Click);
            // 
            // treeChapter
            // 
            this.treeChapter.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeChapter.Location = new System.Drawing.Point(0, 40);
            this.treeChapter.Name = "treeChapter";
            this.treeChapter.Size = new System.Drawing.Size(152, 331);
            this.treeChapter.TabIndex = 2;
            this.treeChapter.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeChapter_AfterSelect);
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(152, 40);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(548, 311);
            this.txtContent.TabIndex = 3;
            // 
            // lblPage
            // 
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPage.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular);
            this.lblPage.ForeColor = System.Drawing.Color.Black;
            this.lblPage.Location = new System.Drawing.Point(152, 351);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(548, 20);
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Dlg_Book
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(700, 420);
            this.ControlBox = false;
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.treeChapter);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Dlg_Book";
            this.Text = "Dlg_Book";
            this.Load += new System.EventHandler(this.Dlg_Book_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Up;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_nextPage;
        private System.Windows.Forms.Button btn_prePage;
        private System.Windows.Forms.Button btn_Down;
        private System.Windows.Forms.TreeView treeChapter;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblPage;
    }
}