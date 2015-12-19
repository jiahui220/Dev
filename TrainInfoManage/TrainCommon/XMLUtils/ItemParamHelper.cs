using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrainCommon
{
    class ItemParamHelper
    {
        //文件路径
        private static string path = FormCommon.basePath + "\\arm_ssfx.cfg";
        private static FileStream fs = null;
        private static StreamReader sr = null;

        /// <summary>
        /// 设置项点的值
        /// </summary>
        /// <param name="id">项点编号</param>
        /// <param name="index">修改的索引:-1为open</param>
        /// <param name="value">对应的值</param>
        /// <returns></returns>
        public static bool ModifyItemParam(int id, int index, int value)
        {
            if (!FileHelper.IsFileExist(path))
            {
                return false;
            }
            string res = String.Empty;
            try
            {
                try
                {
                    fs = new FileStream(path, FileMode.Open);
                }
                catch (Exception ex)
                {
                    return false;
                }
                sr = new StreamReader(fs, Encoding.Unicode);
                while (sr.Peek() > -1)
                {

                    string s = sr.ReadLine();
                    if (s.StartsWith("xd" + id + "_"))
                    {
                        if (index == -1)
                        {
                            if (s.StartsWith("xd" + id + "_open="))
                            {
                                res += "xd" + id + "_open=" + value + "\r\n";
                            }
                            else
                            {
                                res += s.Trim() + "\r\n";
                            }
                        }
                        else
                        {
                            if (s.StartsWith("xd" + id + "_" + index + "="))
                            {
                                res += "xd" + id + "_" + index + "=" + value + "\r\n";
                            }
                            else
                            {
                                res += s.Trim() + "\r\n";
                            }
                        }
                    }
                    else
                    {
                        res += s.Trim() + "\r\n";
                    }
                }
                fs.Close();
                sr.Close();
                FileHelper.Write2File(path, res.Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// 获取项点配置里的值
        /// </summary>
        /// <param name="id">项点编号</param>
        /// <param name="index">值索引:-1为open</param>
        /// <returns></returns>
        public static string GetItemValue(int id, int index)
        {
            if (!FileHelper.IsFileExist(path))
            {
                return null;
            }
            string res = String.Empty;
            try
            {
                try
                {
                    fs = new FileStream(path, FileMode.Open);
                }
                catch (Exception ex)
                {
                    return null;
                }
                sr = new StreamReader(fs, Encoding.Unicode);
                while (sr.Peek() > -1)
                {

                    string s = sr.ReadLine();
                    if (s.StartsWith("xd" + id + "_"))
                    {
                        if (index == -1)
                        {
                            if (s.StartsWith("xd" + id + "_open="))
                            {
                                res = s.Trim().Substring(s.Trim().IndexOf("=") + 1, s.Trim().Length - s.Trim().IndexOf("=") - 1);
                            }
                        }
                        else
                        {
                            if (s.StartsWith("xd" + id + "_" + index + "="))
                            {
                                res = s.Trim().Substring(s.Trim().IndexOf("=") + 1, s.Trim().Length - s.Trim().IndexOf("=") - 1);
                            }
                        }
                    }
                }
                fs.Close();
                sr.Close();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }
    }
}
