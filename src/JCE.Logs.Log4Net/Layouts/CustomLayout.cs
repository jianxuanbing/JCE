using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Layout;

namespace JCE.Logs.Log4Net.Layouts
{
    /// <summary>
    /// 自定义log4net布局组件
    /// </summary>
    public class CustomLayout : PatternLayout
    {
        public CustomLayout()
        {
            AddConverter("property", typeof(CustomPatternLayoutConverter));
        }
    }
}
