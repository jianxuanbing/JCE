/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：Extensions
 * 版本号：v1.0.0.0
 * 唯一标识：ea2bbf53-4efa-4b3a-9241-bd16f39b86cb
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：8/30 星期二 23:33:14
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：8/30 星期二 23:33:14
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
    /// 格式化扩展
    /// </summary>
    public static partial class Extensions
    {
        #region Description(获取描述)
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        /// <returns></returns>
        public static string Description(this bool value)
        {
            return value ? "是" : "否";
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        /// <returns></returns>
        public static string Description(this bool? value)
        {
            return value == null ? "" : Description(value.Value);
        }
        #endregion

    }
}
