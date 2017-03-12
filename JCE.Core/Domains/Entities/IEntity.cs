/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Entities
 * 文件名：IEntity
 * 版本号：v1.0.0.0
 * 唯一标识：dbe8da99-ed19-464d-b050-68f75e701358
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:11:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:11:07
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
    /// 实体
    /// </summary>
    public interface IEntity : IDomainObject
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
    }

    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IEntity<out TKey> : IEntity
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        TKey Id { get; }
    }

    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IEntity<in TEntity, out TKey> : ICompareChange<TEntity>, IEntity<TKey> where TEntity : IEntity
    {

    }
}
