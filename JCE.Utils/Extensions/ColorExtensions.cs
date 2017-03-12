/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：ColorExtensions
 * 版本号：v1.0.0.0
 * 唯一标识：7f7de9e5-5380-4216-84fb-f42ef7eb1298
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:39:36
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:39:36
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 颜色（Color）扩展
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// 转为RGB颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>RGB颜色值</returns>
        public static string ToHtmlColor(this Color color)
        {
            return ColorTranslator.ToHtml(color);
        }
        /// <summary>
        /// 转为OLE颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>OLE颜色值</returns>
        public static int ToOleColor(this Color color)
        {
            return ColorTranslator.ToOle(color);
        }
        /// <summary>
        /// 转为Windows颜色
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>Windows颜色值</returns>
        public static int ToWin32Color(this Color color)
        {
            return ColorTranslator.ToWin32(color);
        }
    }
}
