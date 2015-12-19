using System;

using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TrainCommon
{
    public class ReportAction
    {
        //提交报单
        public  DataSet GetReportData() {

            //获取已提交的补机重联ID及运行编组ID
            int reconId = 0;//补机重联记录ID
            int runGroupID = 0;//运行编组记录ID
            int ID = 0;//机车客户端记录配置ID
            using (DataTable log = DBAction.GetDTFromSQL("select ID,ReconID,RunGroupID from RoboConfig where ReportID=" + LocoInfo.TrainInfo.ReportID))
            {
                if (log.Rows.Count > 0)
                {
                    ID = Convert.ToInt32(log.Rows[log.Rows.Count - 1]["ID"]);
                    reconId = Convert.ToInt32(log.Rows[log.Rows.Count - 1]["ReconID"]);
                    runGroupID = Convert.ToInt32(log.Rows[log.Rows.Count - 1]["RunGroupID"]);
                    runGroupID = runGroupID - 1;//上传记录要包含已上传的最后一条，（最后一条信息可能在上传后有修改）
                }
            }

            //列车运行及编组情况
            using (DataTable ra = DBAction.GetDTFromSQL("select * from  RunAndGroup where  RHId=" + LocoInfo.TrainInfo.ReportID + " and ID>" + runGroupID))
            {
                ra.TableName = "RunAndGroup";
                //运行编组信息为空时，返回空的数据集，不提交报单数据
                if (ra.Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    //报单数据集
                    DataSet ds = new DataSet();
                    //获取报表头数据集
                    DataTable rh = DBAction.GetDTFromSQL("select * from ReportHeader where ID=" + LocoInfo.TrainInfo.ReportID);
                    rh.TableName = "ReportHeader";
                    //获取报表乘务员
                    DataTable st = DBAction.GetDTFromSQL("select * from Steward where RHId=" + LocoInfo.TrainInfo.ReportID);
                    st.TableName = "Steward";
                    //机车出入段
                    DataTable ti = DBAction.GetDTFromSQL("select * from TrainInOut where RHId=" + LocoInfo.TrainInfo.ReportID);
                    ti.TableName = "TrainInOut";
                    //机车领取燃料信息
                    DataTable tg = DBAction.GetDTFromSQL("select * from  TrainGetFuel where  RHId=" + LocoInfo.TrainInfo.ReportID);
                    tg.TableName = "TrainGetFuel";
                    //机车领取油脂
                    DataTable to = DBAction.GetDTFromSQL("select * from  TrainGetOil where  RHId=" + LocoInfo.TrainInfo.ReportID);
                    to.TableName = "TrainGetOil";
                    //补机重联和有动力附挂机车
                    DataTable rt = DBAction.GetDTFromSQL("select * from  Reconnection where  RHId=" + LocoInfo.TrainInfo.ReportID);
                    rt.TableName = "Reconnection";
                    //将报表数据填入报单数据集中
                    ds.Tables.Add(rh.Copy());
                    ds.Tables.Add(st.Copy());
                    ds.Tables.Add(ti.Copy());
                    ds.Tables.Add(tg.Copy());
                    ds.Tables.Add(to.Copy());
                    ds.Tables.Add(rt.Copy());
                    ds.Tables.Add(ra.Copy());
                    if (rt.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.ReconID = Convert.ToInt32(rt.Rows[rt.Rows.Count - 1]["ID"].ToString());
                    }
                    if (ra.Rows.Count > 0)
                    {
                        LocoInfo.TrainInfo.RunGroupID = Convert.ToInt32(ra.Rows[ra.Rows.Count - 1]["ID"].ToString());
                    }
                    return ds;
                }
            }
            
        }
    }
}
