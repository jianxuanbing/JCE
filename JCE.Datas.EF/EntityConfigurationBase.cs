/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Datas.EF
 * 文件名：EntityConfigurationBase
 * 版本号：v1.0.0.0
 * 唯一标识：c91a6f43-5fe3-464d-8599-61e45a124cfe
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 1:03:00
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 1:03:00
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Domains.Entities;

namespace JCE.Datas.EF
{
    /// <summary>
    /// 数据实体映射配置基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityConfigurationBase<TEntity,TKey>:EntityTypeConfiguration<TEntity>,IEntityMapper where TEntity:class ,IEntity<TEntity,TKey>
    {
        /// <summary>
        /// 获取 相关上下文类型，如为null，将使用默认上下文<see cref="DefaultDbContext"/>，否则使用指定的上下文类型
        /// </summary>
        public Type DbContextType
        {
            get { return null; }
        }

        public void RegistTo(ConfigurationRegistrar configurations)
        {
            throw new NotImplementedException();
        }
    }
}
