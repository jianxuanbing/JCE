using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Threads
{
    /// <summary>
    /// 线程操作工具
    /// </summary>
    public static class ThreadUtils
    {
        /// <summary>
        /// 执行多个操作，多个操作将同时进行
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void WaitAll(params Action[] actions)
        {
            if (actions == null)
            {
                return;
            }
            List<Task> tasks=new List<Task>();
            foreach (var action in actions)
            {
                tasks.Add(Task.Factory.StartNew(action,TaskCreationOptions.None));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
