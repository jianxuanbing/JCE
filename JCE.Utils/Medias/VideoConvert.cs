/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Medias
 * 文件名：VideoConvert
 * 版本号：v1.0.0.0
 * 唯一标识：60819406-e1d4-424a-8d1d-c82de71fe44d
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/2/15 20:45:17
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/2/15 20:45:17
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
using JCE.Utils.Common;
using JCE.Utils.Extensions;

namespace JCE.Utils.Medias
{
    /// <summary>
    /// 视频转换器
    /// </summary>
    public class VideoConvert
    {
        #region Fields(字段)

        private string[] _mencoder = new[] { "wmv", "rmvb", "rm" };
        private string[] _ffmpeg = new[] { "asf", "avi", "mpg", "3gp", "mov" };
        #endregion

        #region Property(属性)
        /// <summary>
        /// Ffmpeg 工具文件路径
        /// </summary>
        public static string FfmpegTool = ConfigUtil.GetAppSettings("ffmpeg");
        /// <summary>
        /// Mencoder 工具文件路径
        /// </summary>
        public static string MencoderTool = ConfigUtil.GetAppSettings("mencoder");
        /// <summary>
        /// 保存文件目录
        /// </summary>
        public static string SaveFile = ConfigUtil.GetAppSettings("savefile") + "/";
        /// <summary>
        /// 缩略图文件大小
        /// </summary>
        public static string SizeOfImg = ConfigUtil.GetAppSettings("CatchFlvImgSize");
        /// <summary>
        /// 视频宽度
        /// </summary>
        public static string WidthOfFile = ConfigUtil.GetAppSettings("widthSize");
        /// <summary>
        /// 视频高度
        /// </summary>
        public static string HeightOfFile = ConfigUtil.GetAppSettings("heightSize");
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="VideoConvert"/>类型的实例
        /// </summary>
        public VideoConvert()
        {

        }
        #endregion

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="extension">后缀名</param>
        /// <returns></returns>
        public string GetFileType(string extension)
        {
            string result = string.Empty;
            if (this._ffmpeg.Any(key => key == extension))
            {
                result = "ffmpeg";
            }
            if (result.IsEmpty())
            {
                if (this._mencoder.Any(key => key == extension))
                {
                    result = "mencoder";
                }
            }
            return result;
        }
        /// <summary>
        /// 视频格式转换为Flv格式
        /// </summary>
        /// <param name="filePath">原视频文件路径</param>
        /// <param name="exportPath">生成后的Flv文件路径</param>
        /// <returns></returns>
        public bool ToFlv(string filePath, string exportPath)
        {
            if (!File.Exists(FfmpegTool) || (!File.Exists(Sys.GetPhysicalPath(filePath))))
            {
                return false;
            }
            filePath = Sys.GetPhysicalPath(filePath);
            exportPath = Sys.GetPhysicalPath(exportPath);

            string command = string.Format(" -i \"{0}\" -y -ab 32 -ar 22050 -b 800000 -s 480*360 \"{1}\"", filePath,
                exportPath);

            Process process = new Process();
            process.StartInfo.FileName = FfmpegTool;
            process.StartInfo.Arguments = command;
            process.StartInfo.WorkingDirectory = Sys.GetPhysicalPath("~/tools/");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = false;
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
            process.Dispose();
            return true;
        }

