/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Initialize
 * 文件名：IBasicLoggingInitializer
 * 版本号：v1.0.0.0
 * 唯一标识：26205ae7-18df-4a93-95f9-ce626ae44ccb
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/12/23 15:23:04
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/12/23 15:23:04
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

namespace JCE.Core.Initialize
{
    /// <summary>
    /// 定义基础日志初始化器，用于初始化基础日志功能
    /// </summary>
    public interface IBasicLoggingInitializer
    {
        /// <summary>
        /// 开始初始化基础日志
        /// </summary>
        /// <param name="config">日志配置信息</param>
        void Initialize(LoggingConfig config);
    }
}
