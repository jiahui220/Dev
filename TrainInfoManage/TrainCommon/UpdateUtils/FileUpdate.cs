using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace TrainCommon
{
    /// <summary>
    /// 检测文件更新----------0800
    /// </summary>
    public class FileUpdate
    {
        //要更新的文件列表
        private static List<string> listFile = null;
        private static string path = FormCommon.basePath + "\\UpdateTemp";
        public static string baseversion = "2.14.08.18";

        private static string version = string.Empty;
        private static string id;

        //**********************发送文件更新请求

        /// <summary>
        /// 请求文件更新-------00
        /// </summary>
        public static void RequestUpdate(SckTrains socket)
        {
            //删除更新文件夹下的所有文件
            //FileHelper.DeleteDirectory(path);
            //MessageBox.Show("进入方法");
            DataTable dt = DBAction.GetDTFromSQL("select * from RoboConfig");
            //MessageBox.Show("1");
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["ID"].ToString().Trim();
                SckParams param = new SckParams();
                param.Tml = "Cmd,Type,Version";
                param.Add("Cmd", "0800", false);
                param.Add("Type", "00", false);
                param.Add("Version", dt.Rows[0]["MainVersion"].ToString().Trim(), true);
                param.CreatePacks();
                if (param.PackItems != null && param.PackItems.Count > 0)
                {
                    bool result = socket.Send(param, "TCP");
                    Thread.Sleep(500);
                    if (!result)
                    {
                        RequestUpdate(socket);
                    }
                }
                //BaseVoice.TrainVoice.SpeekVioce("请求更新版本号" + dt.Rows[0]["MainVersion"].ToString().Trim());
                //MessageBox.Show("请求更新版本号：" + dt.Rows[0]["MainVersion"].ToString().Trim());
            }
        }

        //**********************解析接收到的信息
        public static void RevceFile(SckTrains socket, string info)
        {
            //MessageBox.Show(info);
            //除去首尾字符
            info = info.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = info.Split(new char[] { '&' });
            //获取头部信息
            string head = receData[0];
            //获取内容部分信息
            string body = receData[1]; 
            //分解头部内容
            string[] headItems = head.Split(new char[] { '$' });
            if (headItems[1] != "20")
            {
                //获取校验码信息
                string check = receData[2];
                //获取接收到的数据验证码
                string receCode = check.Substring(0, 4);
                //根据内容获取验证码
                UInt16 crcCode = SckCRC16.Parse(body);
                string code = crcCode.ToString("X");
            }
            //MessageBox.Show(headItems[1]);
            switch (headItems[1])
            {
                case "00":
                    //MessageBox.Show("获取文件列表");
                    //BaseVoice.TrainVoice.SpeekVioce("获取文件列表");
                    AnalyzeFileNames(headItems,body);
                    break;
                case "01":
                    //MessageBox.Show("无更新");
                    //BaseVoice.TrainVoice.SpeekVioce("无更新");
                    break;
                case "02":
                    //服务器请求车载端更新
                    AnalyzeUpdateVersion(headItems);
                    break;
                case "10":
                    //MessageBox.Show("下载文件");
                    //BaseVoice.TrainVoice.SpeekVioce("下载文件");
                    AnalyzeFileInfo(headItems,body);
                    break;
                case "11":
                    //MessageBox.Show("文件不存在");
                    if (listFile == null || listFile.Count == 0)
                    {
                        return;
                    }
                    listFile.Remove(body.Trim());
                    if (listFile == null || listFile.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(version))
                        {
                            RParams param = new RParams();
                            param.Add("AssVersion", version);
                            DBAction.Update("RoboConfig", "AssVersion", "ID=" + id, param);
                        }
                        return;
                    }
                    ReplyFileName(socket);
                    break;
                case "20":
                    break;
            }
        }

        /// <summary>
        /// 02 分析服务器要求更新命令
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeUpdateVersion(string[] headItems) 
        {
            try
            {
                //车型
                string trainType = headItems[2];
                //车号
                string trainNum = headItems[3];
                //MessageBox.Show("服务器要求更新----->" + trainType + "-" + trainNum);
                //BaseVoice.TrainVoice.SpeekVioce("服务器请求更新" + trainType + trainNum);
                if (LocoInfo.TrainInfo.TrainType == trainType && LocoInfo.TrainInfo.TrainNum == trainNum)
                {
                    //BaseVoice.TrainVoice.SpeekVioce("车载发送更新请求");
                    RequestUpdate(LocoInfo.TrainInfo.SckTrains);
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        /// <summary>
        /// 解析所有文件名-----00
        /// </summary>
        /// <param name="body"></param>
        public static void AnalyzeFileNames(string[] headItems,string body)
        {
            //MessageBox.Show(headItems[2].Trim() + "  " + body.Trim());
            //获取新版本号
            version = headItems[2].Trim();
            if (string.IsNullOrEmpty(body))
            {
                return;
            }
            listFile = new List<string> ();
            //MessageBox.Show("文件名--------->" + body);
            //将所有的文件名添加到列表
            string[] names = body.Split(',');
            for (int i = 0; i < names.Length; i++)
            {
                if (!string.IsNullOrEmpty(names[i]))
                {
                    listFile.Add(names[i]);
                }
            }
            //请求单个文件信息
            ReplyFileName(LocoInfo.TrainInfo.SckTrains);
        }


        /// <summary>
        /// 解析文件信息------10
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeFileInfo(string[] headItems, string body)
        {
            bool result = false;
            //解析文件路径，获取文件名
            string fileName = headItems[2].Trim().Substring(headItems[2].Trim().LastIndexOf('\\') + 1);
            //获取文件最后修改时间
            DateTime upTime = Convert.ToDateTime(body);
            //判断文件是否已下载
            string fullpath = path + "\\" + fileName;
            if (File.Exists(fullpath))
            {
                //MessageBox.Show("下载文件02");
                //存在同名文件判定是否为最新文件
                FileInfo fi = new FileInfo(fullpath);
                //MessageBox.Show("获取已存在文件信息");
                //获取文件最后修改日期
                DateTime upLocal = fi.LastWriteTime;
                //MessageBox.Show("获取文件修改时间");
                //若本地不是最新文件则重新下载该文件
                if (upTime>upLocal)
                {
                    //MessageBox.Show("下载文件03");
                    //删除旧文件
                    File.Delete(fullpath);
                    //下载新文件
                    result=loadFile(headItems[2].Trim());
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                //MessageBox.Show("下载文件04");
                //无文件则直接下载
                result = loadFile(headItems[2].Trim());
            }
            //下载成功，删除已下载文件记录
            if (result)
            {
                //MessageBox.Show("下载文件05");
                if (listFile.Count>0)
                {
                    //MessageBox.Show("下载文件06");
                    listFile.RemoveAt(0);
                    //请求单个文件信息
                    ReplyFileName(LocoInfo.TrainInfo.SckTrains);
                }
            }
            else
            {
                //下载失败重新下载文件
                AnalyzeFileInfo(headItems, body);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filepath">文件名</param>
        /// <returns></returns>
        public static bool loadFile(string filepath)
        {
            try
            {
                //解析文件路径，获取文件名
                string fileName = filepath.Substring(filepath.LastIndexOf('\\') + 1);
                //BaseVoice.TrainVoice.SpeekVioce("下载文件" + fileName);
                WebFileService.FileTransportService wf = new WebFileService.FileTransportService();
                DataTable dtConfig = LocoInfo.TrainInfo.RoboConfig;
                wf.Url = "http://" + dtConfig.Rows[0]["ServiceAddress"].ToString() + ":" + dtConfig.Rows[0]["ServicePort"].ToString() + "/MAP/FileTransportService/";
                byte[] bs = wf.DownLoad(filepath, "2");
                string loadpath = path + "\\" + fileName;
                if (File.Exists(loadpath))
                {
                    File.Delete(loadpath);
                }
                FileStream stream = new FileStream(loadpath, FileMode.CreateNew);
                stream.Write(bs, 0, bs.Length);
                stream.Flush();
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        //**********************发送应答信息
        /// <summary>
        /// 应答一个文件名-----------10
        /// </summary>
        public static void ReplyFileName(SckTrains socket)
        {
            //如果更新完成后，修改数据库的版本号
            if (listFile == null || listFile.Count == 0)
            {
                //MessageBox.Show("进入-----");
                //MessageBox.Show(id + "----------" +version);
                if (!string.IsNullOrEmpty(version))
                {
                    RParams rpms = new RParams();
                    rpms.Add("AssVersion", version);
                    DBAction.Update("RoboConfig", "AssVersion", "ID=" + id, rpms);
                }
                //BaseVoice.TrainVoice.SpeekVioce("全部更新完成");
                //MessageBox.Show("全部更新完成");
                return;
            }
            //选择文件列表的第一个文件名发送更新请求
            SckParams param = new SckParams();
            param.Tml = "Cmd,Type,FileName";
            param.Add("Cmd", "0800", false);
            param.Add("Type", "10", false);
            param.Add("FileName", listFile[0].Trim(), true);
            param.CreatePacks();
            if (param.PackItems != null && param.PackItems.Count > 0)
            {
                bool result = socket.Send(param, "TCP");
                Thread.Sleep(500);
                if (!result)
                {
                    ReplyFileName(socket);
                }
            }
            //MessageBox.Show("有更新--" + listFile.Count + "----文件名：" + listFile[0].Trim());
        }

    }
}
