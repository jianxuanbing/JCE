/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.34209
 * 机器名称：JIAN
 * 命名空间：JCE.Core.Reflection
 * 文件名：DirectoryAssemblyFinder
 * 版本号：v1.0.0.0
 * 唯一标识：a06b7d0c-6996-49d0-ab6e-9597cd2a6d62
 * 当前的用户域：jian
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/14 星期四 14:18:51
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/14 星期四 14:18:51
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Reflection
{
    /// <summary>
    /// 目标程序集查找器
    /// </summary>
    public class DirectoryAssemblyFinder:IAllAssemblyFinder
    {
        private static readonly IDictionary<string,Assembly[]> AssembliesesDict=new Dictionary<string, Assembly[]>();
        private readonly string _path;

        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        public DirectoryAssemblyFinder() : this(GetBinPath())
        {
            
        }
        /// <summary>
        /// 初始化一个<see cref="DirectoryAssemblyFinder"/>类型的新实例
        /// </summary>
        /// <param name="path">bin目录路径</param>
        public DirectoryAssemblyFinder(string path)
        {
            _path = path;
        }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        public Assembly[] Find(Func<Assembly, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }

        /// <summary>
        /// 查找指定条件的项
        /// </summary>
        /// <returns></returns>
        public Assembly[] FindAll()
        {
            if (AssembliesesDict.ContainsKey(_path))
            {
                return AssembliesesDict[_path];
            }
            string[] files =
                Directory.GetFiles(_path, "*.dll", SearchOption.TopDirectoryOnly)
                    .Concat(Directory.GetFiles(_path, "*.exe", SearchOption.TopDirectoryOnly))
                    .ToArray();
            Assembly[] assemblies = files.Select(Assembly.LoadFrom).Distinct().ToArray();
            AssembliesesDict.Add(_path,assemblies);
            return assemblies;
        }

        /// <summary>
        /// 获取Bin路径
        /// </summary>
        /// <returns></returns>
        private static string GetBinPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(Path.Combine(path), "bin");
        }
    }
}
