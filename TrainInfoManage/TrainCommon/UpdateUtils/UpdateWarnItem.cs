using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace TrainCommon
{
    /// <summary>
    /// 更新项点配置
    /// </summary>
    public class UpdateWarnItem
    {
        /// <summary>
        /// 请求机车项点关闭状态
        /// </summary>
        /// <param name="sck"></param>
        public static void SendWarnItems(SckTrains sck) 
        {
            SckParams param = new SckParams();
            param.Tml = "Cmd,Type,Content";
            param.Add("Cmd", "0900", false);
            param.Add("Type", "00", false);
            param.Add("Content", "00", true);
            List<string> packs = param.CreatePacks();
            sck.Send(param);
        }

        /// <summary>
        /// 分析服务器返回的项点状态
        /// </summary>
        /// <param name="receInfo"></param>
        public static void AnalyzeItemsInfo(string receInfo)
        {
            //除去首尾字符
            receInfo = receInfo.Replace("#{", "").Replace("}#", "");
            //分解包内容
            string[] receData = receInfo.Split(new char[] { '&' });
            //MessageBox.Show("项点回复--->" + receInfo);
            //获取头部内容
            string head = receData[0];
            //拆分头部内容
            string[] headItems = head.Split(new char[] { '$' });
            //获取内容信息
            string body = receData[1];
            //分解内容部分
            string[] items = body.Split(new char[]{','});
            RParams param = new RParams();
            for (int i = 0; i < items.Length; i++)
            {
                param.Items.Clear();
                param.Add("IsOpen", items[i]);
                int itemId=i+1;
                DBAction.Update("AlarmCfg", "IsOpen", "ID=" + itemId, param);
            }

        }

    }
}
