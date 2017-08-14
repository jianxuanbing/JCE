using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Exceptions
{
    /// <summary>
    /// 验证异常
    /// </summary>
    public class ValidationException:Exception
    {
        #region Property(属性)            
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="ValidationException"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        public ValidationException(string message) : base(message)
        {
            
        }
        #endregion
    }
}
