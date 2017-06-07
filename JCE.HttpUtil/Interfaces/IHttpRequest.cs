/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.HttpUtil.Interfaces
 * 文件名：IHttpRequest
 * 版本号：v1.0.0.0
 * 唯一标识：f52b3600-2e74-493d-a69e-c7d09bbaf300
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/4/18 23:58:07
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/4/18 23:58:07
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

namespace JCE.HttpUtil.Interfaces
{
    /// <summary>
    /// 用于描述 HTTP 请求时的参数
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// 基路径
        /// </summary>
        Uri BaseUrl { get; set; }

        /// <summary>
        /// 基路径的相对路径
        /// </summary>
        string Path { get; set; }

        
    }
}
