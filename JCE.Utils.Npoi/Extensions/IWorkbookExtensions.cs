using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace JCE.Utils.Npoi.Extensions
{
    /// <summary>
    /// <see cref="IWorkbook"/> 工作簿扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IWorkbookExtensions
    {
        /// <summary>
        /// 获取Excel格式
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns></returns>
        public static ExcelFormat GetExcelFormat(this IWorkbook workbook)
        {
            ExcelFormat format;
            if ((workbook as HSSFWorkbook) != null)
            {
                format=ExcelFormat.Xlsx;
            }
            else if ((workbook as XSSFWorkbook) != null)
            {
                format = ExcelFormat.Xls;
            }
            else
            {
                format=ExcelFormat.None;
            }
            return format;
        }

        /// <summary>
        /// 获取或创建工作表
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="name">工作表名</param>
        /// <returns></returns>
        public static ISheet GetOrCreateSheet(this IWorkbook workbook, string name)
        {
            return workbook.GetSheet(name) ?? workbook.CreateSheet(name);
        }

        /// <summary>
        /// 获取或创建工作表
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="name">工作表名</param>
        /// <param name="action">工作表 操作</param>
        /// <returns></returns>
        public static IWorkbook GetOrCreateSheet(this IWorkbook workbook, string name, Action<ISheet> action)
        {
            var sheet = workbook.GetOrCreateSheet(name);
            if (action != null)
            {
                action(sheet);
            }
            return workbook;
        }

        /// <summary>
        /// 获取或创建工作表，获取第一页，如果不存在则创建
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns></returns>
        public static ISheet GetOrCreateSheet(this IWorkbook workbook)
        {
            ISheet sheet = null;
            if (workbook.NumberOfSheets > 0)
            {
                sheet = workbook.GetSheetAt(0);
            }
            return sheet ?? workbook.CreateSheet();
        }

        /// <summary>
        /// 获取或创建工作簿
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="action">工作表 操作</param>
        /// <returns></returns>
        public static IWorkbook GetOrCreateSheet(this IWorkbook workbook, Action<ISheet> action)
        {
            var sheet = workbook.GetOrCreateSheet();
            if (action != null)
            {
                action(sheet);
            }
            return workbook;
        }

        /// <summary>
        /// 获取工作表集合
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <returns></returns>
        public static List<ISheet> GetSheets(this IWorkbook workbook)
        {
            List<ISheet> sheets=new List<ISheet>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);
                // 工作表不为空且未隐藏
                if (sheet != null && !workbook.IsSheetHidden(i))
                {
                    sheets.Add(sheet);
                }
            }
            return sheets;
        }

        /// <summary>
        /// 获取或创建数据格式
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="format">格式字符串，如：yyyy-mm-dd</param>
        /// <returns></returns>
        public static short GetOrCreateDataFormat(this IWorkbook workbook, string format)
        {
            return workbook.CreateDataFormat().GetFormat(format);
        }

        /// <summary>
        /// 创建日期样式
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="format">格式化字符串</param>
        /// <returns></returns>
        public static short CreateDateFormat(this IWorkbook workbook, string format = "yyyy-mm-dd")
        {
            return workbook.GetOrCreateDataFormat(format);
        }
    }
}
