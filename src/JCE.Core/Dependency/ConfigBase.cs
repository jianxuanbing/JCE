using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// Ioc配置基类
    /// </summary>
    public abstract class ConfigBase:Module,IConfig
    {
    }
}
