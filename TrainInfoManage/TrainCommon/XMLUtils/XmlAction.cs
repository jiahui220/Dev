using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TrainCommon
{
    /**
     * author:luojia
     */
    public class XmlAction
    {
        #region 公共方法
        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public XmlNode GetRoot()
        {
            return XmlHelper.GetRoot();
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="xPath">节点路径</param>
        /// <param name="attr">节点属性</param>
        /// <param name="value">节点属性值</param>
        /// <returns></returns>
        public XmlNode GetNode(string xPath,string attr, string value)
        {
            return XmlHelper.GetNode(xPath,attr,value);
        }

        public XmlNodeList GetNodeList(string xPath)
        {
            return XmlHelper.GetNodeList(xPath);
        }

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="attr">属性</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public XmlNode CreateNode(string name, string[] attr, string[] value)
        {
            return XmlHelper.CreateElement(name,attr,value);
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="child">要插入的节点</param>
        public void InsertNode(XmlNode parent,XmlNode child)
        {
            XmlHelper.InsertChild(parent,child);
        }

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns></returns>
        public XmlNodeList GetChildNodes(XmlNode node)
        {
            return XmlHelper.GetChildNodes(node);
        }

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attr">属性</param>
        /// <param name="value">属性值</param>
        public void SetAttr(XmlNode node,string[] attr, string[] value)
        {
            XmlHelper.SetNodeAttr(node,attr,value);
        }

        /// <summary>
        /// 获取节点属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public string GetValue(XmlNode node, string attr)
        {
            return XmlHelper.GetNodeValue(node, attr);
        }

        /// <summary>
        /// 设置节点文本
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        public void SetText(XmlNode node ,string text)
        {
            XmlHelper.SetNodeText(node,text);
        }

        /// <summary>
        /// 获取节点文本
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetText(XmlNode node)
        {
            return XmlHelper.GetNodeText(node);
        }

        /// <summary>
        /// 删除节点下指定的子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public void DeleteChild(XmlNode parent, XmlNode child)
        {
            XmlHelper.DeleteChildNode(parent,child);
        }

        /// <summary>
        /// 删除节点的所有子节点
        /// </summary>
        /// <param name="node"></param>
        public void DeleteAllChild(XmlNode node)
        {
            XmlHelper.DeleteAllChild(node);
        }

        /// <summary>
        /// 删除节点及其子节点
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(XmlNode node)
        {
            XmlHelper.DeleteNode(node);
        }
        #endregion

        /// <summary>
        /// 获取所有子节点相对应的属性值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attr"></param>
        /// <returns></returns>
        public List<string> GetChildrenValue(XmlNode parent,string attr)
        {
            if (parent == null)
            {
                return null;
            }
            List<string> list = new List<string>();
            XmlNodeList nodes = GetChildNodes(parent);
            foreach (XmlNode node in nodes)
            {
                list.Add(GetValue(node,attr));
            }
            return list;
        }

        /// <summary>
        /// 获取节点所有子节点的文本
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<string> GetChildrenText(XmlNode parent)
        {
            if (parent == null)
            {
                return null;
            }
            List<string> list = new List<string>();
            XmlNodeList nodes = GetChildNodes(parent);
            foreach (XmlNode node in nodes)
            {
                list.Add(GetText(node));
            }
            return list;
        }

    }
}
