/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：BasicLoggingConfig
 * 版本号：v1.0.0.0
 * 唯一标识：3afcd81c-5e3a-4948-b719-982879fcce94
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:25:52
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:25:52
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
using JCE.Core.Configs.ConfigFile;

namespace JCE.Core.Configs
{
    /// <summary>
    /// 基础日志配置信息
    /// </summary>
    public class BasicLoggingConfig
    {
        /// <summary>
        /// 获取或设置 日志适配器配置信息集合
        /// </summary>
        public ICollection<LoggingAdapterConfig> AdapterConfigs { get; set; }

        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的实例
        /// </summary>
        public BasicLoggingConfig()
        {
            AdapterConfigs=new List<LoggingAdapterConfig>();
        }

        /// <summary>
        /// 初始化一个<see cref="BasicLoggingConfig"/>类型的实例
        /// </summary>
        /// <param name="element">基础日志配置节点</param>
        internal BasicLoggingConfig(BasicLoggingElement element)
        {
            AdapterConfigs =
                element.Adapters.OfType<LoggingAdapterElement>()
                    .Select(adapter => new LoggingAdapterConfig(adapter))
                    .ToList();
        }
    }
}
