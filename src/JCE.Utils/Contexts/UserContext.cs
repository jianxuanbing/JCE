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
    public class UserContext:IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// 空用户上下文
        /// </summary>
        public static readonly IUserContext Null=new NullUserContext();

        /// <summary>
        /// 初始化一个<see cref="UserContext"/>类型的实例
        /// </summary>
        /// <param name="userId">用户编号</param>
        public UserContext(string userId)
        {
            UserId = userId;
        }
    }
}
