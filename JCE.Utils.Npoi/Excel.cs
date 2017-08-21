using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Npoi.Attributes;
using JCE.Utils.Npoi.Configs;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace JCE.Utils.Npoi
{

    /// <summary>
    /// 单元格值转换器，它将值转换为另一个值
    /// </summary>
    /// <param name="row">Excel表的行</param>
    /// <param name="cell">Excel表的列</param>
    /// <param name="value">要转换的值</param>
    /// <returns>转换后的值</returns>
    public delegate object ValueConverter(int row, int cell, object value);

    /// <summary>
    /// 提供从Excel加载<see cref="IEnumerable{T}"/>的一些方法
    /// </summary>
    public static class Excel
    {
        /// <summary>
        /// 获取或设置 Excel设置
        /// </summary>
        public static ExcelSetting Setting { get; set; }=new ExcelSetting();

        /// <summary>
        /// 从指定Excel文件加载数据到<see cref="IEnumerable{T}"/>集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="excelFile">Excel文件</param>
        /// <param name="startRow">开始行</param>
        /// <param name="sheetIndex">工作表索引</param>
        /// <param name="valueConverter">单元格值转换器</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> Load<TEntity>(string excelFile, int startRow = 1, int sheetIndex = 0,
            ValueConverter valueConverter = null) where TEntity : class, new()
        {
            if (!File.Exists(excelFile))
            {
                throw new FileNotFoundException();
            }

            var workbook = InitializeWorkbook(excelFile);

            // 目前，只处理一张（或使用foreach支持多张表）
            var sheet = workbook.GetSheetAt(sheetIndex);

            // 获取可写的属性
            var properties =
                typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            // 获取配置
            bool fluentConfigEnabled = false;
            IFluentConfiguration fluentConfig;
            if(Setting.FluentConfigs.TryGetValue(typeof(TEntity),out fluentConfig))
            {
                fluentConfigEnabled = true;
            }

            // 获取单元格设置
            var cellConfigs = new CellConfig[properties.Length];
            for (var i = 0; i < properties.Length; i++)
            {
                PropertyConfiguration pc;
                var property = properties[i];
                // 优先获取配置（高优先级），没有则获取属性
                if (fluentConfigEnabled && fluentConfig.PropertyConfigs.TryGetValue(property, out pc))
                {
                    cellConfigs[i] = pc.CellConfig;
                }
                else
                {
                    var attrs = property.GetCustomAttributes(typeof(NpoiColumnAttribute), true) as NpoiColumnAttribute[];
                    if (attrs != null && attrs.Length > 0)
                    {
                        cellConfigs[i] = attrs[0].CellConfig;
                    }
                    else
                    {
                        cellConfigs[i] = null;
                    }
                }
            }

            // 获取统计信息设置
            var statistics=new List<StatisticsConfig>();
            if (fluentConfigEnabled)
            {
                statistics.AddRange(fluentConfig.StatisticsConfigs);
            }
            else
            {
                var attrs =
                    typeof(TEntity).GetCustomAttributes(typeof(NpoiStatisticsAttribute), true) as
                        NpoiStatisticsAttribute[];
                if (attrs != null && attrs.Length > 0)
                {
                    foreach (var item in attrs)
                    {
                        statistics.Add(item.StatisticsConfig);
                    }
                }
            }

            var list=new List<TEntity>();
            int idx = 0;

            IRow headerRow = null;

            // 获取物理行
            var rows = sheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                var row = rows.Current as IRow;
                if (idx == 0)
                {
                    headerRow = row;
                }
                idx++;

                if (row.RowNum < startRow)
                {
                    continue;
                }

                var item=new TEntity();
                var itemIsValid = true;
                for (int i = 0; i < properties.Length; i++)
                {
                    var prop = properties[i];

                    int index = i;
                    var config = cellConfigs[i];
                    if (config != null)
                    {
                        index = config.Index;
                        // 尝试从标题和缓存自动发现索引
                        if (index < 0 && config.AutoIndex && !string.IsNullOrWhiteSpace(config.Title))
                        {
                            foreach (var cell in headerRow.Cells)
                            {
                                if (!string.IsNullOrWhiteSpace(cell.StringCellValue))
                                {
                                    if (cell.StringCellValue.Equals(config.Title,
                                        StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        index = cell.ColumnIndex;
                                        // 缓存
                                        config.Index = index;
                                        break;
                                    }
                                }
                            }
                        }
                        // 再次检查
                        if (index < 0)
                        {
                            throw new ApplicationException("请通过 fluent api 或 属性 设置'index'或'autoIndex'");
                        }
                    }

                    var value = row.GetCellValue(index);
                    if (valueConverter != null)
                    {
                        value = valueConverter(row.RowNum, index, value);
                    }
                    if (value == null)
                    {
                        continue;
                    }

                    // 检查是否统计行
                    if (idx > startRow + 1 && index == 0 &&
                        statistics.Any(s => s.Name.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase)))
                    {
                        var st =
                            statistics.FirstOrDefault(
                                s => s.Name.Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase));
                        var formula = row.GetCellValue(st.Columns.First()).ToString();
                        if (formula.StartsWith(st.Formula, StringComparison.InvariantCultureIgnoreCase))
                        {
                            itemIsValid = false;
                            break;
                        }
                    }

                    // 属性类型
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var safeValue = Convert.ChangeType(value, propType, CultureInfo.CurrentCulture);
                    prop.SetValue(item,safeValue,null);
                }

                if (itemIsValid)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="index">单元格索引</param>
        /// <returns></returns>
        internal static object GetCellValue(this IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                return null;
            }

            if (cell.IsMergedCell)
            {
                // TODO:合并单元格该如何处理
            }

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                // TODO:公式计算如何处理？
                case CellType.Formula:
                    return cell.ToString();
                case CellType.Blank:
                case CellType.Unknown:
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 初始化Excel工作簿
        /// </summary>
        /// <param name="excelFile">Excel文件路径</param>
        /// <returns></returns>
        private static IWorkbook InitializeWorkbook(string excelFile)
        {
            if (string.IsNullOrWhiteSpace(excelFile))
            {
                throw new ArgumentNullException(nameof(excelFile));
            }
            string ext = Path.GetExtension(excelFile);
            if (ext.Equals(".xls"))
            {
                using (var file=new FileStream(excelFile,FileMode.Open,FileAccess.Read))
                {
                    return new HSSFWorkbook(file);
                }
            }
            if (ext.Equals(".xlsx"))
            {
                using (var file = new FileStream(excelFile, FileMode.Open, FileAccess.Read))
                {
                    return new XSSFWorkbook(file);
                }
            }
            throw new NotSupportedException($"不是Excel文件 {excelFile}");
        }
    }
}
