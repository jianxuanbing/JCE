using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Helpers;

namespace JCE.Core.DependencyInjection
{
    /// <summary>
    /// 依赖配置
    /// </summary>
    public class DependencyConfiguration
    {

        public DependencyConfiguration(Assembly assembly, IConfig[] configs)
        {
            
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <returns></returns>
        public void Config()
        {
            //Ioc.DefaultContainer.Register()
        }
    }
}
