/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：DirectoryInfoExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：a142bd5c-05f2-4cf1-aa93-1ec7ab1f9f14
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:44:34
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:44:34
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 目录信息（DirectoryInfo）扩展
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        #region GetFiles(获取文件集合)
        /// <summary>
        /// 获取目录中所有文件，根据提供模式进行匹配文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="patterns">模式</param>
        /// <returns>匹配文件列表</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 	</code>
        /// </example>
        public static FileInfo[] GetFiles(this DirectoryInfo directory, params string[] patterns)
        {
            var files = new List<FileInfo>();
            foreach (var pattern in patterns)
            {
                files.AddRange(directory.GetFiles(pattern));
            }
            return files.ToArray();
        }
        #endregion

        #region FindFileRecursive(查找文件)
        /// <summary>
        /// 对指定目录递归搜索，并返回与提供的模式匹配的第一个文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="pattern">匹配模式</param>
        /// <returns>匹配文件</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive("win.ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, string pattern)
        {
            var files = directory.GetFiles(pattern);
            if (files.Length > 0)
            {
                return files[0];
            }
            return directory.GetDirectories().Select(subDirectory => subDirectory.FindFileRecursive(pattern)).FirstOrDefault(foundFile => foundFile != null);
        }
        /// <summary>
        /// 对指定目录递归搜索，并返回与提供的谓词匹配的第一个文件
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="predicate">谓词</param>
        /// <returns>匹配文件</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var file = directory.FindFileRecursive(f => f.Extension == ".ini");
        /// 	</code>
        /// </example>
        public static FileInfo FindFileRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
        {
            foreach (var file in directory.GetFiles())
            {
                if (predicate(file))
                {
                    return file;
                }
            }
            return directory.GetDirectories().Select(subDirectory => subDirectory.FindFileRecursive(predicate)).FirstOrDefault(foundFile => foundFile != null);
        }
        #endregion

        #region FindFilesRecursive(查找文件集合)
        /// <summary>
        /// 对指定目录递归搜索，并返回与提供的模式匹配的所有文件
        /// </summary>
        /// <param name="directory">源目录</param>
        /// <param name="pattern">路径</param>
        /// <returns>目录文件</returns>
        /// <example>
        /// 	<code>
        /// 		var directory = new DirectoryInfo(@"c:\");
        /// 		var files = directory.FindFilesRecursive("*.ini");
        /// 	</code>
        /// </example>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, string pattern)
        {
            var foundFiles = new List<FileInfo>();
            directory.FindFilesRecursive(pattern, foundFiles);
            return foundFiles.ToArray();
        }
        /// <summary>
        /// 对指定目录递归搜索，并返回与提供的模式匹配的所有文件
        /// </summary>
        /// <param name="directory">源目录</param>
        /// <param name="pattern">路径</param>
        /// <param name="foundFiles">目录文件</param>
        private static void FindFilesRecursive(this DirectoryInfo directory, string pattern,
            List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles(pattern));
            directory.GetDirectories().ForEach(d => d.FindFilesRecursive(pattern, foundFiles));
        }
        /// <summary>
        /// 对指定目录递归搜索，并返回与提供的谓词匹配的所有文件
        /// </summary>
        /// <param name="directory">源目录</param>
        /// <param name="predicate">目标目录</param>
        /// <returns>文件信息</returns>
        public static FileInfo[] FindFilesRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate)
        {
            var foundFiles = new List<FileInfo>();
            directory.FindFilesRecursive(predicate, foundFiles);
            return foundFiles.ToArray();
        }
        /// <summary>
        /// 对指定目录递归搜索
        /// </summary>
        /// <param name="directory">源目录</param>
        /// <param name="predicate">谓词</param>
        /// <param name="foundFiles">文件列表</param>
        private static void FindFilesRecursive(this DirectoryInfo directory, Func<FileInfo, bool> predicate,
            List<FileInfo> foundFiles)
        {
            foundFiles.AddRange(directory.GetFiles().Where(predicate));
            directory.GetDirectories().ForEach(d => d.FindFilesRecursive(predicate, foundFiles));
        }
        #endregion

        #region CopyTo(复制目录)
        /// <summary>
        /// 复制目录，将整个目录复制到另外一个路径中
        /// </summary>
        /// <param name="sourceDirectory">源目录</param>
        /// <param name="targetDirectoryPath">目标目录路径</param>
        /// <returns></returns>
        public static DirectoryInfo CopyTo(this DirectoryInfo sourceDirectory, string targetDirectoryPath)
        {
            var targetDirectory = new DirectoryInfo(targetDirectoryPath);
            sourceDirectory.CopyTo(targetDirectoryPath);
            return targetDirectory;
        }
        /// <summary>
        /// 复制目录，将整个目录复制到另外一个路径中
        /// </summary>
        /// <param name="sourceDirectory">源目录</param>
        /// <param name="targettDirectory">目标目录</param>
        public static void CopyTo(this DirectoryInfo sourceDirectory, DirectoryInfo targettDirectory)
        {
            if (targettDirectory.Exists == false)
            {
                targettDirectory.Create();
            }
            foreach (var childDirectory in sourceDirectory.GetDirectories())
            {
                childDirectory.CopyTo(Path.Combine(targettDirectory.FullName, childDirectory.Name));
            }
            foreach (var file in sourceDirectory.GetFiles())
            {
                file.CopyTo(Path.Combine(targettDirectory.FullName, file.Name));
            }
        }
        #endregion

    }
}
