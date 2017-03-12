/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Repositories
 * 文件名：IQueryBase
 * 版本号：v1.0.0.0
 * 唯一标识：e140ec2a-d5ad-4d55-8569-bc5de6f7fce7
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:18:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:18:07
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Domains.Entities;

namespace JCE.Core.Domains.Repositories
{
    /// <summary>
    /// 查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IQueryBase<TEntity> : IPager where TEntity : class, IAggregateRoot
    {
        /// <summary>
        /// 获取谓词
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> GetPrediate();

        /// <summary>
        /// 获取排序
        /// </summary>
        /// <returns></returns>
        string GetOrderBy();
    }
}
