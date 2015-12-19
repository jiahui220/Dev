using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using TrainCommon;
using System.Threading;
using TrainView.ChildFrom;

namespace TrainView.GuideView
{
    public partial class TrainGuideVideo : UserControl
    {
        private Dlg_Tip dlg_Tip = null;
        private Thread updateThread = null;
        private DataTable wt = null;
        private int currentPage = 1;
        private int pageSize = 15;
        private int count = 0;
        public TrainGuideVideo()
        {
            InitializeComponent();
            updateThread = new Thread(new ThreadStart(CheckUpdate));
            updateThread.IsBackground = true;
            count = DBAction.GetRecordCount("Video", "", new RParams());
            initData();
            updateThread.Start();
        }

        //声明一个程序信息类
        System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
        //声明一个程序类
        System.Diagnostics.Process Proc;
        //声明一个委托类型，该委托类型无输入参数和输出参数
        public delegate void ProcessDelegate();

        private void CheckUpdate()
        {
            int count = 0;
            //MessageBox.Show("更新视频列表");
            while (!LocoInfo.TrainInfo.VideoUpdate && count < 5 && LocoInfo.dataConnect == 0)
            {
                //发送本地所有视频ID
                UpdateMedia.SendAllVideoID(LocoInfo.TrainInfo.SckTrains);
                count++;
                updateThread.Join(10000);
            }
            //MessageBox.Show("视频列表更新完成！");
            currentPage = 1;
            pageSize = 15;
            //使用命名方法
            ProcessDelegate showProcess = new ProcessDelegate(initData);
            dgMedia.Invoke(showProcess);

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //加载视频列表
        public void initData()
        {
            #region 测试数据
            //DataSet ds = new DataSet();
            //DataTable ct = new DataTable();
            //ct.Columns.Add("ID");
            //ct.Columns.Add("VideoPath");
            //ct.Columns.Add("VideoName");
            //ct.Columns.Add("UploadPerSon");
            //ct.Columns.Add("UploadDate");
            //ct.Columns.Add("rowno");
            //for (int i = 0; i < 10; i++)
            //{
            //    ct.Rows.Add(i+1,"红尘.wmv","红尘"+i+1+".wmv","admin","2013-8-16 15:30:30",i+1);
            //}
            //ct.Rows.Add(11, "红尘1.wmv", "红尘11.wmv", "admin", "2013-8-16 15:30:30",11);
            //ds.Tables.Add(ct.Copy());

            #endregion

            wt = DBAction.GetPageDT("Video", "*", "", currentPage, pageSize, "UploadTime");
            wt.TableName = "Video";
            dgMedia.DataSource = wt;
            if (wt.Rows.Count > 0)
            {
                dgMedia.Select(0);
                ShowText(wt.Rows[0][0].ToString());
            }
        }


        /// <summary>
        /// 显示选择的信息
        /// </summary>
        /// <param name="wID"></param>
        private void ShowText(string wID)
        {
            using (DataTable alInfo = DBAction.GetDTFromSQL(" select * from Video where ID=" + wID))
            {
                if (alInfo.Rows.Count > 0)
                {
                    string iid = alInfo.Rows[0]["ID"].ToString();
                    lblId.Text = iid;             
                    string name = alInfo.Rows[0]["VideoName"].ToString();
                    lblName.Text = name;
                    lblType.Text = "视频文件";
                }
            }
        }

        #region 设置显示整行
        private void dgMedia_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgMedia.DataSource != null)
            {
                using (DataTable alt = ((DataTable)(dgMedia.DataSource)))
                {
                    int index = dgMedia.CurrentCell.RowNumber;
                    if (alt.Rows.Count > 0)
                    {
                        ShowText(alt.Rows[index][0].ToString());
                        dgMedia.Select(index);
                    }
                }

            }
        }

