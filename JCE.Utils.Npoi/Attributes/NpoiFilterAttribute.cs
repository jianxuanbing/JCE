using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Attributes
{
    /// <summary>
    /// NPOI 过滤属性，表示一个自定义属性来控制Excel过滤器行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class NpoiFilterAttribute:Attribute
    {
        /// <summary>
        /// 获取 过滤器配置
        /// </summary>
        internal FilterConfig FilterConfig { get; }

        /// <summary>
        /// 获取或设置 第一行索引
        /// </summary>
        public int FirstRow
        {
            get { return FilterConfig.FirstRow; }
            set { FilterConfig.FirstRow = value; }
        }

        /// <summary>
        /// 获取或设置 最后一行索引，如果<see cref="LastRow"/>为空，则该值按代码动态计算。
        /// </summary>
        public int? LastRow
        {
            get { return FilterConfig.LastRow; }
            set { FilterConfig.LastRow = value; }
        }

        /// <summary>
        /// 获取或设置 第一列索引
        /// </summary>
        public int FirstCol
        {
            get { return FilterConfig.FirstCol; }
            set { FilterConfig.FirstCol = value; }
        }

        /// <summary>
        /// 获取或设置 最后一列索引
        /// </summary>
        public int LastCol
        {
            get { return FilterConfig.LastCol; }
            set { FilterConfig.LastCol = value; }
        }

        /// <summary>
        /// 初始化一个<see cref="NpoiFilterAttribute"/>类型的实例
        /// </summary>
        public NpoiFilterAttribute()
        {
            FilterConfig=new FilterConfig();
        }
    }
}
