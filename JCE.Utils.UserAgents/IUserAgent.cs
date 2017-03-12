/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：IUserAgent
 * 版本号：v1.0.0.0
 * 唯一标识：e1d1d669-91e8-442b-bf77-dcfa75de9526
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:37:43
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:37:43
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

namespace JCE.Utils.UserAgents
{
    /// <summary>
    /// 用户代理接口
    /// </summary>
    public interface IUserAgent
    {
        /// <summary>
        /// 原始用户访问设备信息
        /// </summary>
        string RawValue { get; set; }
        /// <summary>
        /// 用户代理信息
        /// </summary>
        UserAgentInfo UserAgent { get; }
        /// <summary>
        /// 设备信息
        /// </summary>
        DeviceInfo Device { get; }
        /// <summary>
        /// 系统信息
        /// </summary>
        OSInfo OS { get; }
        /// <summary>
        /// 是否机器人（爬虫）
        /// </summary>
        bool IsBot { get; }
        /// <summary>
        /// 是否移动设备
        /// </summary>
        bool IsMobileDevice { get; }
        /// <summary>
        /// 是否平板设备
        /// </summary>
        bool IsTablet { get; }    
        /// <summary>
        /// 是否PDF转换器
        /// </summary>
        bool IsPdfConverter { get; }
    }
}
