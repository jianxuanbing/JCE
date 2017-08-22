using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// <see cref="ISheet"/> 工作表扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ISheetExtensions
    {
        /// <summary>
        /// 获取或创建行
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns></returns>
        public static IRow GetOrCreateRow(this ISheet sheet, int rowIndex)
        {
            return sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
        }

        /// <summary>
        /// 获取或创建行
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="action">行 操作</param>
        /// <returns></returns>
        public static ISheet GetOrCreateRow(this ISheet sheet, int rowIndex, Action<IRow> action)
        {
            var row = sheet.GetOrCreateRow(rowIndex);
            if (action != null)
            {
                action(row);
            }
            return sheet;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="firstRow">起始行索引</param>
        /// <param name="lastRow">结束行索引</param>
        /// <param name="firstCol">起始列索引</param>
        /// <param name="lastCol">结束列索引</param>
        /// <returns></returns>
        public static CellRangeAddress MergeCell(this ISheet sheet, int firstRow, int lastRow, int firstCol, int lastCol)
        {
            var region=new CellRangeAddress(firstRow,lastRow,firstCol,lastCol);
            sheet.AddMergedRegion(region);
            return region;
        }
    }
}
