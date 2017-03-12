/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：VersionString
 * 版本号：v1.0.0.0
 * 唯一标识：8c74bffc-b955-4346-9594-73d19336c5a2
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:43:33
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:43:33
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
    /// 版本字符串
    /// </summary>
    internal static class VersionString
    {
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="parts">参数</param>
        /// <returns></returns>
        public static string Format(params string[] parts)
        {
            return string.Join(".", (from v in parts where !string.IsNullOrEmpty(v) select v).ToArray<string>());
        }
    }
}
