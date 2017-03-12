/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Domains
 * 文件名：DomainBase
 * 版本号：v1.0.0.0
 * 唯一标识：fd03c892-62a5-4c35-9d52-e37dbe346123
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/13 0:15:19
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/13 0:15:19
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
using JCE.Utils;
using JCE.Utils.Extensions;

namespace JCE.Core.Domains
{
    /// <summary>
    /// 领域层顶级基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DomainBase<T> : StateDescription, INullObject, IDomainObject, ICompareChange<T> where T : IDomainObject
    {
        /// <summary>
        /// 变更值集合
        /// </summary>
        private ChangeValueCollection _changeValues;

        /// <summary>
        /// 初始化一个<see cref="DomainBase{T}"/>类型的实例
        /// </summary>
        protected DomainBase()
        {
        }

        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <returns></returns>
        public virtual bool IsNull()
        {
            return false;
        }

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取变更属性
        /// </summary>
        /// <param name="newEntity">新对象</param>
        /// <returns></returns>
        public ChangeValueCollection GetChanges(T newEntity)
        {
            if (Equals(newEntity, null))
            {
                return new ChangeValueCollection();
            }
            _changeValues = new ChangeValueCollection();
            AddChanges(newEntity);
            return _changeValues;
        }

        /// <summary>
        /// 添加变更列表
        /// </summary>
        /// <param name="newEntity">新对象</param>
        protected virtual void AddChanges(T newEntity) { }

        /// <summary>
        /// 添加变更
        /// </summary>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <param name="description">描述</param>
        /// <param name="oldValue">旧值,范例:this.Name</param>
        /// <param name="newValue">新值,范例:newEntity.Name</param>
        /// <param name="isAttention">是否关注</param>
        protected void AddChange<TValue>(string propertyName, string description, TValue oldValue, TValue newValue,
            bool isAttention = false)
        {
            string oldValueString = oldValue.ToStr().ToLower().Trim();
            string newValueString = newValue.ToStr().ToLower().Trim();
            if (oldValueString == newValueString)
            {
                return;
            }
            _changeValues.Add(propertyName, description, oldValueString, newValueString);
        }

        /// <summary>
        /// 添加变更
        /// </summary>
        /// <typeparam name="TDomainObject">领域对象实体</typeparam>
        /// <param name="oldObject">旧对象</param>
        /// <param name="newObjet">新对象</param>
        protected void AddChange<TDomainObject>(ICompareChange<TDomainObject> oldObject, TDomainObject newObjet)
            where TDomainObject : IDomainObject
        {
            if (Equals(oldObject, null))
            {
                return;
            }
            if (Equals(newObjet, null))
            {
                return;
            }
            _changeValues.AddRange(oldObject.GetChanges(newObjet));
        }

        /// <summary>
        /// 添加变更
        /// </summary>
        /// <typeparam name="TDomainObject">领域对象实体</typeparam>
        /// <param name="oldObjects">旧对象列表</param>
        /// <param name="newObjects">新对象列表</param>
        protected void AddChange<TDomainObject>(IEnumerable<ICompareChange<TDomainObject>> oldObjects,
            IEnumerable<TDomainObject> newObjects) where TDomainObject : IDomainObject
        {
            if (Equals(oldObjects, null))
            {
                return;
            }
            if (Equals(newObjects, null))
            {
                return;
            }
            var oldList = oldObjects.ToList();
            var newList = newObjects.ToList();
            for (int i = 0; i < oldList.Count; i++)
            {
                if (newList.Count <= i)
                {
                    return;
                }
                AddChange(oldList[i], newList[i]);
            }
        }
    }
}
