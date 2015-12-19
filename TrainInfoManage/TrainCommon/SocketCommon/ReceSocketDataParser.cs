using System;

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TrainCommon
{
    public class ReceSocketDataParser
    {
        /// <summary>
        /// 分析通知公告信息
        /// </summary>
        /// <param name="info">接收到的信息</param>
        public static void AnalyzeAnnInfo(string info) 
        {
            try
            {
                //MessageBox.Show("公告信息");
                //除去首尾字符
                info = info.Replace("#{", "").Replace("}#", "");
                //MessageBox.Show(info);
                //分解包内容
                string[] receData = info.Split(new char[] { '&' });
                //获取头部信息
                string head = receData[0];
                //MessageBox.Show("-1");
                //获取内容部分信息
                string body = receData[1];
                //MessageBox.Show("-2");
                //获取校验码信息
                string check = receData[2];
                //MessageBox.Show("-3");
                //获取接收到的数据验证码
                //string receCode = check.Substring(0, 4);
                //根据内容获取验证码
                //UInt16 crcCode = SckCRC16.Parse(body);
                //string code = crcCode.ToString("X");
                //验证数据是否正常
                //if (code != receCode)
                //{
                //    return;
                //}
                //分解头部内容
                //MessageBox.Show("1");
                string[] headItems = head.Split(new char[] { '$' });
                //通知标题
                string title = headItems[1];
                //MessageBox.Show("2");
                //发送人
                string senPerson = headItems[2];
                //MessageBox.Show("3");
                //发送时间
                string sendTime = headItems[3];
                //MessageBox.Show("4");
                //新的通知公告条数
                LocoInfo.TrainInfo.AnnCount = LocoInfo.TrainInfo.AnnCount+1;
                //MessageBox.Show(info);
                //MessageBox.Show("5");
                RParams param = new RParams();
                param.Add("Title", title);
                param.Add("AnnoContent", body);
                param.Add("SendPerson", senPerson);
                param.Add("ReceTime", BaseLibrary.ConversionTime(sendTime,0));
                DBAction.Insert("Announcement", param);
                //MessageBox.Show("6");
                BaseVoice.TrainVoice.SpeekVioce("您好！您有一条新消息！");
                //MessageBox.Show("公告条数"+LocoInfo.TrainInfo.AnnCount);

            }
            catch (Exception ex)
            {
                LogDaily.logerr(ex.ToString());
            }
           
        }
    }
}
