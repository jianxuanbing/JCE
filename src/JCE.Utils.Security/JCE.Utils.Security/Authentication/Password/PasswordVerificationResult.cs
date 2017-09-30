using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Security.Authentication.Password
{
    /// <summary>
    /// 密码验证结果
    /// </summary>
    public enum PasswordVerificationResult
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed=0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success=1,
        /// <summary>
        /// 正常，默认值
        /// </summary>
        [Description("正常")]
        Normal=2
    }
}
