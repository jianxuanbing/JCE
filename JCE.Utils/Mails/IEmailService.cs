/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails
 * 文件名：IEmailService
 * 版本号：v1.0.0.0
 * 唯一标识：20ca08a1-f6d6-483e-ba95-532462f149c1
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/31 9:52:10
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/31 9:52:10
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Mails
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 邮件服务设置
        /// </summary>
        EmailServiceSettings Settings { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">通知消息</param>
        void Send(NotificationMessage message);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">通知消息</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        void Send(NotificationMessage message, string credentialsUser, string credentialsPassword);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        void Send(MailMessage message);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        /// <param name="useCredentials">是否使用凭证</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        void Send(MailMessage message, bool useCredentials, string credentialsUser, string credentialsPassword);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from">发送方</param>
        /// <param name="to">接收方</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="useCredentials">是否使用凭证</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        void Send(string from, string to, string subject, string body, bool useCredentials, string credentialsUser,
            string credentialsPassword);
    }
}
