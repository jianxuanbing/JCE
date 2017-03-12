/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils
 * 文件名：Sys
 * 版本号：v1.0.0.0
 * 唯一标识：8b32a9ee-e005-4be9-ad9a-310498d7a2d5
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/12 22:32:24
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/12 22:32:24
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Common;

namespace JCE.Utils
{
    /// <summary>
    /// 系统操作
    /// </summary>
    public class Sys
    {
        #region Line(换行符)
        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line
        {
            get { return Environment.NewLine; }
        }
        #endregion

        #region Guid(全局唯一标识符)
        /// <summary>
        /// 全局唯一标识符
        /// </summary>
        public static Guid Guid
        {
            get { return Guid.NewGuid(); }
        }
        #endregion

        #region GetType(获取类型)
        /// <summary>
        /// 获取类型,对可空类型进行处理
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static Type GetType<T>()
        {
            return Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
        }
        #endregion

        #region GetPhysicalPath(获取物理路径)
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string GetPhysicalPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return string.Empty;
            }
            if (HttpContext.Current == null)
            {
                if (relativePath.StartsWith("~"))
                {
                    relativePath = relativePath.Remove(0, 2);
                }
                return Path.GetFullPath(relativePath);
            }
            if (relativePath.StartsWith("~"))
            {
                return HttpContext.Current.Server.MapPath(relativePath);
            }
            if (relativePath.StartsWith("/") || relativePath.StartsWith("\\"))
            {
                return HttpContext.Current.Server.MapPath("~" + relativePath);
            }
            return HttpContext.Current.Server.MapPath("~/" + relativePath);
        }
        #endregion

        #region StartProcess(启动进程)
        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void StartProcess(string processName)
        {
            Process.Start(processName);
        }
        #endregion

        #region CurrentAppDomain(获取当前应用程序域)
        /// <summary>
        /// 获取当前应用程序域
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get { return Thread.GetDomain(); }
        }
        #endregion

        #region Clone(克隆对象)
        /// <summary>
        /// 使用序列化机制克隆一个对象
        /// </summary>
        /// <typeparam name="T">原始对象的类名</typeparam>
        /// <param name="instance">原始对象实例</param>
        /// <returns></returns>
        public static T Clone<T>(T instance) where T : class
        {
            byte[] buffer = SerializeUtil.ToBytes(instance);
            return SerializeUtil.FromBytes<T>(buffer);
        }
        #endregion

        #region GetMethodName(获取指定调用层级的方法名)
        /// <summary>
        /// 获取指定调用层级的方法名
        /// </summary>
        /// <param name="level">调用层级</param>
        /// <returns></returns>
        public static string GetMethodName(int level)
        {
            //创建一个堆栈跟踪
            StackTrace trace = new StackTrace();
            //获取指定调用层级的方法名
            return trace.GetFrame(level).GetMethod().Name;
        }
        #endregion
    }
}
