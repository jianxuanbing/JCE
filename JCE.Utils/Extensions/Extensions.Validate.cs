/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：Extensions.Validate
 * 版本号：v1.0.0.0
 * 唯一标识：76ac1ac9-f374-4f10-bf51-a357b5b24651
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/19 23:52:38
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/19 23:52:38
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

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static partial class Extensions
    {
        #region CheckNull(检测空值)
        /// <summary>
        /// 检测空值,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        #endregion

        #region IsEmpty(是否为空)
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == null)
            {
                return true;
            }
            return IsEmpty(value.Value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid value)
        {
            if (value == Guid.Empty)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
