/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：ContextCollection
 * 版本号：v1.0.0.0
 * 唯一标识：6aba70b3-ec3d-4299-b457-b59e0e039507
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:45:39
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:45:39
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Properties;
using JCE.Utils.Extensions;

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据上下文配置节点集合
    /// </summary>
    internal class ContextCollection: ConfigurationElementCollection
    {
        /// <summary>
        /// 上下文键
        /// </summary>
        private const string ContextKey = "context";

        /// <summary>
        /// 获取在派生的类中重写时用于标识配置文件中此元素集合的名称。
        /// </summary>
        /// <returns>
        /// 集合的名称；否则为空字符串。 默认值为空字符串。
        /// </returns>
        protected override string ElementName
        {
            get { return ContextKey; }
        }

        /// <summary>
        /// 获取 <see cref="T:System.Configuration.ConfigurationElementCollection"/> 的类型。
        /// </summary>
        /// <returns>
        /// 此集合的 <see cref="T:System.Configuration.ConfigurationElementCollectionType"/>。
        /// </returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// 当在派生的类中重写时，创建一个新的 <see cref="T:System.Configuration.ConfigurationElement"/>。
        /// </summary>
        /// <returns>
        /// 一个新的 <see cref="T:System.Configuration.ConfigurationElement"/>。
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ContextElement();
        }

        /// <summary>
        /// 在派生类中重写时获取指定配置元素的元素键。
        /// </summary>
        /// <returns>
        /// 一个 <see cref="T:System.Object"/>，用作指定 <see cref="T:System.Configuration.ConfigurationElement"/> 的键。
        /// </returns>
        /// <param name="element">要为其返回键的 <see cref="T:System.Configuration.ConfigurationElement"/>。</param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ContextElement)element).ContextTypeName;
        }

        /// <summary>
        /// 在派生类中添加指定元素的元素键
        /// </summary>
        /// <param name="element">要添加的键 <see cref="T:System.Configuration.ConfigurationElement"/>。</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            var key = GetElementKey(element);
            if (BaseGet(key) != null)
            {
                throw new InvalidOperationException(Resources.ConfigFile_ItemKeyDefineRepeated.FormatWith(key));
            }
            base.BaseAdd(element);
        }

        /// <summary>
        /// 在派生类中添加指定元素的元素键
        /// </summary>
        /// <param name="index"> 要添加指定 <see cref="System.Configuration.ConfigurationElement"/> 的索引位置。</param>
        /// <param name="element"> 要相加的 <see cref="System.Configuration.ConfigurationElement"/>。</param>
        protected override void BaseAdd(int index, ConfigurationElement element)
        {
            var key = GetElementKey(element);
            if (BaseGet(key) != null)
            {
                throw new InvalidOperationException(Resources.ConfigFile_ItemKeyDefineRepeated.FormatWith(key));
            }

            base.BaseAdd(index, element);
        }
    }
}
