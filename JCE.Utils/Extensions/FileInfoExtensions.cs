/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：FileInfoExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：ed611e35-4705-4769-a910-ad03209a8b92
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:47:51
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:47:51
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
using JCE.Utils.Exceptions;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 文件信息（FileInfo）扩展
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="newName">新名</param>
        /// <returns>重命名文件</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, string newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }
        /// <summary>
        /// 文件重命名，并不改变扩展名
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="newName">新名</param>
        /// <returns>重命名文件</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.RenameFileWithoutExtension("test3");
        /// 	</code>
        /// </example>
        public static FileInfo RenameFileWithoutExtension(this FileInfo file, string newName)
        {
            var fileName = string.Concat(newName, file.Extension);
            file.Rename(fileName);
            return file;
        }
        /// <summary>
        /// 更改文件扩展名
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="newExtension">新扩展名</param>
        /// <returns>重命名文件</returns>
        /// <example>
        /// 	<code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.ChangeExtension("xml");
        /// 	</code>
        /// </example>
        public static FileInfo ChangeExtension(this FileInfo file, string newExtension)
        {
            newExtension = newExtension.EnsureStartsWith(".");
            var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
            file.Rename(fileName);
            return file;
        }
        /// <summary>
        /// 批量更改文件扩展名
        /// </summary>
        /// <param name="files">批量文件</param>
        /// <param name="newExtension">新扩展名</param>
        /// <returns>重命名文件</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.ChangeExtensions("tmp");
        /// 	</code>
        /// </example>
        public static FileInfo[] ChangeExtensions(this FileInfo[] files, string newExtension)
        {
            files.ForEach(f => f.ChangeExtension(newExtension));
            return files;
        }
        /// <summary>
        /// 批量删除文件，忽略中断错误
        /// </summary>
        /// <param name="files">批量文件</param>
        /// <param name="consolidateExceptions">是否合并异常，失败不中断</param>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files, bool consolidateExceptions = true)
        {
            if (consolidateExceptions)
            {
                List<Exception> exceptions = new List<Exception>();
                foreach (var file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
                if (exceptions.Any())
                {
                    throw JceException.Combine(
                        "Error while deleting one or several files, see InnerExceptions array for details.", exceptions);
                }
            }
            else
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }
        }
        /// <summary>
        /// 批量复制文件到新的文件夹，忽略中断错误
        /// </summary>
        /// <param name="files">批量文件</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="consolidateExceptions">是否合并异常，失败不中断</param>
        /// <returns>新创建的文件副本</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, string targetPath, bool consolidateExceptions = true)
        {
            var copiedFiles = new List<FileInfo>();
            List<Exception> exceptions = null;
            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    copiedFiles.Add(file.CopyTo(fileName));
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                        {
                            exceptions = new List<Exception>();
                        }
                        exceptions.Add(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            if ((exceptions != null) && (exceptions.Count > 0))
            {
                throw JceException.Combine(
                    "Error while copying one or several files, see InnerExceptions array for details.",
                    exceptions.ToArray());
            }
            return copiedFiles.ToArray();
        }
        /// <summary>
        /// 批量剪切文件到新的文件夹，忽略中断错误
        /// </summary>
        /// <param name="files">批量文件</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="consolidateExceptions">是否合并异常，失败不中断</param>
        /// <returns>剪切后的文件</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, string targetPath, bool consolidateExceptions = true)
        {
            List<Exception> exceptions = null;
            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                        {
                            exceptions = new List<Exception>();
                        }
                        exceptions.Add(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            if ((exceptions != null) && (exceptions.Count > 0))
            {
                throw JceException.Combine(
                    "Error while copying one or several files, see InnerExceptions array for details.",
                    exceptions.ToArray());
            }
            return files;
        }
        /// <summary>
        /// 批量设置文件属性
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="attributes">文件属性</param>
        /// <returns>更改后的文件</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributes(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributes(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
            {
                file.Attributes = attributes;
            }
            return files;
        }
        /// <summary>
        /// 批量添加文件属性（任何现有属性）
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="attributes">文件属性</param>
        /// <returns>更改后的文件</returns>
        /// <example>
        /// 	<code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributesAdditive(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributesAdditive(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
            {
                file.Attributes = (file.Attributes | attributes);
            }
            return files;
        }
    }
}
