using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi
{
    /// <summary>
    /// 统计信息配置，表示指定实体的Excel统计信息配置
    /// </summary>
    public class StatisticsConfig
    {
        /// <summary>
        /// 获取或设置 统计信息名称，默认名称位置为（最后一行，第一个单元格）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 单元格公式，如SUM、AVERAGE等，可用于垂直统计。
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// 获取或设置 统计信息的列索引。如果<see cref="Formula"/>是SUM，而<see cref="Columns"/>是[1,3]，例如：列1和列3将是SUM第一行到最后一行。
        /// </summary>
        public int[] Columns { get; set; }
    }
}
