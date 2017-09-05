using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Exceptions
{
    /// <summary>
    /// 异常工具
    /// </summary>
    public class ExceptionUtils
    {
        #region GetErrorFileLineNumber(获取导致异常的代码行号)
        /// <summary>
        /// 获取导致异常的代码行号
        /// 注：Debug 模式下才是源码里的代码行号，Release 模式下代码由于被优化，行号可能不准
        /// 返回 -1 表示异常
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>代码行号</returns>
        public static int GetErrorFileLineNumber(Exception ex)
        {
            try
            {
                var trace = new StackTrace(ex, true);
                var index = trace.FrameCount == 1 ? 0 : trace.FrameCount - 1;
                return trace.GetFrame(index).GetFileLineNumber();
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region GetErrorFileName(获取导致异常的代码文件名)
        /// <summary>
        /// 获取导致异常的代码文件名
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>代码文件名</returns>
        public static string GetErrorFileName(Exception ex)
        {
            try
            {
                var trace=new StackTrace(ex,true);
                var index = trace.FrameCount == 1 ? 0 : trace.FrameCount - 1;
                return trace.GetFrame(index).GetFileName();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion        

        #region GetErrorMessage(获取导致异常的错误原因)
        /// <summary>
        /// 获取导致异常的错误原因
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误原因</returns>
        public static string GetErrorMessage(Exception ex)
        {
            try
            {
                var trace=new StackTrace(ex,true);
                var fs = trace.GetFrames().Select(x => x.GetMethod().Name).Reverse();
                return string.Join(" -> ", fs) + " -> " + ex.Message;
            }
            catch
            {
                return string.Empty;                
            }
        }
        #endregion
    }
}
