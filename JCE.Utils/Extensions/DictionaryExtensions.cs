/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DictionaryExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：e4fa9d24-e875-4092-9f7a-b3a6499ae81c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:44:13
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:44:13
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 字典（Dictionary）扩展
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Sort(字典排序)
        /// <summary>
        /// 对指定的字典排序
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns>排序后的字典</returns>
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SortedDictionary<TKey, TValue>(dictionary);
        }
        /// <summary>
        /// 对指定的字典排序
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="comparer">比较器，用于排序字典</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            IComparer<TKey> comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }
        /// <summary>
        /// 对指定的字典排序，根据值元素进行排序
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return (new SortedDictionary<TKey, TValue>(dictionary)).OrderBy(x => x.Value)
                .ToDictionary(item => item.Key, item => item.Value);
        }
        #endregion

        #region Invert(字典反转)
        /// <summary>
        /// 对指定的字典反转，创建新字典（值作为键、键作为值）
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns></returns>
        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return dictionary.ToDictionary(x => x.Value, x => x.Key);
        }
        #endregion

        #region ToHashTable(字典转换为哈希表)
        /// <summary>
        /// 对指定字典转换为HashTable
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns></returns>
        public static Hashtable ToHashTable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var table = new Hashtable();
            foreach (var item in dictionary)
            {
                table.Add(item.Key, item.Value);
            }
            return table;
        }
        #endregion

        #region GetValue(获取指定键的值)
        /// <summary>
        /// 获取指定键集合中匹配的第一个值
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="keys">键集合</param>
        /// <returns></returns>
        public static TValue GetFirstValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue,
            params TKey[] keys)
        {
            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
            }
            return defaultValue;
        }
        /// <summary>
        /// 获取指定键的值，如果找不到指定键的值则抛出异常
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default(TValue))
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }
        /// <summary>
        /// 获取指定键的值，如果找不到指定键的值则抛出指定异常
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <param name="key">键</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public static TValue GetOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Exception exception)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            throw exception;
        }

        /// <summary>
        /// 获取指定Key对应的Value，若未找到将使用指定的委托增加值
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <param name="setValue">设置值</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            Func<TKey, TValue> setValue)
        {
            TValue value = default(TValue);
            if (!dict.TryGetValue(key, out value))
            {
                value = setValue(key);
                dict.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// 获取指定Key对应的Value，若未找到将抛异常
        /// </summary>
        /// <typeparam name="TKey">键</typeparam>
        /// <typeparam name="TValue">值</typeparam>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value = default(TValue);
            if (!dict.TryGetValue(key, out value))
            {
                throw new KeyNotFoundException("没有找到key:"+key.ToString());
            }
            return value;
        }
        #endregion

        #region IsEmpty(是否为空)
        /// <summary>
        /// 判断字典是否为空
        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <returns></returns>
        public static bool IsEmpty(this IDictionary dictionary)
        {
            dictionary.ExceptionIfNullOrEmpty("The collection cannot be null.", "dictionary");
            return dictionary.Count == 0;
        }
        /// <summary>
        /// 判断字典是否为空
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dictionary">字典</param>
        /// <returns></returns>
        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            dictionary.ExceptionIfNullOrEmpty("The collection cannot be null.", "dictionary");
            return dictionary.Count == 0;
        }
        #endregion

        #region TryAdd(尝试添加键值对到字典)
        /// <summary>
        /// 尝试将键值对添加到字典中：如果不存在，则添加；存在，不添加也不抛异常
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key,value);
            }
            return dict;
        }
        #endregion

        #region Update(添加或更新字典)
        /// <summary>
        /// 将键值对添加或更新到字典中：如果不存在，则添加；存在，则更新
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dict">字典</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Update<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key,value);
            }
            else
            {
                dict[key] = value;
            }
            return dict;
        }
        #endregion

        #region AddRange(批量添加键值对到字典)
        /// <summary>
        /// 批量添加键值对到字典中
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="dict">字典</param>
        /// <param name="values">键值对集合</param>
        /// <param name="replaceExisted">是否替换已存在的键值对</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) && replaceExisted)
                {
                    dict[item.Key] = item.Value;
                    continue;
                }
                if (!dict.ContainsKey(item.Key))
                {
                    dict.Add(item.Key, item.Value);
                }
            }
            return dict;
        }
        #endregion

        #region GetKey(根据Value反向查找Key)
        /// <summary>
        /// 根据Value值反向查找Key
        /// </summary>
        /// <typeparam name="T1">Key</typeparam>
        /// <typeparam name="T2">Value</typeparam>
        /// <param name="dic">字典对象</param>
        /// <param name="t2">键值</param>
        /// <returns></returns>
        public static T1 GetKey<T1, T2>(this Dictionary<T1, T2> dic, T2 t2)
        {
            foreach (var obj in dic.Where(obj=>obj.Value.Equals(t2)))
            {
                return obj.Key;
            }
            return default(T1);
        }
        #endregion

        #region ToQueryString(转换成查询字符串)
        /// <summary>
        /// 将字典转换成查询字符串
        /// </summary>
        /// <typeparam name="TK">键类型</typeparam>
        /// <typeparam name="TV">值类型</typeparam>
        /// <param name="source">源字典</param>
        /// <returns></returns>
        public static string ToQueryString<TK, TV>(this IDictionary<TK, TV> source)
        {
            if (source == null || !source.Any())
            {
                return string.Empty;
            }
            Str sb=new Str();
            foreach (var item in source)
            {
                sb.Append("{0}={1}&", item.Key.ToString(),item.Value.ToString());
            }
            sb.RemoveEnd("&");
            return sb.ToString();
        }
        #endregion
    }
}
