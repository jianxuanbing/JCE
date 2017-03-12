/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs.ConfigFile
 * 文件名：BasicLoggingElement
 * 版本号：v1.0.0.0
 * 唯一标识：adb811a2-e21f-4d50-9708-7421d03b7295
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:26:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:26:36
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
    /// 基础日志配置节点
    /// </summary>
    internal class BasicLoggingElement:ConfigurationElement
    {
        private const string AdapterKey = "adapters";

        /// <summary>
        /// 获取或设置 日志适配器配置节点集合
        /// </summary>
        [ConfigurationProperty(AdapterKey)]
        public virtual LoggingAdapterCollection Adapters
        {
            get { return (LoggingAdapterCollection) base[AdapterKey]; }
        }

        
    }
}
