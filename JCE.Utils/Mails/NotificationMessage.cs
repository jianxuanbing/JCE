/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails
 * 文件名：NotificationMessage
 * 版本号：v1.0.0.0
 * 唯一标识：c6c82b2f-7a5c-4c02-a547-5cfaf2b857ae
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/31 9:38:47
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/31 9:38:47
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Mails
{
    /// <summary>
    /// 通知消息
    /// </summary>
    public class NotificationMessage
    {
        /// <summary>
        /// 接收方
        /// </summary>
        public string To;

        /// <summary>
        /// 发送方
        /// </summary>
        public string From;

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject;

        /// <summary>
        /// 内容
        /// </summary>
        public string Body;

        /// <summary>
        /// 消息模板ID
        /// </summary>
        public string MessageTemplateId;

        /// <summary>
        /// 是否Html
        /// </summary>
        public bool IsHtml = true;

        /// <summary>
        /// 参数字典
        /// </summary>
        public IDictionary<string, string> Values;

        /// <summary>
        /// 初始化一个<see cref="NotificationMessage"/>类型的实例
        /// </summary>
        /// <param name="values">参数字典</param>
        /// <param name="to">接收方</param>
        /// <param name="from">发送方</param>
        /// <param name="subject">标题</param>
        /// <param name="messageTemplateId">消息模板ID</param>
        public NotificationMessage(IDictionary<string, string> values, string to, string from, string subject,
            string messageTemplateId)
        {
            To = to;
            From = from;
            Subject = subject;
            Values = values;
            MessageTemplateId = messageTemplateId;
        }
    }
}
