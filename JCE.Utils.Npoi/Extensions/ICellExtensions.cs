using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Common;
using NPOI.SS.UserModel;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// <see cref="ICell"/> 单元格扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ICellExtensions
    {
        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">值</param>
        /// <param name="type">类型</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="format">格式化字符串</param>
        public static void SetCellValueExt(this ICell cell, object value, Type type,Type enumType,
            string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (value == null)
            {
                cell.SetCellValue("");
            }
            else if (type == typeof(bool))
            {
                cell.SetCellValue(value.CastTo<bool>());                
            }
            else if (type == typeof(DateTime))
            {
                cell.SetCellValue(value.CastTo<DateTime>());
                cell.SetCellStyle(
                    style => style.SetDataFormat(cell.Row.Sheet.Workbook.CreateDateFormat(format)));                
            }
            else if (type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long))
            {
                cell.SetCellValue(value.CastTo<int>());
            }
            else if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
            {
                cell.SetCellValue(value.CastTo<double>());
            }
            else if (value is IFormattable)
            {
                var fv = value as IFormattable;
                cell.SetCellValue(fv.ToString(format, CultureInfo.CurrentCulture));
            }
            else
            {
                cell.SetCellValue(value.ToString());
            }

            if (enumType != null && enumType.IsEnum)
            {
                var enumDesc=EnumUtil.GetDescription(enumType, value);
                cell.SetCellValue(enumDesc);
            }
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">值</param>
        /// <param name="format">格式化字符串</param>
        public static void SetCellValueExt(this ICell cell, object value,string format= "yyyy-MM-dd HH:mm:ss")
        {
            if (value == null)
            {
                return;
            }
            switch (value.GetType().ToString())
            {
                case "System.String":
                    cell.SetCellValue(value.ToString());
                    break;
                case "System.DateTime":
                    cell.SetCellValue(value.CastTo<DateTime>());
                    cell.SetCellStyle(
                        style => style.SetDataFormat(cell.Row.Sheet.Workbook.CreateDateFormat(format)));
                    break;
                case "System.Boolean":
                    cell.SetCellValue(value.CastTo<bool>());
                    break;
                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    cell.SetCellValue(value.CastTo<int>());
                    break;
                case "System.Double":
                case "System.Decimal":
                    cell.SetCellValue(value.CastTo<double>());
                    break;
                case "System.DBNull":
                default:
                    cell.SetCellValue("");
                    break;
            }
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        public static object GetCellValue(this ICell cell)
        {
            object value = null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Error:
                    value = cell.ErrorCellValue;
                    break;
                case CellType.Formula:
                    value = cell.StringCellValue;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue;
                    break;
                case CellType.String:
                    value = cell.StringCellValue;
                    break;
                case CellType.Unknown:
                    break;
                default:
                    break;
            }
            return value;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        public static T GetCellValue<T>(this ICell cell)
        {
            return cell.GetCellValue().CastTo<T>();
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cell">单元格</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetCellValue<T>(this ICell cell, T defaultValue)
        {
            return cell.GetCellValue().CastTo<T>(defaultValue);
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellStyle">单元格样式</param>
        /// <returns></returns>
        public static ICell SetCellStyle(this ICell cell, ICellStyle cellStyle)
        {
            cell.CellStyle = cellStyle;
            return cell;
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="action">单元格样式</param>
        /// <returns></returns>
        public static ICell SetCellStyle(this ICell cell,
            Action<ICellStyle> action)
        {
            var cellStyle = cell.Row.Sheet.Workbook.CreateCellStyle();
            if (cell.CellStyle != null)
            {
                cell.CellStyle.CloneStyleFrom(cellStyle);
            }
            if (action != null)
            {
                action(cellStyle);
            }
            cell.CellStyle = cellStyle;
            return cell;
        }
    }
}
