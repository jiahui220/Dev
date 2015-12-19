using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace TrainCommon
{
    /// <summary>
    /// 媒体更新
    /// ①发送本地的视频ID至服务器
    /// ②服务器分析接收到的ID,返回需删除的视频，及新增的视频信息。其中需删除信息类型标志为01，需增加信息标志为02
    /// </summary>
    public class UpdateMedia
    {
        /// <summary>
        /// 发送所有视频ID至服务器
        /// </summary>
        public static void SendAllVideoID(SckTrains socket) 
        {
            try
            {
                //MessageBox.Show("1");
                SckParams param = new SckParams();
                //查询本地的数据库的视频信息
                string strSql = "select VideoId from Video";
                using (DataTable vt = DBAction.GetDTFromSQL(strSql))
                {
                    //视频连接字符串
                    string idList = "";
                    if (vt.Rows.Count > 0)
                    {
                        //MessageBox.Show("3");
                        for (int i = 0; i < vt.Rows.Count; i++)
                        {
                            idList += vt.Rows[i][0].ToString() + ",";
                        }
                        if (idList.Length > 0)
                        {
                            idList = idList.Substring(0, idList.Length - 1);
                        }
                    }
                    //MessageBox.Show("4");
                    param.Tml = "Cmd,Type,AllVideoId";
                    param.Add("Cmd", "0501", false);
                    param.Add("Type", "00", false);
                    if (idList.Trim().Length > 0)
                    {
                        //本地数据库不为空，将本地视频信息发送至服务器
                        param.Add("AllVideoId", idList, true);
                    }
                    else
                    {
                        param.Add("AllVideoId", "0", true);
                    }
                    //MessageBox.Show("5");
                    List<string> vs = param.CreatePacks();
                    //MessageBox.Show("发送视频" + vs[0]);
                    socket.Send(param, "TCP");  
                }
   
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 分析服务器返回的视频资料信息
        /// </summary>
        /// <param name="receInfo"></param>
        public static void AnalyzeVideoInfo(string receInfo) 
        {
            //除去首尾字符
            receInfo = receInfo.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = receInfo.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("接收的内容--------->" + body);
            //获取返回类型
            string type = headItems[1];
            //MessageBox.Show("接收的类型" + type);
            switch (type)
            {
                case "01":   
                    if (body.Trim().Length>0)
                    {
                        //拆分内容信息
                        string[] bodyItems = body.Split(new char[] { ',' });
                        for (int i = 0; i < bodyItems.Length; i++)
                        {
                            string vid = bodyItems[i].Trim();
                            //删除视频信息
                            using (DataTable vt = DBAction.GetDTFromSQL("select VideoName from Video where VideoId=" + vid))
                            {
                                if (vt.Rows.Count > 0)
                                {
                                    //视频文件是否存在，删除本地视频
                                    string videoPath = FormCommon.basePath + "\\Video\\" + vt.Rows[0][0].ToString();
                                    if (File.Exists(videoPath))
                                    {
                                        File.Delete(videoPath);
                                    }
                                }
                                //删除数据库记录
                                DBAction.Delete("Video", "VideoId=" + vid);
                                //MessageBox.Show("删除视频");
                            }
                        }    
                    }
                    break;
                case "02":
                    if (body.Trim().Length>0)
                    {
                        //拆分内容信息
                        string[] bodyItems = body.Split(new char[] { ',' });
                        using (DataTable ot = DBAction.GetDTFromSQL("select ID from Video where VideoId=" + bodyItems[0]))
                        {
                            //不存在则添加
                            if (ot.Rows.Count==0)
                            {
                                RParams param = new RParams();
                                param.Add("VideoId", bodyItems[0]);
                                param.Add("VideoPath", bodyItems[1]);
                                param.Add("VideoName", bodyItems[2]);
                                param.Add("UploadUser", bodyItems[3]);
                                param.Add("UploadTime", BaseLibrary.ConversionTime(bodyItems[4], 0));
                                DBAction.Insert("Video", param);
                                //MessageBox.Show("添加视频" + bodyItems[2]);
                            }
                        }
                    }
                    break;
                case "03":
                    //MessageBox.Show("接收结束");
                    LocoInfo.TrainInfo.VideoUpdate = true;
                    break;
                default:
                    break;
            }
        
        }

        /// <summary>
        /// 发送所有视频ID至服务器
        /// </summary>
        public static void SendAllImgID(SckTrains socket)
        {
            //MessageBox.Show("1");
            SckParams param = new SckParams();
            //查询本地的数据库的视频信息
            string strSql = "select ImgId from Details";
            using (DataTable vt = DBAction.GetDTFromSQL(strSql))
            {
                //视频连接字符串
                string idList = "";
                if (vt.Rows.Count > 0)
                {
                    for (int i = 0; i < vt.Rows.Count; i++)
                    {
                        idList += vt.Rows[i][0].ToString() + ",";
                    }
                    if (idList.Length > 0)
                    {
                        idList = idList.Substring(0, idList.Length - 1);
                    }
                }
                //MessageBox.Show("2");
                param.Tml = "Cmd,Type,AllImgId";
                param.Add("Cmd", "0502", false);
                param.Add("Type", "00", false);
                if (idList.Trim().Length > 0)
                {
                    //本地数据库不为空，将本地视频信息发送至服务器
                    param.Add("AllImgId", idList, true);
                }
                else
                {
                    param.Add("AllImgId", "0", true);
                }
                //MessageBox.Show("3");
                List<string> ims = param.CreatePacks();
                //MessageBox.Show("发送本地施工图-------->" + ims[0]);
                socket.Send(param, "TCP");
            }

        }

        /// <summary>
        /// 分析服务器返回的施工图资料信息
        /// </summary>
        /// <param name="receInfo"></param>
        public static void AnalyzeImgInfo(string receInfo)
        {
            //除去首尾字符
            receInfo = receInfo.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = receInfo.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("接收信息--------->" + receInfo);
            //获取返回类型
            string type = headItems[1];
            switch (type)
            {
                case "01":
                    if (body.Trim().Length > 0)
                    {
                        //拆分内容信息
                        string[] bodyItems = body.Split(new char[] { ',' });
                        for (int i = 0; i < bodyItems.Length; i++)
                        {
                            string imgid = bodyItems[i].Trim();
                            //删除施工图信息
                            using (DataTable vt = DBAction.GetDTFromSQL("select PhotoName from Details where ImgId=" + imgid))
                            {
                                if (vt.Rows.Count > 0)
                                {
                                    //施工图文件是否存在，删除本地施工图
                                    string imgPath = FormCommon.basePath + "\\Details\\" + vt.Rows[0][0].ToString();
                                    if (File.Exists(imgPath))
                                    {
                                        File.Delete(imgPath);
                                    }
                                }
                                //删除数据库记录
                                DBAction.Delete("Details", "ImgId=" + imgid);
                                //MessageBox.Show("删除施工图");
                            }

                        }
                    }
                    break;
                case "02":
                    if (body.Trim().Length > 0)
                    {
                        //拆分内容信息
                        string[] bodyItems = body.Split(new char[] { ',' });
                        //判断是否重复数据
                        string strSql = "select ImgId from Details where ImgId=" + bodyItems[0];
                        using(DataTable ot=DBAction.GetDTFromSQL(strSql))
                        {
                            //不是重复数据则添加
                            if (ot.Rows.Count==0)
                            {
                                RParams param = new RParams();
                                param.Add("ImgId", bodyItems[0]);
                                param.Add("FilePath", bodyItems[1]);
                                param.Add("PhotoName", bodyItems[2]);
                                param.Add("UploadUser", bodyItems[3]);
                                param.Add("UploadTime", BaseLibrary.ConversionTime(bodyItems[4], 0));
                                DBAction.Insert("Details", param);
                                //MessageBox.Show("添加施工图");
                            }
                        }
                    }
                    break;
                case "03":
                    //MessageBox.Show("接收结束");
                    LocoInfo.TrainInfo.Imgupdate = true;
                    break;
                default:
                    break;
            }

        }
    }
}
