using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// 常用验证 操作
    /// </summary>
    public static class Valid
    {
        #region IsEmail(是否电子邮件)
        /// <summary>
        /// 是否电子邮件
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool IsEmail(string value, bool isRestrict = false)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            string pattern = isRestrict
                ? @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"
                : @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
        }
        #endregion

        #region IsPhoneNumber(是否合法的手机号码)
        /// <summary>
        /// 是否合法手机号码
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return Regex.IsMatch(value, @"^(0|86|17951)?(13[0-9]|15[012356789]|18[0-9]|14[57]|17[678])[0-9]{8}$");
        }
        #endregion
    }
}
