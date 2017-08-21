using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Npoi.Configs
{
    /// <summary>
    /// 提供NPOI配置的接口
    /// </summary>
    internal interface IFluentConfiguration
    {
        /// <summary>
        /// 获取 属性配置
        /// </summary>
        IDictionary<PropertyInfo,PropertyConfiguration> PropertyConfigs { get; }

        /// <summary>
        /// 获取 统计信息配置
        /// </summary>
        IList<StatisticsConfig> StatisticsConfigs { get; }

        /// <summary>
        /// 获取 过滤器配置
        /// </summary>
        IList<FilterConfig> FilterConfigs { get; }

        /// <summary>
        /// 获取 冻结配置
        /// </summary>
        IList<FreezeConfig> FreezeConfigs { get;  }
    }
}