        /// <summary>
        /// 生成Flv视频的缩略图
        /// </summary>
        /// <param name="filePath">视频文件路径</param>
        /// <returns>缩略图路径</returns>
        public string CreateThumbnail(string filePath)
        {
            if (!File.Exists(FfmpegTool) || (!File.Exists(Sys.GetPhysicalPath(filePath))))
            {
                return "";
            }

            try
            {
                string flvImgPath = filePath.Substring(0, filePath.Length - 4) + ".jpg";
                string command = string.Format(" -i {0} -y -f image2 -t 0.1 -s {1} {2}", Sys.GetPhysicalPath(filePath),
                    SizeOfImg, Sys.GetPhysicalPath(flvImgPath));
                Process process=new Process();
                process.StartInfo.FileName = FfmpegTool;
                process.StartInfo.Arguments = command;
                process.StartInfo.WindowStyle=ProcessWindowStyle.Normal;
                try
                {
                    process.Start();
                }
                catch
                {
                    return "";
                }
                finally
                {
                    process.Close();
                    process.Dispose();
                }
                Thread.Sleep(4000);
                if (File.Exists(Sys.GetPhysicalPath(flvImgPath)))
                {
                    return flvImgPath;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 生成视频缩略图
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="imgPath">图片路径</param>
        /// <returns></returns>
        public string CreateThumbnail(string fileName, string imgPath)
        {
            string ffmpeg = Sys.GetPhysicalPath(FfmpegTool);
            string flvImg = imgPath + ".jpg";
            string flvImgSize = SizeOfImg;
            string command = string.Format(" -i {0} -y -f image2 -ss 2 -vframes 1 -s {1} {2}", fileName, flvImgSize,
                flvImg);
            ProcessStartInfo imgStartInfo=new ProcessStartInfo(ffmpeg);
            imgStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
            imgStartInfo.Arguments = command;

            try
            {
                Process.Start(imgStartInfo);
            }
            catch
            {
                return "";                
            }
            if (File.Exists(flvImg))
            {
                return flvImg;
            }
            return "";
        }

        /// <summary>
        /// 运行FFMpeg的视频解码（绝对路径）
        /// </summary>
        /// <param name="filePath">上传视频文件的路径（原文件）</param>
        /// <param name="playPath">转换后的文件路径（网络播放文件）</param>
        /// <param name="imgPath">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟路径，失败:返回空字符串</returns>
        public string FfmpegConvertPhy(string filePath, string playPath, string imgPath)
        {
            string ffmpeg = Sys.GetPhysicalPath(FfmpegTool);
            if ((!File.Exists(ffmpeg)) || (!File.Exists(filePath)))
            {
                return "";
            }
            string flvFile = Path.ChangeExtension(playPath, ".flv");
            string command = string.Format(" -i {0} -ab 56 -ar 22050 -b 500 -r 15 -s {1}x{2} {3}", filePath, WidthOfFile,
                HeightOfFile, flvFile);
            ProcessStartInfo fileStartInfo=new ProcessStartInfo(ffmpeg);
            fileStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
            fileStartInfo.Arguments = command;
            try
            {
                Process.Start(fileStartInfo);//转换
                return CreateThumbnail(filePath, imgPath);//截图
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 运行FFMpeg的视频解码（绝对路径）
        /// </summary>
        /// <param name="filePath">上传视频文件的路径（原文件）</param>
        /// <param name="playPath">转换后的文件路径（网络播放文件）</param>
        /// <param name="imgPath">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟路径，失败:返回空字符串</returns>
        public string FfmpegConvertVir(string filePath, string playPath, string imgPath)
        {
            string ffmpeg = Sys.GetPhysicalPath(FfmpegTool);
            if ((!File.Exists(ffmpeg)) || (!File.Exists(filePath)))
            {
                return "";
            }
            string flvImg = Path.ChangeExtension(Sys.GetPhysicalPath(imgPath), ".jpg");
            string flvFile = Path.ChangeExtension(Sys.GetPhysicalPath(playPath), ".flv");

            ProcessStartInfo imgStartInfo=new ProcessStartInfo(ffmpeg);
            string imgCommand = string.Format(" -i {0} -y -f image2 -t 0.001 -s {1} {2}", filePath, SizeOfImg,
                flvImg);
            imgStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
            imgStartInfo.Arguments = imgCommand;

            ProcessStartInfo flvStartInfo=new ProcessStartInfo(ffmpeg);
            string flvCommand = string.Format(" -i {0} -ab 56 -ar 22050 -b 500 -r 15 -s {1}x{2} {3}", filePath,
                WidthOfFile,
                HeightOfFile, flvFile);
            flvStartInfo.WindowStyle=ProcessWindowStyle.Hidden;
            flvStartInfo.Arguments = flvCommand;

            try
            {
                Process.Start(flvStartInfo);
                Process.Start(imgStartInfo);
            }
            catch
            {
                return "";                
            }
            //注意:图片截取成功后,数据由内存缓存写到磁盘需要时间较长,大概在3,4秒甚至更长;   
            //这儿需要延时后再检测,我服务器延时8秒,即如果超过8秒图片仍不存在,认为截图失败;
            if (File.Exists(flvImg))
            {
                return flvImg;
            }
            return "";
        }
        /// <summary>
        /// 运行Mencoder的视频解码（绝对路径）
        /// </summary>
        /// <param name="filePath">上传视频文件的路径（原文件）</param>
        /// <param name="playPath">转换后的文件路径（网络播放文件）</param>
        /// <param name="imgPath">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟路径，失败:返回空字符串</returns>
        public string MencoderConvertPhy(string filePath, string playPath, string imgPath)
        {
            string mencoder = Sys.GetPhysicalPath(MencoderTool);
            if ((!File.Exists(mencoder)) || (!File.Exists(filePath)))
            {
                return "";
            }
            string flvFile = Path.ChangeExtension(playPath, ".flv");
            string command = string.Format(" {0} -o {1} -of lavf -lavfopts i_certify_that_my_video_stream_does_not_use_b_frames -oac mp3lame -lameopts abr:br=56 -ovc lavc -lavcopts vcodec=flv:vbitrate=200:mbd=2:mv0:trell:v4mv:cbp:last_pred=1:dia=-1:cmp=0:vb_strategy=1 -vf scale={2}:{3} -ofps 12 -srate 22050",filePath,flvFile,WidthOfFile,HeightOfFile);            
            ProcessStartInfo fileStartInfo = new ProcessStartInfo(mencoder);
            fileStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            fileStartInfo.Arguments = command;
            try
            {
                Process.Start(fileStartInfo);//转换
                return CreateThumbnail(filePath, imgPath);//截图
            }
            catch
            {
                return "";
            }
        }
    }
}
