using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TrainCommon
{
    public class FileHelper
    {
        //文件读取流
        private static StreamWriter sw = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileHelper()
        {
            
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetExt(string path)
        {
            int start = path.LastIndexOf(".");
            string postfix = path.Substring(start);
            return postfix;
        }

        /// <summary>
        /// 获取文件的扩展名
        /// </summary>
        /// <param name="fi">文件</param>
        /// <returns></returns>
        public static string GetExt(FileInfo fi)
        {
            return fi.Extension;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="fi">文件</param>
        /// <returns></returns>
        public static string GetFileName(FileInfo fi)
        {
            return fi.Name;
        }

        /// <summary>
        /// 根据路径获取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static FileInfo GetFile(string path)
        {
            try
            {
                return new FileInfo(path);
            }
            catch (Exception e)
            {
                LogDaily.logerr(e.ToString());
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool CreateFile(string path)
        {
            try
            {
                File.Create(path);
                return true;
            }
            catch (Exception e)
            {
                LogDaily.logerr(e.ToString());
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// 向文件写入内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">要写入的内容</param>
        public static void Write2File(string path, string content)
        {
            try
            {
                if (!IsFileExist(path))
                {
                    CreateFile(path);
                }
                sw = new StreamWriter(path);
                sw.WriteLine(content);
                sw.Close();
            }
            catch (Exception e)
            {
                LogDaily.logerr(e.ToString());
                throw new Exception(e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 向文件追加内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">要写入的内容</param>
        public static void Append2File(string path, string content)
        {
            try
            {
                sw = File.AppendText(path);
                sw.Write(content);
                sw.Close();
            }
            catch (Exception e)
            {
                LogDaily.logerr(e.ToString());
                throw new Exception(e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static char[] ReadFile(string path)
        {
            FileStream fs = null;
            char[] cpara = null;
            if (File.Exists(path))
            {
                //打开现有文件以进行读取。
                fs = File.OpenRead(path);
                int b1;
                System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
                while ((b1 = fs.ReadByte()) != -1)
                {
                    tempStream.WriteByte(((byte)b1));
                }
                byte[] fileContent = tempStream.ToArray();
                cpara = new char[fileContent.Length];
                for (int i = 0; i < fileContent.Length; i++)
                {
                    cpara[i] = System.Convert.ToChar(fileContent[i]);
                }
            }
            return cpara;
        }

        public static byte[] ReadFileBinary(string path)
        {
            byte[] buf = null;
            try
            {
                BinaryReader br = null;
                FileStream fs = null;

                if (File.Exists(path))
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    br = new BinaryReader(fs,Encoding.Unicode);
                    buf = br.ReadBytes((int)fs.Length);
                    br.Close();
                    fs.Close();
                }
            }
            catch (Exception e)
            {
            }
            return buf;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="start">起始索引</param>
        /// <param name="length">读取的长度</param>
        /// <returns></returns>
        //public static string ReadFile(string path, int start, int length)
        //{
        //    string s = null;
        //    if (IsFileExist(path))
        //    {
        //        s = ReadFile(path);
        //        if (s != null)
        //        {
        //            if (s.Length - start < length)
        //            {
        //                s = s.Substring(start, s.Length - start);
        //            }
        //            else
        //            {
        //                s = s.Substring(start, length);
        //            }
        //        }
        //    }
        //    return s;
        //}

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void DeleteFile(string path)
        {
            if(IsFileExist(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 拷贝单个文件到指定的目录
        /// </summary>
        /// <param name="from"></param>
        /// <param name="target"></param>
        public static void CopyFile(string filePath, string destDirectory)
        {
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            File.Copy(filePath,destDirectory + filePath.Substring(filePath.LastIndexOf("\\")),true);
            DeleteFile(filePath);
        }

        /// <summary>
        /// 拷贝目录及其子文件到指定的目录
        /// </summary>
        /// <param name="from"></param>
        /// <param name="target"></param>
        public static void CopyFiles(string fromDirectory, string destDirectory)
        {
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            //拷贝文件夹
            string[] subDirectories = Directory.GetDirectories(fromDirectory);
            if (subDirectories.Length > 0)
            {
                foreach (string subDir in subDirectories)
                {
                    //拷贝文件夹下的文件
                    CopyFiles(subDir,destDirectory + subDir.Substring(subDir.LastIndexOf("\\")));
                }
            }
            //拷贝文件
            string[] subFiles = Directory.GetFiles(fromDirectory);
            if (subFiles.Length > 0)
            {
                foreach (string subPath in subFiles)
                {
                    CopyFile(subPath,destDirectory);
                }
            }
            //删除原始目录及其所有子文件
            //Directory.Delete(fromDirectory,true);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            DirectoryInfo info = new DirectoryInfo(path);
            info.Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}