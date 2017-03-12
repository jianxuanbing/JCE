/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Medias
 * 文件名：Mp3Util
 * 版本号：v1.0.0.0
 * 唯一标识：43dd1df6-b1ec-44da-9cc1-38c7187958d6
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 22:36:26
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 22:36:26
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Medias
{
    /// <summary>
    /// Mp3文件播放操作工具类
    /// </summary>
    public class Mp3Util
    {
        /// <summary>
        /// MCI发送文本
        /// </summary>
        /// <param name="strCommand">命令</param>
        /// <param name="strReturn">返回内容</param>
        /// <param name="iReturnLength">返回长度</param>
        /// <param name="hwndCallback">回调指针</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength,
            IntPtr hwndCallback);
        /// <summary>
        /// 播放Mp3音乐
        /// </summary>
        /// <param name="mp3FielName">mp3文件名</param>
        /// <param name="repeat">是否循环播放,true:是,false:否</param>
        public static void Play(string mp3FielName, bool repeat)
        {
            mciSendString($"open \"{mp3FielName}\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString($"play MediaFile{(repeat ? " repeat" : string.Empty)}", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 播放Mp3音乐
        /// </summary>
        /// <param name="mp3EmbeddedResource">内嵌的mp3资源</param>
        /// <param name="repeat">是否循环播放,true:是,false:否</param>
        public static void Play(byte[] mp3EmbeddedResource, bool repeat)
        {
            ExtractResource(mp3EmbeddedResource, Path.GetTempPath() + "resource.tmp");
            mciSendString($"open \"{(Path.GetTempPath() + "resource.tmp")}\" type mpegvideo alias MediaFile", null, 0,
                IntPtr.Zero);
            mciSendString($"play MediaFile{(repeat ? " repeat" : string.Empty)}", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        public static void Pause()
        {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public static void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 提取资源
        /// </summary>
        /// <param name="res">字节数组</param>
        /// <param name="filePath">文件路径</param>
        private static void ExtractResource(byte[] res, string filePath)
        {
            FileStream fs;
            BinaryWriter bw;
            if (!File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs);

                foreach (var b in res)
                {
                    bw.Write(b);
                }
                bw.Close();
                fs.Close();
            }
        }
    }
}
