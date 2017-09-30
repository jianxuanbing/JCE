using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Security.Authentication.Password.Hasher
{
    /// <summary>
    /// SHA256 密码散列器（加密器）
    /// </summary>
    public class SHA256PasswordHasher:IPasswordHasher
    {
        /// <summary>
        /// SHA256 加密算法
        /// </summary>
        private static readonly SHA256 HashAlgorithm=SHA256.Create();

        /// <summary>
        /// 加盐密匙
        /// </summary>
        private string _salt = "JCE.Utils.Secrurity";

        /// <summary>
        /// 初始化一个<see cref="SHA256PasswordHasher"/>类型的实例
        /// </summary>
        public SHA256PasswordHasher() { }

        /// <summary>
        /// 初始化一个<see cref="SHA256PasswordHasher"/>类型的实例
        /// </summary>
        /// <param name="salt">加盐密匙</param>
        public SHA256PasswordHasher(string salt) : this()
        {
            this._salt = salt;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密后的密码</returns>
        public string Encrypt(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            var hashing = HashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(_salt + password));
            var hashPassword = Convert.ToBase64String(hashing);
            return hashPassword;
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="encryptedPassword">加密后的密码</param>
        /// <param name="providedPassword">原始密码</param>
        /// <returns></returns>
        public PasswordVerificationResult Verify(string encryptedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(encryptedPassword))
            {
                throw new ArgumentNullException("encryptedPassword");
            }
            if (string.IsNullOrWhiteSpace(providedPassword))
            {
                throw new ArgumentNullException("providedPassword");
            }

            byte[] decodeHashedPassword = Convert.FromBase64String(encryptedPassword);
            if (decodeHashedPassword.Length == 0)
            {
                return PasswordVerificationResult.Failed;
            }

            var actualBase64Password = Encrypt(providedPassword);
            byte[] actualHashedPassword = Convert.FromBase64String(actualBase64Password);

            if (ByteArrayEqual(decodeHashedPassword, actualHashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }

        /// <summary>
        /// 比较两个字节数组的相等性
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="target">目标字节数组</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining|MethodImplOptions.NoOptimization)]
        private static bool ByteArrayEqual(byte[] source, byte[] target)
        {
            if (source == null && target == null)
            {
                return true;
            }
            if (source == null || target == null || source.Length != target.Length)
            {
                return false;
            }
            var areaSame = true;
            for (var i = 0; i < source.Length; i++)
            {
                areaSame &= source[i] == target[i];
            }
            return areaSame;
        }
    }
}
