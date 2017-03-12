/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：JceFrameworkSection
 * 版本号：v1.0.0.0
 * 唯一标识：26c4d40b-1dc8-46db-b1f7-7a8110f35337
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 16:22:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 16:22:08
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

namespace JCE.Core.Configs.ConfigFile
{
    /// <summary>
    /// Jce框架节点
    /// </summary>
    internal class JceFrameworkSection:ConfigurationSection
    {
        private const string XmlnsKey = "xmlns";
        private const string DataKey = "data";
        private const string LoggingKey = "logging";
        private const string CacheKey = "cache";

        /// <summary>
        /// 获取或设置 日志配置
        /// </summary>
        [ConfigurationProperty(LoggingKey)]
        public virtual LoggingElement Logging
        {
            get { return (LoggingElement) this[LoggingKey]; }
            set { this[LoggingKey] = value; }
        }

        [ConfigurationProperty(XmlnsKey, IsRequired = false)]
        private string Xmlns
        {
            get { return (string)this[XmlnsKey]; }
            set { this[XmlnsKey] = value; }
        }

        [ConfigurationProperty(DataKey)]
        public virtual DataElement Data
        {
            get { return (DataElement)this[DataKey]; }
            set { this[DataKey] = value; }
        }
    }
}
