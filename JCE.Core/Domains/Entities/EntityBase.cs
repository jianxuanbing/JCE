/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains.Entities
 * 文件名：EntityBase
 * 版本号：v1.0.0.0
 * 唯一标识：adf45fff-014f-41fd-bea3-7c81605108c2
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:12:49
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:12:49
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils;

namespace JCE.Core.Domains.Entities
{
    /// <summary>
    /// 领域实体，可持久到数据库的领域模型的基类
    /// </summary>    
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    [Serializable]
    public abstract class EntityBase<TEntity, TKey> : DomainBase<TEntity>, IEntity<TEntity, TKey> where TEntity : IEntity
    {
        /// <summary>
        /// 初始化一个<see cref="EntityBase{TEntity,TKey}"/>类型的实例
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        [Key]
        public TKey Id { get; private set; }

        #region Equals(相等运算)
        /// <summary>
        /// 相等运算
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is EntityBase<TEntity, TKey>))
            {
                return false;
            }
            return this == (EntityBase<TEntity, TKey>)obj;
        }

        #endregion

        #region GetHashCode(获取哈希)
        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ReferenceEquals(Id, null) ? 0 : Id.GetHashCode();
        }

        #endregion

        #region ==(相等比较)
        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="entity1">领域实体1</param>
        /// <param name="entity2">领域实体2</param>
        /// <returns></returns>
        public static bool operator ==(EntityBase<TEntity, TKey> entity1, EntityBase<TEntity, TKey> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }
            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }
            if (Equals(entity1.Id, null))
            {
                return false;
            }
            if (entity1.Id.Equals(default(TKey)))
            {
                return false;
            }
            return entity1.Id.Equals(entity2.Id);
        }
        #endregion

        #region !=(不相等比较)
        /// <summary>
        /// 不相等比较
        /// </summary>
        /// <param name="entity1">领域实体1</param>
        /// <param name="entity2">领域实体2</param>
        /// <returns></returns>
        public static bool operator !=(EntityBase<TEntity, TKey> entity1, EntityBase<TEntity, TKey> entity2)
        {
            return !(entity1 == entity2);
        }
        #endregion

        #region Init(初始化)
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            InitId();
        }

        /// <summary>
        /// 初始化ID
        /// </summary>
        private void InitId()
        {
            if (Id.Equals(default(TKey)))
            {
                Id = CreateId();
            }
        }

        /// <summary>
        /// 创建标识
        /// </summary>
        /// <returns></returns>
        protected virtual TKey CreateId()
        {
            return Conv.To<TKey>(Guid.NewGuid());
        }
        #endregion

    }
}
