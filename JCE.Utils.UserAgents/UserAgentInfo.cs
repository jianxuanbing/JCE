/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：UserAgentInfo
 * 版本号：v1.0.0.0
 * 唯一标识：b503f84a-428e-4315-b91c-2dc59892cd51
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:47:06
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:47:06
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
    /// 用户代理信息
    /// </summary>
    public sealed class UserAgentInfo
    {
        public string Family { get; private set; }
        public string Major { get; private set; }
        public string Minor { get; private set; }
        public string Patch { get; private set; }

        public UserAgentInfo(string family, string major, string minor, string patch)
        {
            this.Family = family;
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
        }
        /// <summary>
        /// 重写方法，返回用户代理信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = VersionString.Format(Major, Minor, Patch);
            return (this.Family + (!string.IsNullOrEmpty(str) ? (" " + str) : null));
        }
    }
}
