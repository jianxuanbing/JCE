using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// <see cref="IFont"/> 字体扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IFontExtensions
    {
        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns></returns>
        public static IFont SetFontSize(this IFont font, short fontSize)
        {
            font.FontHeightInPoints = fontSize;
            return font;
        }

        /// <summary>
        /// 设置字体颜色
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="color">字体颜色</param>
        /// <returns></returns>
        public static IFont SetColor(this IFont font, short color)
        {
            font.Color = color;
            return font;
        }

        /// <summary>
        /// 设置粗体
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="boldweight">粗体大小</param>
        /// <returns></returns>
        public static IFont SetBoldweight(this IFont font, short boldweight)
        {
            font.Boldweight = boldweight;
            return font;
        }
    }
}
