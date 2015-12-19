namespace EasyFrom
{
    partial class TrainBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainBase));
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblArriveTime = new System.Windows.Forms.Label();
            this.lblCurrStation = new System.Windows.Forms.Label();
            this.tagStartTime = new System.Windows.Forms.Label();
            this.tagArriveTime = new System.Windows.Forms.Label();
            this.tagCurrStation = new System.Windows.Forms.Label();
            this.txtReceiveTime = new System.Windows.Forms.TextBox();
            this.tagRecevieTime = new System.Windows.Forms.Label();
            this.txtWorkTime = new System.Windows.Forms.TextBox();
            this.tagWorkTime = new System.Windows.Forms.Label();
            this.pic_right_center_line = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblDriver = new System.Windows.Forms.Label();
            this.tagDriver = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.tagNum = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.tagId = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.tagType = new System.Windows.Forms.Label();
            this.txtSubmitTime = new System.Windows.Forms.TextBox();
            this.tagSubmitTime = new System.Windows.Forms.Label();
            this.lblKilometer = new System.Windows.Forms.Label();
            this.tagKilometer = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.tagLatitude = new System.Windows.Forms.Label();
            this.tagLongitude = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Notice = new System.Windows.Forms.Button();
            this.lbl_Notice = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.sp_Lkj = new System.IO.Ports.SerialPort(this.components);
            this.sp_GPS = new System.IO.Ports.SerialPort(this.components);
            this.sp_Can = new System.IO.Ports.SerialPort(this.components);
            this.lblKiloNum = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStartTime
            // 
            this.lblStartTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartTime.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblStartTime.ForeColor = System.Drawing.Color.Blue;
            this.lblStartTime.Location = new System.Drawing.Point(120, 129);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(265, 35);
            // 
            // lblArriveTime
            // 
            this.lblArriveTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblArriveTime.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblArriveTime.ForeColor = System.Drawing.Color.Blue;
            this.lblArriveTime.Location = new System.Drawing.Point(120, 78);
            this.lblArriveTime.Name = "lblArriveTime";
            this.lblArriveTime.Size = new System.Drawing.Size(265, 30);
            // 
            // lblCurrStation
            // 
            this.lblCurrStation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrStation.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblCurrStation.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrStation.Location = new System.Drawing.Point(120, 27);
            this.lblCurrStation.Name = "lblCurrStation";
            this.lblCurrStation.Size = new System.Drawing.Size(265, 31);
            // 
            // tagStartTime
            // 
            this.tagStartTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagStartTime.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagStartTime.ForeColor = System.Drawing.Color.Black;
            this.tagStartTime.Location = new System.Drawing.Point(19, 129);
            this.tagStartTime.Name = "tagStartTime";
            this.tagStartTime.Size = new System.Drawing.Size(126, 35);
            this.tagStartTime.Text = "开车时间:";
            // 
            // tagArriveTime
            // 
            this.tagArriveTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagArriveTime.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagArriveTime.ForeColor = System.Drawing.Color.Black;
            this.tagArriveTime.Location = new System.Drawing.Point(19, 78);
            this.tagArriveTime.Name = "tagArriveTime";
            this.tagArriveTime.Size = new System.Drawing.Size(126, 30);
            this.tagArriveTime.Text = "到达时间:";
            // 
            // tagCurrStation
            // 
            this.tagCurrStation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagCurrStation.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagCurrStation.ForeColor = System.Drawing.Color.Black;
            this.tagCurrStation.Location = new System.Drawing.Point(19, 27);
            this.tagCurrStation.Name = "tagCurrStation";
            this.tagCurrStation.Size = new System.Drawing.Size(126, 31);
            this.tagCurrStation.Text = "本　　站:";
            // 
            // txtReceiveTime
            // 
            this.txtReceiveTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtReceiveTime.BackColor = System.Drawing.Color.White;
            this.txtReceiveTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtReceiveTime.Location = new System.Drawing.Point(398, 127);
            this.txtReceiveTime.Name = "txtReceiveTime";
            this.txtReceiveTime.Size = new System.Drawing.Size(265, 28);
            this.txtReceiveTime.TabIndex = 14;
            this.txtReceiveTime.GotFocus += new System.EventHandler(this.txtReceiveTime_GotFocus);
            // 
            // tagRecevieTime
            // 
            this.tagRecevieTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tagRecevieTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagRecevieTime.ForeColor = System.Drawing.Color.OrangeRed;
            this.tagRecevieTime.Location = new System.Drawing.Point(401, 101);
            this.tagRecevieTime.Name = "tagRecevieTime";
            this.tagRecevieTime.Size = new System.Drawing.Size(133, 25);
            this.tagRecevieTime.Text = "接车时分:";
            // 
            // txtWorkTime
            // 
            this.txtWorkTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtWorkTime.BackColor = System.Drawing.Color.White;
            this.txtWorkTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtWorkTime.Location = new System.Drawing.Point(398, 53);
            this.txtWorkTime.Name = "txtWorkTime";
            this.txtWorkTime.Size = new System.Drawing.Size(265, 28);
            this.txtWorkTime.TabIndex = 15;
            this.txtWorkTime.GotFocus += new System.EventHandler(this.txtWorkTime_GotFocus);
            // 
            // tagWorkTime
            // 
            this.tagWorkTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tagWorkTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagWorkTime.ForeColor = System.Drawing.Color.OrangeRed;
            this.tagWorkTime.Location = new System.Drawing.Point(401, 27);
            this.tagWorkTime.Name = "tagWorkTime";
            this.tagWorkTime.Size = new System.Drawing.Size(133, 25);
            this.tagWorkTime.Text = "出勤时分:";
            // 
            // pic_right_center_line
            // 
            this.pic_right_center_line.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pic_right_center_line.Image = ((System.Drawing.Image)(resources.GetObject("pic_right_center_line.Image")));
            this.pic_right_center_line.Location = new System.Drawing.Point(19, 172);
            this.pic_right_center_line.Name = "pic_right_center_line";
            this.pic_right_center_line.Size = new System.Drawing.Size(389, 1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(19, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 32);
            this.label1.Text = "车　速:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSpeed.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular);
            this.lblSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblSpeed.Location = new System.Drawing.Point(121, 297);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(198, 57);
            // 
            // lblDriver
            // 
            this.lblDriver.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDriver.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblDriver.ForeColor = System.Drawing.Color.Blue;
            this.lblDriver.Location = new System.Drawing.Point(276, 255);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(118, 32);
            // 
            // tagDriver
            // 
            this.tagDriver.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagDriver.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagDriver.ForeColor = System.Drawing.Color.Black;
            this.tagDriver.Location = new System.Drawing.Point(193, 255);
            this.tagDriver.Name = "tagDriver";
            this.tagDriver.Size = new System.Drawing.Size(91, 32);
            this.tagDriver.Text = "司　机:";
            // 
            // lblNum
            // 
            this.lblNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNum.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblNum.ForeColor = System.Drawing.Color.Blue;
            this.lblNum.Location = new System.Drawing.Point(107, 255);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(80, 32);
            // 
            // tagNum
            // 
            this.tagNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagNum.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagNum.ForeColor = System.Drawing.Color.Black;
            this.tagNum.Location = new System.Drawing.Point(19, 255);
            this.tagNum.Name = "tagNum";
            this.tagNum.Size = new System.Drawing.Size(97, 32);
            this.tagNum.Text = "车　次:";
            // 
            // lblId
            // 
            this.lblId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblId.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblId.ForeColor = System.Drawing.Color.Blue;
            this.lblId.Location = new System.Drawing.Point(276, 204);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(118, 33);
            // 
            // tagId
            // 
            this.tagId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagId.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagId.ForeColor = System.Drawing.Color.Black;
            this.tagId.Location = new System.Drawing.Point(193, 204);
            this.tagId.Name = "tagId";
            this.tagId.Size = new System.Drawing.Size(91, 33);
            this.tagId.Text = "车　号:";
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblType.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblType.ForeColor = System.Drawing.Color.Blue;
            this.lblType.Location = new System.Drawing.Point(107, 204);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(80, 33);
            // 
            // tagType
            // 
            this.tagType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tagType.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagType.ForeColor = System.Drawing.Color.Black;
            this.tagType.Location = new System.Drawing.Point(19, 204);
            this.tagType.Name = "tagType";
            this.tagType.Size = new System.Drawing.Size(97, 33);
            this.tagType.Text = "机　型:";
            // 
            // txtSubmitTime
            // 
            this.txtSubmitTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtSubmitTime.BackColor = System.Drawing.Color.White;
            this.txtSubmitTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.txtSubmitTime.Location = new System.Drawing.Point(398, 209);
            this.txtSubmitTime.Name = "txtSubmitTime";
            this.txtSubmitTime.Size = new System.Drawing.Size(186, 28);
            this.txtSubmitTime.TabIndex = 47;
            this.txtSubmitTime.TextChanged += new System.EventHandler(this.txtSubmitTime_TextChanged);
            this.txtSubmitTime.GotFocus += new System.EventHandler(this.txtSubmitTime_GotFocus);
            // 
            // tagSubmitTime
            // 
            this.tagSubmitTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tagSubmitTime.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular);
            this.tagSubmitTime.ForeColor = System.Drawing.Color.OrangeRed;
            this.tagSubmitTime.Location = new System.Drawing.Point(401, 183);
            this.tagSubmitTime.Name = "tagSubmitTime";
            this.tagSubmitTime.Size = new System.Drawing.Size(133, 25);
            this.tagSubmitTime.Text = "交车时分:";
            // 
            // lblKilometer
            // 
            this.lblKilometer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKilometer.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblKilometer.ForeColor = System.Drawing.Color.Blue;
            this.lblKilometer.Location = new System.Drawing.Point(493, 324);
            this.lblKilometer.Name = "lblKilometer";
            this.lblKilometer.Size = new System.Drawing.Size(170, 31);
            // 
            // tagKilometer
            // 
            this.tagKilometer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tagKilometer.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagKilometer.ForeColor = System.Drawing.Color.Black;
            this.tagKilometer.Location = new System.Drawing.Point(401, 327);
            this.tagKilometer.Name = "tagKilometer";
            this.tagKilometer.Size = new System.Drawing.Size(86, 31);
            this.tagKilometer.Text = "公里标:";
            // 
            // lblLongitude
            // 
            this.lblLongitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLongitude.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblLongitude.ForeColor = System.Drawing.Color.Blue;
            this.lblLongitude.Location = new System.Drawing.Point(493, 250);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(170, 31);
            // 
            // lblLatitude
            // 
            this.lblLatitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLatitude.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblLatitude.ForeColor = System.Drawing.Color.Blue;
            this.lblLatitude.Location = new System.Drawing.Point(493, 285);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(170, 34);
            this.lblLatitude.ParentChanged += new System.EventHandler(this.lblLatitude_ParentChanged);
            // 
            // tagLatitude
            // 
            this.tagLatitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tagLatitude.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagLatitude.ForeColor = System.Drawing.Color.Black;
            this.tagLatitude.Location = new System.Drawing.Point(401, 291);
            this.tagLatitude.Name = "tagLatitude";
            this.tagLatitude.Size = new System.Drawing.Size(86, 34);
            this.tagLatitude.Text = "纬　度:";
            // 
            // tagLongitude
            // 
            this.tagLongitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tagLongitude.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.tagLongitude.ForeColor = System.Drawing.Color.Black;
            this.tagLongitude.Location = new System.Drawing.Point(401, 253);
            this.tagLongitude.Name = "tagLongitude";
            this.tagLongitude.Size = new System.Drawing.Size(86, 31);
            this.tagLongitude.Text = "经　度:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.panel1.Controls.Add(this.btn_Notice);
            this.panel1.Controls.Add(this.lbl_Notice);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 403);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 40);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.GotFocus += new System.EventHandler(this.panel1_GotFocus);
            // 
            // btn_Notice
            // 
            this.btn_Notice.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Notice.Location = new System.Drawing.Point(607, 0);
            this.btn_Notice.Name = "btn_Notice";
            this.btn_Notice.Size = new System.Drawing.Size(72, 40);
            this.btn_Notice.TabIndex = 0;
            this.btn_Notice.Text = "查看";
            // 
            // lbl_Notice
            // 
            this.lbl_Notice.ForeColor = System.Drawing.Color.White;
            this.lbl_Notice.Location = new System.Drawing.Point(374, 9);
            this.lbl_Notice.Name = "lbl_Notice";
            this.lbl_Notice.Size = new System.Drawing.Size(229, 20);
            this.lbl_Notice.ParentChanged += new System.EventHandler(this.lbl_Notice_ParentChanged);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
            this.btn_Clear.ForeColor = System.Drawing.Color.Black;
            this.btn_Clear.Location = new System.Drawing.Point(590, 208);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(73, 30);
            this.btn_Clear.TabIndex = 0;
            this.btn_Clear.Text = "清 空";
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // sp_Lkj
            // 
            this.sp_Lkj.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_Lkj_DataReceived);
            // 
            // sp_GPS
            // 
            this.sp_GPS.BaudRate = 115200;
            this.sp_GPS.PortName = "COM8";
            this.sp_GPS.ReadTimeout = 1000;
            this.sp_GPS.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_GPS_DataReceived);
            // 
            // sp_Can
            // 
            this.sp_Can.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sp_Can_DataReceived);
            // 
            // lblKiloNum
            // 
            this.lblKiloNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKiloNum.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular);
            this.lblKiloNum.ForeColor = System.Drawing.Color.Blue;
            this.lblKiloNum.Location = new System.Drawing.Point(493, 362);
            this.lblKiloNum.Name = "lblKiloNum";
            this.lblKiloNum.Size = new System.Drawing.Size(170, 25);
            // 
            // TrainBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.Controls.Add(this.lblKiloNum);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSubmitTime);
            this.Controls.Add(this.tagSubmitTime);
            this.Controls.Add(this.lblKilometer);
            this.Controls.Add(this.tagKilometer);
            this.Controls.Add(this.lblLongitude);
            this.Controls.Add(this.lblLatitude);
            this.Controls.Add(this.tagLatitude);
            this.Controls.Add(this.tagLongitude);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.tagDriver);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.tagNum);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.tagId);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.tagType);
            this.Controls.Add(this.pic_right_center_line);
            this.Controls.Add(this.txtReceiveTime);
            this.Controls.Add(this.tagRecevieTime);
            this.Controls.Add(this.txtWorkTime);
            this.Controls.Add(this.tagWorkTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.lblArriveTime);
            this.Controls.Add(this.lblCurrStation);
            this.Controls.Add(this.tagStartTime);
            this.Controls.Add(this.tagArriveTime);
            this.Controls.Add(this.tagCurrStation);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TrainBase";
            this.Size = new System.Drawing.Size(679, 443);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TrainBase_Paint);
            this.Click += new System.EventHandler(this.TrainBase_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblArriveTime;
        private System.Windows.Forms.Label lblCurrStation;
        private System.Windows.Forms.Label tagStartTime;
        private System.Windows.Forms.Label tagArriveTime;
        private System.Windows.Forms.Label tagCurrStation;
        private System.Windows.Forms.TextBox txtReceiveTime;
        private System.Windows.Forms.Label tagRecevieTime;
        private System.Windows.Forms.TextBox txtWorkTime;
        private System.Windows.Forms.Label tagWorkTime;
        private System.Windows.Forms.PictureBox pic_right_center_line;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.Label tagDriver;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label tagNum;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label tagId;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label tagType;
        private System.Windows.Forms.TextBox txtSubmitTime;
        private System.Windows.Forms.Label tagSubmitTime;
        private System.Windows.Forms.Label lblKilometer;
        private System.Windows.Forms.Label tagKilometer;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label tagLatitude;
        private System.Windows.Forms.Label tagLongitude;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Notice;
        private System.Windows.Forms.Button btn_Clear;
        private System.IO.Ports.SerialPort sp_Lkj;
        private System.IO.Ports.SerialPort sp_GPS;
        private System.IO.Ports.SerialPort sp_Can;
        public System.Windows.Forms.Button btn_Notice;
        private System.Windows.Forms.Label lblKiloNum;

    }
}
