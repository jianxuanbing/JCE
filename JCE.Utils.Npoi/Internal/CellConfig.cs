using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi
{
    /// <summary>
    /// 单元格配置，表示指定模型属性对应的Excel单元格配置
    /// </summary>
    internal class CellConfig
    {
        /// <summary>
        /// 获取或设置 列的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置 是否自动索引，如果<see cref="Index"/>未设置，并且<see cref="AutoIndex"/>设置为true，则将尝试通过其标题查找列索引
        /// </summary>
        public bool AutoIndex { get; set; }

        /// <summary>
        /// 获取或设置 列索引
        /// </summary>
        public int Index { get; set; } = -1;

        /// <summary>
        /// 获取或设置 是否允许合并相同值的单元格
        /// </summary>
        public bool AllowMerge { get; set; }

        /// <summary>
        /// 获取或设置 是否忽略当前属性的值
        /// </summary>
        public bool IsIgnored { get; set; }

        /// <summary>
        /// 获取或设置 格式化格式
        /// </summary>
        public string Formatter { get; set; }

        /// <summary>
        /// 获取或设置 自定义枚举
        /// </summary>
        public Type CustomEnum { get; set; }
    }
}
