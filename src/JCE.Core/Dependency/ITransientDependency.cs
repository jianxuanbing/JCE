using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Core.Dependency
{
    /// <summary>
    /// 依赖注入接口，实现该接口将自动注册到IOC容器，生命周期为每次创建一个新实例
    /// </summary>
    public interface ITransientDependency:IDependency
    {
    }
}
