/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：AssemblyExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：b3971dfb-c3c2-46f4-a9c5-c26d21e11903
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:25:23
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:25:23
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 程序集（Assembly）扩展
    /// </summary>
    public static class AssemblyExtensions
    {
        #region GetFileVersion(获取程序集的文件版本号)
        /// <summary>
        /// 获取程序集的文件版本号
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>程序集文件版本号</returns>
        public static Version GetFileVersion(this Assembly assembly)
        {
            assembly.CheckNotNull("assembly");
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.FileVersion);
        }
        #endregion

        #region GetProductVersion(获取程序集的产品版本)
        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>程序集产品版本</returns>
        public static Version GetProductVersion(this Assembly assembly)
        {
            assembly.CheckNotNull("assembly");
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.ProductVersion);
        }
        #endregion
        
    }
}
