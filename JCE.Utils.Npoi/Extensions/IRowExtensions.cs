using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// <see cref="IRow"/> 行扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IRowExtensions
    {
        /// <summary>
        /// 获取或创建单元格
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cellIndex">单元格索引</param>
        /// <returns></returns>
        public static ICell GetOrCreateCell(this IRow row, int cellIndex)
        {
            return row.GetCell(cellIndex) ?? row.CreateCell(cellIndex);
        }

        /// <summary>
        /// 获取或创建单元格
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="cellIndex">单元格索引</param>
        /// <param name="action">单元格 操作</param>
        /// <returns></returns>
        public static IRow GetOrCreateCell(this IRow row, int cellIndex, Action<ICell> action)
        {
            var cell = row.GetOrCreateCell(cellIndex);
            if (action != null)
            {
                action(cell);
            }
            return row;
        }
    }
}