        private void dgMedia_GotFocus(object sender, EventArgs e)
        {
            if (dgMedia.DataSource != null)
            {
                int index = dgMedia.CurrentCell.RowNumber;
                if (((DataTable)(dgMedia.DataSource)).Rows.Count > 0)
                {
                    dgMedia.Select(index);
                }
            }
        }
        #endregion

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgMedia.DataSource == null)
            {
                return;
            }
            int total = pageSize * currentPage;
            if (count > total)
            {
                currentPage = currentPage + 1;
                initData();
            }
            else
            {
               
                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是最后一页！";
                
                dlg_Tip.ShowDialog();
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Uppage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgMedia.DataSource == null)
            {
                return;
            }
            if (currentPage > 1)
            {
                currentPage = currentPage - 1;
                initData();
            }
            else
            {
                
                    dlg_Tip = new Dlg_Tip();
                    dlg_Tip.TipInfo = "当前已经是第一页！";
                
                dlg_Tip.ShowDialog();
            }
        }
        //上一条
        private void btn_Up_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgMedia.DataSource != null)
            {
                int index = dgMedia.CurrentCell.RowNumber;//获取当前选定的行
                if (index > 0)//判断是否为第一行
                {
                    dgMedia.UnSelect(index);//取消选定行
                    dgMedia.CurrentRowIndex = index - 1;
                    dgMedia.Select(index - 1);//选择上一条
                    using (DataTable wt = DBAction.GetPageDT("Video", "*", "", currentPage, pageSize, "UploadTime"))
                    {
                        ShowText(wt.Rows[index - 1][0].ToString());
                    }
                }
            }
        }
        //下一条
        private void btn_Down_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgMedia.DataSource != null)
            {
                int index = dgMedia.CurrentCell.RowNumber;//获取当前选定的行
                if (index < (((DataTable)(dgMedia.DataSource)).Rows.Count - 1))//判断是否为最后一行
                {

                    dgMedia.UnSelect(index);//取消选定行
                    dgMedia.CurrentRowIndex = index + 1;
                    dgMedia.Select(index + 1);//选择下一条
                    using (DataTable wt = DBAction.GetPageDT("Video", "*", "", currentPage, pageSize, "UploadTime"))
                    {
                        ShowText(wt.Rows[index + 1][0].ToString());
                    }
                }
            }
        }
        private void ptb_Music_Click(object sender, EventArgs e)
        {

        }

        private string filPath = "";

        private string fileName = "";

        private string path = "";

        /// <summary>
        /// 播放视频
        /// </summary>
        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (wt.Rows.Count <= 0)
            {
                return;
            }
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgMedia.DataSource != null)
            {
                int index = dgMedia.CurrentCell.RowNumber;
                using (DataTable dt = (DataTable)(dgMedia.DataSource))
                {
                    filPath = dt.Rows[index]["VideoPath"].ToString(); ;
                    fileName = dt.Rows[index]["VideoName"].ToString();
                    if (filPath.Trim().Length > 0)
                    {
                        path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Video\\" + fileName;
                        //设置外部程序名
                        Info.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\TCPMP 0.81\\player.exe";
                        if (File.Exists(path))
                        {
                            //设置程序参数
                            Info.Arguments = path;
                            //启动外部程序 
                            Proc = System.Diagnostics.Process.Start(Info);
                        }
                        else
                        {
                            Dlg_Ask ask = new Dlg_Ask();
                            ask.AskInfo = "视频未下载，是否下载视频。下载视频将在后台进行不影响当前使用，下载完成后将自动播放。";
                            ask.Closed += new EventHandler(ask_Closed);
                            ask.ShowDialog();
                        }

                    }
                }


            }

        }

        public Thread loadTh = null;

        /// <summary>
        /// 提示是否下载视频
        /// </summary>
        private void ask_Closed(object sender, EventArgs e)
        {
            bool isLoad = (bool)((Dlg_Ask)sender).Tag;
            if (isLoad)
            {
                loadTh = new Thread(new ThreadStart(LoadVideo));
                loadTh.IsBackground = true;
                loadTh.Start();
            }
        }

        /// <summary>
        /// 下载视频文件
        /// </summary>
        public void LoadVideo()
        {
            try
            {
                WebFileService.FileTransportService wf = new WebFileService.FileTransportService();
                BaseLibrary.getUrl();
                wf.Url = "http://" + LocoInfo.TrainInfo.Url + "/MAP/FileTransportService/";
                byte[] bs = wf.DownLoad(filPath, "0");

                if (bs.Length > 0)
                {
                    //MessageBox.Show(bs.Length.ToString());
                    FileStream stream = new FileStream(path, FileMode.CreateNew);
                    stream.Write(bs, 0, bs.Length);
                    stream.Flush();
                    stream.Close();
                    //MessageBox.Show("下载结束");
                    //设置程序参数
                    Info.Arguments = path;
                    //启动外部程序 *
                    Proc = System.Diagnostics.Process.Start(Info);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("文件下载失败");
            }
        }

    }
}
