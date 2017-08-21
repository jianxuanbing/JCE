using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi
{
    /// <summary>
    /// 冻结配置，表示指定实体的Excel冻结配置
    /// </summary>
    public class FreezeConfig
    {
        /// <summary>
        /// 获取或设置 要冻结单元格的列号
        /// </summary>
        public int ColSplit { get; set; } = 0;

        /// <summary>
        /// 获取或设置 要冻结单元格的行号
        /// </summary>
        public int RowSplit { get; set; } = 1;

        /// <summary>
        /// 获取或设置 最左边的列索引
        /// </summary>
        public int LeftMostColumn { get; set; } = 0;

        /// <summary>
        /// 获取或设置 最顶行的索引
        /// </summary>
        public int TopRow { get; set; } = -1;
    }
}
