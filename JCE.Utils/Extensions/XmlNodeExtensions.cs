/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：XmlNodeExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：5ca507e5-4f7d-417e-a74e-810abc0d6be6
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:56:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:56:35
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Xml节点（XmlNode）/Xml文档（XmlDocument）扩展
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// 创建Xml子节点，并追加到父节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="name">子节点的名称</param>
        /// <param name="namespaceUri">节点的命名空间</param>
        /// <returns>新创建的Xml节点</returns>
        public static XmlNode CreateChildNode(this XmlNode parentNode, string name, string namespaceUri = "")
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            XmlNode node = null;
            node = namespaceUri != "" ? document.CreateElement(name, namespaceUri) : document.CreateElement(name);
            parentNode.AppendChild(node);
            return node;
        }
        /// <summary>
        /// 创建CData节，并追加到父节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="data">CData节</param>
        /// <returns>新创建的CData节</returns>
        public static XmlCDataSection CreateCDataSection(this XmlNode parentNode, string data = "")
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            var node = document.CreateCDataSection(data);
            parentNode.AppendChild(node);
            return node;
        }
        /// <summary>
        /// 返回CData节的内容
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>CData节内容</returns>
        public static string GetCDataSection(this XmlNode parentNode)
        {
            return parentNode.ChildNodes.OfType<XmlCDataSection>().Select(node => (node).Value).FirstOrDefault();
        }

        #region GetAttribute(获取Xml节点属性值)
        /// <summary>
        /// 获取Xml节点属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="defaultValue">默认值，如果没有匹配属性存在</param>
        /// <returns>属性值</returns>
        public static string GetAttribute(this XmlNode node, string attributeName, string defaultValue = null)
        {
            var attribute = node.Attributes[attributeName];
            return (attribute != null ? attribute.InnerText : defaultValue);
        }
        /// <summary>
        /// 获取Xml节点属性值，并将属性值转换为指定数据类型的属性值
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="node">节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="defaultValue">默认值，如果没有匹配属性存在</param>
        /// <returns>属性值</returns>
        public static T GetAttribute<T>(this XmlNode node, string attributeName, T defaultValue = default(T))
        {
            var value = node.GetAttribute(attributeName);
            if (!value.IsNullOrEmpty())
            {
                if (typeof(T) == typeof(Type))
                {
                    return (T)(object)Type.GetType(value, true);
                }
                return value.ConvertTo(defaultValue);
            }
            return defaultValue;
        }
        #endregion

        #region SetAttribute(设置Xml节点属性值)
        /// <summary>
        /// 设置Xml节点属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetAttribute(this XmlNode node, string name, object value)
        {
            node.SetAttribute(name, value != null ? value.ToString() : null);
        }
        /// <summary>
        /// 设置Xml节点属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetAttribute(this XmlNode node, string name, string value)
        {
            if (node != null)
            {
                var attribute = node.Attributes[name, node.NamespaceURI];
                if (attribute == null)
                {
                    attribute = node.OwnerDocument.CreateAttribute(name, node.OwnerDocument.NamespaceURI);
                    node.Attributes.Append(attribute);
                }
                attribute.InnerText = value;
            }
        }
        #endregion

    }
}
