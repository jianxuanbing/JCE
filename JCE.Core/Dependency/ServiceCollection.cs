/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Dependency
 * 文件名：ServiceCollection
 * 版本号：v1.0.0.0
 * 唯一标识：a1042978-5521-4593-8e61-598bc5f8618d
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:41:39
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:41:39
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入服务映射信息集合
    /// </summary>
    public class ServiceCollection:IServiceCollection
    {
        private readonly List<ServiceDescriptor> _descriptors=new List<ServiceDescriptor>();

        #region 构造函数
        /// <summary>
        /// 初始化一个<see cref="ServiceCollection"/>类型的新实例
        /// </summary>
        internal ServiceCollection() { }
        #endregion

        #region GetEnumerator(获取循环访问集合的枚举数)
        /// <summary>
        /// 获取循环访问集合的枚举数，返回一个循环访问集合的枚举数
        /// </summary>
        /// <returns>可用于循环访问集合的枚举数</returns>
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }
        /// <summary>
        /// 获取循环访问集合的枚举数，返回一个循环访问集合的枚举数
        /// </summary>
        /// <returns>可用于循环访问集合的枚举数</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Add(添加映射描述信息)
        /// <summary>
        /// 添加映射描述信息
        /// </summary>
        /// <param name="item">映射描述信息</param>
        public void Add(ServiceDescriptor item)
        {
            _descriptors.Add(item);
        }        
        #endregion

        #region Clear(清空映射描述信息)
        /// <summary>
        /// 清空映射描述信息
        /// </summary>
        public void Clear()
        {
            _descriptors.Clear();
        }
        #endregion

        #region Contains(确定映射描述信息是否在集合中)
        /// <summary>
        /// 确定映射描述信息是否在集合中
        /// </summary>
        /// <param name="item">映射描述信息</param>
        /// <returns></returns>
        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }
        #endregion

        #region CopyTo(复制数组到指定位置)
        /// <summary>
        /// 将整个集合复制到兼容的一维数组中，从目标数组的指定索引位置开始放置
        /// </summary>
        /// <param name="array">作为从集合复制的元素的目标位置的一维数组。 数组必须具有从零开始的索引。</param>
        /// <param name="arrayIndex">数组中从零开始的索引，从此索引处开始进行复制。</param>
        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array,arrayIndex);
        }
        #endregion

        #region Remove(移除指定映射描述信息)
        /// <summary>
        /// 移除指定映射描述信息
        /// </summary>
        /// <param name="item">映射描述信息</param>
        /// <returns></returns>
        public bool Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IndexOf(获取指定映射描述信息索引)
        /// <summary>
        /// 获取指定映射描述信息索引
        /// </summary>
        /// <param name="item">映射描述信息</param>
        /// <returns></returns>
        public int IndexOf(ServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }
        #endregion

        #region Insert(在指定索引插入映射描述信息)
        /// <summary>
        /// 在指定索引插入映射描述信息
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="item">映射描述信息</param>
        public void Insert(int index, ServiceDescriptor item)
        {
            _descriptors.Insert(index,item);
        }
        #endregion

        #region RemoveAt(移除指定索引的映射描述信息)
        /// <summary>
        /// 移除指定索引的映射描述信息
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }
        #endregion

        #region Clone(克隆创建当前集合的副本)
        /// <summary>
        /// 克隆创建当前集合的副本
        /// </summary>
        /// <returns></returns>
        public IServiceCollection Clone()
        {
            ServiceCollection collection=new ServiceCollection();
            foreach (ServiceDescriptor descriptor in this)
            {
                collection.Add(descriptor);    
            }
            return collection;
        }
        #endregion

        #region Property(属性)
        /// <summary>
        /// 集合元素总数
        /// </summary>
        public int Count
        {
            get { return _descriptors.Count; }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 获取或设置 指定索引的元素
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public ServiceDescriptor this[int index]
        {
            get { return _descriptors[index]; }
            set { _descriptors[index] = value; }
        }
        #endregion
        
    }
}
