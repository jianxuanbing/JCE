/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Encrypts
 * 文件名：RSACrypt
 * 版本号：v1.0.0.0
 * 唯一标识：37e5ac04-f1a2-45df-be7d-ec13945958e3
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/11 9:03:51
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/11 9:03:51
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// RSA加密算法，可用于数据加密或者数字签名
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RSACrypt
    {
        #region Constructor(构造函数)
        /// <summary>
        /// 构造函数，空
        /// </summary>
        public RSACrypt()
        {

        }
        #endregion

        #region RsaKey(生成RSA密匙)
        /// <summary>
        /// 生成RSA密匙
        /// </summary>
        /// <param name="xmlKey">密匙</param>
        /// <param name="xmlPublicKey">公有密匙</param>
        public void RsaKey(out string xmlKey, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKey = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion
    }
}
