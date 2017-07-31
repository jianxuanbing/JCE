/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails
 * 文件名：EmailServiceSettings
 * 版本号：v1.0.0.0
 * 唯一标识：b02c435d-0163-40a4-9727-05e39c0b1fe4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/7/31 9:45:02
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/7/31 9:45:02
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
    /// 邮件服务设置
    /// </summary>
    public class EmailServiceSettings
    {
        /// <summary>
        /// Smtp服务器，发送邮件的服务器地址
        /// </summary>
        public string SmtpService { get; set; }

        /// <summary>
        /// 发送方
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 是否使用默认端口，如果设为false，默认25端口
        /// </summary>

        public bool UsePort { get; set; }


        /// <summary>
        /// 邮件服务器验证的用户名
        /// </summary>
        public string AuthenticationUserName { get; set; }

        /// <summary>
        /// 邮件服务器验证的用户密码
        /// </summary>
        public string AuthenticationPassword { get; set; }

        /// <summary>
        /// 是否需要身份认证
        /// </summary>
        public bool IsAuthenticationRequired { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 初始化一个<see cref="EmailServiceSettings"/>类型的实例
        /// </summary>

        public EmailServiceSettings()
        {
            UsePort = false;
        }

        /// <summary>
        /// 初始化一个<see cref="EmailServiceSettings"/>类型的实例
        /// </summary>
        /// <param name="smtpService">Smtp服务器</param>
        /// <param name="port">端口号</param>
        public EmailServiceSettings(string smtpService, int port)
        {
            SmtpService = smtpService;
            Port = port;
        }
    }
}
