using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Common
{
    /// <summary>
    /// system.web节点类型
    /// </summary>
    public enum SystemWebSections
    {
        /// <summary>
        /// 配置 Web 应用程序的身份验证
        /// </summary>
        Authentication,

        /// <summary>
        /// 定义用于支持 Web 应用程序的编译基础结构的配置设置
        /// </summary>
        Compilation,

        /// <summary>
        /// 配置 ASP.NET 自定义错误
        /// </summary>
        CustomErrors,

        /// <summary>
        /// 定义用于支持 Web 应用程序的全球化基础结构的配置设置
        /// </summary>
        Globalization,

        /// <summary>
        /// 配置 ASP.NET HTTP 运行时
        /// </summary>
        HttpRuntime,

        /// <summary>
        /// 配置 Web 应用程序的标识
        /// </summary>
        Identity,

        /// <summary>
        /// 配置 ASP.NET 跟踪服务
        /// </summary>
        Trace
    }
}
