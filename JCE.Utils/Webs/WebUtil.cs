using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JCE.Utils.Files;

namespace JCE.Utils.Webs
{
    /// <summary>
    /// Web帮助类 - File操作
    /// </summary>
    public partial class WebUtil
    {
        #region DownloadFile(下载文件)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        public static void DownloadFile(string filePath, string fileName)
        {
            DownloadFile(filePath, fileName, Encoding.UTF8);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void DownloadFile(string filePath, string fileName, Encoding encoding)
        {
            var bytes = FileUtil.ReadToBytes(filePath);
            Download(bytes, fileName, encoding);
        }
        #endregion

        #region DownloadUrl(从Http地址下载)
        /// <summary>
        /// 从Http地址下载
        /// </summary>
        /// <param name="url">Http地址，范例：http://www.test.com/a.rar</param>
        /// <param name="fileName">文件名，包括扩展名</param>
        public static void DownloadUrl(string url, string fileName)
        {
            DownloadUrl(url, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 从Http地址下载
        /// </summary>
        /// <param name="url">Http地址，范例：http://www.test.com/a.rar</param>
        /// <param name="fileName">文件名，包括扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void DownloadUrl(string url, string fileName, Encoding encoding)
        {
            var client = new WebClient();
            var bytes = client.DownloadData(url);
            Download(bytes, fileName, encoding);
        }
        #endregion

        #region Download(下载)
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        public static void Download(string text, string fileName)
        {
            Download(text, fileName, Encoding.UTF8);
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download(string text, string fileName, Encoding encoding)
        {
            var bytes = FileUtil.ToBytes(text, encoding);
            Download(bytes, fileName, encoding);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        public static void Download(Stream stream, string fileName)
        {
            Download(stream, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download(Stream stream, string fileName, Encoding encoding)
        {
            Download(FileUtil.ToBytes(stream), fileName, encoding);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名，包括扩展名</param>
        public static void Download(byte[] bytes, string fileName)
        {
            Download(bytes, fileName, Encoding.UTF8);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <param name="encoding">字符编码</param>
        public static void Download(byte[] bytes, string fileName, Encoding encoding)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return;
            }
            HttpContext.Current.Response.ContentType = "application/cotet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment;filename=" + UrlUtil.UrlEncode(fileName.Replace(" ", "")));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.ContentEncoding = encoding;
            HttpContext.Current.Response.End();
        }
        #endregion
    }
}
