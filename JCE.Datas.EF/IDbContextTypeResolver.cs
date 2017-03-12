/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：IDbContextTypeResolver
 * 版本号：v1.0.0.0
 * 唯一标识：dc41558f-fb6d-4a8d-8724-acbef9b09d45
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:31:02
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:31:02
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
using JCE.Core.Dependency;
using JCE.Core.Domains.Entities;
using JCE.Core.Domains.Uow;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 定义数据上下文实例创建器
    /// </summary>
    public interface IDbContextTypeResolver:ISingletonDependency
    {
        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <returns></returns>
        IUnitOfWork Resolve<TEntity, TKey>() where TEntity : IEntity<TKey>;

        /// <summary>
        /// 由实体类型获取关联的上下文类型
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        IUnitOfWork Resolve(Type entityType);
    }
}
