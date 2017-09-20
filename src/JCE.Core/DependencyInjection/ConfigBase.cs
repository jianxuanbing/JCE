using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.IocManager;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// Ioc配置基类
    /// </summary>
    public abstract class ConfigBase:IConfig
    {        
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="iocBuilder">Ioc生成器</param>
        public abstract void Register(IIocBuilder iocBuilder);
    }
}
