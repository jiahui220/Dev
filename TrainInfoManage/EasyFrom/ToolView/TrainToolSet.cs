using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.Runtime.InteropServices;
using TrainView.ChildFrom;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Threading;
namespace TrainView.ToolView
{
    public partial class TrainToolSet : UserControl
    {
       //按钮颜色设置
        Boolean isTrue = true;
        /// <summary>
        /// 是否设置音量
        /// </summary>
        private bool isVolume = false;
        /// <summary>
        /// 是否设置屏保时间
        /// </summary>
        private bool isScreen = false;
        /// <summary>
        /// 数字输入框
        /// </summary>
        private Dlg_Number dlg_Number = null;
        /// <summary>
        /// 是否进入管理
        /// </summary>
        private bool isMange = false;
        /// <summary>
        /// 按下重启按钮胡时间
        /// </summary>
        private int pressedSecond = 0;
        public const uint POWER_FORCE = 0x00001000;

        [DllImport("Coredll.dll")]
        public static extern int SetSystemPowerState(string psState, SystemPowerStateFlags StateFlags, uint Options);
        [DllImport("coredll.dll")]
        private static extern int waveOutSetVolume(int hwo, System.UInt32 pdwVolume);//设置音量
        [DllImport("coredll.dll")]
        private static extern uint waveOutGetVolume(int hwo, out System.UInt32 pdwVolume); //获取音量

        public enum SystemPowerStateFlags : uint
        {
            POWER_STATE_ON = 0x00010000,  // on state
            POWER_STATE_OFF = 0x00020000,  // no power, full off
            POWER_STATE_CRITICAL = 0x00040000,  // critical off
            POWER_STATE_BOOT = 0x00080000,  // boot state
            POWER_STATE_IDLE = 0x00100000,  // idle state
            POWER_STATE_SUSPEND = 0x00200000,  // suspend state
            POWER_STATE_UNATTENDED = 0x00400000,  // Unattended state.
            POWER_STATE_RESET = 0x00800000,  // reset state
            POWER_STATE_USERIDLE = 0x01000000,  // user idle state
            POWER_STATE_BACKLIGHTON = 0x02000000,  // device scree backlight on
            POWER_STATE_PASSWORD = 0x10000000   // This state is password protected.
        }

        public TrainToolSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置报警语音
        /// </summary>
        private void btnSVoice_Click(object sender, EventArgs e)
        {
            if (btnSVoice.Text=="关闭")
            {
                LocoInfo.TrainInfo.IsVoice = true;
                btnSVoice.Text = "开启";
                BaseVoice.TrainVoice.SpeekVioce("设置机车报警语音成功");
            }
            else
            {
                LocoInfo.TrainInfo.IsVoice = false;
                btnSVoice.Text = "关闭";
                BaseVoice.TrainVoice.SpeekVioce("取消机车报警语音成功");
            }
        }

        /// <summary>
        /// 音量减
        /// </summary>
        private void btn_Voiced_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            int currSound = GetVolume();
            if (currSound < 10)
            {
                SetVolume(0);
            }
            else
            {
                SetVolume(currSound - 9);
            }
            lblVolume.Text = GetVolume().ToString();
            isVolume = true;
            tmrRestart.Enabled = true;
            lblVolume.Visible = true;
            timer1.Interval = 5000;  
            if (timer1.Enabled == false)
                timer1.Enabled = true;
        }

