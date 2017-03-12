/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Repositories
 * 文件名：IRepository
 * 版本号：v1.0.0.0
 * 唯一标识：b13b8515-d10f-4d3c-b23d-c399bae9d606
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:08:55
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:08:55
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JCE.Core.Dependency;
using JCE.Core.Domains.Entities;
using JCE.Core.Domains.Uow;

namespace JCE.Core.Domains.Repositories
{
    /// <summary>
    /// 仓储，定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IRepository<TEntity, in TKey> : IScopeDependency where TEntity : class, IAggregateRoot
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TEntity entity);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="newEntity">新实体</param>
        /// <param name="oldEntity">数据库中旧的实体</param>
        void Update(TEntity newEntity, TEntity oldEntity);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Remove(TKey id);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove(TEntity entity);

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        void Remove(IEnumerable<TKey> ids);

        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Remove(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="predicate">条件</param>
        void Remove(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <returns></returns>
        List<TEntity> FindAll();

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取未跟踪的实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> FindAsNoTraking();

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Find();
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        TEntity Find(params object[] id);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">实体标识列表</param>
        /// <returns></returns>
        List<TEntity> Find(IEnumerable<TKey> ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 索引器查找，获取指定标识的实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        TEntity this[TKey id] { get; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query);

        /// <summary>
        /// 保存
        /// </summary>
        void Save();

        /// <summary>
        /// 获取实体总数
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 清空实体
        /// </summary>
        void Clear();

        /// <summary>
        /// 清空缓存
        /// </summary>
        void ClearCache();

        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork GetUnitOfWork();
    }

    /// <summary>
    /// 仓储，定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IAggregateRoot { }
}
