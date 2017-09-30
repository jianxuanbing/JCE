using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Security.Authentication.Password
{
    /// <summary>
    /// 密码散列器（加密器）
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密后的密码</returns>
        string Encrypt(string password);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="encryptedPassword">加密后的密码</param>
        /// <param name="providedPassword">原始密码</param>
        /// <returns></returns>
        PasswordVerificationResult Verify(string encryptedPassword, string providedPassword);
    }
}
