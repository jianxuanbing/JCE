using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi
{
    /// <summary>
    /// 过滤器配置，表示指定实体的Excel过滤器配置
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 获取或设置 第一行索引
        /// </summary>
        public int FirstRow { get; set; }

        /// <summary>
        /// 获取或设置 最后一行索引，如果<see cref="LastRow"/>为空，则该值按代码动态计算。
        /// </summary>
        public int? LastRow { get; set; }

        /// <summary>
        /// 获取或设置 第一列索引
        /// </summary>
        public int FirstCol { get; set; }

        /// <summary>
        /// 获取或设置 最后一列索引
        /// </summary>
        public int LastCol { get; set; }
    }
}
