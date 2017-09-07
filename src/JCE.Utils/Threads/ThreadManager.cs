using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Threads
{
    /// <summary>
    /// 多线程管理器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class ThreadManager<T>
    {
        #region Fields(字段)
        /// <summary>
        /// 异步操作
        /// </summary>
        private IList<Task> _tasks=new List<Task>();

        /// <summary>
        /// 待处理任务列表
        /// </summary>
        private IEnumerable<T> _list;

        /// <summary>
        /// 调用方法
        /// </summary>
        private Action<T> _action;

        #endregion

        #region Property(属性)
        /// <summary>
        /// 线程数量
        /// </summary>
        public int ThreadCount { get; private set; }

        /// <summary>
        /// 每个线程处理任务数
        /// </summary>
        public int Size { get; private set; }

        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="ThreadManager{T}"/>类型的实例
        /// </summary>
        /// <param name="threadCount">线程数量</param>
        /// <param name="list">待处理任务列表</param>
        /// <param name="action">调用方法</param>
        public ThreadManager(int threadCount, IEnumerable<T> list, Action<T> action)
        {            
            this._list = list;
            this._action = action;
            this.ThreadCount = threadCount;
            this.Size = this._list.Count()/threadCount + 1;
        }
        #endregion

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < ThreadCount; i++)
            {
                Run(i);
            }
            foreach (var task in _tasks)
            {
                task.Wait();
            }
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="i"></param>
        private void Run(int i)
        {
            var task = Task.Run(() =>
            {
                var listSub = _list.Skip(i*Size).Take(Size);
                foreach (var item in listSub)
                {
                    _action(item);
                }
            });
            _tasks.Add(task);
        }
    }
}
