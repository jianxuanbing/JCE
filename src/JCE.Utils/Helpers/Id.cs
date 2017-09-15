using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Utils.Helpers.Internal;

namespace JCE.Utils.Helpers
{
    /// <summary>
    /// Id生成器
    /// </summary>
    public static class Id
    {
        /// <summary>
        /// 创建Id
        /// </summary>
        /// <returns></returns>
        public static string Create()
        {
            return ObjectId.GenerateNewStringId();
        }
    }
}