        /// <summary>
        /// 音量加
        /// </summary>
        private void btn_Voiceu_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            int currSound = GetVolume();
            if (currSound > 90)
            {
                SetVolume(100);
            }
            else
            {
                SetVolume(currSound + 11);
            }
            lblVolume.Text = GetVolume().ToString();
            isVolume = true;           
            tmrRestart.Enabled = true;
            lblVolume.Visible = true;
            timer1.Interval = 5000;  
            if (timer1.Enabled == false)
                timer1.Enabled = true;
        }


        #region 设置音量
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="value"></param>
        private void SetVolume(int sound)
        {
            System.UInt32 Value = (System.UInt32)((double)0xffff * (double)sound / (double)(100));
            if (Value < 0)
            {
                Value = 0;
            }
            if (Value > 0xffff)
            {
                Value = 0xffff;
            }
            System.UInt32 left = (System.UInt32)Value;//左声道音量
            System.UInt32 right = (System.UInt32)Value;//右声道音量
            waveOutSetVolume(0, left << 16 | right); //"<<"左移，“|”逻辑或运算
        }

        /// <summary>
        /// 获取音量
        /// </summary>
        /// <returns></returns>
        private int GetVolume()
        {
            uint v;
            IntPtr p = new IntPtr(0);
            uint i = waveOutGetVolume((int)p, out v);
            uint vleft = v & 0xFFFF;
            uint vright = (v & 0xFFFF0000) >> 16;
            return (int.Parse(vleft.ToString()) | int.Parse(vright.ToString())) * 100 / 0xFFFF;
        }
        #endregion

        /// <summary>
        /// 屏保时间减
        /// </summary>
        private void btn_Timed_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (LocoInfo.TrainInfo.ScreenMinutes == 1)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 1;
            }
            else if (LocoInfo.TrainInfo.ScreenMinutes == 5)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 1;
            }
            else if (LocoInfo.TrainInfo.ScreenMinutes == 10)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 5;
            }
            else
            {
                LocoInfo.TrainInfo.ScreenMinutes -= 10;
            }
            lblScreen.Text = LocoInfo.TrainInfo.ScreenMinutes + "分钟";
            isScreen = true;
            tmrRestart.Enabled = true;
            lblScreen.Visible = true;
            timer2.Interval = 5000;  
            if (timer2.Enabled == false)
                timer2.Enabled = true;
        }

        /// <summary>
        /// 屏保时间增
        /// </summary>
        private void btn_Timeu_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (LocoInfo.TrainInfo.ScreenMinutes == 60)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 60;
            }
            else if (LocoInfo.TrainInfo.ScreenMinutes == 1)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 5;
            }
            else if (LocoInfo.TrainInfo.ScreenMinutes == 5)
            {
                LocoInfo.TrainInfo.ScreenMinutes = 10;
            }
            else
            {
                LocoInfo.TrainInfo.ScreenMinutes += 10;
            }
            lblScreen.Text = LocoInfo.TrainInfo.ScreenMinutes + "分钟";
            isScreen = true;
            tmrRestart.Enabled = true;
            lblScreen.Visible = true;
            timer2.Interval = 5000;  
            if (timer2.Enabled == false)
                timer2.Enabled = true;
        }

        /// <summary>
        /// 进入管理
        /// </summary>
        private void btn_Manage_Click(object sender, EventArgs e)
        {
            if (!isMange)
            {
                if (dlg_Number == null)
                {
                    dlg_Number = new Dlg_Number();
                    dlg_Number.Closed += new EventHandler(GetPassword);
                }
                dlg_Number.ShowDialog();
            }
            else
            {
                pl_Manage.Visible = false;
                btn_Manage.Text = "进入管理";
                isMange = false;
            }
        }

        /// <summary>
        /// 获取管理密码
        /// </summary>
        private void GetPassword(object sender, EventArgs e)
        {
            string pwd = (string)((Dlg_Number)sender).Tag;
            if (string.IsNullOrEmpty(pwd))
            {
                return;
            }
            if (pwd == LocoInfo.TrainInfo.RoboConfig.Rows[0]["ManagePwd"].ToString())
            {
                pl_Manage.Visible = true;

                txtFile.Visible=true;
                btn_Choice.Visible=true;
                btn_Send.Visible=true;
                label5.Visible=true;
                label4.Visible=true;
                btnPower.Visible=true;
                label3.Visible = true;
                btn_Restart.Visible = true;

                isMange = true;
                btn_Manage.Text = "退出管理";
            }
        }

        /// <summary>
        /// 按钮按下计时器
        /// </summary>
        private void tmrRestart_Tick(object sender, EventArgs e)
        {
            try
            {
                pressedSecond++;
                if (pressedSecond == 3 && isVolume)
                {
                    isVolume = false;
                    pressedSecond = 0;
                    lblVolume.Text = "";
                    tmrRestart.Enabled = false;
                    tmrRestart.Dispose();
                }
                if (pressedSecond == 3 && isScreen)
                {
                    isScreen = false;
                    pressedSecond = 0;
                    lblScreen.Text = "";
                    tmrRestart.Enabled = false;
                    tmrRestart.Dispose();
                }
            }
            catch (Exception ex)
            {
              
            }
        }

        /// <summary>
        /// 重启系统
        /// </summary>
        public static void Restart()
        {
            //重启系统
            SetSystemPowerState(null, SystemPowerStateFlags.POWER_STATE_RESET, POWER_FORCE);
        }

        /// <summary>
        /// 设置自启动
        /// </summary>
        private void btnPower_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            RParams param = new RParams();
            if (btnPower.Text=="关闭")
            {
                SetAutoRun(true);
                param.Items.Clear();
                param.Add("IsAutoRun", "1");
                DBAction.Update("RoboConfig", "IsAutoRun", "ID=" + LocoInfo.TrainInfo.RoboConfig.Rows[0]["ID"].ToString(), param);
                BaseVoice.TrainVoice.SpeekVioce("设置自启动成功");
                btnPower.Text = "开启";
            }
            else
            {
                SetAutoRun(false);
                param.Items.Clear();
                param.Add("IsAutoRun", "0");
                DBAction.Update("RoboConfig", "IsAutoRun", "ID=" + LocoInfo.TrainInfo.RoboConfig.Rows[0]["ID"].ToString(), param);
                BaseVoice.TrainVoice.SpeekVioce("取消自启动成功");
                btnPower.Text = "关闭";
            }
        }

        #region 设置自启动方法
        /// <summary>
        /// 设置自启动
        /// </summary>
        /// <param name="isAutoRun"></param>
        /// <returns></returns>
        public static bool SetAutoRun(bool isAutoRun)
        {
            RegistryKey rKey = null;
            try
            {
                //设置自启动-->80为启动优先级
                string keyName = "Launch80";
                string exePath = TrainForm.basePath + "\\UpdateWizard.exe";
                rKey = Registry.LocalMachine.OpenSubKey("init", true);
                if (rKey == null)
                {
                    rKey = Registry.LocalMachine.CreateSubKey("init");
                }
                rKey = Registry.LocalMachine.OpenSubKey("init", true);
                if (isAutoRun)
                {
                    rKey.SetValue(keyName, exePath);
                }
                else
                {
                    rKey.SetValue(keyName, "explorer.exe");
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (rKey != null)
                {
                    rKey.Close();
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 选择文件
        /// </summary>
        private void btn_Choice_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            dlgFileSelect.InitialDirectory = TrainForm.basePath;
            dlgFileSelect.Filter = "所有文件(*.*)|*.*";
            if (dlgFileSelect.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = dlgFileSelect.FileName;
            }
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            //if (isTrue)
            //{
            //    this.btn_Send.ForeColor = Color.Silver;
            //    ////this.btn_Send.BackColor = System.Drawing.Color.Red;
            //    ////this.btn_Send.BackColor = Color.Blue;
            //}
            string filePath = txtFile.Text;
            if (!String.IsNullOrEmpty(filePath.Trim()))
            {
                LocoInfo.TrainInfo.IsSendFile = true;
                LocoInfo.TrainInfo.FilePath = filePath;          
            }
        }

        private void lblVolume_ParentChanged(object sender, EventArgs e)
        {

        }

        private void TrainToolSet_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblVolume.Visible = false;
            timer1.Enabled = false;
       }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblScreen.Visible = false;
            timer2.Enabled = false;
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            tmrRestart.Enabled = false;
                //断开所有拨号
                RasDailAction.DisAllContect();
                //重启系统
                SetSystemPowerState(null, SystemPowerStateFlags.POWER_STATE_RESET, POWER_FORCE);
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //重启程序
            if (DialAction.IsConnected())
            {
                DialAction.Close();
            }
            TrainForm.ShowTaskBar();
            //杀掉当前进程
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
    }
}
