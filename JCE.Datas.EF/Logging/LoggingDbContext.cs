/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF.Logging
 * 文件名：LoggingDbContext
 * 版本号：v1.0.0.0
 * 唯一标识：6f34d072-66fb-41dc-8ddf-bc7c0ca7944e
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:57:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:57:58
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

namespace JCE.Datas.EF.Logging
{
    /// <summary>
    /// 日志数据上下文
    /// </summary>
    public class LoggingDbContext:DbContextBase<LoggingDbContext>
    {
        /// <summary>
        /// 获取 是否允许数据库日志记录
        /// </summary>
        protected override bool DataLoggingEnabled { get { return false; } }
    }
}
