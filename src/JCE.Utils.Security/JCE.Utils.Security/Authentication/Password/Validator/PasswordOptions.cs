using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Security.Authentication.Password.Validator
{
    /// <summary>
    /// 密码配置
    /// </summary>
    public class PasswordOptions
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// 是否不包含字母及数字
        /// </summary>
        public bool NonLetterOrDigit { get; set; }

        /// <summary>
        /// 是否包含小写
        /// </summary>
        public bool Lowercase { get; set; }

        /// <summary>
        /// 是否包含大写
        /// </summary>
        public bool Uppercase { get; set; }

        /// <summary>
        /// 是否包含数字
        /// </summary>
        public bool Digit { get; set; }

        /// <summary>
        /// 初始化一个<see cref="PasswordOptions"/>类型的实例
        /// </summary>
        public PasswordOptions()
        {
            MinLength = 6;
            MaxLength = 25;
            NonLetterOrDigit = false;
            Lowercase = true;
            Uppercase = true;
            Digit = true;
        }
    }
}
