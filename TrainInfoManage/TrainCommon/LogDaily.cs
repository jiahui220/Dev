using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace TrainCommon
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public class LogDaily
    {
        /// <summary>
        /// 记录报错日志
        /// </summary>
        /// <param name="err">错误信息</param>
        public static void logerr(string err)
        {
            try
            {
                string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                string filePath = basePath + "\\log.txt";//这里是你的已知文件

                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("时间：" + String.Format("{0:yyyy年MM月dd日 HH:mm:ss}", DateTime.Now) + " 错误内容:" + err + "\r\n");//写你的字符串。
                sw.Close();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
