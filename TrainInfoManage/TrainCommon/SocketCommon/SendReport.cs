using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace TrainCommon
{
    public class SendReport
    {
        public static void SendReportHead(SckTrains socket, string item) 
        {
            //报单ID
            int rid = LocoInfo.TrainInfo.ReportID;
            SckParams param = new SckParams();
            //获取报单头信息
            using (DataTable rh = DBAction.GetDTFromSQL("select CreateTime,RailCorp,Depot,TrainType,TrainNum from ReportHeader where ID=" + rid))
            //乘务员信息
            using (DataTable st = DBAction.GetDTFromSQL("select DriverNum,SubDriverNum,DutyTime,ReceiveTime,DeliverTime from Steward where RHId=" + rid))
            //出入段信息
            using (DataTable ot = DBAction.GetDTFromSQL("select OutLocalTime,InExternalTime,OutExternalTime,InLocalTime from TrainInOut where  RHId=" + rid))
            //领取燃料
            using (DataTable ft = DBAction.GetDTFromSQL("select Receive,Deliver from TrainGetFuel where RHId=" + rid))
            {
                //组合报表头内容
                string strConment = "";
                if (rh.Rows.Count > 0)
                {
                    strConment += rh.Rows[0]["CreateTime"].ToString() == "" ? "%," : rh.Rows[0]["CreateTime"].ToString() + ",";
                    strConment += rh.Rows[0]["RailCorp"].ToString() == "" ? "%," : rh.Rows[0]["RailCorp"].ToString() + ",";
                    strConment += rh.Rows[0]["Depot"].ToString() == "" ? "%," : rh.Rows[0]["Depot"].ToString() + ",";
                    strConment += rh.Rows[0]["TrainType"].ToString() == "" ? "%," : rh.Rows[0]["TrainType"].ToString() + ",";
                    strConment += rh.Rows[0]["TrainNum"].ToString() == "" ? "%," : rh.Rows[0]["TrainNum"].ToString() + ",";
                }
                else
                {
                    strConment += "%,%,%,%,%,";
                }

                if (st.Rows.Count > 0)
                {
                    strConment += st.Rows[0]["DriverNum"].ToString() == "" ? "%," : st.Rows[0]["DriverNum"].ToString() + ",";
                    strConment += st.Rows[0]["SubDriverNum"].ToString() == "" ? "%," : st.Rows[0]["SubDriverNum"].ToString() + ",";
                    strConment += st.Rows[0]["DutyTime"].ToString() == "" ? "%," : st.Rows[0]["DutyTime"].ToString() + ",";
                    strConment += st.Rows[0]["ReceiveTime"].ToString() == "" ? "%," : st.Rows[0]["ReceiveTime"].ToString() + ",";
                    strConment += st.Rows[0]["DeliverTime"].ToString() == "" ? "%," : st.Rows[0]["DeliverTime"].ToString() + ",";
                }
                else
                {
                    strConment += "%,%,%,%,%,";
                }

                if (ot.Rows.Count > 0)
                {
                    strConment += ot.Rows[0]["OutLocalTime"].ToString() == "" ? "%," : ot.Rows[0]["OutLocalTime"].ToString() + ",";
                    strConment += ot.Rows[0]["InExternalTime"].ToString() == "" ? "%," : ot.Rows[0]["InExternalTime"].ToString() + ",";
                    strConment += ot.Rows[0]["OutExternalTime"].ToString() == "" ? "%," : ot.Rows[0]["OutExternalTime"].ToString() + ",";
                    strConment += ot.Rows[0]["InLocalTime"].ToString() == "" ? "%," : ot.Rows[0]["InLocalTime"].ToString() + ",";
                }
                else
                {
                    strConment += "%,%,%,%,";
                }
                if (ft.Rows.Count > 0)
                {
                    strConment += ft.Rows[0]["Receive"].ToString() == "" ? "%," : ft.Rows[0]["Receive"].ToString() + ",";
                    strConment += ft.Rows[0]["Deliver"].ToString() == "" ? "%" : ft.Rows[0]["Deliver"].ToString();
                }
                else
                {
                    strConment += "%,%";
                }
                string recon = "0";
                //补机重联
                using (DataTable dt = DBAction.GetDTFromSQL("select * from Reconnection where RHId=" + rid))
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string trType = dt.Rows[i]["TrainType"].ToString();
                            string reDistance = dt.Rows[i]["RegionDistance"].ToString();
                            string belong = dt.Rows[i]["Belong"].ToString();

                            trType = trType.Trim() == "" ? "%" : trType.Trim();
                            reDistance = reDistance.Trim() == "" ? "%" : reDistance.Trim();
                            belong = belong.Trim() == "" ? "%" : belong.Trim();
                            recon += trType + "@" + reDistance + "@" + belong + "&";
                        }
                        if (recon.Trim().Length > 0)
                        {
                            recon = recon.Substring(0, recon.Length - 1);
                        }
                    }

                    //车型
                    string typeNum = LocoInfo.TrainInfo.TypeNum;
                    //车号
                    string trainNum = LocoInfo.TrainInfo.TrainNum;
                    //创建时间
                    string createTime = rh.Rows[0]["CreateTime"].ToString();
                    //发送报单头
                    param.Tml = "Cmd,Style,Type,TypeNum,TrainNum,CreateTime,ReportID,Reconnection,Conment";
                    param.Add("Cmd", "0201", false);
                    param.Add("Style", "00", false);
                    param.Add("Type", "00", false);
                    param.Add("TypeNum", typeNum, false);
                    param.Add("TrainNum", trainNum, false);
                    param.Add("CreateTime", String.Format("{0:yyMMddHHmmss}", Convert.ToDateTime(createTime)), false);
                    param.Add("ReportID", rid + "", false);
                    param.Add("Reconnection", recon, false);
                    param.Add("Conment", strConment, true);
                    List<string> packs = param.CreatePacks();
                    bool result = socket.Send(param);
                }

            }

           
        }

        /// <summary>
        /// 分析报单回复信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="item"></param>
        public static void AnalyzeReport(SckTrains socket, string item)
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
            //Cmd,Type,TypeNum,TrainNum,CreateTime,ReportID
            //获取返回类型
            string type = headItems[1];
            //已提交运行编组ID
            //string groupId = headItems[2];
            switch (type)
            {
                case "00":
                    //MessageBox.Show("回复报单运行编组信息");
                    ReplyReportGroup(socket,headItems,body);
                    break;
                case "01":
                    //MessageBox.Show("报单信息发送完成");
                    UpdateGroupSend(headItems); 
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 00 发送报单运行编组信息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="item"></param>
        public static void ReplyReportGroup(SckTrains socket, string[] headItems, string body)
        {
            #region  查询未发送的报单编组

            //判定接收到的请求是否为该车型请求
            if (LocoInfo.TrainInfo.TypeNum!=headItems[2]||LocoInfo.TrainInfo.TrainNum!=headItems[3])
            {
                return;
            }
            string strSql = "select GroupSendID from SendLog where ReportSendID=" + body;

            //已发送运行编组ID
            int gid = 0;
            using (DataTable rb = DBAction.GetDTFromSQL(strSql))
            {
                if (rb.Rows.Count > 0)
                {
                    string rg = rb.Rows[0]["GroupSendID"].ToString().Trim();
                    gid = rg == "" ? 0 : Convert.ToInt32(rb.Rows[0]["GroupSendID"]);
                }
                string rid = body;
                strSql = "select * from RunAndGroup where RHId=" + rid + " and ID>" + (gid - 1);
                using (DataTable gt = DBAction.GetDTFromSQL(strSql))
                {
                    if (gt.Rows.Count == 0)
                    {
                        LocoInfo.TrainInfo.IsSendSucess = true;
                        return;
                    }
            #endregion
                    string[] groups = new string[gt.Rows.Count];
                    for (int i = 0; i < gt.Rows.Count; i++)
                    {
                        string g0 = gt.Rows[i]["TrainNum"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TrainNum"].ToString().Trim();
                        string g1 = gt.Rows[i]["StationName"].ToString().Trim() == "" ? "%" : gt.Rows[i]["StationName"].ToString().Trim();
                        string g2 = gt.Rows[i]["ArrivedTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["ArrivedTime"].ToString().Trim();
                        string g3 = gt.Rows[i]["SetOutTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SetOutTime"].ToString().Trim();
                        string g4 = gt.Rows[i]["StopTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["StopTime"].ToString().Trim();
                        string g5 = gt.Rows[i]["SwitchTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SwitchTime"].ToString().Trim();
                        string g6 = gt.Rows[i]["OutStopTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["OutStopTime"].ToString().Trim();
                        string g7 = gt.Rows[i]["EarlyTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["EarlyTime"].ToString().Trim();
                        string g8 = gt.Rows[i]["LateTime"].ToString().Trim() == "" ? "%" : gt.Rows[i]["LateTime"].ToString().Trim();
                        string g9 = gt.Rows[i]["Reason"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Reason"].ToString().Trim();
                        string g10 = gt.Rows[i]["WorkMark"].ToString().Trim() == "" ? "%" : gt.Rows[i]["WorkMark"].ToString().Trim();
                        string g11 = gt.Rows[i]["RegionCode"].ToString().Trim() == "" ? "%" : gt.Rows[i]["RegionCode"].ToString().Trim();
                        string g12 = gt.Rows[i]["RegionDistance"].ToString().Trim() == "" ? "%" : gt.Rows[i]["RegionDistance"].ToString().Trim();
                        string g13 = gt.Rows[i]["TotalWeight"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TotalWeight"].ToString().Trim();
                        string g14 = gt.Rows[i]["LoadWeight"].ToString().Trim() == "" ? "%" : gt.Rows[i]["LoadWeight"].ToString().Trim();
                        string g15 = gt.Rows[i]["BusNumSum"].ToString().Trim() == "" ? "%" : gt.Rows[i]["BusNumSum"].ToString().Trim();
                        string g16 = gt.Rows[i]["Supplementtary"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Supplementtary"].ToString().Trim();
                        string g17 = gt.Rows[i]["SuppOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["SuppOffice"].ToString().Trim();
                        string g18 = gt.Rows[i]["WeightTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["WeightTruck"].ToString().Trim();
                        string g19 = gt.Rows[i]["EmptyTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["EmptyTruck"].ToString().Trim();
                        string g20 = gt.Rows[i]["NoUseTruck"].ToString().Trim() == "" ? "%" : gt.Rows[i]["NoUseTruck"].ToString().Trim();
                        string g21 = gt.Rows[i]["GetPassenger"].ToString().Trim() == "" ? "%" : gt.Rows[i]["GetPassenger"].ToString().Trim();
                        string g22 = gt.Rows[i]["Other"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Other"].ToString().Trim();
                        string g23 = gt.Rows[i]["Summation"].ToString().Trim() == "" ? "%" : gt.Rows[i]["Summation"].ToString().Trim();
                        string g24 = gt.Rows[i]["TrainChange"].ToString().Trim() == "" ? "%" : gt.Rows[i]["TrainChange"].ToString().Trim();
                        string g25 = gt.Rows[i]["BelongOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["BelongOffice"].ToString().Trim();
                        string g26 = gt.Rows[i]["UndertakeOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["UndertakeOffice"].ToString().Trim();
                        string g27 = gt.Rows[i]["UndertakeOffice"].ToString().Trim() == "" ? "%" : gt.Rows[i]["UndertakeOffice"].ToString().Trim();

                        string group = g0 + "," + g1 + "," + g2 + "," + g3 + "," + g4 + "," + g5 + "," + g6 + "," + g7 + "," + g8 + "," + g9 + "," + g10 + "," + g11 + "," + g12 + "," + g13 + "," + g14 + "," + g15 + "," + g16 + "," + g17 + "," + g18 + "," + g19 + "," + g20 + "," + g21 + "," + g22 + "," + g23 + "," + g24 + "," + g25 + "," + g26 + "," + g27 + "," + gt.Rows[i]["ID"].ToString().Trim();
                        groups[i] = group;
                    }
                    //发送报单运行编组
                    SckParams param = new SckParams();
                    param.Clear();
                    //"Cmd,Type,TypeNum,TrainNum,CreateTime,ReportID";
                    param.Tml = "Cmd,Style,TypeNum,TrainNum,CreateTime,ReportID,Group";
                    param.Add("Cmd", "0202", false);
                    param.Add("Style", "00", false);
                    param.Add("TypeNum", headItems[2], false);
                    param.Add("TrainNum", headItems[3], false);
                    param.Add("CreateTime", headItems[4], false);
                    param.Add("ReportID", body, false);
                    param.Add("Group", groups, true);
                    //MessageBox.Show("发送编组条数" + groups.Length);
                    param.CreatePacks();
                    socket.Send(param);

                }
            }

            //if (serverGroup.Trim().Length > 0)
            //{
            //    strSql += " and ID not in (" + serverGroup.Trim() + ") ";
            //}
            
        }

        /// <summary>
        /// 修改已发送运行编组信息
        /// </summary>
        public static void UpdateGroupSend(string[] headItems) 
        {
            LocoInfo.TrainInfo.IsSendSucess = true;
            //MessageBox.Show(headItems[3] + "报单信息发送完成----------->" + headItems[2]);
            //"Cmd,Type,ReportID,GroupID,Content";
            using (RParams pram = new RParams())
            {
                pram.Add("GroupSendID", headItems[3]);
                DBAction.Update("SendLog", "GroupSendID", "ReportSendID=" + headItems[2], pram);
            }
        }

    }
}
