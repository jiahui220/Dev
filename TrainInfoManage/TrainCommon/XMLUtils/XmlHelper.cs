using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;

namespace TrainCommon
{
    /**
     * author:luojia
     */
    /// <summary>
    /// xml文件操作类
    /// </summary>
    public class XmlHelper
    {
        public static string xmlFileName = "Config.xml";
        //系统配置文件存在的路径
        public static string xmlPath = TrainForm.basePath + "\\" + xmlFileName;
        public static XmlDocument xmlDoc = null;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public XmlHelper()
        { 
         
        }

        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <returns></returns>
        public static bool LoadXml()
        {
            try
            {
                if (xmlDoc == null)
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return true;
            }
            catch (Exception xe)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return true;
            }
        }

        /// <summary>
        /// 备份配置XML
        /// </summary>
        public static bool BackupXML()
        {
            try
            {
                //备份文件夹
                string backupFoler = TrainForm.basePath + "\\BackUP";
                if (!Directory.Exists(backupFoler))
                {
                    Directory.CreateDirectory(backupFoler);
                }
                File.Copy(xmlPath, backupFoler + "\\Config.xml", true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool InitXML() 
        {
            try
            {
                //备份XML文件
                string backupFile = TrainForm.basePath + "\\BackUP\\Config.xml";
                if (File.Exists(backupFile))
                {
                    File.Copy(backupFile, xmlPath, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }


        /// <summary>
        /// 保存到XML文件
        /// </summary>
        /// <returns></returns>
        public static bool SaveXml()
        {
            try
            {
                if (xmlDoc != null)
                {
                    xmlDoc.Save(xmlPath);
                }
                return true;
            }
            catch (XmlException xe)
            {
                LogDaily.logerr(xe.ToString());
                return false;
            }
        }

        /// <summary>
        /// 创建元素
        /// </summary>
        /// <param name="name">元素名</param>
        /// <param name="attr">元素属性</param>
        /// <param name="value">元素属性值</param>
        /// <returns></returns>
        public static XmlElement CreateElement(string name, string[] attr, string[] value)
        {
            try
            {
                if (attr != null && value != null && attr.Length != value.Length)
                {
                    return null;
                }
                if (LoadXml())
                {
                    XmlElement element = xmlDoc.CreateElement(name);
                    if (attr != null && value != null)
                    {
                        for (int i = 0; i < attr.Length; i++)
                        {
                            element.SetAttribute(attr[i], value[i]);
                        }
                    }
                    return element;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

            
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public static XmlNode GetRoot()
        {
            try
            {
                if (LoadXml())
                {
                    return xmlDoc.DocumentElement;
                }
                return null;
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return xmlDoc.DocumentElement;
            }


        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="xPath">节点路径：如/root/booktype</param>
        /// <param name="attr">节点属性,可以为null</param>
        /// <param name="value">节点属性值,可以为null</param>
        /// <returns></returns>
        public static XmlNode GetNode(string xPath, string attr, string value)
        {
            try
            {
                if (LoadXml())
                {
                    if (String.IsNullOrEmpty(attr) || String.IsNullOrEmpty(value))
                    {
                        return xmlDoc.SelectSingleNode(xPath);
                    }
                    else
                    {
                        XmlNodeList list = xmlDoc.SelectNodes(xPath);
                        foreach (XmlNode node in list)
                        {
                            XmlElement element = (XmlElement)node;
                            if (element.GetAttribute(attr).Equals(value))
                            {
                                return element;
                            }
                        }
                        if (InitXML())
                        {
                            xmlDoc = new XmlDocument();
                            xmlDoc.Load(xmlPath);
                        }
                        return xmlDoc.SelectSingleNode(xPath);
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="xPath">节点路径</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(string xPath)
        {
            try
            {
                if (LoadXml())
                {
                    return xmlDoc.SelectNodes(xPath);
                }
                return null;
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return xmlDoc.SelectNodes(xPath);
            }

        }

        /// <summary>
        /// 获取节点的所有子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodes(XmlNode node)
        {
            try
            {
                return node.ChildNodes;
            }
            catch (Exception ex)
            {
                //LogDaily.logerr(ex.ToString());
                //throw new Exception(ex.Message);
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return node.ChildNodes;
            }
        }

        /// <summary>
        /// 向父节点插入子节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="child">子节点</param>
        public static void InsertChild(XmlNode parent, XmlNode child)
        {
            try
            {
                if (LoadXml())
                {
                    parent.AppendChild(child);
                }
                SaveXml();
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                parent.AppendChild(child);
                SaveXml();
            }

        }

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attr">属性</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public static bool SetNodeAttr(XmlNode node, string[] attr, string[] value)
        {
            try
            {
                if (attr != null && value != null && attr.Length != value.Length)
                {
                    return false;
                }
                if (attr == null || value == null)
                {
                    return false;
                }
                if (LoadXml())
                {
                    for (int i = 0; i < attr.Length; i++)
                    {
                        ((XmlElement)node).SetAttribute(attr[i], value[i]);
                    }
                }
                SaveXml();
                return true;
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return false;
            }

        }

        /// <summary>
        /// 获取节点属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attr">属性</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlNode node, string attr)
        {
            try
            {
                if (LoadXml())
                {
                    return ((XmlElement)node).GetAttribute(attr);
                }
                return null;
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return ((XmlElement)node).GetAttribute(attr);
            }

        }

        /// <summary>
        /// 设置节点的文本值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="text">文本</param>
        public static void SetNodeText(XmlNode node, string text)
        {
            if (LoadXml())
            {
                ((XmlElement)node).InnerText = text;
            }
            SaveXml();
        }

        /// <summary>
        /// 获取节点的文本值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetNodeText(XmlNode node)
        {
            string str = "";
            try
            {
                str = ((XmlElement)node).InnerText;
                if (str == "" || str==null)
                {
                    if (InitXML())
                    {
                        xmlDoc = new XmlDocument();
                        xmlDoc.Load(xmlPath);
                    }
                    str = ((XmlElement)node).InnerText;
                }
                return str;
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
                return ((XmlElement)node).InnerText;
            }

        }

        /// <summary>
        /// 移除节点的所有子节点
        /// </summary>
        /// <param name="parent">节点</param>
        public static void DeleteAllChild(XmlNode parent)
        {
            try
            {
                if (LoadXml())
                {
                    parent.RemoveAll();
                }
                SaveXml();
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
            }

        }

        /// <summary>
        /// 移除节点下指定的子节点
        /// </summary>
        /// <param name="parent">节点</param>
        /// <param name="child">子节点</param>
        public static void DeleteChildNode(XmlNode parent, XmlNode child)
        {
            try
            {
                if (LoadXml())
                {
                    parent.RemoveChild(child);
                }
                SaveXml();
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
            }

        }

        /// <summary>
        /// 移除当前节点
        /// </summary>
        /// <param name="node">当前节点</param>
        public static void DeleteNode(XmlNode node)
        {
            try
            {
                DeleteAllChild(node);
                XmlNode parent = node.ParentNode;
                DeleteChildNode(parent, node);
            }
            catch (Exception)
            {
                if (InitXML())
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);
                }
            }

        }

    }
}
