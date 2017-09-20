using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Contexts
{
    /// <summary>
    /// 空用户上下文
    /// </summary>
    public class NullUserContext:IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId => string.Empty;
    }
}
