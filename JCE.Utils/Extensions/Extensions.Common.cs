/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：Extensions
 * 版本号：v1.0.0.0
 * 唯一标识：e00da428-94b0-47fa-b812-8b8a6669709d
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/25 星期四 22:57:08
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/25 星期四 22:57:08
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
using JCE.Utils.Common;
using JCE.Utils.Exceptions;
using JCE.Utils.Logging;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 公共扩展
    /// </summary>
    public static partial class Extensions
    {
        #region SafeValue(安全返回值)
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="value">可空值</param>
        /// <returns></returns>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        #endregion

        #region Description(获取描述)
        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        /// <returns></returns>
        public static string Description(this Enum instance)
        {
            return EnumUtil.GetDescription(instance.GetType(), instance);
        }
        #endregion

        #region Log(写日志)
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="log">日志</param>
        public static void Log(this Exception exception, ILog log)
        {
            Warning.WriteLog(log,exception);
        }
        #endregion
    }
}
