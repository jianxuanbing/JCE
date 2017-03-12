/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Entities
 * 文件名：IAggregateRoot
 * 版本号：v1.0.0.0
 * 唯一标识：61668268-0be7-4f66-a73b-0b1a5e56fa98
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:10:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:10:38
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

namespace JCE.Core.Domains.Entities
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// 版本号（乐观锁）
        /// </summary>
        byte[] Version { get; set; }
    }

    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot { }

    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IAggregateRoot<in TEntity, out TKey> : IEntity<TEntity, TKey>, IAggregateRoot<TKey> where TEntity : IAggregateRoot { }
}
