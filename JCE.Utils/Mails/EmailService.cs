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
using JCE.Utils.Extensions;

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
        /// <param name="message">邮件消息</param>
        public void Send(MailMessage message)
        {
            InternalSend(message, Settings.User,
                Settings.Password);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        public void Send(MailMessage message, string credentialsUser, string credentialsPassword)
        {
            InternalSend(message, credentialsUser, credentialsPassword);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">邮件内容</param>
        public void Send(string receiver, string body)
        {            
            Send(new MailInfo() {Receiver = receiver, ReceiverName = receiver, Body = body});
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">内容</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        public void Send(string receiver, string body, bool isSingleSend)
        {
            Send(new MailInfo() {Receiver = receiver, Body = body, Subject = body}, isSingleSend);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人名字</param>
        /// <param name="body">内容</param>
        public void Send(string receiver, string receiverName, string body)
        {
            Send(new MailInfo() {Receiver = receiver,ReceiverName = receiver,Body = body});
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人名字</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        public void Send(string receiver, string receiverName, string subject, string body)
        {
            Send(new MailInfo() {Receiver = receiver, ReceiverName = receiverName, Body = body, Subject = subject});
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        public void Send(string receiver, string subject, string body, bool isSingleSend)
        {
            Send(new MailInfo() {Receiver = receiver, Body = body, Subject = subject}, isSingleSend);
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="attachments">附件列表</param>
        public void Send(MailInfo info, params Attachment[] attachments)
        {
            var message=new MailMessage();
            foreach (var item in attachments)
            {
                message.Attachments.Add(item);
            }
            Send(info, message);
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="filePath">附件路径</param>
        public void Send(MailInfo info, string filePath)
        {
            var message=new MailMessage();
            message.Attachments.Add(new Attachment(filePath));
            Send(info,message);
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="filePath">附件路径</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        public void Send(MailInfo info, string filePath, bool isSingleSend)
        {
            var message=new MailMessage();
            message.Attachments.Add(new Attachment(filePath));
            Send(info, isSingleSend, message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="message">邮件小，默认为null</param>
        public void Send(MailInfo info,MailMessage message=null)
        {
            InternalSend(info, message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        /// <param name="message">邮件消息，默认为null</param>
        public void Send(MailInfo info, bool isSingleSend, MailMessage message = null)
        {
            InternalSend(info, message, isSingleSend);
        }

        /// <summary>
        /// 内部发送邮件
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="message">邮件消息，默认为null</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        protected void InternalSend(MailInfo info, MailMessage message = null, bool isSingleSend = false)
        {
            message = message ?? new MailMessage();
            message.Subject = info.Subject;
            message.Body = info.Body;
            message.From = new MailAddress(Settings.From, Settings.DisplayName);            
            if (!info.Replay.IsNullOrEmpty())
            {
                message.ReplyToList.Add(new MailAddress(info.Replay));
            }            
            if (!info.CC.IsNullOrEmpty())
            {
                message.CC.Add(info.CC);
            }

            if (isSingleSend && info.Receiver.Contains(","))
            {
                foreach (var item in info.Receiver.Split(','))
                {
                    message.To.Clear();
                    message.To.Add(item);

                    InternalSend(message,Settings.User, Settings.Password);
                }
                return;
            }
            if (!isSingleSend && info.Receiver.Contains(","))
            {
                message.To.Add(info.Receiver);
                    
            }
            else
            {
                message.To.Add(new MailAddress(info.Receiver,
                    info.ReceiverName.IsNullOrEmpty() ? info.Receiver : info.ReceiverName));
            }
            InternalSend(message, Settings.User, Settings.Password);
        }

        /// <summary>
        /// 内部发送邮件
        /// </summary>
        /// <param name="mailMessage">邮件消息</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        private void InternalSend(MailMessage mailMessage, string credentialsUser,
            string credentialsPassword)
        {
            
            try
            {
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                mailMessage.IsBodyHtml = Settings.IsHtml;
                
                string host = Settings.Host;
                int port = Settings.Port;

                client.Host = host;               
                if (Settings.UsePort)
                {
                    client.Port = port == 0 ? 25 : port;
                }
                client.EnableSsl = Settings.EnableSsl;
                client.Credentials = new NetworkCredential(credentialsUser, credentialsPassword);                
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

    }
}
