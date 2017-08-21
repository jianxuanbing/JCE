using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// ICellStyle 单元格样式扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal static class ICellStyleExtensions
    {
        /// <summary>
        /// 设置数据格式
        /// </summary>
        /// <param name="cellStyle">单元格样式</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns></returns>
        public static ICellStyle SetDataFormat(this ICellStyle cellStyle, short dataFormat)
        {
            cellStyle.DataFormat = dataFormat;
            return cellStyle;
        }

        /// <summary>
        /// 设置水平对齐
        /// </summary>
        /// <param name="cellStyle">单元格样式</param>
        /// <param name="horizontalAlignment">水平对齐</param>
        /// <returns></returns>
        public static ICellStyle SetHorizontalAlignment(this ICellStyle cellStyle,
            HorizontalAlignment horizontalAlignment)
        {
            cellStyle.Alignment = horizontalAlignment;
            return cellStyle;
        }
    }
}
