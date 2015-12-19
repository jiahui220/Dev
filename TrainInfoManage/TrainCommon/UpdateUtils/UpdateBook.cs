using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Collections;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace TrainCommon
{
    /// <summary>
    /// 电子书更新
    /// </summary>
    public class UpdateBook
    {
        public static bool IsEnd = false;
        public static bool isRequest = false;
        public static string lastBookId = string.Empty;
        public static string lastChapterId = string.Empty;
        public static string type = string.Empty;
        public static List<int> listPackets = null;
        public static bool IsInit = false;
        public static int count = 0;

        /// <summary>
        /// 发送所有的书籍编号--00
        /// </summary>
        /// <param name="bookId"></param>
        public static void SendAllBookId(SckTrains socket)
        {
            using (DataTable dt = DBAction.GetDTFromSQL("select * from Book"))
            {
                string content = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    content += dt.Rows[i]["BookId"].ToString().Trim() + " ";
                }
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim().Replace(" ", ",");
                }
                SckParams param = new SckParams();
                try
                {
                    param.Tml = "Cmd,Type,AllBookId";
                    param.Add("Cmd", "0400", false);
                    param.Add("Type", "00", false);
                    if (string.IsNullOrEmpty(content))
                    {
                        param.Add("AllBookId", "0", true);
                    }
                    else
                    {
                        param.Add("AllBookId", content, true);
                    }
                    param.CreatePacks();
                    if (param.PackItems != null && param.PackItems.Count > 0)
                    {
                        bool result = socket.Send(param, "TCP");
                        Thread.Sleep(500);
                        if (!result && count < 5)
                        {
                            SendAllBookId(socket);
                            count++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogDaily.logerr(ex.ToString());
                    //MessageBox.Show(ex + "--------------01");
                }
            }

        }

        /// <summary>
        /// 发送书籍的编号和时间--10
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bookId"></param>
        /// <param name="latest">时间格式：yyMMddhhmmss</param>
        public static void SendBook(SckTrains socket,string bookId,string latest)
        {
            SckParams param = new SckParams();
            try
            {
                param.Tml = "Cmd,Type,BookId,Latest";
                param.Add("Cmd", "0400", false);
                param.Add("Type", "10", false);
                param.Add("BookId", bookId, false);
                param.Add("Latest", latest, true);
                param.CreatePacks();
                if (param.PackItems != null && param.PackItems.Count > 0)
                {
                    bool result = socket.Send(param, "TCP");
                    Thread.Sleep(500);
                    //if (!result)
                    //{
                    //    SendBook(socket, bookId, latest);
                    //}
                }
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show(ex + "--------------02");
            }
        }

        /// <summary>
        /// 发送某本书籍的所有章节编号--20
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bookId"></param>
        public static void SendAllChapterId(SckTrains socket, string bookId)
        {
            using (DataTable dt = DBAction.GetDTFromSQL("select * from Chapter where BookId=" + bookId))
            {
                string content = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    content += dt.Rows[i]["ChapterId"].ToString().Trim() + " ";
                }
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim().Replace(" ", ",");
                }
                SckParams param = new SckParams();
                try
                {
                    param.Tml = "Cmd,Type,BookId,AllChapterId";
                    param.Add("Cmd", "0400", false);
                    param.Add("Type", "20", false);
                    param.Add("BookId", bookId, false);
                    if (string.IsNullOrEmpty(content))
                    {
                        param.Add("AllChapterId", "0", true);
                    }
                    else
                    {
                        param.Add("AllChapterId", content, true);
                    }
                    param.CreatePacks();
                    if (param.PackItems != null && param.PackItems.Count > 0)
                    {
                        bool result = socket.Send(param, "TCP");
                        Thread.Sleep(500);
                        //if (!result)
                        //{
                        //    SendAllChapterId(socket,bookId);
                        //}
                    }
                }
                catch (Exception ex)
                {
                    LogDaily.logerr(ex.ToString());
                    //MessageBox.Show(ex + "--------------03");
                }
            }
        }

        /// <summary>
        /// 发送某本书籍的章节编号和时间--30
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bookId"></param>
        /// <param name="chapterId"></param>
        /// <param name="latest">时间格式：yyMMddhhmmss</param>
        public static void SendChapterId(SckTrains socket,string bookId, string chapterId,string latest)
        {
            SckParams param = new SckParams();
            try
            {
                param.Tml = "Cmd,Type,BookId,ChapterId,Latest";
                param.Add("Cmd", "0400", false);
                param.Add("Type", "30", false);
                param.Add("BookId", bookId, false);
                param.Add("ChapterId", chapterId, false);
                param.Add("Latest", latest, true);
                param.CreatePacks();
                if (param.PackItems != null && param.PackItems.Count > 0)
                {
                    bool result = socket.Send(param, "TCP");
                    Thread.Sleep(500);
                    //if (!result)
                    //{
                    //    SendChapterId(socket, bookId, chapterId, latest);
                    //}
                }
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show(ex + "--------------04");
            }

        }

        /// <summary>
        /// 发送某本书的所有章节更新完成--40
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="bookId"></param>
        public static void SendFinishAllChapter(SckTrains socket, string bookId)
        {
            SckParams param = new SckParams();
            try
            {
                param.Tml = "Cmd,Type,BookId";
                param.Add("Cmd", "0400", false);
                param.Add("Type", "40", false);
                param.Add("BookId", bookId, true);
                param.CreatePacks();
                if (param.PackItems != null && param.PackItems.Count > 0)
                {
                    bool result = socket.Send(param, "TCP");
                    Thread.Sleep(500);
                    //if (!result)
                    //{
                    //    SendFinishAllChapter(socket,bookId);
                    //}
                }
            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
                //MessageBox.Show(ex + "--------------05");
            }
            IsInit = false;
        }


        ///**********************以下为分析就收到的关于书籍的方法*********************

        private static List<string> listBook = null;
        private static List<string> listChapter = null;

        /// <summary>
        /// 获取书籍编号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBook()
        {
            using (DataTable dt = DBAction.GetDTFromSQL("select * from Book"))
            {
                if (dt.Rows.Count == 0)
                {
                    return listBook;
                }
                listBook = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBook.Add(dt.Rows[i]["BookId"].ToString());
                }
                return listBook;
            }
        }

        /// <summary>
        /// 获取书籍章节编号
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public static List<string> GetChapter(string bookId)
        {
            using (DataTable dt = DBAction.GetDTFromSQL("select * from Chapter where BookId=" + bookId))
            {
                if (dt.Rows.Count == 0)
                {
                    return listChapter;
                }
                listChapter = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listChapter.Add(dt.Rows[i]["ChapterId"].ToString());
                }
                return listChapter;
            }

        }

        /// <summary>
        /// 发送书籍
        /// </summary>
        public static void SendBook(SckTrains socket)
        {
            if (LocoInfo.TrainInfo.ClientSocket != null)
            {
                using (DataTable dt = DBAction.GetDTFromSQL("select * from Book where BookId=" + listBook[0]))
                {
                    if (dt.Rows.Count == 0)
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Latest"].ToString()))
                    {
                        SendBook(socket, listBook[0], Convert.ToDateTime(dt.Rows[0]["Latest"].ToString()).ToString("yyMMddhhmmss"));
                    }
                    else
                    {
                        SendBook(socket, listBook[0], DateTime.Now.ToString("yyMMddhhmmss"));
                    }
                }
            }
        }

        /// <summary>
        /// 发送章节
        /// </summary>
        public static void SendChapter(SckTrains socket)
        {
            IsInit = false;
            if (LocoInfo.TrainInfo.ClientSocket != null)
            {
                using (DataTable dt = DBAction.GetDTFromSQL("select * from Chapter where BookId=" + listBook[0] + " and ChapterId=" + listChapter[0]))
                {
                    if (dt.Rows.Count == 0)
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Latest"].ToString()))
                    {
                        SendChapterId(socket, listBook[0], listChapter[0], Convert.ToDateTime(dt.Rows[0]["Latest"].ToString()).ToString("yyMMddhhmmss"));
                    }
                    else
                    {
                        SendChapterId(socket, listBook[0], listChapter[0], DateTime.Now.ToString("yyMMddhhmmss"));
                    }
                }
            }
        }

        public static void AnalyzeBook(SckTrains socket,string info)
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
            //获取校验码信息
            string check = receData[2];
            //获取接收到的数据验证码
            string receCode = check.Substring(0, 4);
            //根据内容获取验证码
            UInt16 crcCode = SckCRC16.Parse(body);
            string code = crcCode.ToString("X");
            //当前包索引
            int curr = 1;
            try
            {
                curr = Convert.ToInt32(check.Trim().Substring(check.Length - 2, 2));
            }
            catch{}
            //总包数
            int total = 1;
            try
            {
                total = Convert.ToInt32(check.Trim().Substring(check.Length - 4, 2));
            }
            catch { }
            //验证数据是否正常
            if (total == 1 && code != receCode)
            {
                return;
            }
            if (total > 1 && curr == 1)
            {
                listPackets = new List<int>();
            }
            //分解头部内容
            string[] headItems = head.Split(new char[] { '$' });
            type = headItems[1];
            //MessageBox.Show(headItems[1]);
            switch (headItems[1])
            { 
                ///*********************书籍编号更新
                case "01":
                    AnalyzeBookId(body);
                    listBook = GetBook();
                    //MessageBox.Show(listBook.Count.ToString());
                    if (listBook == null || listBook.Count == 0)
                    {
                        return;
                    }
                    SendBook(socket);
                    //MessageBox.Show(listBook.Count.ToString());
                    break;
                ///*********************检测书籍是否有更新
                case "11":
                    AnalyzeBookName(headItems,body);
                    if (listBook == null || listBook.Count == 0)
                    {
                        IsEnd = true;
                        return;
                    }
                    SendAllChapterId(socket, listBook[0]);
                    break;
                case "12":
                    if (isRequest)
                    {
                        if (!listBook.Contains(lastBookId) && !string.IsNullOrEmpty(lastBookId))
                        {
                            listBook.Insert(0, lastBookId);
                        }
                    }
                    if (listBook == null || listBook.Count == 0)
                    {
                        IsEnd = true;
                        return;
                    }
                    lastBookId = listBook[0];
                    listBook.RemoveAt(0);
                    //MessageBox.Show(listBook.Count.ToString());
                    if (listBook == null || listBook.Count == 0)
                    {
                        return;
                    }
                    SendBook(socket);
                    break;
                ///*********************书籍所有章节编号更新
                case "21":
                    AnalyzeChapterId(headItems,body);
                    if (listBook == null || listBook.Count == 0)
                    {
                        return;
                    }
                    listChapter = GetChapter(listBook[0]);
                    if (listChapter == null || listChapter.Count == 0)
                    {
                        return;
                    }
                    SendChapter(socket);
                    //MessageBox.Show("11111111111111111");
                    break;
                ///*********************检测书籍章节是否有更新
                case "31":
                    AnalyzeChapter(headItems,body,total,curr);
                    if (isRequest)
                    {
                        if (!listBook.Contains(lastBookId) && !string.IsNullOrEmpty(lastBookId))
                        {
                            listBook.Insert(0, lastBookId);
                        }
                        if (!listChapter.Contains(lastChapterId) && !string.IsNullOrEmpty(lastChapterId))
                        {
                            listChapter.Insert(0, lastChapterId);
                        }
                    }
                    //MessageBox.Show(total + " " + curr);
                    if (total == curr)
                    {
                        if (listChapter == null || listChapter.Count == 0)
                        {
                            SendFinishAllChapter(socket, listBook[0]);
                            lastBookId = listBook[0];
                            listBook.RemoveAt(0);
                            if (listBook == null || listBook.Count == 0)
                            {
                                return;
                            }
                            SendBook(socket);
                            return;
                        }
                        lastChapterId = listChapter[0];
                        listChapter.RemoveAt(0);
                        if (listChapter == null || listChapter.Count == 0)
                        {
                            SendFinishAllChapter(socket, listBook[0]);
                            lastBookId = listBook[0];
                            listBook.RemoveAt(0);
                            if (listBook == null || listBook.Count == 0)
                            {
                                return;
                            }
                            SendBook(socket);
                            return;
                        }
                        SendChapter(socket);
                    }
                    break;
                case "32":
                    if (isRequest)
                    {
                        if (!listBook.Contains(lastBookId) && !string.IsNullOrEmpty(lastBookId))
                        {
                            listBook.Insert(0, lastBookId);
                        }
                        if (!listChapter.Contains(lastChapterId) && !string.IsNullOrEmpty(lastChapterId))
                        {
                            listChapter.Insert(0, lastChapterId);
                        }
                    }
                    if (listChapter == null || listChapter.Count == 0)
                    {
                        SendFinishAllChapter(socket, listBook[0]);
                        lastBookId = listBook[0];
                        listBook.RemoveAt(0);
                        if (listBook == null || listBook.Count == 0)
                        {
                            return;
                        }
                        SendBook(socket);
                        return;
                    }
                    lastChapterId = listChapter[0];
                    listChapter.RemoveAt(0);
                    if (listChapter == null || listChapter.Count == 0)
                    {
                        SendFinishAllChapter(socket, listBook[0]);
                        lastBookId = listBook[0];
                        listBook.RemoveAt(0);
                        if (listBook == null || listBook.Count == 0)
                        {
                            return;
                        }
                        SendBook(socket);
                        return;
                    }
                    SendChapter(socket);
                    break;
                ///*********************书籍的所有章节完成更新
                case "41":
                    AnalyzeBookLatest(headItems,body);
                    if (listBook == null || listBook.Count == 0)
                    {
                        IsEnd = true;
                        return;
                    }
                    SendBook(socket);
                    break;
            }
        }

        public static void AnalyzeBookId(string body)
        {
            string[] books = body.Split('%');
            AnalyzeBookIdDel(books[0].Trim());
            AnalyzeBookAdd(books[1].Trim());
        }

        /// <summary>
        /// 删除多余的书籍----01
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeBookIdDel(string body)
        {
            string[] books = body.Split(',');
            for (int i = 0; i < books.Length; i++)
            {
                if (!string.IsNullOrEmpty(books[i]))
                {
                    DBAction.Delete("Book", "BookId=" + books[i].Trim());
                    DBAction.Delete("Chapter", "BookId=" + books[i].Trim());
                }
            }
        }

        /// <summary>
        /// 添加书籍编号----02
        /// </summary>
        /// <param name="body"></param>
        public static void AnalyzeBookAdd(string body)
        {
            string[] books = body.Split(',');
            using (RParams pms = new RParams())
            {
                for (int i = 0; i < books.Length; i++)
                {
                    if (!string.IsNullOrEmpty(books[i]))
                    {
                        if (!DBAction.HasData("Book", "BookId=" + books[i].Trim(), new RParams()))
                        {
                            pms.Items.Clear();
                            pms.Add("BookId", books[i].Trim());
                            DBAction.Insert("Book", pms);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 更新书籍名称----11
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeBookName(string[] headItems,string body)
        {
            using (RParams pms = new RParams())
            {
                pms.Add("BookName", body.Trim());
                DBAction.Update("Book", "BookName", "BookId=" + headItems[2], pms);
            }

        }

        public static void AnalyzeChapterId(string[] headItems, string body)
        {
            string[] chapters = body.Split('%');
            AnalyzeChapterDel(headItems,chapters[0]);
            AnalyzeChapterAdd(headItems,chapters[1]);
        }

        /// <summary>
        /// 删除书籍下多余的章节----21
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeChapterDel(string[] headItems, string body)
        {
            string[] chapters = body.Split(',');
            for (int i = 0; i < chapters.Length; i++)
            {
                if (!string.IsNullOrEmpty(chapters[i]))
                {
                    DBAction.Delete("Chapter", "BookId=" + headItems[2] + " and ChapterId=" + chapters[i].Trim());
                }
            }
        }

        /// <summary>
        /// 为书籍添加新增的章节----22
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeChapterAdd(string[] headItems, string body)
        {
            string[] chapters = body.Split(',');
            RParams pms = new RParams();
            for (int i = 0; i < chapters.Length; i++)
            {
                if (!string.IsNullOrEmpty(chapters[i]))
                {
                    if (!DBAction.HasData("Chapter", "ChapterId=" + chapters[i].Trim() + " and BookId=" + headItems[2], new RParams()))
                    {
                        pms.Items.Clear();
                        pms.Add("BookId", headItems[2]);
                        pms.Add("ChapterId", chapters[i].Trim());
                        DBAction.Insert("Chapter", pms);
                    }
                }
            }
        }

        /// <summary>
        /// 更新章节内容----31
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeChapter(string[] headItems, string body,int total,int curr)
        {
            bool isExist = true;
            if (total == 1)
            {
                isExist = false;
            }
            else
            {
                if (listPackets != null)
                {
                    isExist = listPackets.Contains(curr);
                }
            }
            if (isExist)
            {
                return;
            }
            //MessageBox.Show(headItems[2] + " " + headItems[3] + " " + body.Trim());
            using (DataTable dt = DBAction.GetDTFromSQL("select * from Chapter where BookId=" + headItems[2] + " and ChapterId=" + headItems[3]))
            {
                if (dt.Rows.Count < 0)
                {
                    return;
                }
                string letest = BaseLibrary.ConversionTime(headItems[5], 0);
                RParams pms = null;
                if (!isExist && curr > 1)
                {
                    using (pms = new RParams())
                    {
                        pms.Add("Content", dt.Rows[0]["Content"].ToString().Trim() + body.Trim());
                        DBAction.Update("Chapter", "Content", "BookId=" + headItems[2] + " and ChapterId=" + headItems[3], pms);
                        if (listPackets != null)
                        {
                            listPackets.Add(curr);
                        }
                    }

                }
                if (!isExist && curr == 1)
                {
                    using (pms = new RParams())
                    {
                        pms.Add("ChapterName", headItems[4]);
                        pms.Add("Content", body.Trim());
                        pms.Add("Latest", letest);
                        DBAction.Update("Chapter", "ChapterName,Content,Latest", "BookId=" + headItems[2] + " and ChapterId=" + headItems[3], pms);
                        if (listPackets != null)
                        {
                            listPackets.Add(curr);
                        }
                    }

                }
            }

            //MessageBox.Show("插入成功");
        }

        /// <summary>
        /// 更新书籍时间----41
        /// </summary>
        /// <param name="headItems"></param>
        /// <param name="body"></param>
        public static void AnalyzeBookLatest(string[] headItems, string body)
        {
            RParams pms = new RParams();
            pms.Add("Latest",BaseLibrary.ConversionTime(body.Trim(),0));
            DBAction.Update("Book", "Latest", "BookId=" + headItems[2], pms);
        }
    }
}
