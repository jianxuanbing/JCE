/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Data
 * 文件名：EqualityUtil
 * 版本号：v1.0.0.0
 * 唯一标识：0cbca11c-1709-4d12-a029-75ba088469e5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:22:06
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:22:06
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

namespace JCE.Utils.Data
{
    /// <summary>
    /// 相等比较辅助类，用于快速创建<see cref="IEqualityComparer{T}"/>的实例
    /// </summary>
    /// <example>
    /// var equalityComparer1 = EqualityHelper{Person}.CreateComparer(p => p.ID);
    /// var equalityComparer2 = EqualityHelper[Person].CreateComparer(p => p.Name);
    /// var equalityComparer3 = EqualityHelper[Person].CreateComparer(p => p.Birthday.Year);
    /// </example>
    /// <typeparam name="T">实体类型</typeparam>
    public static class EqualityUtil<T>
    {
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>的实例
        /// </summary>
        /// <typeparam name="TV"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector)
        {
            return new CommonEqualityComparer<TV>(keySelector);
        }
        /// <summary>
        /// 创建指定对比委托<paramref name="keySelector"/>与结果二次比较器<paramref name="comparer"/>的实例
        /// </summary>
        /// <typeparam name="TV"></typeparam>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEqualityComparer<T> CreateComparer<TV>(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
        {
            return new CommonEqualityComparer<TV>(keySelector, comparer);
        }

        private class CommonEqualityComparer<TV> : IEqualityComparer<T>
        {
            private readonly IEqualityComparer<TV> _comparer;
            private readonly Func<T, TV> _keySelector;

            public CommonEqualityComparer(Func<T, TV> keySelector, IEqualityComparer<TV> comparer)
            {
                _keySelector = keySelector;
                _comparer = comparer;
            }
            public CommonEqualityComparer(Func<T, TV> keySelector) : this(keySelector, EqualityComparer<TV>.Default) { }


            public bool Equals(T x, T y)
            {
                return _comparer.Equals(_keySelector(x), _keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return _comparer.GetHashCode(_keySelector(obj));
            }
        }
    }
}
