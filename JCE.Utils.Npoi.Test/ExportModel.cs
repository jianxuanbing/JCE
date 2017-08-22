using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Npoi.Attributes;

namespace JCE.Utils.Npoi.Test
{
    [NpoiStatistics(Name = "合计金额",Formula = "SUM",Columns = new []{4})]
    [NpoiFilter(FirstCol = 0,FirstRow = 0,LastCol = 3)]
    [NpoiFreeze(ColSplit = 2,RowSplit = 1,LeftMostColumn = 2,TopRow = 1)]
    public class ExportModel
    {
        [NpoiColumn(Title = "系统编号",Index = 0)]
        public Guid Id { get; set; }

        [NpoiColumn(Title = "姓名",Index = 1)]
        public string Name { get; set; }

        [NpoiColumn(Title = "排序",Index = 2)]
        public int Sort { get; set; }

        [NpoiColumn(Title = "性别",Index = 3)]
        public string Sex { get; set; }

        [NpoiColumn(Title = "性别",  Index = 7,CustomEnum = typeof(Sex))]
        public int SexNum { get; set; }

        [NpoiColumn(Title = "帐户金额",Index = 4)]
        public decimal Amount { get; set; }

        [NpoiColumn(Title = "权重",Index = 5)]
        public int Power { get; set; }

        [NpoiColumn(Title = "创建时间",Formatter = "yyyy-MM-dd HH:mm:ss",Index = 6,AllowMerge = true)]
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Boy=0,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Girl=1
    }
}
