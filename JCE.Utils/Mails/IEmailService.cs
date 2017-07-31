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
        /// <param name="message">邮件消息</param>
        void Send(MailMessage message);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">邮件消息</param>
        /// <param name="credentialsUser">凭证用户名</param>
        /// <param name="credentialsPassword">凭证密码</param>
        void Send(MailMessage message,string credentialsUser, string credentialsPassword);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">邮件内容</param>
        void Send(string receiver, string body);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">内容</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        void Send(string receiver, string body, bool isSingleSend);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人名字</param>
        /// <param name="body">内容</param>
        void Send(string receiver, string receiverName, string body);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人名字</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        void Send(string receiver, string receiverName, string subject, string body);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        void Send(string receiver, string subject, string body, bool isSingleSend);

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="attachments">附件列表</param>
        void Send(MailInfo info, params Attachment[] attachments);

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="filePath">附件路径</param>
        void Send(MailInfo info, string filePath);

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="filePath">附件路径</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        void Send(MailInfo info, string filePath, bool isSingleSend);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="message">邮件小，默认为null</param>
        void Send(MailInfo info, MailMessage message = null);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">邮件信息</param>
        /// <param name="isSingleSend">是否群发单显，当邮件接收人为多个时，可选择该模式，即可对多个接收人分别发送，收件方不会知道这封邮件有多个收件人</param>
        /// <param name="message">邮件消息，默认为null</param>
        void Send(MailInfo info, bool isSingleSend, MailMessage message = null);

    }
}
