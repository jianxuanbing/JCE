/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Mails.Pop3
 * 文件名：Pop3Setting
 * 版本号：v1.0.0.0
 * 唯一标识：1c86ff66-39de-4dd1-84ef-e65d35826489
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 23:06:02
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 23:06:02
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

namespace JCE.Utils.Mails.Pop3
{
    /// <summary>
    /// Pop3设置
    /// </summary>
    public class Pop3Setting
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
        /// 是否使用SSL连接
        /// </summary>
        public bool UseSSL { get; set; }

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
