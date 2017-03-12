/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes
 * 文件名：Singleton
 * 版本号：v1.0.0.0
 * 唯一标识：cb888b8f-7e32-4b58-9101-8b8a30c6c1d9
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:32:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:32:36
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Modes
{
    /// <summary>
    /// 通用单例模式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class Singleton<T>
    {
        static Dictionary<Type, object> _lockers = new Dictionary<Type, object>();
        static T _instance;
        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static T GetInstance(params object[] parameters)
        {
            if (_instance == null)
            {
                Type type = typeof(T);
                var locker = _lockers.GetOrDefault(type);
                if (locker == null)
                {
                    lock (_lockers)
                    {
                        locker = _lockers.Get(type, x => new object());
                    }
                }
                lock (locker)
                {
                    if (_instance == null)
                    {
                        var cons =
                            type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                .FirstOrDefault();
                        _instance = (T)cons.Invoke(parameters);
                    }
                }
            }
            return _instance;
        }
    }
}
