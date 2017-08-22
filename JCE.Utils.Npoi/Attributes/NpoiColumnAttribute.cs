using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Attributes
{
    /// <summary>
    /// NPOI 列属性，表示一个自定义属性来控制对象的属性在Excel列行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = true)]
    public class NpoiColumnAttribute:Attribute
    {
        /// <summary>
        /// 获取列配置
        /// </summary>
        internal CellConfig CellConfig { get; }

        /// <summary>
        /// 获取或设置 列的标题
        /// </summary>
        public string Title {
            get { return CellConfig.Title; }
            set { CellConfig.Title = value; }
        }

        /// <summary>
        /// 获取或设置 是否自动索引，如果<see cref="Index"/>未设置，并且<see cref="AutoIndex"/>设置为true，则将尝试通过其标题查找列索引
        /// </summary>
        public bool AutoIndex
        {
            get { return CellConfig.AutoIndex; }
            set { CellConfig.AutoIndex = value; }
        }

        /// <summary>
        /// 获取或设置 列索引
        /// </summary>
        public int Index
        {
            get { return CellConfig.Index; }
            set { CellConfig.Index = value; }
        }

        /// <summary>
        /// 获取或设置 是否允许合并相同的值单元格
        /// </summary>
        public bool AllowMerge
        {
            get { return CellConfig.AllowMerge; }
            set { CellConfig.AllowMerge = value; }
        }

        /// <summary>
        /// 获取或设置 是否忽略当前属性的值
        /// </summary>
        public bool IsIgnored
        {
            get { return CellConfig.IsIgnored; }
            set { CellConfig.IsIgnored = value; }
        }

        /// <summary>
        /// 获取或设置 格式化格式
        /// </summary>
        public string Formatter
        {
            get { return CellConfig.Formatter; }
            set { CellConfig.Formatter = value; }
        }

        /// <summary>
        /// 获取或设置 自定义枚举
        /// </summary>
        public Type CustomEnum
        {
            get { return CellConfig.CustomEnum; }
            set { CellConfig.CustomEnum = value; }
        }

        /// <summary>
        /// 初始化一个<see cref="NpoiColumnAttribute"/>类型的实例
        /// </summary>
        public NpoiColumnAttribute()
        {
            CellConfig=new CellConfig();
        }
    }
}
