using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;
using JCE.Utils.Logs;

namespace JCE.Logs.Extensions
{
    /// <summary>
    /// Aop扩展
    /// </summary>
    public static partial class AspectExtensions
    {
        /// <summary>
        /// 添加日志参数
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="log">日志</param>
        public static void AppendTo(this Parameter parameter, ILog log)
        {
            log.Params(parameter.ParameterInfo.ParameterType.FullName, parameter.Name, GetParameterValue(parameter));
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        private static string GetParameterValue(Parameter parameter)
        {
            if (Reflection.IsGenericCollection(parameter.RawType) == false)
            {
                return parameter.Value.SafeString();
            }
            if (!(parameter.Value is IEnumerable<object>))
            {
                return parameter.Value.SafeString();
            }
            return ((IEnumerable<object>) parameter).Select(t => t.SafeString()).Join();
        }
    }
}
