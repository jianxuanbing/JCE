/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DecimalExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：12049404-bb40-42da-bec8-3b55decb0af9
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 20:46:58
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 20:46:58
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
    /// decimal类型的扩展辅助操作类
    /// </summary>
    public static class DecimalExtensions
    {
        #region RoundDecimalPoints(将数值四舍五入，保留指定小数位数)
        /// <summary>
        /// 将数值四舍五入，保留指定小数位数
        /// </summary>
        /// <param name="value">decimal</param>
        /// <param name="decimalPoints">四舍五入后的小数位数</param>
        /// <returns>四舍五入后的十进制数</returns>
        public static decimal RoundDecimalPoints(this decimal value, int decimalPoints)
        {
            return Math.Round(value, decimalPoints);
        }
        #endregion

        #region RoundToTwoDecimalPoints(将数值四舍五入，保留两位小数)
        /// <summary>
        /// 将数值四舍五入，保留两位小数
        /// </summary>
        /// <param name="value">decimal</param>
        /// <returns>四舍五入后的小数</returns>
        public static decimal RoundToTwoDecimalPoints(this decimal value)
        {
            return Math.Round(value, 2);
        }
        #endregion

        #region Abs(返回数字的绝对值)
        /// <summary>
        /// 返回数字的绝对值
        /// </summary>
        /// <param name="value">decimal</param>
        /// <returns>绝对值</returns>
        public static decimal Abs(this decimal value)
        {
            return Math.Abs(value);
        }
        /// <summary>
        /// 返回数字的绝对值
        /// </summary>
        /// <param name="value">枚举集合decimal</param>
        /// <returns>绝对值的枚举集合</returns>
        public static IEnumerable<decimal> Abs(this IEnumerable<decimal> value)
        {
            return value.Select(d => d.Abs());
        }

        #endregion
    }
}
