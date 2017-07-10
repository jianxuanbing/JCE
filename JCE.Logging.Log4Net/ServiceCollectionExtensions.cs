/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Logging.Log4Net
 * 文件名：ServiceCollectionExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：f5007ef9-a495-4ef7-ac82-f6bd2e05e65a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/26 11:54:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/26 11:54:17
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
using JCE.Core.Dependency;
using JCE.Core.Initialize;

namespace JCE.Logging.Log4Net
{
    /// <summary>
    /// 服务映射信息集合扩展操作
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Log4Net日志功能相关映射信息
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        public static void AddLog4NetServices(this IServiceCollection services)
        {
            //if (JceConfig.LoggingConfigReseter == null)
            //{
            //    JceConfig.LoggingConfigReseter=new Log4NetLoggingConfigReseter();
            //}
            //services.AddSingleton<IBasicLoggingInitializer, Log4NetLoggingInitializer>();
            //services.AddSingleton<Log4NetLoggerAdapter>();
        }
    }
}
