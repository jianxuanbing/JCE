using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using JCE.Core.Aspects.Base;
using JCE.Logs.Extensions;
using JCE.Utils.Extensions;
using JCE.Utils.Logs;
using JCE.Utils.Logs.Extensions;

namespace JCE.Logs.Aspects
{
    /// <summary>
    /// 日志 属性基类
    /// </summary>
    public abstract class LogAttributeBase:InterceptorBase
    {        
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取方法名
        /// </summary>
        /// <param name="context">Aspect上下文</param>
        /// <returns></returns>
        private string GetMethodName(AspectContext context)
        {
            return $"{context.ServiceMethod.DeclaringType.FullName}.{context.ServiceMethod.Name}";
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="log">日志操作</param>
        /// <returns></returns>
        protected virtual bool Enabled(ILog log)
        {
            return true;
        }

        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="log"></param>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        private void ExecuteBefore(ILog log, AspectContext context, string methodName)
        {
            log.Caption($"{context.ServiceMethod.Name}方法执行前")
                .Class(context.ServiceMethod.DeclaringType.FullName)
                .Method(methodName);
            foreach (var parameter in context.GetParameters())
            {
                parameter.AppendTo(log);
            }
            WriteLog(log);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        protected abstract void WriteLog(ILog log);

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="log"></param>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        private void ExecuteAfter(ILog log, AspectContext context, string methodName)
        {
            var parameter = context.GetReturnParameter();
            log.Caption($"{context.ServiceMethod.Name}方法执行后")
                .Method(methodName)
                .Content("返回类型: {0},返回值:{1}", parameter.ParameterInfo.ParameterType.FullName,
                    parameter.Value.SafeString());
            WriteLog(log);
        }
    }
}
