using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注册器
    /// </summary>
    public interface IRegist
    {
        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="builder">容器生成器</param>
        void Regist(ContainerBuilder builder);
    }
}
