/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails
 * 文件名：MailInfo
 * 版本号：v1.0.0.0
 * 唯一标识：cd698fcc-c2e8-4e9b-a341-602abcba0060
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/31 13:05:29
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/31 13:05:29
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
using JCE.Utils.Extensions;

namespace JCE.Utils.Mails
{
    /// <summary>
    /// 发送邮件的信息
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        private string _subject;

        /// <summary>
        /// 接收者名字
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 接收者邮箱（多个用英文","号分割）
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get
            {
                if (_subject.IsNullOrEmpty() && _subject.Length > 15)
                {
                    return Body.Substring(0, 15);
                }
                return _subject;
            }
            set { _subject = value; }
        }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 抄送人集合（多个用英文","分割）
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 回复地址
        /// </summary>
        public string Replay { get; set; }
    }
}
