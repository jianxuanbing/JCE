/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.HttpUtil.Sealed
 * 文件名：HttpRequestMethod
 * 版本号：v1.0.0.0
 * 唯一标识：5f7c6059-f759-4b5b-9f84-347ecdf8c9ce
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/4/19 0:00:20
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/4/19 0:00:20
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.HttpUtil.Sealed
{
    /// <summary>
    /// 表示 HTTP 请求的方式
    /// </summary>
    public enum HttpRequestMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        [Description("GET")]
        Get,
        /// <summary>
        /// POST
        /// </summary>
        [Description("POST")]
        Post,
        /// <summary>
        /// HEAD
        /// </summary>
        [Description("HEAD")]
        Head,
        /// <summary>
        /// TRACE
        /// </summary>
        [Description("TRACE")]
        Trace,
        /// <summary>
        /// PUT
        /// </summary>
        [Description("PUT")]
        Put,
        /// <summary>
        /// DELETE
        /// </summary>
        [Description("DELETE")]
        Delete,
        /// <summary>
        /// OPTIONS
        /// </summary>
        [Description("OPTIONS")]
        Options,
        /// <summary>
        /// CONNECT
        /// </summary>
        [Description("CONNECT")]
        Connect,
        /// <summary>
        /// PATCH
        /// </summary>
        [Description("PATCH")]
        Patch,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom,
    }
}
