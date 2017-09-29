using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Layout.Pattern;

namespace JCE.Logs.Log4Net.Layouts
{
    /// <summary>
    /// 自定义log4net布局转换组件
    /// </summary>
    public class CustomPatternLayoutConverter : PatternLayoutConverter
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="writer">文本写入器</param>
        /// <param name="loggingEvent">日志事件</param>
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (Option == null)
            {
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
            else
            {
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
        }

        /// <summary>
        /// 查找日志对象的属性值
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="loggingEvent">日志事件</param>
        /// <returns></returns>
        private object LookupProperty(string property, LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
            {
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            }
            return propertyValue;
        }
    }
}
