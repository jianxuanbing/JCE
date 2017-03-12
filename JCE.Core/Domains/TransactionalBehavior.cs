/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：TransactionalBehavior
 * 版本号：v1.0.0.0
 * 唯一标识：f4584ff4-9c3c-45a2-9a2b-1a377735ec0b
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:20:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:20:47
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

namespace JCE.Core.Domains
{
    /// <summary>
    /// 在执行数据库命令或查询期间控制事务创建行为。
    /// </summary>
    public enum TransactionalBehavior
    {
        /// <summary>
        /// 如果存在现有事务，则使用它，否则在没有事务的情况下执行命令或查询。
        /// </summary>
        DoNotEnsureTransaction,

        /// <summary>
        /// 如果不存在任何事务，则使用新事务进行操作。
        /// </summary>
        EnsureTransaction
    }
}
