/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails
 * 文件名：EmailService
 * 版本号：v1.0.0.0
 * 唯一标识：e691c694-e9cc-44b8-974b-55df7fb38e7d
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/31 9:58:45
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/31 9:58:45
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Mails
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public class EmailService:IEmailService
    {
        /// <summary>
        /// 邮件服务设置
        /// </summary>
        public EmailServiceSettings Settings { get; set; }

        /// <summary>
        /// 初始化一个<see cref="EmailService"/>类型的实例
        /// </summary>
        /// <param name="settings">邮件服务设置</param>
        public EmailService(EmailServiceSettings settings)
        {
            Init(settings);
        }

        /// <summary>
        /// 初始化邮件服务配置
        /// </summary>
        /// <param name="config">邮件服务设置</param>
        public void Init(EmailServiceSettings config)
        {
            this.Settings = config;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">通知消息</param>
        public void Send(NotificationMessage message)
        {
            MailMessage mailMessage = new MailMessage(Settings.From, message.To, message.Subject, message.Body);
            mailMessage.IsBodyHtml = message.IsHtml;
            InternalSend(mailMessage, Settings.IsAuthenticationRequired, Settings.AuthenticationUserName,
                Settings.AuthenticationPassword);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">通知消息</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        public void Send(NotificationMessage message, string credentialsUser, string credentialsPassword)
        {
            MailMessage mailMessage = new MailMessage(Settings.From, message.To, message.Subject, message.Body);
            mailMessage.IsBodyHtml = message.IsHtml;
            InternalSend(mailMessage, true, credentialsUser, credentialsPassword);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        public void Send(MailMessage message)
        {
            InternalSend(message, Settings.IsAuthenticationRequired, Settings.AuthenticationUserName,
                Settings.AuthenticationPassword);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        /// <param name="useCredentials">是否使用凭证</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        public void Send(MailMessage message, bool useCredentials, string credentialsUser, string credentialsPassword)
        {
            InternalSend(message, useCredentials, credentialsUser, credentialsPassword);
        }

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
        public void Send(string from, string to, string subject, string body, bool useCredentials, string credentialsUser,
            string credentialsPassword)
        {
            MailMessage message = new MailMessage(from, to, subject, body);
            message.IsBodyHtml = true;
            InternalSend(message, useCredentials, credentialsUser, credentialsPassword);
        }

        /// <summary>
        /// 内部发送邮件
        /// </summary>
        /// <param name="mailMessage">邮件消息</param>
        /// <param name="useCredentials">是否使用凭证</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        private void InternalSend(MailMessage mailMessage, bool useCredentials, string credentialsUser,
            string credentialsPassword)
        {
            try
            {
                string host = Settings.SmtpService;
                int port = Settings.Port;

                SmtpClient client = null;
                client = Settings.UsePort ? new SmtpClient(host, port == 0 ? 25 : port) : new SmtpClient(host);
                if (useCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(credentialsUser, credentialsPassword);
                }
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

    }
}
