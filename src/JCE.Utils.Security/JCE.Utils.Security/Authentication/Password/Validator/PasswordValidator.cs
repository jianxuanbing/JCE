using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Security.Authentication.Password.Validator
{
    /// <summary>
    /// 密码验证器
    /// </summary>
    public static class PasswordValidator
    {
        /// <summary>
        /// 默认密码验证配置
        /// </summary>
        private static readonly PasswordOptions _defaultOptions=new PasswordOptions();

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="errors">错误集合</param>
        /// <returns></returns>
        public static bool Validate(string password, out ICollection<string> errors)
        {
            return Validate(password, _defaultOptions, out errors);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="options">密码验证配置</param>
        /// <param name="errors">错误集合</param>
        /// <returns></returns>
        public static bool Validate(string password, PasswordOptions options, out ICollection<string> errors)
        {
            errors=new List<string>();
            if (string.IsNullOrWhiteSpace(password) || password.Length < options.MinLength)
            {
                errors.Add("密码太短");
            }
            if (password != null)
            {
                if (password.Length > options.MaxLength)
                {
                    errors.Add("密码太长");
                }
                if (options.NonLetterOrDigit && password.All(IsLetterOrDigit))
                {
                    errors.Add("密码不能包含字母或数字");
                }
                if (options.Digit && !password.Any(IsDigit))
                {
                    errors.Add("密码必须包含数字");
                }
                if (options.Lowercase && !password.Any(IsLower))
                {
                    errors.Add("密码必须包含小写字母");
                }
                if (options.Uppercase && !password.Any(IsUpper))
                {
                    errors.Add("密码必须包含大写字母");
                }
            }
            return errors.Count == 0;
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        internal static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// 是否小写字母
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        internal static bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        /// <summary>
        /// 是否大写字母
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        internal static bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        /// <summary>
        /// 是否字母或数字
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        internal static bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}
