using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Helpers
{
    /// <summary>
    /// Npoi对Excel操作的工具类
    /// </summary>
    internal static class ExcelUtil
    {
        /// <summary>
        /// 获取指定格式的工作簿
        /// </summary>
        /// <param name="excelFormat">格式类型</param>
        /// <returns></returns>
        public static NPOI.SS.UserModel.IWorkbook GetWorkbook(ExcelFormat excelFormat)
        {
            NPOI.SS.UserModel.IWorkbook workbook = null;
            switch (excelFormat)
            {
                case ExcelFormat.Xls:
                    workbook=new NPOI.HSSF.UserModel.HSSFWorkbook();
                    break;
                case ExcelFormat.Xlsx:
                    workbook=new NPOI.XSSF.UserModel.XSSFWorkbook();
                    break;
                default:
                    break;
            }
            return workbook;
        }

        /// <summary>
        /// 获取工作簿
        /// </summary>
        /// <param name="inputStream">.xls或.xlsx文件流</param>
        /// <returns></returns>
        public static NPOI.SS.UserModel.IWorkbook GetWorkbook(Stream inputStream)
        {
            // 注意：SS.UserModel.WorkbookFactory需要添加“NPOI.Openxml4Net.dll”的引用

            // WorkbookFactory内部可以通过inputStream判断是2003还是2007
            return NPOI.SS.UserModel.WorkbookFactory.Create(inputStream);
        }

        /// <summary>
        /// 获取工作簿
        /// </summary>
        /// <param name="file">.xls或.xlsx文件的物理路径</param>
        /// <returns></returns>
        public static NPOI.SS.UserModel.IWorkbook GetWorkbook(string file)
        {
            return NPOI.SS.UserModel.WorkbookFactory.Create(file);
        }
    }
}
