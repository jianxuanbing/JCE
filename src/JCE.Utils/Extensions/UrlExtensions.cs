using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// Url扩展
    /// </summary>
    public static class UrlExtensions
    {
        #region UrlEncode(Url编码处理)
        /// <summary>
        /// Url编码处理
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string UrlEncode(this string input)
        {
            if (input.IsEmpty())
            {
                return string.Empty;
            }
            return Uri.EscapeDataString(input);
        }

        /// <summary>
        /// Url编码处理
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string UrlEncode(this object input)
        {
            if (input.IsNull())
            {
                return string.Empty;
            }
            return Uri.EscapeDataString(input.ToString());
        }
        #endregion

        #region UrlDecode(Url解码处理)
        /// <summary>
        /// Url解码处理
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string UrlDecode(this string input)
        {
            if (input.IsEmpty())
            {
                return string.Empty;
            }
            return Uri.UnescapeDataString(input);
        }
        #endregion

    }
}
