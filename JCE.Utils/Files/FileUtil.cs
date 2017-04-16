/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Files
 * 文件名：FileUtil
 * 版本号：v1.0.0.0
 * 唯一标识：2f1dafae-6b2f-4a98-a317-05e5fbcb1161
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:10:37
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:10:37
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JCE.Utils.Extensions;

namespace JCE.Utils.Files
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    // 流类型转换
    public partial class FileUtil
    {
        #region GetContentType(根据扩展名获取文件内容类型)
        /// <summary>
        /// 根据扩展名获取文件内容类型
        /// </summary>
        /// <param name="extension">扩展名</param>
        /// <returns></returns>
        public static string GetContentType(string extension)
        {
            string contentType = "";
            var dict = Const.FileExtensionDict;
            extension = extension.ToLower();
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }
            dict.TryGetValue(extension, out contentType);
            return contentType;
        }
        #endregion

        #region Read(读取文件到字符串)
        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns></returns>
        public static string Read(string filePath)
        {
            return Read(filePath, Const.DefaultEncoding);
        }
        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Read(string filePath, Encoding encoding)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }
            using (StreamReader reader = new StreamReader(filePath, encoding))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion

        #region ReadToBytes(将文件读取到字节流中)
        /// <summary>
        /// 将文件读取到字节流中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns></returns>
        public static byte[] ReadToBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(filePath);
            int fileSize = (int)fileInfo.Length;
            using (BinaryReader reader = new BinaryReader(fileInfo.Open(FileMode.Open)))
            {
                return reader.ReadBytes(fileSize);
            }
        }
        #endregion

        #region Write(将字节流写入文件)
        /// <summary>
        /// 将字符串写入文件，文件不存在则创建
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">数据</param>
        public static void Write(string filePath, string content)
        {
            Write(filePath, ToBytes(content.ToStr()));
        }

        /// <summary>
        /// 将字符串写入文件，文件不存在则创建
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="bytes">数据</param>
        public static void Write(string filePath, byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }
            if (bytes == null)
            {
                return;
            }
            File.WriteAllBytes(filePath, bytes);
        }
        #endregion

        #region Delete(删除文件)
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePaths">文件集合的绝对路径</param>
        public static void Delete(IEnumerable<string> filePaths)
        {
            foreach (string filePath in filePaths)
            {
                Delete(filePath);
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void Delete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }
        #endregion

        #region KillFile(强力粉碎文件)
        /// <summary>
        /// 强力粉碎文件，如果文件被打开，很难粉碎
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <param name="deleteCount">删除次数</param>
        /// <param name="randomData">随机数据填充文件，默认true</param>
        /// <param name="blanks">空白填充文件，默认false</param>
        /// <returns>true:粉碎成功,false:粉碎失败</returns>        
        public static bool KillFile(string fileName, int deleteCount, bool randomData = true, bool blanks = false)
        {
            const int bufferLength = 1024000;
            bool ret = true;
            try
            {
                using (
                    FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite,
                        FileShare.ReadWrite))
                {
                    FileInfo file = new FileInfo(fileName);
                    long count = file.Length;
                    long offset = 0;
                    var rowDataBuffer = new byte[bufferLength];
                    while (count >= 0)
                    {
                        int iNumOfDataRead = stream.Read(rowDataBuffer, 0, bufferLength);
                        if (iNumOfDataRead == 0)
                        {
                            break;
                        }
                        if (randomData)
                        {
                            Random randomByte = new Random();
                            randomByte.NextBytes(rowDataBuffer);
                        }
                        else if (blanks)
                        {
                            for (int i = 0; i < iNumOfDataRead; i++)
                            {
                                rowDataBuffer[i] = Convert.ToByte(Convert.ToChar(deleteCount));
                            }
                        }
                        //写新内容到文件
                        for (int i = 0; i < deleteCount; i++)
                        {
                            stream.Seek(offset, SeekOrigin.Begin);
                            stream.Write(rowDataBuffer, 0, iNumOfDataRead);
                            ;
                        }
                        offset += iNumOfDataRead;
                        count -= iNumOfDataRead;
                    }
                }
                //每一个文件名字符代替随机数从0到9
                string newName = "";
                do
                {
                    Random random = new Random();
                    string cleanName = Path.GetFileName(fileName);
                    string dirName = Path.GetDirectoryName(fileName);
                    int iMoreRandomLetters = random.Next(9);
                    //为了更安全，不要只使用原文件名的大小，添加一些随机字母
                    for (int i = 0; i < cleanName.Length + iMoreRandomLetters; i++)
                    {
                        newName += random.Next(9).ToString();
                    }
                    newName = dirName + "\\" + newName;
                } while (File.Exists(newName));
                //重命名文件的新随机的名字
                File.Move(fileName, newName);
                File.Delete(newName);
            }
            catch
            {
                //可能其他原因删除失败，使用我们自己的方法强制删除
                try
                {
                    string filename = fileName;//要检查被哪个进程占用的文件
                    Process tool = new Process()
                    {
                        StartInfo =
                        {
                            FileName = "handle.exe",
                            Arguments = filename + " /accepteula",
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        }
                    };
                    tool.Start();
                    tool.WaitForExit();
                    string outputTool = tool.StandardOutput.ReadToEnd();
                    string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
                    foreach (Match match in Regex.Matches(outputTool, matchPattern))
                    {
                        //结束掉所有正在使用这个文件的程序
                        Process.GetProcessById(int.Parse(match.Value)).Kill();
                    }
                    File.Delete(filename);
                }
                catch
                {

                    ret = false;
                }
            }
            return ret;
        }
        #endregion

        #region GetAllFiles(获取目录中全部文件列表)
        /// <summary>
        /// 获取目录中全部文件列表，包括子目录
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        /// <returns></returns>
        public static List<string> GetAllFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();
        }
        #endregion

        #region GetEncoding(获取文件编码)
        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath)
        {
            return GetEncoding(filePath, Encoding.Default);
        }
        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="defaultEncoding">默认编码</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath, Encoding defaultEncoding)
        {
            Encoding targetEncoding = defaultEncoding;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4))
            {
                if (fs != null && fs.Length >= 2)
                {
                    long pos = fs.Position;
                    fs.Position = 0;
                    int[] buffer = new int[4];
                    buffer[0] = fs.ReadByte();
                    buffer[1] = fs.ReadByte();
                    buffer[2] = fs.ReadByte();
                    buffer[3] = fs.ReadByte();
                    fs.Position = pos;

                    if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                    {
                        targetEncoding = Encoding.BigEndianUnicode;
                    }
                    if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                    {
                        targetEncoding = Encoding.Unicode;
                    }
                    if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                    {
                        targetEncoding = Encoding.UTF8;
                    }
                }
            }
            return targetEncoding;
        }
        #endregion
    }
}
