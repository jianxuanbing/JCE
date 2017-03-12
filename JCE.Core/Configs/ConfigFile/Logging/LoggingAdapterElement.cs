/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：LoggingAdapterElement
 * 版本号：v1.0.0.0
 * 唯一标识：3faa0a4f-771b-4721-abc0-1e1a38502a7b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:06:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:06:07
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
    /// 日志输出适配器配置节点
    /// </summary>
    internal class LoggingAdapterElement: ConfigurationElement
    {
        #region Field(字段)
        /// <summary>
        /// 适配器名称节点
        /// </summary>
        private const string NameKey = "name";
        /// <summary>
        /// 是否启用节点
        /// </summary>
        private const string EnabledKey = "enabled";
        /// <summary>
        /// 适配器类型名称节点
        /// </summary>
        private const string TypeKey = "type";
        #endregion

        #region Property(属性)
        /// <summary>
        /// 获取或设置 适配器名称
        /// </summary>
        [ConfigurationProperty(NameKey,IsRequired = true,IsKey = true)]
        public virtual string Name
        {
            get { return (string) this[NameKey]; }
            set { this[NameKey] = value; }
        }
        /// <summary>
        /// 获取或设置 是否启用
        /// </summary>
        [ConfigurationProperty(EnabledKey, DefaultValue = true)]
        public virtual bool Enabled
        {
            get { return (bool)this[EnabledKey]; }
            set { this[EnabledKey] = value; }
        }
        /// <summary>
        /// 获取或设置 适配器类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string AdapterTypeName
        {
            get { return (string)this[TypeKey]; }
            set { this[TypeKey] = value; }
        }
        #endregion
    }
}
