using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 比较模板选项
    /// </summary>
    public enum ComparsionTemplateOptions
    {
        /// <summary>
        /// 简单模板比较
        /// </summary>
        Default,
        /// <summary>
        /// 字符串前缀模板比较
        /// </summary>
        FromStart,
        /// <summary>
        /// 字符串后缀模板比较
        /// </summary>
        AtTheEnd,
        /// <summary>
        /// 全字符串模板比较
        /// </summary>
        Whole
    }
}
