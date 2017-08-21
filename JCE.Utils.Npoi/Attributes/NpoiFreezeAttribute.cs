using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Attributes
{
    /// <summary>
    /// NPOI 冻结属性，表示一个自定义属性来控制Excel冻结行为
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class NpoiFreezeAttribute:Attribute
    {
        /// <summary>
        /// 获取 冻结配置
        /// </summary>
        internal FreezeConfig FreezeConfig { get; }

        /// <summary>
        /// 获取或设置 要冻结单元格的列号
        /// </summary>
        public int ColSplit
        {
            get { return FreezeConfig.ColSplit; }
            set { FreezeConfig.ColSplit = value; }
        }

        /// <summary>
        /// 获取或设置 要冻结单元格的行号
        /// </summary>
        public int RowSplit
        {
            get { return FreezeConfig.RowSplit; }
            set { FreezeConfig.RowSplit = value; }
        }

        /// <summary>
        /// 获取或设置 最左边的列索引
        /// </summary>
        public int LeftMostColumn
        {
            get { return FreezeConfig.LeftMostColumn; }
            set { FreezeConfig.LeftMostColumn = value; }
        }

        /// <summary>
        /// 获取或设置 最顶行的索引
        /// </summary>
        public int TopRow
        {
            get { return FreezeConfig.TopRow; }
            set { FreezeConfig.TopRow = value; }
        }


        /// <summary>
        /// 初始化一个<see cref="NpoiFreezeAttribute"/>类型的实例
        /// </summary>
        public NpoiFreezeAttribute()
        {
            FreezeConfig=new FreezeConfig();
        }
    }
}
