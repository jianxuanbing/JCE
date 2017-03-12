/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Modes
 * 文件名：TimestampId
 * 版本号：v1.0.0.0
 * 唯一标识：844f41f5-8531-44c8-bfc3-2a96eda53b93
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/10 23:32:44
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/10 23:32:44
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Modes
{
    /// <summary>
    /// 时间戳ID，借鉴雪花算法，生成唯一时间戳ID
    /// 参考文章：http://www.cnblogs.com/rjf1979/p/6282855.html
    /// </summary>
    public class TimestampId
    {
        private long _lastTimestamp;
        private long _sequence;//计数从零开始
        private readonly DateTime? _initialDateTime;
        private static TimestampId _timestampId;
        private const int MAX_END_NUMBER = 9999;

        private TimestampId(DateTime? initialDateTime)
        {
            _initialDateTime = initialDateTime;
        }

        /// <summary>
        /// 获取单个实例对象
        /// </summary>
        /// <param name="initialDateTime">初始化时间，与当前时间做一个相差取时间戳</param>
        /// <returns></returns>
        public static TimestampId GetInstance(DateTime? initialDateTime = null)
        {
            if (initialDateTime.IsNull())
            {
                Interlocked.CompareExchange(ref _timestampId, new TimestampId(initialDateTime), null);
            }
            return _timestampId;
        }

        /// <summary>
        /// 初始化时间，作用时间戳的相差
        /// </summary>
        protected DateTime InitialDateTime
        {
            get
            {
                if (_initialDateTime == null || _initialDateTime.Value == DateTime.MinValue)
                {
                    return new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
                }
                return _initialDateTime.Value;
            }
        }

        /// <summary>
        /// 获取唯一时间戳ID
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            long temp;
            var timestamp = GetUniqueTimeStamp(_lastTimestamp, out temp);
            return $"{timestamp}{Fill(temp)}";
        }

        /// <summary>
        /// 补位填充
        /// </summary>
        /// <param name="temp">数字</param>
        /// <returns></returns>
        private string Fill(long temp)
        {
            var num = temp.ToString();
            IList<char> chars = new List<char>();
            for (int i = 0; i < MAX_END_NUMBER.ToString().Length-num.Length; i++)
            {
                chars.Add('0');
            }
            return new string(chars.ToArray()) + num;
        }

        /// <summary>
        /// 获取唯一时间戳
        /// </summary>
        /// <param name="lastTimeStamp">最后时间戳</param>
        /// <param name="temp">临时时间戳</param>
        /// <returns></returns>
        public long GetUniqueTimeStamp(long lastTimeStamp, out long temp)
        {
            lock (this)
            {
                temp = 1;
                var timeStamp = GetTimeStamp();
                if (timeStamp == _lastTimestamp)
                {
                    _sequence = _sequence + 1;
                    temp = _sequence;
                    if (temp >= MAX_END_NUMBER)
                    {
                        timeStamp = GetTimeStamp();
                        _lastTimestamp = timeStamp;
                        temp = _sequence = 1;
                    }
                }
                else
                {
                    _sequence = 1;
                    _lastTimestamp = timeStamp;
                }
                return timeStamp;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private long GetTimeStamp()
        {
            if (InitialDateTime >= DateTime.Now)
            {
                throw new Exception("初始化时间比当前时间还大，不合理");
            }
            var ts = DateTime.UtcNow - InitialDateTime;
            return (long) ts.TotalMilliseconds;
        }
    }
}
