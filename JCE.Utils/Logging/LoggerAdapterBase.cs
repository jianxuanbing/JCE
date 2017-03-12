/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Logging
 * 文件名：LoggerAdapterBase
 * 版本号：v1.0.0.0
 * 唯一标识：862d752f-7d3c-445b-a953-0d711e5d0e2c
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/14 星期日 15:36:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/14 星期日 15:36:47
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Extensions;
using JCE.Utils.Properties;

namespace JCE.Utils.Logging
{
    /// <summary>
    /// 日志适配器基类，按名称缓存的日志实现适配器基类，用于创建并管理指定类型的日志实例
    /// </summary>
    public abstract class LoggerAdapterBase : ILoggerAdapter
    {
        #region Field(字段)
        /// <summary>
        /// 缓存日志字典
        /// </summary>
        private readonly ConcurrentDictionary<string, ILog> _cacheLoggers;
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 构造函数，初始化一个<see cref="LoggerAdapterBase"/>类型的新实例
        /// </summary>
        protected LoggerAdapterBase()
        {
            _cacheLoggers = new ConcurrentDictionary<string, ILog>();
        }
        #endregion

        /// <summary>
        /// 创建日志实例，如缓存日志字典存在该名称日志实例，则返回，否则创建新实例并缓存起来
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected abstract ILog CreateLogger(string name);

        #region GetLogger(获取日志实例)
        /// <summary>
        /// 由指定类型获取<see cref="ILog"/>日志实例
        /// </summary>
        /// <param name="type">指定类型</param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            type.CheckNotNull("type");
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 由指定名称获取<see cref="ILog"/>日志实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        public ILog GetLogger(string name)
        {
            name.CheckNotNullOrEmpty("name");
            return GetLoggerInternal(name);
        }
        #endregion

        #region ClearLoggerCache(清空缓存中的日志实例)
        /// <summary>
        /// 清空缓存中的日志实例
        /// </summary>
        protected virtual void ClearLoggerCache()
        {
            _cacheLoggers.Clear();
        }
        #endregion

        #region GetLoggerInternal(获取指定名称的日志实例)
        /// <summary>
        /// 获取指定名称的日志实例，如不存在则创建实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns>日志实例</returns>
        /// <exception cref="NotSupportedException">指定名称的日志缓存实例不存在则返回异常<see cref="NotSupportedException"/></exception>
        protected virtual ILog GetLoggerInternal(string name)
        {
            ILog log;
            if (_cacheLoggers.TryGetValue(name, out log))
            {
                return log;
            }
            log = CreateLogger(name);
            if (log == null)
            {
                throw new NotSupportedException(Resources.Logging_CreateLogInstanceReturnNull);
            }
            _cacheLoggers[name] = log;
            return log;
        }
        #endregion
    }
}
