using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Contexts
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        string UserId { get; }
    }
}
