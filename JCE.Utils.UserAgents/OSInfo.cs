/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：OSInfo
 * 版本号：v1.0.0.0
 * 唯一标识：02cc6ac8-127b-4888-8554-8062028f64d0
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:40:25
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:40:25
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
    /// 系统信息
    /// </summary>
    public sealed class OSInfo
    {
        public string Family { get; private set; }
        public string Major { get; private set; }
        public string Minor { get; private set; }
        public string Patch { get; private set; }
        public string PatchMinor { get; private set; }

        public OSInfo(string family, string major, string minor, string patch, string patchMinor)
        {
            this.Family = family;
            this.Major = major;
            this.Minor = minor;
            this.Patch = patch;
            this.PatchMinor = patchMinor;
        }
        /// <summary>
        /// 重写，返回版本信息字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = VersionString.Format(Major, Minor, Patch, PatchMinor);
            return (this.Family + (!string.IsNullOrEmpty(str) ? (" " + str) : null));
        }        
    }
}
