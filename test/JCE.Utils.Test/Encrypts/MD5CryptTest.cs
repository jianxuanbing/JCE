using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Encrypts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Utils.Test.Encrypts
{
    [TestClass]
    public class MD5CryptTest
    {
        [TestMethod]
        public void TestEncrypt()
        {
            var key = "2017091517";
            var result = MD5Crypt.Encrypt(key);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestEncrypt16()
        {
            var key = "2017091517";
            var result = MD5Crypt.EncryptBy16(key);
            Console.WriteLine(result);
        }
    }
}
