/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：XmlExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：0e6b6251-1d00-428e-bc04-7406fe08fbc5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:56:18
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:56:18
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
using System.Xml.Linq;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Xml扩展
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// 将XmlNodex转换为XElement
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <returns>XElment对象 </returns>
        public static XElement ToXElement(this XmlNode node)
        {
            XDocument xdoc = new XDocument();
            using (XmlWriter xmlWriter = xdoc.CreateWriter())
            {
                node.WriteTo(xmlWriter);
            }
            return xdoc.Root;
        }
        /// <summary>
        /// 将XElement转换为XmlNode
        /// </summary>
        /// <param name="element">XElement</param>
        /// <returns>XmlNode对象</returns>
        public static XmlNode ToXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlReader);
                return xml;
            }
        }
    }
}
