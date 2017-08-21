using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Npoi.Attributes;

namespace JCE.Utils.Npoi.Test
{
    [NpoiStatistics(Name = "合计金额",Formula = "SUM",Columns = new []{4})]
    [NpoiFilter(FirstCol = 0,FirstRow = 0,LastCol = 2)]
    [NpoiFreeze(ColSplit = 2,RowSplit = 1,LeftMostColumn = 2,TopRow = 1)]
    public class ExportModel
    {
        [NpoiColumn(Title = "系统编号")]
        public Guid Id { get; set; }

        [NpoiColumn(Title = "姓名")]
        public string Name { get; set; }

        [NpoiColumn(Title = "排序")]
        public int Sort { get; set; }

        [NpoiColumn(Title = "性别",AllowMerge = true)]
        public string Sex { get; set; }

        [NpoiColumn(Title = "帐户金额",Index = 4)]
        public decimal Amount { get; set; }

        [NpoiColumn(Title = "权重")]
        public int Power { get; set; }

        [NpoiColumn(Title = "创建时间",Formatter = "yyyy-MM-dd HH:mm:ss")]
        public DateTime CreateTime { get; set; }
    }
}
