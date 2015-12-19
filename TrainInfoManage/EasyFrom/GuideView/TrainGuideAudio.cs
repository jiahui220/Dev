using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TrainCommon;
using System.IO;

namespace TrainView.GuideView
{
    public partial class TrainGuideAudio : UserControl
    {
        public TrainGuideAudio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Choice_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            dlgFileSelect.InitialDirectory = TrainForm.basePath;
            dlgFileSelect.Filter = "所有音频文件|*.mp3;*.mp4;*.wav;*.wma|MP3音乐文件(*.mp3)|*.mp3|MP4音乐文件(*.mp4)|*.mp4|Wave音乐文件(*.wav)|*.wav|Window Media音乐文件(*.wma)|*.wma";
            if (dlgFileSelect.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = dlgFileSelect.FileName;
                int start = txtFile.Text.LastIndexOf("\\");
                int end = txtFile.Text.LastIndexOf(".");
                txtName.Text = txtFile.Text.Substring(start + 1, end - start - 1);
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (String.IsNullOrEmpty(txtName.Text.Trim()) || String.IsNullOrEmpty(txtFile.Text.Trim()))
            {
                return;
            }
            //上传音频
            txtName.Text = String.Empty;
            txtFile.Text = String.Empty;
        }
    }
}
