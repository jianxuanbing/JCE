using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Http.Enums
{
    /// <summary>
    /// 响应状态
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// 没有响应
        /// </summary>
        None,
        /// <summary>
        /// 响应OK
        /// </summary>
        Completed,
        /// <summary>
        /// 响应出错
        /// </summary>
        Error,
        /// <summary>
        /// 响应出错，但正确返回数据
        /// </summary>
        ErrorButResponse,
        /// <summary>
        /// 超时
        /// </summary>
        TimeOut,
        /// <summary>
        /// 取消
        /// </summary>
        Aborted
    }
}
