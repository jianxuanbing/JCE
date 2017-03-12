/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Configs
 * 文件名：ILoggingConfigReseter
 * 版本号：v1.0.0.0
 * 唯一标识：96fedda7-e7b7-4871-966f-105d0c84c1c1
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 16:11:11
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 16:11:11
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

namespace JCE.Core.Configs
{
    /// <summary>
    /// 定义日志配置信息重置功能
    /// </summary>
    public interface ILoggingConfigReseter
    {
        /// <summary>
        /// 日志配置信息重置
        /// </summary>
        /// <param name="config">待重置的日志配置信息</param>
        /// <returns>重置后的日志配置信息</returns>
        LoggingConfig Reset(LoggingConfig config);
    }
}
