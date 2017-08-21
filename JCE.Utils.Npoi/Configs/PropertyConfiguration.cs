using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Configs
{
    /// <summary>
    /// Excel属性配置，表示规定属性的Excel配置
    /// </summary>
    public class PropertyConfiguration
    {
        /// <summary>
        /// 单元格配置
        /// </summary>
        internal CellConfig CellConfig { get; }        

        /// <summary>
        /// 初始化一个<see cref="PropertyConfiguration"/>类型的实例
        /// </summary>
        public PropertyConfiguration()
        {
            CellConfig=new CellConfig();
        }

        /// <summary>
        /// 设置Excel单元格索引
        /// </summary>
        /// <param name="index">单元格索引</param>
        /// <returns></returns>
        public PropertyConfiguration HasExcelIndex(int index)
        {
            CellConfig.Index = index;
            return this;
        }

        /// <summary>
        /// 设置Excel列标题（第一行）
        /// </summary>
        /// <param name="title">列的标题名</param>
        /// <returns></returns>
        public PropertyConfiguration HasExcelTitle(string title)
        {
            CellConfig.Title = title;
            return this;
        }

        /// <summary>
        /// 设置Excel列格式化
        /// </summary>
        /// <param name="formatter">格式化</param>
        /// <returns></returns>
        public PropertyConfiguration HasDataFormatter(string formatter)
        {
            CellConfig.Formatter = formatter;
            return this;
        }

        /// <summary>
        /// 设置Excel自动索引
        /// </summary>
        /// <returns></returns>
        public PropertyConfiguration HasAutoIndex()
        {
            CellConfig.AutoIndex = true;
            return this;
        }

        /// <summary>
        /// 设置Excel允许合并相同值的单元格
        /// </summary>
        /// <returns></returns>
        public PropertyConfiguration IsMergeEnabled()
        {
            CellConfig.AllowMerge = true;
            return this;
        }

        /// <summary>
        /// 设置Excel导出忽略当前属性
        /// </summary>
        public void IsIgnored()
        {
            CellConfig.IsIgnored = true;
        }

        /// <summary>
        /// 设置Excel单元格，指定索引
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="title">列标题</param>
        /// <param name="formatter">格式化字符串</param>
        /// <param name="allowMerge">是否允许合并相同值的单元格</param>
        public void HasExcelCell(int index, string title, string formatter, bool allowMerge)
        {
            CellConfig.Index = index;
            CellConfig.Title = title;
            CellConfig.Formatter = formatter;
            CellConfig.AutoIndex = false;
            CellConfig.AllowMerge = allowMerge;
        }

        /// <summary>
        /// 设置Excel单元格，自动索引
        /// </summary>
        /// <param name="title">列标题</param>
        /// <param name="formatter">格式化字符串</param>
        /// <param name="allowMerge">是否允许合并相同值的单元格</param>
        public void HasExcelCell(string title, string formatter, bool allowMerge)
        {
            CellConfig.Index = 1;
            CellConfig.Title = title;
            CellConfig.Formatter = formatter;
            CellConfig.AutoIndex = true;
            CellConfig.AllowMerge = allowMerge;
        }
    }
}
