/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Medias
 * 文件名：ExifThumbnail
 * 版本号：v1.0.0.0
 * 唯一标识：599c3f0c-4b53-4d17-acda-b36020c1c8b4
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 22:15:12
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 22:15:12
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Medias
{
    /// <summary>
    /// Exif缩略图信息
    /// </summary>
    public class ExifThumbnail
    {
        /// <summary>
        /// 从GDI Plus中获取属性项
        /// </summary>
        /// <param name="image">图片指针</param>
        /// <param name="propid">属性ID</param>
        /// <param name="size">长度</param>
        /// <param name="buffer">缓存</param>
        /// <returns></returns>
        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipGetPropertyItem(IntPtr image, int propid, int size, IntPtr buffer);
        /// <summary>
        /// 从GDI Plus中获取图片大小
        /// </summary>
        /// <param name="image">图片指针</param>
        /// <param name="propid">属性ID</param>
        /// <param name="size">输出图片大小</param>
        /// <returns></returns>
        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipGetPropertyItemSize(IntPtr image, int propid, out int size);
        /// <summary>
        /// 从GDI Plus中获取图片文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="image">输出图片指针</param>
        /// <returns></returns>
        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int GdipLoadImageFromFile(string filename, out IntPtr image);
        /// <summary>
        /// 从GDI Plus中释放图片
        /// </summary>
        /// <param name="image">图片指针</param>
        /// <returns></returns>
        [DllImport("gdiplus.dll", EntryPoint = "GdipDisposeImage", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int GdipDisposeImage(IntPtr image);
        /// <summary>
        /// 缩略图数据大小
        /// </summary>
        private static int THUMBNAIL_DATA = 0x501B;

        /// <summary>
        /// 读取缩略图
        /// </summary>
        /// <param name="imagePath">图片文件路径</param>
        /// <returns></returns>
        public Image ReadThumb(string imagePath)
        {
            const int GDI_ERR_PROP_NOT_FOUND = 19;//属性无法找到的错误
            const int GDI_ERR_OUT_OF_MEMORY = 3;

            IntPtr hImage = IntPtr.Zero;
            IntPtr buffer = IntPtr.Zero;//保存缩略图数据
            int ret;
            ret = GdipLoadImageFromFile(imagePath, out hImage);

            try
            {
                if (ret != 0)
                {
                    throw CreateException(ret);
                }
                int propSize;
                ret = GdipGetPropertyItemSize(hImage, THUMBNAIL_DATA, out propSize);
                //如果图片没有缩略图数据，则返回Null
                if (ret == GDI_ERR_PROP_NOT_FOUND)
                {
                    return null;
                }
                if (ret != 0)
                {
                    throw CreateException(ret);
                }
                //分配缓冲区
                buffer = Marshal.AllocHGlobal(propSize);
                if (buffer == IntPtr.Zero)
                {
                    throw CreateException(GDI_ERR_OUT_OF_MEMORY);
                }
                ret = GdipGetPropertyItem(hImage, THUMBNAIL_DATA, propSize, buffer);
                if (ret != 0)
                {
                    throw CreateException(ret);
                }
                //如果缓存包含缩略图数据，则需要保存并转换成图片
                return ConvertFromMemory(buffer);
            }
            finally
            {
                //释放缓存
                if (buffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(buffer);
                }
                GdipDisposeImage(hImage);
            }
        }

        /// <summary>
        /// 根据GDI+ 错误码生成异常信息
        /// </summary>
        /// <param name="gdipErrorCode">GDI+错误码</param>
        /// <returns></returns>
        private static Exception CreateException(int gdipErrorCode)
        {
            switch (gdipErrorCode)
            {
                case 1:
                    return new ExternalException("GDI+ Generic Error", -2147467259);
                case 2:
                    return new ArgumentException("GDI+ Invalid Parameter");
                case 3:
                    return new OutOfMemoryException("GDI+ Out Of Memory");
                case 4:
                    return new InvalidOperationException("GDI+ Object Busy");
                case 5:
                    return new OutOfMemoryException("GDI+ Insufficient Buffer");
                case 7:
                    return new ExternalException("GDI+ Generic Error", -2147467259);
                case 8:
                    return new InvalidOperationException("GDI+ Wrong State");
                case 9:
                    return new ExternalException("GDI+ Aborted", -2147467260);
                case 10:
                    return new FileNotFoundException("GDI+ File Not Found");
                case 11:
                    return new OverflowException("GDI+ Over Flow");
                case 12:
                    return new ExternalException("GDI+ Access Denied", -2147024891);
                case 13:
                    return new ArgumentException("GDI+ Unknow Image Format");
                case 18:
                    return new ExternalException("GDI+ Not Initialized", -2147467259);
                case 20:
                    return new ArgumentException("GDI+ Property Not Supported Error");
            }
            return new ExternalException("GDI+ Unkown Error", -2147418113);
        }
        /// <summary>
        /// 将指针缓存数据转换成属性项以及图片
        /// </summary>
        /// <param name="thumbData">指针缓存数据</param>
        /// <returns></returns>
        private static Image ConvertFromMemory(IntPtr thumbData)
        {
            PropertyItemInternal prop =
                (PropertyItemInternal)Marshal.PtrToStructure(thumbData, typeof(PropertyItemInternal));

            //图片数据来自于字节数组，写入所有字节数组到缓存中并从缓存中创建新的图片
            byte[] imageBytes = prop.Value;
            MemoryStream stream = new MemoryStream(imageBytes.Length);
            stream.Write(imageBytes, 0, imageBytes.Length);

            return Image.FromStream(stream);
        }
        /// <summary>
        /// 内部属性项
        /// Used in Marshal.PtrToStructure().
        /// We need this dummy class because Imaging.PropertyItem is not a "blittable"
        /// class and Marshal.PtrToStructure only accepted blittable classes.
        /// (It's not blitable because it uses a byte[] array and that's not a blittable
        /// type. See MSDN for a definition of Blittable.)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class PropertyItemInternal
        {
            public int id = 0;
            public int len = 0;
            public short type = 0;
            public IntPtr value = IntPtr.Zero;

            public byte[] Value
            {
                get
                {
                    byte[] bytes = new byte[(uint)len];
                    Marshal.Copy(value, bytes, 0, len);
                    return bytes;
                }
            }
        }
    }
}
