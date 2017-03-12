/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails.Smtp
 * 文件名：SmtpSetting
 * 版本号：v1.0.0.0
 * 唯一标识：224a2318-6625-40fe-ae3d-8867fffa52c0
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 22:44:35
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 22:44:35
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

namespace JCE.Utils.Mails.Smtp
{
    /// <summary>
    /// Smtp设置
    /// </summary>
    public class SmtpSetting
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 是否需要身份认证
        /// </summary>
        public bool Authentication { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
