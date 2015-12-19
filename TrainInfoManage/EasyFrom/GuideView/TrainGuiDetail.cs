using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TrainCommon;
using System.Threading;
using TrainView.ChildFrom;

namespace TrainView.GuideView
{
    public partial class TrainGuiDetail : UserControl
    {
        private Dlg_Tip dlg_Tip = null;

        private Dlg_Check_Detail dlg_Check_Detail = null;
        private DataTable ct = new DataTable();
        private string filePath = "";
        private int currentPage = 1;
        private int pageSize = 60;
        private int count = 0;
        private string path = "";

        public TrainGuiDetail()
        {
            InitializeComponent();
            initData();
        }

        private void TrainGuiDetail_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        //初始化数据
        public void initData()
        {
            ct = DBAction.GetPageDT("Details", "*", "", currentPage, pageSize, "UploadTime");
            ct.TableName = "Details";
            dgPic.DataSource = ct;
            if (ct.Rows.Count > 0)
            {
                dgPic.Select(0);
            }
            //}

        }


        /// <summary>
        /// 上一条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgPic.DataSource != null)
            {
                int index = dgPic.CurrentCell.RowNumber;//获取当前选定的行
                if (index > 0)//判断是否为第一行
                {
                    dgPic.UnSelect(index);//取消选定行
                    dgPic.CurrentRowIndex = index - 1;
                    dgPic.Select(index - 1);//选择上一条
                }
            }
        }

        /// <summary>
        /// 下一条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Downitem_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgPic.DataSource != null)
            {
                int index = dgPic.CurrentCell.RowNumber;//获取当前选定的行
                if (index < (((DataTable)(dgPic.DataSource)).Rows.Count - 1))//判断是否为最后一行
                {

                    dgPic.UnSelect(index);//取消选定行
                    dgPic.CurrentRowIndex = index + 1;
                    dgPic.Select(index + 1);//选择下一条               
                }
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Look_Click(object sender, EventArgs e)
        {
            if (ct.Rows.Count <= 0)
            {
                return;
            }
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dlg_Check_Detail == null)
            {
                dlg_Check_Detail = new Dlg_Check_Detail();
            }
            if (dgPic.DataSource != null)
            {
                int index = dgPic.CurrentCell.RowNumber;//获取当前选定的行
                //判断图片是否存在
                filePath = ct.Rows[index]["FilePath"].ToString();
                path = TrainForm.basePath + "\\Details\\" + filePath;
                if (File.Exists(path))
                {
                    dlg_Check_Detail.ImgDt = ct;
                    dlg_Check_Detail.ImgIndex = index;
                    dlg_Check_Detail.ShowDialog();
                }
                else
                {
                    Dlg_Ask ask = new Dlg_Ask();
                    ask.AskInfo = "图片未下载，是否下载图片。下载图片将在后台进行不影响当前使用，下载完成后将自动显示。";
                    ask.Closed += new EventHandler(ask_Closed);
                    ask.ShowDialog();
                }
            }
        }
        public Thread loadTh = null;

        private void ask_Closed(object sender, EventArgs e)
        {
            bool isLoad = (bool)((Dlg_Ask)sender).Tag;
            if (isLoad)
            {
                loadTh = new Thread(new ThreadStart(LoadImg));
                loadTh.IsBackground = true;
                loadTh.Start();
            }
        }

        public void LoadImg()
        {
            try
            {
                WebFileService.FileTransportService wf = new WebFileService.FileTransportService();
                BaseLibrary.getUrl();
                wf.Url = "http://" + LocoInfo.TrainInfo.Url + "/MAP/FileTransportService/";
                byte[] bs = wf.DownLoad(filePath, "1");
                if (bs.Length > 0)
                {
                    FileStream stream = new FileStream(path, FileMode.CreateNew);
                    stream.Write(bs, 0, bs.Length);
                    stream.Flush();
                    stream.Close();

                    int index = dgPic.CurrentCell.RowNumber;//获取当前选定的行
                    dlg_Check_Detail.ImgDt = ct;
                    dlg_Check_Detail.ImgIndex = index;
                    dlg_Check_Detail.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Dlg_Tip dlfrm = new Dlg_Tip();
                dlfrm.TipInfo = "下载图片失败，请检查联网是否正常。";
                dlfrm.ShowDialog();
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
            if (dgPic.DataSource == null)
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

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Downpage_Click(object sender, EventArgs e)
        {
            LocoInfo.TrainInfo.LastDateTime = DateTime.Now;
            if (dgPic.DataSource == null)
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

        //选择整行
        private void dgPic_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgPic.DataSource != null)
            {
                int index = dgPic.CurrentCell.RowNumber;
                if (((DataTable)(dgPic.DataSource)).Rows.Count > 0)
                {
                    dgPic.Select(index);
                }
            }
        }

        private void dgPic_GotFocus(object sender, EventArgs e)
        {
            if (dgPic.DataSource != null)
            {
                int index = dgPic.CurrentCell.RowNumber;
                if (((DataTable)(dgPic.DataSource)).Rows.Count > 0)
                {
                    dgPic.Select(index);
                }
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

        }
       
    }
}
