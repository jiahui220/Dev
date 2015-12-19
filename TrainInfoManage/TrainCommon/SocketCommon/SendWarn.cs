using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace TrainCommon
{
    public class SendWarn
    {
        #region 报警向服务实时发送报警信息

        private static int LogWarnID = 0;
        //已发报警ID
        private static int warnId = 0;
        //报警所属司机
        private static string warnDrNum = "";

        #region 原发送报警方法
        ///// <summary>
        /////发送报警项点信息 
        ///// </summary>
        ///// <param name="socket"></param>
        ///// <param name="item"></param>
        //public static void SendWarnItems(SckTrains socket, string item)
        //{
        //    try
        //    {
        //        //数据库查询语句
        //        string strSql = "";
        //        //获取已发送报警ID
        //        int sid = 0;
        //        string logNum = "";
        //        DataTable lt = null;
        //        //当前是否含有报警信息
        //        if (LocoInfo.TrainInfo.CurrWarnId != 0)
        //        {
        //            //读取已发送的报警ID
        //            strSql = "select DriverNum,WarnSendID from SendLog where ID=" + LocoInfo.TrainInfo.LogId;
        //            using (lt = DBAction.GetDTFromSQL(strSql))
        //            {
        //                if (lt.Rows.Count > 0)
        //                {
        //                    sid = Convert.ToInt32(lt.Rows[0]["WarnSendID"]);
        //                    //判定当前司机报警是否为第一条报警
        //                    if (sid == 0)
        //                    {
        //                        sid = LocoInfo.TrainInfo.CurrWarnId - 1;
        //                    }
        //                    logNum = lt.Rows[0]["DriverNum"].ToString();
        //                    LogWarnID = LocoInfo.TrainInfo.LogId;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //当前未报警则查询以往未发送报警信息
        //            strSql = "select ID,DriverNum,WarnSendID from SendLog where WarnSendID!='-1'";
        //            using (lt = DBAction.GetDTFromSQL(strSql))
        //            {
        //                if (lt.Rows.Count > 0)
        //                {
        //                    sid = Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["WarnSendID"]);
        //                    LogWarnID = Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["ID"]);
        //                    logNum = lt.Rows[0]["DriverNum"].ToString();
        //                }
        //            }
        //        }
        //        //MessageBox.Show("司机编号-------->" + logNum);
        //        //获取未发送的报警
        //        using (DataTable wt = DBAction.GetDTFromSQL("select * from AlarmRunInfo where DriverNum='" + logNum + "' and ID>" + sid))
        //        {
        //            //判断是否存在未发送报警信息
        //            if (wt.Rows.Count > 0)
        //            {
        //                //报警时间
        //                string createTime = wt.Rows[0]["CreateTime"].ToString();
        //                string typeNum = wt.Rows[0]["TypeNum"].ToString();
        //                string trainNum = wt.Rows[0]["TrainId"].ToString();
        //                string driverNum = wt.Rows[0]["DriverNum"].ToString();
        //                string subDriverNum = wt.Rows[0]["SubDriverNum"].ToString();
        //                string warnItems = wt.Rows[0]["WarnItems"].ToString();
        //                //最后一条历史报警记录
        //                string lastWarnID = wt.Rows[wt.Rows.Count - 1]["ID"].ToString();
        //                //当前发送的报警记录
        //                string sendWarnID = wt.Rows[0]["ID"].ToString();
        //                DateTime warnDate = Convert.ToDateTime(createTime);

        //                SckParams param = new SckParams();
        //                //发送报警头信息
        //                param.Tml = "Cmd,Style,Type,CreateTime,TypeNum,TrainId,DriverNum,SubDriverNum,WarnID,WarnItems";
        //                param.Add("Cmd", "0301", false);
        //                param.Add("Style", "00", false);
        //                param.Add("Type", "00", false);
        //                param.Add("CreateTime", String.Format("{0:yyMMddHHmmss}", warnDate), false);
        //                param.Add("TypeNum", typeNum, false);
        //                param.Add("TrainId", trainNum, false);
        //                param.Add("DriverNum", driverNum, false);
        //                param.Add("SubDriverNum", subDriverNum, false);
        //                param.Add("WarnID", sendWarnID, false);
        //                param.Add("WarnItems", warnItems.Trim(), true);
        //                param.CreatePacks();
        //                //MessageBox.Show("发送报警项点");
        //                socket.Send(param);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogDaily.logerr(ex.ToString());
        //    }
        //}

        #endregion

        /// <summary>
        ///发送报警项点信息 
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="item"></param>
        public static void SendWarnItems(SckTrains socket, string item) 
        {
            try
            {
                //①查询当前值班司机已发报警
                using (DataTable lt = DBAction.GetDTFromSQL("select WarnSendID from SendLog where ID=" + LocoInfo.TrainInfo.LogId + " and DriverNum='"+LocoInfo.TrainInfo.DriverNum+"' "))
                {
                    //是否存在当前司机记录
                    if (lt.Rows.Count>0)
                    {
                        //获取当前司机已发报警ID
                        warnId =Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["WarnSendID"]);
                        //报警司机号
                        warnDrNum = LocoInfo.TrainInfo.DriverNum;
                        //当前发送的记录ID
                        LogWarnID = LocoInfo.TrainInfo.LogId;
                    }//②查询其他司机未发报警新
                    else
                    {
                        using (DataTable olt = DBAction.GetDTFromSQL("select ID,DriverNum,WarnSendID from SendLog where WarnSendID!='-1' "))
                        {
                            //获取历史记录ID
                            LogWarnID = Convert.ToInt32(olt.Rows[olt.Rows.Count - 1]["ID"]);
                            //获取记录司机号
                            warnDrNum = olt.Rows[olt.Rows.Count - 1]["DriverNum"].ToString();
                            //当前发送记录ID
                            warnId =Convert.ToInt32(lt.Rows[lt.Rows.Count - 1]["WarnSendID"]);
                        }

                    } 
                }
                using (DataTable wt = DBAction.GetDTFromSQL("select * from AlarmRunInfo where DriverNum='" + warnDrNum + "' and ID>" + warnId))
                {
                    //判断是否存在未发送报警信息
                    if (wt.Rows.Count > 0)
                    {
                        //报警时间
                        string createTime = wt.Rows[0]["CreateTime"].ToString();
                        string typeNum = wt.Rows[0]["TypeNum"].ToString();
                        string trainNum = wt.Rows[0]["TrainId"].ToString();
                        string driverNum = wt.Rows[0]["DriverNum"].ToString();
                        string subDriverNum = wt.Rows[0]["SubDriverNum"].ToString();
                        string warnItems = wt.Rows[0]["WarnItems"].ToString();
                        //最后一条历史报警记录
                        string lastWarnID = wt.Rows[wt.Rows.Count - 1]["ID"].ToString();
                        //当前发送的报警记录
                        string sendWarnID = wt.Rows[0]["ID"].ToString();
                        DateTime warnDate = Convert.ToDateTime(createTime);

                        SckParams param = new SckParams();
                        //发送报警头信息
                        param.Tml = "Cmd,Style,Type,CreateTime,TypeNum,TrainId,DriverNum,SubDriverNum,WarnID,WarnItems";
                        param.Add("Cmd", "0301", false);
                        param.Add("Style", "00", false);
                        param.Add("Type", "00", false);
                        param.Add("CreateTime", String.Format("{0:yyMMddHHmmss}", warnDate), false);
                        param.Add("TypeNum", typeNum, false);
                        param.Add("TrainId", trainNum, false);
                        param.Add("DriverNum", driverNum, false);
                        param.Add("SubDriverNum", subDriverNum, false);
                        param.Add("WarnID", sendWarnID, false);
                        param.Add("WarnItems", warnItems.Trim(), true);
                        //MessageBox.Show("发送报警项点");
                        List<string> packs = param.CreatePacks();
                        LogDaily.logerr(packs[0]);
                        socket.Send(param);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogDaily.logerr(ex.ToString());
            }
        }

        /// <summary>
        /// 回复报警信息回应
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="item"></param>
        public static void AnalyzeWarn(SckTrains socket, string item)
        {

            //除去首尾字符
            item = item.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = item.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("接收的内容--------->" + body);
            //获取返回类型
            string type = headItems[1];
            //报警ID
            string wranId = headItems[2];
            switch (type)
            {
                case "00":
                    //MessageBox.Show("回复报警运行信息");
                    ReplyWarnRun(socket,headItems);
                    break;
                case "01":
                    //MessageBox.Show("报警信息发送完成");
                    //更新已发送报警信息
                    UpdateWarnSend(wranId);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 00 发送报警运行信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="item"></param>
        public static void ReplyWarnRun(SckTrains socket, string[] headItems)
        {
            //判断该信息是否为该机车数据
            if (LocoInfo.TrainInfo.TrainNum!=headItems[5]||LocoInfo.TrainInfo.TypeNum!=headItems[4])
            {
                return;
            }
            SckParams param = null;
            //MessageBox.Show(BaseLibrary.ConversionTime(headItems[3], 0));
            DateTime currDate =Convert.ToDateTime(BaseLibrary.ConversionTime(headItems[3],0));
            string strSql = "select TrainRunInfo from TrainRuning where RecordTime between '" + currDate.AddMinutes(-10) + "' and '" + currDate.AddMinutes(10) + "' ";
            using (DataTable wrt = DBAction.GetDTFromSQL(strSql))
            {
                if (wrt.Rows.Count > 0)
                {
                    string[] runlogs = new string[wrt.Rows.Count];
                    for (int i = 0; i < wrt.Rows.Count; i++)
                    {
                        runlogs[i] = wrt.Rows[i][0].ToString();
                    }
                    //发送报警运行信息
                    param = new SckParams();
                    param.Clear();
                    param.Tml = "Cmd,Style,Type,CreateTime,TypeNum,TrainId,DriverNum,SubDriverNum,WarnID,WarnRun";
                    //"Cmd,Type,WarnID,WarnTime,TrainType,TrainNum,DriverNum,SubDriverNum,Conment";
                    param.Add("Cmd", "0302", false);
                    param.Add("Style", "00", false);
                    param.Add("Type", "01", false);
                    param.Add("CreateTime", String.Format("{0:yyMMddHHmmss}", currDate), false);
                    param.Add("TypeNum", headItems[4], false);
                    param.Add("TrainId", headItems[5], false);
                    param.Add("DriverNum", headItems[6], false);
                    param.Add("SubDriverNum", headItems[7], false);
                    param.Add("WarnID", headItems[2], false);
                    param.Add("WarnRun", runlogs, true);
                    param.CreatePacks();
                    //MessageBox.Show("发送报警运行信息01");
                    socket.Send(param);

                }
                else
                {
                    param = new SckParams();
                    param.Tml = "Cmd,Style,Type,Content";
                    param.Add("Cmd", "0302", false);
                    param.Add("Style", "00", false);
                    param.Add("Type", "02", false);//无报警运行信息
                    param.Add("Content", "00", true);
                    param.CreatePacks();
                    //MessageBox.Show("发送报警运行信息02");
                    socket.Send(param);


                }
                //删除已发送报警数据
                DBAction.Delete("AlarmRunInfo", "ID=" + headItems[2]);
            }

        }

        /// <summary>
        /// 更新已发送报警信息
        /// </summary>
        /// <param name="warnID">报警ID</param>
        public static void UpdateWarnSend(string warnID) 
        {
            RParams param = new RParams();
            using (DataTable wt = DBAction.GetDTFromSQL("select * from AlarmRunInfo where DriverNum='" + warnDrNum + "' and ID>" + warnID))
            {
                //判定历史报警记录是否发送完成
                if (wt.Rows.Count == 0 && LogWarnID!=LocoInfo.TrainInfo.LogId)
                {
                    //完成发送，将已发送值赋为-1
                    param.Add("WarnSendID", "-1");
                }
                else
                {
                    //未发送完全，或为当前记录司机则只记录已发送ID
                    param.Add("WarnSendID", warnID);
                }
            }
            DBAction.Update("SendLog", "WarnSendID", "ID=" + LogWarnID, param);
        }

        #endregion
    }
}
