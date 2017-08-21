using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Attributes
{
    /// <summary>
    /// NPOI 统计属性，表示一些简单统计信息的自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class NpoiStatisticsAttribute:Attribute
    {
        /// <summary>
        /// 统计信息配置
        /// </summary>
        internal StatisticsConfig StatisticsConfig { get; }

        /// <summary>
        /// 获取或设置 统计信息名称，默认名称位置为（最后一行，第一个单元格）
        /// </summary>
        public string Name
        {
            get { return StatisticsConfig.Name; }
            set { StatisticsConfig.Name = value; }
        }

        /// <summary>
        /// 获取或设置 单元格公式，如SUM、AVERAGE等，可用于垂直统计。
        /// </summary>
        public string Formula
        {
            get { return StatisticsConfig.Formula; }
            set { StatisticsConfig.Formula = value; }
        }
        
        /// <summary>
        /// 获取或设置 统计信息的列索引。如果<see cref="Formula"/>是SUM，而<see cref="Columns"/>是[1,3]，例如：列1和列3将是SUM第一行到最后一行。
        /// </summary>
        public int[] Columns
        {
            get { return StatisticsConfig.Columns; }
            set { StatisticsConfig.Columns = value; }
        }

        /// <summary>
        /// 初始化一个<see cref="NpoiStatisticsAttribute"/>类型的实例
        /// </summary>
        public NpoiStatisticsAttribute()
        {
            StatisticsConfig=new StatisticsConfig();
        }
    }
}
