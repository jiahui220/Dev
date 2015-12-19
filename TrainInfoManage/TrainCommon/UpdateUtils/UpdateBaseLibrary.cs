using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TrainCommon
{
    /// <summary>
    /// 更新基础库
    /// </summary>
    public class UpdateBaseLibrary
    {
        /// <summary>
        /// 请求更新车型库
        /// </summary>
        /// <param name="typeNum">车型编号</param>
        public static void UpdateTrainType(SckTrains socket,string typeNum)
        {
            SckParams param = new SckParams();
            //设置模板
            param.Tml = "Cmd,TypeNum,Conment";
            param.Add("Cmd", "1000", false);
            param.Add("TypeNum", typeNum, false);
            param.Add("Conment", "00", true);
            List<string> packs = param.CreatePacks();
            //发送车型请求
            socket.Send(param);
        }

        /// <summary>
        /// 分析接收到的车型
        /// </summary>
        public static void AnalyzeTrainType(string rece)
        {
            //除去首尾字符
            rece = rece.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = rece.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("回复报单头接收的内容--------->" + item);
            //获取车型编号
            string typeNum = headItems[1];
            //添加车型到车型库
            if (body!="%")
            {
                RParams pram = new RParams();
                pram.Add("iJXCode", typeNum);
                pram.Add("sJXName", body);
                DBAction.Insert("TrainTypeDiction", pram);
            }
        }

        /// <summary>
        /// 请求更新司机库
        /// </summary>
        /// <param name="driverNum"></param>
        public static void UpdateDriverName(SckTrains socket, string driverNum)
        {
            SckParams param = new SckParams();
            //设置模板
            param.Tml = "Cmd,DriverNum,Conment";
            param.Add("Cmd", "1100", false);
            param.Add("DriverNum", driverNum, false);
            param.Add("Conment", "00", true);
            List<string> packs = param.CreatePacks();
            //发送车型请求
            socket.Send(param);

        }

        /// <summary>
        /// 分析接收到的司机信息
        /// </summary>
        /// <param name="rece"></param>
        public static void AnalyzeDriverName(string rece)
        {
            //除去首尾字符
            rece = rece.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = rece.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("回复报单头接收的内容--------->" + item);
            //获取车型编号
            string driverNum = headItems[1];
            //添加车型到车型库
            if (body != "%")
            {
                RParams pram = new RParams();
                pram.Add("iSJCode", driverNum);
                pram.Add("sSJName", body);
                DBAction.Insert("DriverDiction", pram);
            }
        
        }

        /// <summary>
        /// 请求更新站名库
        /// </summary>
        /// <param name="czCode"></param>
        /// <param name="jlCode"></param>
        public static void UpdateStationName(SckTrains socket, string czCode, string jlCode) 
        {
            SckParams param = new SckParams();
            //设置模板
            param.Tml = "Cmd,CzCode,JlCode";
            param.Add("Cmd", "1200", false);
            param.Add("CzCode", czCode, false);
            param.Add("JlCode", jlCode, true);
            List<string> packs = param.CreatePacks();
            //发送车型请求
            socket.Send(param);
        }


        /// <summary>
        /// 分析接收到的站名信息
        /// </summary>
        /// <param name="rece"></param>
        public static void AnalyzeStationName(string rece)
        {
            //除去首尾字符
            rece = rece.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = rece.Split(new char[] { '&' });
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //MessageBox.Show("回复报单头接收的内容--------->" + item);
            //获取车站号
            string czCode = headItems[1];
            //交路号
            string jlCode = headItems[2];
            //添加车型到车型库
            if (body != "%")
            {
                RParams pram = new RParams();
                pram.Add("iJLCode", jlCode);
                pram.Add("iCZCode", czCode);
                pram.Add("sCZName", body);
                DBAction.Insert("StationDiction", pram);
            }
        }

    }
}
