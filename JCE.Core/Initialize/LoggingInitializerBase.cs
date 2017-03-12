/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Initialize
 * 文件名：LoggingInitializerBase
 * 版本号：v1.0.0.0
 * 唯一标识：852dd3b9-7b8f-4fae-8c24-d6b3c31edd42
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:00:26
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:00:26
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
using JCE.Core.Configs;
using JCE.Utils.Extensions;
using JCE.Utils.Logging;

namespace JCE.Core.Initialize
{
    /// <summary>
    /// 日志初始化器基类
    /// </summary>
    public abstract class LoggingInitializerBase
    {
        /// <summary>
        /// 获取或设置 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 从日志适配器配置节点初始化日志适配器
        /// </summary>
        /// <param name="config">日志适配器配置节点</param>
        protected virtual void SetLoggingFromAdapterConfig(LoggingAdapterConfig config)
        {
            config.CheckNotNull("config");
            if (!config.Enabled)
            {
                return;
            }
            ILoggerAdapter adapter=ServiceProvider.GetService(config.AdapterType) as ILoggerAdapter;
            //Activator.CreateInstance(config.AdapterType) as ILoggerAdapter;

            if (adapter == null)
            {
                return;
            }
            LogManager.AddLoggerAdapter(adapter);
        }
    }
}
